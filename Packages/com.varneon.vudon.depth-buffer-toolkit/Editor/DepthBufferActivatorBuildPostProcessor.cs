using UdonSharpEditor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using VRC.SDK3.Components;

namespace Varneon.VUdon.DepthBufferToolkit.Editor
{
    /// <summary>
    /// Build postprocessor for initializing the depth buffer activator
    /// </summary>
    public static class DepthBufferActivatorBuildPostProcessor
    {
        private const string LogPrefix = "[<color=Cyan>CameraDepthBufferActivator</color>]:";

        [PostProcessScene(-1)] // Ensure that this post-processor runs before U# converts all UdonSharpBehaviours to UdonBehaviours
        private static void PostProcessActivator()
        {
            DepthBufferActivator activator = Object.FindObjectOfType<DepthBufferActivator>();

            EditorUtility.SetDirty(activator);

            activator.GetComponent<PostProcessVolume>().enabled = true;

            GameObject activatorGO = activator.gameObject;

            VRCSceneDescriptor descriptor = Object.FindObjectOfType<VRCSceneDescriptor>();

            if (!descriptor) { Debug.LogWarning($"{LogPrefix} Scene doesn't have a VRCSceneDescriptor!"); return; }

            GameObject referenceCamera = descriptor.ReferenceCamera;

            if (!referenceCamera || !referenceCamera.GetComponent<Camera>()) { Debug.LogWarning($"{LogPrefix} VRCSceneDescriptor doesn't have reference camera assigned!"); return; }

            PostProcessLayer postProcessLayer = referenceCamera.GetComponent<PostProcessLayer>();

            if (!postProcessLayer) { Debug.LogWarning($"{LogPrefix} Reference camera doesn't have a PostProcessLayer attached to it!"); return; }

            int volumeLayerMask = postProcessLayer.volumeLayer;

            int firstVolumeLayer = -1;

            for (int i = 0; i < 32; i++)
            {
                if ((volumeLayerMask & (1 << i)) != 0) { firstVolumeLayer = i; continue; }
            }

            if (firstVolumeLayer < 0) { Debug.LogWarning($"{LogPrefix} Reference camera's PostProcessLayer doesn't have any volume layers defined!"); return; }

            activatorGO.layer = firstVolumeLayer;

            foreach(VRCMirrorReflection mirror in activator.Mirrors)
            {
                if(mirror == null) { continue; }

                mirror.gameObject.AddUdonSharpComponent<MirrorDepthBufferActivator>();
            }
        }
    }
}
