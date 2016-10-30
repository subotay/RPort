using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attributes : MonoBehaviour{
	public float ARMOR;
	public float BONDMG;
	public float HIT;
	public float EVA;
	public float CRIT;
	public float MHP;
	public float HPREG;
	public float MSTAM;
	public float STAREG;
	public float FATK;
	public float FRES;
	public float EATK;
	public float ERES;

	public float STR;
	public float AGI;
	public float VIT;
	public float END;
	public float SPI;

	public float hp,stam;
	[HideInInspector]public float accHp, accStam;
	public float energ;
	public float atkcost;
	public bool melee;
    public bool dumb;
    public float range;
    public enum Speed { SLOW, NORMAL, HASTED}
    public Speed speed;
	/*------------------------------------------*/

	public float mhp(){ return VIT * 10 + MHP;}
	public float mstam(){ return END*2+ MSTAM;}
	//float accHp;
	public float hpreg(){ return VIT*.2f+ HPREG; }
	//float accStam;
	public float stareg(){ return END*.2f+ STAREG;}

	public float hit(){ return (STR+ AGI-10)/2+ HIT;}
	public float eva(){ return AGI-5+ EVA;}

	public float fatk(){ return STR-5+ FATK;}
	public float fres(){ return VIT-5+ FRES;}
	public float eatk(){ return SPI-5+ EATK;}
	public float eres() { return END-5+ ERES;}
	//dmg   //+ weapon dmg(erou)

	public float dmg(){
		return (melee? 2*STR: 2*AGI)  //stat bonus
			+ BONDMG; //simuleaza arma
	}

	/* crit direct  //poate depinde de skill
   armor direct
*/



}

