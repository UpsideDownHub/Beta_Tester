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
        public Sprite firstFrame;
        public Sprite secondFrame;
        public CountDown countDown = new CountDown(3);
        public CountDown countDownEffect = new CountDown(2);
        SpriteRenderer sr;
        static float timeToRespaw = 3;

        float cameraPosition;
        bool isActivated;

        private void Start()
        {
            countDown.StartToCount();
            sr = GetComponent<SpriteRenderer>();
            cameraPosition = Camera.main.orthographicSize * Camera.main.aspect;
        }

        private void Update()
        {
            if (transform.position.x <= Camera.main.transform.position.x)
            {
                if (transform.position.x + cameraPosition + 0.67786f <= Camera.main.transform.position.x)
                    isActivated = false;
                else
                    isActivated = true;
            }
            else
            {
                isActivated = false;
            }

            if (isActivated)
            {
                CountDown.DecreaseTime(countDownEffect);

                if (countDownEffect.ReturnedToZero)
                {
                    countDown.Rate = timeToRespaw;
                    countDown.StartToCount();
                }

                CountDown.DecreaseTime(countDown);

                if (countDown.ReturnedToZero)
                {
                    countDown.StartToCount();
                    Instantiate(prefabFireBall, transform.position + new Vector3(0.421f, -0.373f, 0), Quaternion.identity);
                    sr.sprite = secondFrame;
                    Invoke("FramesInteraction", 1);
                }
            }
        }

        public void StartEffect()
        {
            countDown.Rate = 1.5f;
            countDown.StartToCount();
        }

        void FramesInteraction()
        {
            sr.sprite = firstFrame;
        }
    }
}
