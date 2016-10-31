using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attributes : MonoBehaviour, ISerializationCallbackReceiver {
    public enum Bonus {
        ARMOR,
        CRIT,
        DMG, 
        HIT, 
        EVA,
        MHP,
        MSTAM,
        HPREG,
        STAREG
    }
    private Dictionary<Bonus, float> bonus;
    [SerializeField] private List<Bonus> bonus_keys;
    [SerializeField] private List<float> bonus_values;

    public void updateBonus(Bonus b, float amt) {
        bonus[b] += amt;
        switch (b) {
            case Bonus.ARMOR:
                armor.Val = bonus[b];
                break;
            case Bonus.CRIT:
                crit.Val = bonus[b];
                break;
            case Bonus.DMG:
                dmg.Val = melee ? 2 * STR : 2 * AGI  //stat 
                          + bonus[b];                //weapon 
                break;
            case Bonus.HIT:
                hit.Val = (STR + AGI - 10) / 2 + bonus[b];
                break;
            case Bonus.EVA:
                eva.Val = AGI - 5 + bonus[b];
                break;
            case Bonus.MHP:
                hp.MaxVal = VIT * 10 + + bonus[b];
                break;
            case Bonus.MSTAM:
                stam.MaxVal = END * 2 + + bonus[b];
                break;
            case Bonus.HPREG:
                hpreg.Val = VIT * .1f + +bonus[b];
                break;
            case Bonus.STAREG:
                stareg.Val = END * .1f + +bonus[b];
                break;
            default: break;
        }
    }


    [SerializeField] private float _STR;
    [SerializeField] private float _AGI;
    [SerializeField] private float _VIT;
    [SerializeField] private float _END;
    public float STR {
        get { return _STR; } 
        set {
            _STR = value;
            hit.Val = (value + AGI - 10) / 2 + bonus[Bonus.HIT];
            if (melee)
                dmg.Val = 2 * value + bonus[Bonus.DMG];
        }
    }

    public float AGI {
        get { return _AGI;}
        set {
            _AGI = value;
            hit.Val = (value + STR - 10) / 2 + bonus[Bonus.HIT];
            eva.Val = value- 5 + bonus[Bonus.EVA];
            if (!melee)
                dmg.Val = 2 * value + bonus[Bonus.DMG];
        }
    }

    public float VIT {
        get {
            return _VIT;
        }

        set {
            _VIT = value;
            hp.MaxVal = value * 10 + +bonus[Bonus.MHP];
            hpreg.Val = value * .1f + +bonus[Bonus.HPREG];
        }
    }

    public float END {
        get {
            return _END;
        }

        set {
            _END = value;
            stam.MaxVal = value * 2 + +bonus[Bonus.MSTAM];
            stareg.Val = value* .1f + +bonus[Bonus.STAREG];
        }
    }


    public Stat hp, stam, hit, eva, hpreg ,stareg, dmg, armor, crit;
	[HideInInspector]public float accHp, accStam;

	public float energ;
	public float atkcost;
    public bool melee;
    public bool dumb;
    public float range;

    public enum Speed { SLOW, NORMAL, HASTED}
    public Speed speed;

    void Awake() {
        armor.MaxVal = 1000;
        hp.MaxVal = 1000;
        stam.MaxVal = 1000;
        hit.MaxVal = 100;
        eva.MaxVal = 100;
        hpreg.MaxVal = 1000;
        stareg.MaxVal = 1000;
        dmg.MaxVal = 1000;
        crit.MaxVal = 100;
        bonus = new Dictionary<Bonus, float>();
        bonus.Add(Bonus.ARMOR, 5);
        bonus.Add(Bonus.CRIT, 0);
        bonus.Add(Bonus.DMG, 5);
        bonus.Add(Bonus.EVA, 0);
        bonus.Add(Bonus.HIT, 0);
        bonus.Add(Bonus.MHP, 10);
        bonus.Add(Bonus.MSTAM, 5);
        bonus.Add(Bonus.HPREG, 1);
        bonus.Add(Bonus.STAREG, .5f);
        STR = 5;
        AGI = 5;
        END = 5;
        VIT = 10;
        List<Bonus> keys = new List<Bonus>(bonus.Keys);
        foreach (Bonus b in keys)
            updateBonus(b, 0);
        hp.Val = 100;
        stam.Val = 10;
    }
   
    public void OnBeforeSerialize() {
        bonus_keys = new List<Bonus>(bonus.Keys);
        bonus_values = new List<float>();
        foreach (Bonus b in bonus_keys) {
            bonus_values.Add(bonus[b]);
        }
    }

    public void OnAfterDeserialize() {
        bonus = new Dictionary<Bonus, float>();
        for (var i=0; i<bonus_keys.Count; i++) {
            bonus.Add(bonus_keys[i], bonus_values[i]);
        }
    }
}

