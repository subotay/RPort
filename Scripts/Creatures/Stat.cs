using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.UI;

[Serializable]
public class Stat  {
    [SerializeField]private FilledBar bar;
    
    [SerializeField]private float maxVal;
    public float MaxVal {
        get { return maxVal; }

        set {
            maxVal = value;
            if (bar!= null)
                bar.MaxVal = value;
        }
    }
    [SerializeField] private float val;
    public float Val {
        get { return val; }

        set {
            val = Mathf.Clamp(value, 0, MaxVal);
            if (bar !=null)
                bar.Amount = val;
        }
    }

    public void init() {
        Val = val;
        MaxVal = maxVal;
    }
}
