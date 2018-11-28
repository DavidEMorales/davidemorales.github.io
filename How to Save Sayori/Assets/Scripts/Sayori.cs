using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sayori : MonoBehaviour {

	public GameObject snug;
	public GameObject cheer;
	public GameObject sad;
	public GameObject failure;
	public GameObject ending;

	public List<GameObject> haunters;
	public LifeController lc;
	public int score = 0;
	private int maxScore = 200;

	public bool isSad = false;
	private bool wantsToCheer = false;
	private int cheerTimer = 0;
	private int maxCheerTime = 22;

	// Use this for initialization
	void Start () {
		maxScore = Random.Range(200, 300);
		haunters = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

		if(score >= maxScore){
			cheer.SetActive(false);
			sad.SetActive(false);
			snug.SetActive(false);
			ending.SetActive(true);
			isSad = false;
			return;
		}

		if (lc.health == 0) {
			cheer.SetActive(false);
			sad.SetActive(false);
			snug.SetActive(false);
			failure.SetActive(true);
			return;
		}


		if(wantsToCheer){
			if (!isSad) {
				cheer.SetActive(true);
				sad.SetActive(false);
				snug.SetActive(false);
				cheerTimer = maxCheerTime;
			}
			wantsToCheer = false;
		}

		if(isSad){
			if (!sad.activeInHierarchy) {
				sad.SetActive(true);
				snug.SetActive(false);
				cheer.SetActive(false);
			}
			cheerTimer = 0;
			return;
		}


		if (cheerTimer > 0) {
			cheerTimer--;
		} 
		else {
			snug.SetActive(true);
			cheer.SetActive(false);
			sad.SetActive(false);
		}


	}

	public void GetSad(GameObject newHaunter){
		isSad = true;
		haunters.Add(newHaunter);
	}

	public void Cheer(GameObject haunter){
		haunters.Remove(haunter);
		if (haunters.Count == 0) {
			isSad = false;
		}
		wantsToCheer = true;
	}
		
}
