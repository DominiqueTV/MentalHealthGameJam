using UnityEngine;
using TMPro;

namespace ETS.Realtime
{
    public class ButtonA_UI : MonoBehaviour
    {
        [SerializeField] private ETS.Realtime.MultipleButtons buttons;


        void Update()
        {
            if (buttons) GetComponent<TMP_Text>().text = buttons.buttonA.aPressed.ToString();
        }
    }
}
