using Assets.Script.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Totem : MonoBehaviour
    {
        CountDown countDown = new CountDown(1);
        static float timeToRespaw = 1;
        
        private void Update()
        {
            CountDown.DecreaseTime(countDown);

            if (countDown.ReturnedToZero)
            {
                //Intantiate
            }
        }
    }
}
