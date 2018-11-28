using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	private int spawnDelay = 60;
	public Sayori sayori;
	private int currStep = 0;
	public GameObject raincloud;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sayori.lc.health == 0) 
			return;
		

		currStep++;
		if (currStep < spawnDelay) return;
		currStep = 0;
		if(spawnDelay > 11) spawnDelay--;
			

		GameObject newCloud = GameObject.Instantiate(raincloud);
		newCloud.transform.position = new Vector2(Random.Range(-10.0f,10.0f),5f);
		newCloud.SetActive(true);

	}
}
