
using UnityEngine;

namespace UnityCore {

    namespace Audio {

        public class TestMusicAudio : MonoBehaviour
        {

            public MusicController audioController;

#region Unity Functions
#if UNITY_EDITOR
            private void Update() {
                if (Input.GetKeyUp(KeyCode.R)) {
                    audioController.PlayAudio(Music.Music_01, true);
                }
                if (Input.GetKeyUp(KeyCode.F)) {
                    audioController.StopAudio(Music.Music_01, true);
                }
                if (Input.GetKeyUp(KeyCode.V)) {
                    audioController.RestartAudio(Music.Music_01, true);
                }
                if (Input.GetKeyUp(KeyCode.T)) {
                    audioController.PlayAudio(Music.Music_02, true);
                }
                if (Input.GetKeyUp(KeyCode.G)) {
                    audioController.StopAudio(Music.Music_02, true);
                }
                if (Input.GetKeyUp(KeyCode.B)) {
                    audioController.RestartAudio(Music.Music_02, true);
                }
            }
#endif
#endregion
        }
    }
}
