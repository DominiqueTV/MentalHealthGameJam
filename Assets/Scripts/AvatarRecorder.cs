using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AvatarRecorder : MonoBehaviour
{
    Microphone mic;

    public AnimationClip clip;
    private GameObjectRecorder m_Recorder;
    public GameObject clonePrefab;
    private GameObject cloneInstance;


    public void StartRecording()
    {
        // Create recorder and record the script GameObject.
        m_Recorder = new GameObjectRecorder(gameObject);

        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    public void StopRecording()
    {
        if (clip == null)
            return;

        if (m_Recorder.isRecording)
        {
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(clip);

            if (clonePrefab && !cloneInstance)
            {
                cloneInstance = Instantiate(clonePrefab, transform.position, transform.rotation);
            } 
            else
            {
                cloneInstance.transform.position = transform.position;
                cloneInstance.transform.rotation = transform.rotation;
            }
        }
    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        // Take a snapshot and record all the bindings values for this frame.
        if (m_Recorder) m_Recorder.TakeSnapshot(Time.deltaTime);
    }
}
