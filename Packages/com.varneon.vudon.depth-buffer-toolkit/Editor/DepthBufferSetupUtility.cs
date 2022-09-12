using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Varneon.VUdon.DepthBufferToolkit
{
    /// <summary>
    /// Simple editor utility for setting up depth rendering on all scene cameras
    /// </summary>
    public static class DepthBufferSetupUtility
    {
        /// <summary>
        /// Enable depth buffer on all scene cameras
        /// </summary>
        [MenuItem("Varneon/VUdon/DepthBufferToolkit/Enable Depth Buffer On Scene Cameras")]
        public static void EnableDepthBufferOnSceneCameras() { SetDepthBufferEnabledOnSceneCameras(true); }

        /// <summary>
        /// Disable depth buffer on all scene cameras
        /// </summary>
        [MenuItem("Varneon/VUdon/DepthBufferToolkit/Disable Depth Buffer On Scene Cameras")]
        public static void DisableDepthBufferOnSceneCameras() { SetDepthBufferEnabledOnSceneCameras(false); }

        /// <summary>
        /// Sets depth buffer enabled on all scene cameras
        /// </summary>
        /// <param name="enabled"></param>
        public static void SetDepthBufferEnabledOnSceneCameras(bool enabled)
        {
            foreach (Camera camera in Resources.FindObjectsOfTypeAll<Camera>().Where(c => c.scene != null && !PrefabUtility.IsPartOfPrefabAsset(c) && c.hideFlags == HideFlags.None))
            {
                camera.depthTextureMode = enabled ? DepthTextureMode.Depth : DepthTextureMode.None;
            }
        }

        [MenuItem("Varneon/VUdon/DepthBufferToolkit/Add Runtime Activator To Scene")]
        public static void AddActivatorUdonPrefabToScene()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath("cbbf4de90e13ea3459ad6945a3176e42")));
        }
    }
}
