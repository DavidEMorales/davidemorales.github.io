using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject player;
	Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		lastPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (player.transform.position - lastPosition);
		lastPosition = player.transform.position;
	}
}
