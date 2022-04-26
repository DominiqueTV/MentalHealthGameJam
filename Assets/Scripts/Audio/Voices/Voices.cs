using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Voices : MonoBehaviour
{
    [SerializeField]
    private bool debug = false;

    public static Voices instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip[] intro;

    [SerializeField]
    public AudioClip[] instruction;

    [SerializeField]
    public AudioClip[] positives;

    [SerializeField]
    public AudioClip[] negatives;


    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private int ReturnRandom(AudioClip[] listName)
    {
        return Random.Range(0, listName.Length);
    }

    public void PlayRandomAudioClip(AudioClip[] listName)
    {
        AudioClip audioClip = (AudioClip) listName.GetValue(ReturnRandom(listName));
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayRandomPositive()
    {
        AudioClip audioClip = (AudioClip) positives.GetValue(ReturnRandom(positives));
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayRandomNegatives()
    {
        AudioClip audioClip = (AudioClip) negatives.GetValue(ReturnRandom(negatives));
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayIntroClip(int clipID)
    {
        AudioClip audioClip = (AudioClip)intro.GetValue(clipID);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayInstructionClip(int clipID)
    {
        AudioClip audioClip = (AudioClip)instruction.GetValue(clipID);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayPositiveClip(int clipID)
    {
        AudioClip audioClip = (AudioClip) positives.GetValue(clipID);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayNegativeClip(int clipID)
    {
        AudioClip audioClip = (AudioClip) negatives.GetValue(clipID);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    #region Tests

    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                PlayRandomAudioClip(intro);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                PlayRandomAudioClip(positives);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                PlayRandomAudioClip(negatives);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                PlayRandomAudioClip(instruction);
            }
        }
    }
    #endregion

}
