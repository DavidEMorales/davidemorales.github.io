using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscReset : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 startingPos;
	private Quaternion startingRot;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		startingPos = rb.position;
		startingRot = rb.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnTriggerEnter(Collider col){
		if(col.CompareTag("Tree")){
			resetDisc();
		}
	}*/

	public void resetDisc(){
		rb.position = startingPos;
		rb.velocity = Vector3.zero;
		rb.rotation = startingRot;
	}
}
