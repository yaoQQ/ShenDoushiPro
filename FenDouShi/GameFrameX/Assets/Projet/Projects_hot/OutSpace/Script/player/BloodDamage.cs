using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class BloodDamage : MonoBehaviour
    {
        private Image bloodUp;
        private Image bloodDown;
        private Image bloodRight;
        private Image bloodLeft;
        public void Awake()
        {
            bloodUp = this.transform.Find("bloodUp").GetComponent<Image>();
            bloodDown = this.transform.Find("bloodDown").GetComponent<Image>();
            bloodRight = this.transform.Find("bloodRight").GetComponent<Image>();
            bloodLeft = this.transform.Find("bloodLeft").GetComponent<Image>();

            bloodUp.gameObject.SetActive(false);
            bloodDown.gameObject.SetActive(false);
            bloodRight.gameObject.SetActive(false);
            bloodLeft.gameObject.SetActive(false);
        }

        public Image damageFard(farward fa)
        {
            Image target = null;
            switch (fa)
            {
                case farward.down:
                    target = bloodDown;
                    break;
                case farward.left:
                    target = bloodLeft;
                    break;
                case farward.right:
                    target = bloodRight;
                    break;
                case farward.up:
                    target = bloodUp;
                    break;
            }
            return target;
        }
    }
