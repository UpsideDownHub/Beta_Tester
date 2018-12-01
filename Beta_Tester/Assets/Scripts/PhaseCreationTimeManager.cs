using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Helpers;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PhaseCreationTimeManager : MonoBehaviour
    {
        public static int time;
        public static CountDown timeCount = new CountDown(1000);

        public Text timeText;

        private void Start()
        {
            timeCount.CoolDown = 1000;
        }

        private void Update()
        {
            if (timeCount.CoolDown > 0)
            {
                CountDown.DecreaseTime(timeCount);
            }
            else if (timeCount.CoolDown < 0)
                timeCount.CoolDown = 0;

            timeText.text = timeCount.CoolDown.ToString().Replace('.', ':');
            
        }

    }
}
