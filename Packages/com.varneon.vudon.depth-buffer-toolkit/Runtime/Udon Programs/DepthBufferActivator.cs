using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;

namespace Varneon.VUdon.DepthBufferToolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DepthBufferActivator : UdonSharpBehaviour
    {
        public Camera[] Cameras;

        public VRCMirrorReflection[] Mirrors;

        private void Start()
        {
            foreach (Camera camera in Cameras)
            {
                if(camera != null)
                {
                    camera.depthTextureMode = DepthTextureMode.Depth;
                }
            }

            Destroy(gameObject);
        }
    }
}
