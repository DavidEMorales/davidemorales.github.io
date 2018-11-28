using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour {

	public Sayori sayori;
	private bool haunted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(sayori.lc.health == 0 || sayori.ending.activeInHierarchy)
			GameObject.Destroy(this.gameObject);
		

		if (Vector3.Magnitude (sayori.transform.position - transform.position) > 1) {
			transform.position = Vector2.Lerp (transform.position, sayori.transform.position, Time.deltaTime);
		} 
		else if(!haunted){
			sayori.GetSad(this.gameObject);
			haunted = true;
		}

	}

	void OnMouseDown(){
		sayori.score++;
		sayori.Cheer(this.gameObject);
		GameObject.Destroy(this.gameObject);
	}
}
