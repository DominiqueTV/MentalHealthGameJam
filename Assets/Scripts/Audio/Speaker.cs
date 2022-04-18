using System.Collections;
using System.Collections.Generic;
using UnityCore.Audio;
using UnityEngine;


namespace ETS.Audio
{
    public class Speaker : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // play ambience
            AmbienceController.instance.PlayAudio(Ambience.AMB_01, true);
            // play ambient music
            //AmbienceController.instance.PlayAudio(Ambience.AMB_02, true, 5f);

        }
    }
}
