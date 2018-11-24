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
        public GameObject prefabFireBall;
        public CountDown countDown = new CountDown(3);
        public CountDown countDownEffect = new CountDown(2);
        static float timeToRespaw = 3;

        private void Start()
        {
            countDown.StartToCount();
        }

        private void Update()
        {
            CountDown.DecreaseTime(countDownEffect);
            if (countDownEffect.ReturnedToZero)
            {
                countDown.Rate = timeToRespaw;
                countDown.StartToCount();
            }
            CountDown.DecreaseTime(countDown);
            print(countDown.CoolDown);
            if (countDown.ReturnedToZero)
            {
                countDown.StartToCount();
                Instantiate(prefabFireBall, transform.position, Quaternion.identity);
            }
        }

        public void StartEffect()
        {
            countDown.Rate = 1.5f;
            countDown.StartToCount();
        }
    }
}
