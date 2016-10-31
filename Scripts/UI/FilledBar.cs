using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class FilledBar: MonoBehaviour {
        [SerializeField]
        private float speed = 2f;

        [SerializeField]
        private Image fill;

        [SerializeField]
        private Text displayedText;

        private float amount;
        public float Amount {
            set {
                displayedText.text = value + " / " + MaxVal;
                amount = setAmount(value, 0, MaxVal, 0, 1);
            }
        }

        public float MaxVal { get; set; }



        void Update() {
            if (amount != fill.fillAmount) {
                fill.fillAmount = Mathf.Lerp(fill.fillAmount, amount, Time.deltaTime * speed);
            }
        }

        private float setAmount(float val, float inMin, float inMax, float outMin, float outMax ) {
            return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    }
}
