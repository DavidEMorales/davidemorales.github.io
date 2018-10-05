using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutting : MonoBehaviour {

	public AudioSource falling;
	private Rigidbody rb;
	private bool wasCut = false;
	private Vector3 aimTowards = Vector3.up;
	public float tiltSpeed = 10f;
	private Animator anim;
	public int scoreValue;
	public ScoreController SC;
	private TreeCutting[] TCs;
	private TreeCutting[] childTCs;
	private TextMesh[] childTMs;
	private MeshCollider[] mcs;
	public ParticleSystem[] sparks;
	public GameObject scoreDisplay;

    //public bool testcut = false;

    SphereCollider treeFallCollider;
    float collideTimer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		falling = GetComponents<AudioSource>()[0];
		rb = GetComponent<Rigidbody>();
		TCs = transform.root.GetComponentsInChildren<TreeCutting>();
		childTCs = transform.GetComponentsInChildren<TreeCutting>();
		childTMs = transform.GetComponentsInChildren<TextMesh>();
		mcs = GetComponents<MeshCollider>();
        treeFallCollider = gameObject.AddComponent<SphereCollider>();
        treeFallCollider.isTrigger = true;
        treeFallCollider.center = Vector3.zero;
        treeFallCollider.radius = 10.0f;
        treeFallCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        /*if (testcut)
        {
            treeFallCollider.enabled = true;
            collideTimer = 1.0f;
            testcut = false;
        }*/

        if (collideTimer > 0)
        {
            collideTimer -= Time.deltaTime;
            if (collideTimer <= 0) {
                treeFallCollider.enabled = false;
            }
        }
    }

	void FixedUpdate(){
		
	}

	void OnTriggerEnter(Collider col){
		if(col.CompareTag("Disc")){

			bool topIsFalling = false;

			foreach(TreeCutting cTC in childTCs){
				if(cTC.enabled){
					topIsFalling = topIsFalling || (cTC.anim.enabled && (!cTC.anim.GetCurrentAnimatorStateInfo(0).IsName("DeadTree")));
				}
			}

			if(!topIsFalling){

				//Scoring system might need some work to make sure people aim for the bottom,
				//Right now difference is unnoticeable
				SC.score += scoreValue;
				scoreDisplay.GetComponent<TextMesh>().text = "+" + scoreValue;
				scoreDisplay.GetComponent<MeshRenderer>().enabled = true;
				scoreDisplay.GetComponent<Animator>().enabled = true;


				foreach(TreeCutting TC in TCs){
					if(TC != this){
						TC.scoreValue -= 2 * scoreValue;
						TC.scoreDisplay.GetComponent<TextMesh>().text = "+" + TC.scoreValue; 
					}
				}

				foreach(TextMesh cTM in childTMs){
					cTM.text = "";
				}

				foreach(TreeCutting cTC in childTCs){
					cTC.mcs[1].enabled = false;
				}


				anim.SetBool("isCut", true);
				anim.enabled = true;

				falling.Play();

				sparks[0].Play();
				sparks[1].Play();

                treeFallCollider.enabled = true;
                collideTimer = 1.0f;
            }
        }
	}



	/*void OnCollisionEnter(Collision coll){
		Debug.Log ("Collided.");

		Collider col = coll.collider;

		if(col.CompareTag("Disc")){
			this.rb. [[ADD RIGIDBODIES as KINEMATIC]]
			this.GetComponent<MeshRenderer>().enabled = false;
			this.GetComponent<Collider>().enabled = false;
			falling.Play();
		}
	}*/
}
