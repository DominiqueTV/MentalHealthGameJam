
using UnityEngine;

namespace UnityCore {

    namespace Audio {

        public class TestAmbientAudio : MonoBehaviour
        {

            public AmbienceController audioController;

#region Unity Functions
#if UNITY_EDITOR
            private void Update() {
                if (Input.GetKeyUp(KeyCode.R)) {
                    audioController.PlayAudio(Ambience.AMB_01, true);
                }
                if (Input.GetKeyUp(KeyCode.F)) {
                    audioController.StopAudio(Ambience.AMB_01, true);
                }
                if (Input.GetKeyUp(KeyCode.V)) {
                    audioController.RestartAudio(Ambience.AMB_01, true);
                }
                if (Input.GetKeyUp(KeyCode.T)) {
                    audioController.PlayAudio(Ambience.AMB_02, true);
                }
                if (Input.GetKeyUp(KeyCode.G)) {
                    audioController.StopAudio(Ambience.AMB_02, true);
                }
                if (Input.GetKeyUp(KeyCode.B)) {
                    audioController.RestartAudio(Ambience.AMB_02, true);
                }
            }
#endif
#endregion
        }
    }
}
