using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;

namespace Varneon.VUdon.DepthBufferToolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    [RequireComponent(typeof(VRCMirrorReflection))]
    public class MirrorDepthBufferActivator : UdonSharpBehaviour
    {
        private const string LOG_PREFIX = "[<color=Cyan>MirrorDepthBufferActivator</color]:";

        private void OnWillRenderObject()
        {
            GameObject root = GameObject.Find(string.Format("/MirrorCam{0}", name));

            if (root != null)
            {
                Camera camera = root.GetComponent<Camera>();

                if (camera != null)
                {
                    camera.depthTextureMode = DepthTextureMode.Depth;
                }
                else { Debug.LogWarning($"{LOG_PREFIX} Failed to activate depth buffer! (Camera is null!)"); }
            }
            else { Debug.LogWarning($"{LOG_PREFIX} Failed to activate depth buffer! (Root is null!)"); }

            Destroy(this);
        }
    }
}
