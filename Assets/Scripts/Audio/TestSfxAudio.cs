
using UnityEngine;

namespace UnityCore {

    namespace Audio {

        public class TestSfxAudio : MonoBehaviour
        {

            public SfxController audioController;

#region Unity Functions
#if UNITY_EDITOR
            private void Update() {
                if (Input.GetKeyUp(KeyCode.R)) {
                    audioController.PlayAudio(SFX.SFX_01, true);
                }
                if (Input.GetKeyUp(KeyCode.F)) {
                    audioController.StopAudio(SFX.SFX_01, true);
                }
                if (Input.GetKeyUp(KeyCode.V)) {
                    audioController.RestartAudio(SFX.SFX_01, true);
                }
                if (Input.GetKeyUp(KeyCode.T)) {
                    audioController.PlayAudio(SFX.SFX_02, true);
                }
                if (Input.GetKeyUp(KeyCode.G)) {
                    audioController.StopAudio(SFX.SFX_02, true);
                }
                if (Input.GetKeyUp(KeyCode.B)) {
                    audioController.RestartAudio(SFX.SFX_02, true);
                }
            }
#endif
#endregion
        }
    }
}
