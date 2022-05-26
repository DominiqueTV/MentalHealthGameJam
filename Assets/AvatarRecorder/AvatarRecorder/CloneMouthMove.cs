using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CloneMouthMove : MonoBehaviour
{
    AudioSource audioSource;
    public Transform mouth;


    private float[] sampleData;



    private float mouthSize;
    private float currentUpdateTime;
    private float updateStep;
    private float clipLoudness;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        /*
        currentUpdateTime += Time.deltaTime;

        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0;
            audioSource.clip?.GetData(sampleData, audioSource.timeSamples);

            clipLoudness = 0;
            foreach (var sample in sampleData)
                clipLoudness += Mathf.Abs(sample);

            clipLoudness /= sampleData.Length;



            if (audioSource && mouth)
            {
                // Use the current voice volume (a value between 0 - 1) to calculate the target mouth size (between 0.1 and 1.0)
                float targetMouthSize = Mathf.Lerp(0.1f, 1.0f, clipLoudness);

                // Animate the mouth size towards the target mouth size to keep the open / close animation smooth
                mouthSize = Mathf.Lerp(mouthSize, targetMouthSize, 30.0f * Time.deltaTime);

                // Apply the mouth size to the scale of the mouth geometry
                Vector3 localScale = mouth.localScale;
                localScale.y = mouthSize;
                mouth.localScale = localScale;

            }

        }
        */
    }
}
