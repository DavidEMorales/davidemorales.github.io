using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	public float speed;
	bool jumped = false;
	Vector3 initialPosition;

	// Use this for initialization
	void Start(){
		rb = GetComponent<Rigidbody>();
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update(){
		if (Input.GetAxis ("Horizontal") > 0) {
			rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
		} 
		else if (Input.GetAxis ("Horizontal") < 0) {
			rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
		} 
		else {
			rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
		}

		if (Input.GetAxis ("Vertical") > 0) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
		} 
		else if (Input.GetAxis ("Vertical") < 0) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
		} 
		else {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
		}


		if (Input.GetButtonDown ("Jump") && !jumped){
			rb.AddForce (10 * Vector3.up * Input.GetAxis ("Jump"), ForceMode.Impulse);
			jumped = true;
		}

		if (Input.GetButtonDown ("Restart")){
			SceneManager.LoadScene("Scene1");
		}


		if(transform.position.y < -2){
			ResetPosition();
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.CompareTag("Floor")){
			jumped = false;
		}
	}

	void ResetPosition(){
		transform.position = initialPosition;
		rb.velocity = Vector3.zero;
	}
}
