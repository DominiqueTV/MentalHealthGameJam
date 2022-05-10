using UnityEngine;
using TMPro;


namespace ETS
{
    public class ButtonB_UI : MonoBehaviour
    {
        [SerializeField] private ETS.Realtime.MultipleButtons buttons;


        void Update()
        {
            if (buttons) GetComponent<TMP_Text>().text = buttons.buttonB.bPressed.ToString();
        }
    }
}
