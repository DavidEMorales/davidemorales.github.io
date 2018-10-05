using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour {

	private Animator anim;
	private bool isEating;
	public ScoreController SC;
	private bool isDead = false;
	public AudioSource screaming;
	public GameObject scoreDisplay;

    public bool testScare;
    float shockTimer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        int toEat = Random.Range(1, 100);

		if(toEat > 93 && isEating){
			isEating = false;
			anim.SetBool("isEating", false);
		}

		if(toEat < 6 && !isEating){
			isEating = true;
			anim.SetBool("isEating", true);
		}

		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

		if(state.IsName("Die2"))
		{
			anim.SetBool("isDead", false); 
		}
		if(state.IsName("End"))
		{
			anim.enabled = false;
		}


        if (shockTimer > 0)
        {
            Debug.Log(shockTimer);
            shockTimer -= Time.deltaTime;
            if (shockTimer <= 0)
            {
                anim.SetBool("isShocked", false);
            }
        }
    }


	void OnTriggerEnter(Collider col){
		if (col.CompareTag("Disc") && !isDead){
			anim.SetBool("isDead", true);
			isDead = true;
			SC.score -= 500;
			scoreDisplay.GetComponent<MeshRenderer>().enabled = true;
			scoreDisplay.GetComponent<Animator>().enabled = true;
			screaming.Play();

        }
        if (col.gameObject.tag == "Tree")
        {
            Debug.Log("COLLIDED");
            anim.SetBool("isShocked", true);
            shockTimer = 2.0f;
        }
    }

}
