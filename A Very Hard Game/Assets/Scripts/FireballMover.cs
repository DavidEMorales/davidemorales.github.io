using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireballMover : MonoBehaviour {

	GameObject cannon;
	GameObject player;
	CoinCollector coinCollector;
	int time;
	public int lifetime;
	public bool active = false;

	// Use this for initialization
	void Start () {
		time = 0;
		player = GameObject.Find("Player");
		coinCollector = GameObject.Find("CoinCollector").GetComponent<CoinCollector>();
	}
	
	// Update is called once per frame
	void Update () {
		time++;
		if(active && time >= lifetime && lifetime != -1){
			print ("Destroyed fireball (time)");
			Destroy(this.gameObject);
		}

	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			collider.gameObject.SendMessage("ResetPosition");
			coinCollector.BroadcastMessage("RestartLevel");
		}
	}

	void OnRestart(){
		if(active){
			print ("Destroyed fireball");
			Destroy(this.gameObject);
		}
	}
}
