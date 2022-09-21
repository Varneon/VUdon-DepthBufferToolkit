using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using VRC.SDK3.Components;

namespace Varneon.VUdon.DepthBufferToolkit.Editor
{
    /// <summary>
    /// Custom editor for the camera depth buffer activator UdonSharpBehaviour
    /// </summary>
    [CustomEditor(typeof(DepthBufferActivator))]
    public class DepthBufferActivatorEditor : UnityEditor.Editor
    {
        private DepthBufferActivator activator;

        private bool isPrefabInspector;

        private PreProcessorError preProcessorError;

        private enum PreProcessorError
        {
            None,
            NoSceneDescriptor,
            NoReferenceCamera,
            NoPostProcessingLayer,
            NoVolumeLayersDefined,
            MirrorsWithDuplicateNames
        }

        public override void OnInspectorGUI()
        {
            if (isPrefabInspector)
            {
                EditorGUILayout.HelpBox("Add this prefab to your scene to automatically enable depth buffer on the main camera", MessageType.Info, true);

                return;
            }

            switch (preProcessorError)
            {
                case PreProcessorError.NoSceneDescriptor:
                    EditorGUILayout.HelpBox("Scene doesn't have VRCSceneDescriptor in it!", MessageType.Error, true);
                    break;
                case PreProcessorError.NoReferenceCamera:
                    EditorGUILayout.HelpBox("VRCSceneDescriptor doesn't have reference camera defined!", MessageType.Error, true);
                    break;
                case PreProcessorError.NoPostProcessingLayer:
                    EditorGUILayout.HelpBox("Reference camera doesn't have PostProcessLayer attached to it!", MessageType.Error, true);
                    break;
                case PreProcessorError.NoVolumeLayersDefined:
                    EditorGUILayout.HelpBox("Reference camera's PostProcessLayer doesn't have any volume layers defined!", MessageType.Error, true);
                    break;
                case PreProcessorError.MirrorsWithDuplicateNames:
                    EditorGUILayout.HelpBox("Mirrors with duplicate names found! Make sure that all mirrors linked to DepthBufferActivator have unique names", MessageType.Error, true);
                    break;
            }

            base.OnInspectorGUI();

            GUILayout.Space(20);

            EditorGUILayout.HelpBox("Add all cameras and mirrors to the arrays above to activate the depth buffer rendering on them", MessageType.Info, true);

            if (GUILayout.Button("Add all cameras from the current scene"))
            {
                Undo.RecordObject(activator, "Link scene cameras to depth buffer activator");

                GameObject[] sceneRoots = activator.gameObject.scene.GetRootGameObjects();

                activator.Cameras = Resources.FindObjectsOfTypeAll<Camera>().Where(c => !PrefabUtility.IsPartOfPrefabAsset(c) && c.hideFlags == HideFlags.None && sceneRoots.Contains(c.transform.root.gameObject)).ToArray();

                EditorUtility.SetDirty(activator);
            }

            if(GUILayout.Button("Add all mirrors from the current scene"))
            {
                Undo.RecordObject(activator, "Link scene mirrors to depth buffer activator");

                activator.Mirrors = Resources.FindObjectsOfTypeAll<VRCMirrorReflection>().Where(c => !PrefabUtility.IsPartOfPrefabAsset(c) && c.hideFlags == HideFlags.None).ToArray();

                EditorUtility.SetDirty(activator);
            }
        }

        private void OnEnable()
        {
            activator = target as DepthBufferActivator;

            activator.GetComponent<PostProcessVolume>().hideFlags = HideFlags.NotEditable;

            isPrefabInspector = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null || PrefabUtility.IsPartOfPrefabAsset(activator);

            preProcessorError = PreProcessActivator();
        }

        private PreProcessorError PreProcessActivator()
        {
            VRCSceneDescriptor descriptor = FindObjectOfType<VRCSceneDescriptor>();

            if (!descriptor) { return PreProcessorError.NoSceneDescriptor; }

            GameObject referenceCamera = descriptor.ReferenceCamera;

            if (!referenceCamera || !referenceCamera.GetComponent<Camera>()) { return PreProcessorError.NoReferenceCamera; }

            PostProcessLayer postProcessLayer = referenceCamera.GetComponent<PostProcessLayer>();

            if (!postProcessLayer) { return PreProcessorError.NoPostProcessingLayer; }

            if (postProcessLayer.volumeLayer == 0) { return PreProcessorError.NoVolumeLayersDefined; }

            Dictionary<string, int> mirrorNameCounts = new Dictionary<string, int>();

            foreach(string name in activator.Mirrors.Select(m => m.name))
            {
                mirrorNameCounts.TryGetValue(name, out int count);
                mirrorNameCounts[name] = count + 1;
            }

            if(mirrorNameCounts.Where(c => c.Value > 1).Count() > 0) { return PreProcessorError.MirrorsWithDuplicateNames; }

            return PreProcessorError.None;
        }
    }
}
