using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETS
{
    public class ToggleButton : MonoBehaviour
    {
        public void Toggle(GameObject obj)
        {
            if (obj.activeSelf == true)
                obj.SetActive(false);
            else
                obj.SetActive(true);
        }
    }
}
