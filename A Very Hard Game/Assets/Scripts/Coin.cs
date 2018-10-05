using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	CoinCollector coinCollector;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		coinCollector = GameObject.Find("CoinCollector").GetComponent<CoinCollector>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f, rotationSpeed, 0f));
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			coinCollector.SendMessage("RemoveCoin");
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
		}
	}

	void RestartLevel(){
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<SphereCollider>().enabled = true;
	}
}
