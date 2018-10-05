using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frysics : MonoBehaviour {

	private Vector3 tangent;
	public bool thrown = false;
	public float straightenSpeed;
	public float launchSpeed;
	public bool taken = false;
	public int spawnerNum;

	private Rigidbody rb;
	private MeshCollider[] mcs;
	private DiscReset dr;
	private AudioSource whoosh;
	private AudioSource drop;
	private AudioSource thunk;

	/*private Rigidbody rb;
	private Vector3 prevVelocity;
	private Quaternion prevRotation;
	private Vector3 velocity;
	private Quaternion rotation;*/

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		mcs = GetComponents<MeshCollider>();
		whoosh = GetComponents<AudioSource>()[0];
		drop = GetComponents<AudioSource>()[1];
		thunk = GetComponents<AudioSource>()[2];
		/*rb = GetComponent<Rigidbody>();
		velocity = rb.velocity;
		rotation = rb.rotation;
		prevVelocity = rb.velocity;
		prevRotation = rb.rotation;*/
	}

	void FixedUpdate(){
		dr = this.GetComponent<DiscReset>();
		tangent = transform.up;
		/*prevVelocity = velocity;
		prevRotation = rotation;
		velocity = rb.velocity;
		rotation = rb.rotation;*/

		if(thrown){
			Quaternion lookUp = Quaternion.LookRotation(Vector3.forward);
			Quaternion current = transform.localRotation;

			transform.localRotation = Quaternion.Slerp (current, lookUp, straightenSpeed * Time.fixedDeltaTime);

			transform.RotateAround(transform.position, transform.up, 12.0f);
			//transform.localRotation = Quaternion.Slerp (current, current + transform., straightenSpeed * Time.fixedDeltaTime);
		}

		if (mcs[1].enabled && transform.position.y < 0) {
			//drop.Play();
			thrown = false;
			transform.localRotation = Quaternion.LookRotation(Vector3.forward);
			transform.position = new Vector3 (transform.position.x, 0.01f, transform.position.z);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		GameObject other = col.gameObject;

		//rb.velocity = prevVelocity;
		//rb.rotation = prevRotation;

		/*if(other.CompareTag("Tree")){
			dr.resetDisc();
			thunk.Play();
		}*/
		if(other.CompareTag("Disc")){
			drop.Play();
		}
		if(!other.CompareTag("Controller")){
			mcs[0].enabled = true;
			thrown = false;
		}
	}
}

