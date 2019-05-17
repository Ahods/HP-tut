using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public int maxHP;
    public int hp;

	/// <summary>
    /// Only thing that is needed here.
    /// </summary>
	void Start () {
        maxHP = 5;
        hp = 5;
	}
	
}
