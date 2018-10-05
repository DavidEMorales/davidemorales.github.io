using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

	public string nextScene;
	CoinCollector coinCollector;

	// Use this for initialization
	void Start () {
		coinCollector = GameObject.Find("CoinCollector").GetComponent<CoinCollector>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player" && coinCollector.coins == 0){
			SceneManager.LoadScene(nextScene);
		}
	}
}
