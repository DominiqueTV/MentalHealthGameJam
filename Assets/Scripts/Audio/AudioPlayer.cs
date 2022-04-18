using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject objPos;

    /*
    private void OnEnable()
    {
        
        StartTutorialEvents.OnStartButtonPressed += PlayPositive;
        WeaponTutorialEvents.OnWeaponGrabbed += PlayPositive;
        TutorialRaycastWeapon.OnPlayerHasShot += PlayPositive;
        TargetTutorialEvents.OnHitEnoughTargets += PlayPositive;
        TargetTutorialEvents.OnHitMoreTargets += PlayPositive;
        GameManager.GameStarted += PlayGameMusic;
        GameManager.GameEnded += StopGameMusic;
    }

    private void OnDisable()
    {
        StartTutorialEvents.OnStartButtonPressed -= PlayPositive;
        WeaponTutorialEvents.OnWeaponGrabbed -= PlayPositive;
        TutorialRaycastWeapon.OnPlayerHasShot -= PlayPositive;
        TargetTutorialEvents.OnHitEnoughTargets-= PlayPositive;
        TargetTutorialEvents.OnHitMoreTargets -= PlayPositive;
        GameManager.GameStarted -= PlayGameMusic;
        GameManager.GameEnded -= StopGameMusic;
        

    }
    */

    void Start()
    {
        // play ambience
        AmbienceController.instance.PlayAudio(Ambience.AMB_01, true);
        // play ambient music
        AmbienceController.instance.PlayAudio(Ambience.AMB_02, true, 5f);

        // play intro
        StartCoroutine(PlayIntro());
    }

    // plays music loader, the closer a hand gets to an object
    private void CreateMusicalTension(Transform objPos, Transform handPos)
    {

    }

    private void PlayGameMusic()
    {
        AmbienceController.instance.StopAudio(Ambience.AMB_03, true);
        MusicController.instance.PlayAudio(Music.Music_01, true);
    }

    private void StopGameMusic()
    {
        MusicController.instance.StopAudio(Music.Music_01, true);
        AmbienceController.instance.PlayAudio(Ambience.AMB_03, true);
    }


    private IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(5);
        Voices.instance.PlayIntroClip(0);
        yield return new WaitForSeconds(5);
        Voices.instance.PlayInstructionClip(0);
    }

    private void PlayPositive()
    {
        Voices.instance.PlayRandomPositive();
    }
}
