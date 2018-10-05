using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSpawner : MonoBehaviour {

	public GameObject spawnblade;

	// Use this for initialization
	void Start () {
		//in future, make field public and drag in object
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnBlade(){
		GameObject newBlade = Object.Instantiate(spawnblade);

		MeshCollider[] MCs = newBlade.GetComponents<MeshCollider>();
		MCs[1].enabled = true;
		newBlade.GetComponent<MeshRenderer>().enabled = true;
	}

}
