using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

	int time;
	public GameObject Fireball;
	public int period;
	public int speed;
	public int lifetime;

	// Use this for initialization
	void Start () {
		time = 0;
	}

	// Update is called once per frame
	void Update () {
		time++;

		if(time % period == 0){
			GameObject fireball = Instantiate (Fireball, transform.position, Quaternion.identity);
			fireball.GetComponent<FireballMover>().active = true;
			fireball.GetComponent<FireballMover>().lifetime = lifetime;
			fireball.GetComponent<FireballMover>().enabled = true;
			fireball.GetComponent<MeshRenderer>().enabled = true;
			fireball.GetComponent<Rigidbody>().AddForce(speed * 100 * transform.up, ForceMode.Force);
			time = 0;
		}
	}

	void OnRestart(){
		time = 0;
	}

}
