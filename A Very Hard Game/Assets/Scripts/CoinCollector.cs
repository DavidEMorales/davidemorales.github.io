using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour {

	public int coins;

	// Use this for initialization
	void Start () {
		coins = GameObject.FindGameObjectsWithTag("Coin").Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RemoveCoin(){
		coins--;
	}

	void RestartLevel(){
		coins = GameObject.FindGameObjectsWithTag("Coin").Length;
	}
}
