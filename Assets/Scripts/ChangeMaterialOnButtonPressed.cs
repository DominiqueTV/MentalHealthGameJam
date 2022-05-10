using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETS
{
    public class ChangeMaterialOnButtonPressed : MonoBehaviour
    {
        [SerializeField] private Material pressedMat;
        [SerializeField] private Material nonPressedMat;

        private Renderer rend;

        private void Start()
        {
            rend = GetComponent<Renderer>();
        }

        [EasyButtons.Button]
        public void ChangeMaterialToPressed()
        {
            if(rend != null && pressedMat != null)
            {
                rend.material = pressedMat;
            }
        }

        [EasyButtons.Button]
        public void ChangeMaterialToNonPressed()
        {
            if (rend != null && nonPressedMat != null)
            {
                rend.material = nonPressedMat;
            }
        }

    }
}
