using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

	public Sayori sayori;
	public GameObject[] bottles;
	public int health;

	private int wobbleLength = 200;
	public int currWobble = 0;

	// Use this for initialization
	void Start () {
		health = bottles.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (health == 0)
			return;


		Animator anim = bottles[health-1].transform.GetChild(0).GetComponent<Animator>();
		if (sayori.isSad) {
			anim.SetBool("Wobbling", true);
			currWobble++;
			if (currWobble >= wobbleLength) {
				anim.SetBool("Falling", true);
			}
		} 
		else {
			anim.SetBool("Wobbling", false);
			currWobble = 0;
		}

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Dead")){
			bottles[health-1].transform.GetChild(0).gameObject.SetActive(false);
			bottles[health-1].transform.GetChild(1).gameObject.SetActive(true);
			Transform currLight = bottles [health - 1].transform.GetChild(2);
			bottles [health - 1].transform.GetChild(2).position = 
				new Vector3(currLight.position.x,currLight.position.y-(bottles[bottles.Length-1].transform.GetChild(2).position.y - currLight.position.y),currLight.position.z);
			bottles[health-1].transform.GetChild(2).gameObject.SetActive(true);
			health--;
			currWobble = 0;
		}

	}
}
