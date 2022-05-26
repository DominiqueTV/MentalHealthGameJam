using UnityEngine;
using UnityEditor;


namespace AvatarRecorder
{
    [CustomEditor(typeof(AvatarRecorderV3))]
    public class AvatarRecorderV3Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            Texture2D logo = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/AvatarRecorder/logoSmall.png", typeof(Texture2D));
            GUILayout.Box(logo, GUILayout.ExpandWidth(true));

           
            AvatarRecorderV3 v3 = (AvatarRecorderV3)target;

            if (GUILayout.Button("StartRecording"))
                v3.StartRecording();

            if (GUILayout.Button("StopRecording"))
                v3.StopRecording();

            if (GUILayout.Button("PlayRecording"))
                v3.PlayRecording();

            base.OnInspectorGUI();
        }
    }
}
