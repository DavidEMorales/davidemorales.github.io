using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	public bool gripButtonDown = false;
	public bool gripButtonUp = false;
	public bool gripButtonPressed = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public bool triggerButtonDown = false;
	public bool triggerButtonUp = false;
	public bool triggerButtonPrevPressed = false;
	public bool triggerButtonPressed = false;

	private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	public bool menuButtonDown = false;
	public bool menuButtonUp = false;
	public bool menuButtonPrevPressed = false;
	public bool menuButtonPressed = false;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller{ get{ return SteamVR_Controller.Input((int)trackedObj.index); } }

	private GameObject touching;
	private FixedJoint joint;

	private Rigidbody holding;
	private bool throwing = false;
	public int stored_velocities;
	private Vector3[] velocities;
	private int velocity_index = 0;
	private bool paused = false;
	public UIController UI;

	private GameObject[] trees;
	private GameObject[] sawblades;
	private GameObject[] discSpawners;
	public GameObject[] controllers;

	// Use this for initialization
	void Start () {
		velocities = new Vector3[5];
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		joint = GetComponent<FixedJoint> ();
		trees = GameObject.FindGameObjectsWithTag("Tree");
		sawblades = GameObject.FindGameObjectsWithTag("Disc");
		discSpawners = GameObject.FindGameObjectsWithTag("Spawner");
		controllers = GameObject.FindGameObjectsWithTag("Controller");
	}
	
	// Update is called once per frame
	void Update () {
		if(controller == null){
			Debug.Log("Controller not initialized.\n");
			return;
		}

		gripButtonDown = controller.GetPressDown(gripButton);
		gripButtonUp = controller.GetPressUp(gripButton);
		gripButtonPressed = controller.GetPress(gripButton);
		triggerButtonDown = controller.GetPressDown(triggerButton);
		triggerButtonUp = controller.GetPressUp(triggerButton);
		triggerButtonPressed = controller.GetPress(triggerButton);
		menuButtonDown = controller.GetPressDown(menuButton);
		menuButtonUp = controller.GetPressUp(menuButton);
		menuButtonPressed = controller.GetPress(menuButton);        


		if (triggerButtonDown) {
			PickupObj ();
		}
		if(triggerButtonPrevPressed && !triggerButtonPressed){
			DropObj();
		}
		if(menuButtonPrevPressed && !menuButtonPressed){
			UI.toObjectMode();
		}

		if (menuButtonDown) {
			if (paused) {
				UI.toObjectMode ();
			} 
			else {
				UI.toPointerMode();
			}
		}

		triggerButtonPrevPressed = triggerButtonPressed;

	}


	void FixedUpdate(){

		Transform origin;
		if (trackedObj.origin != null) {
			origin = trackedObj.origin;
		} 
		else {
			origin = trackedObj.transform.parent;
		}

		if (origin != null) {
			velocities[velocity_index] = origin.TransformVector(controller.velocity);
		} 
		else {
			velocities[velocity_index] = controller.velocity;
		}
		velocity_index++;
		velocity_index = velocity_index % 5;

		if(throwing){

			float launchSpeed = 1.0f; //placeholder for Frysics.cs override (in case object is not a disc)
			if(holding.CompareTag("Disc")){
				launchSpeed = holding.GetComponent<Frysics>().launchSpeed;
				holding.GetComponent<Frysics>().thrown = true;
			}


			if (trackedObj.origin != null) {
				velocities[5] = holding.velocity = (velocities[(velocity_index+1)%5]) * launchSpeed;
				//holding.angularVelocity = origin.TransformVector (controller.angularVelocity);
			} 
			else {
				holding.velocity = (velocities[(velocity_index+1)%5]) * launchSpeed;
				//holding.angularVelocity = controller.angularVelocity;
			}

			//holding.maxAngularVelocity = holding.angularVelocity.magnitude;

			throwing = false;
			holding = null;
		}
	}


	void OnTriggerEnter(Collider col){
		Debug.Log ("Entering trigger.");
		if(col.CompareTag("Disc")){
			Debug.Log ("Trigger is pickup.");
			touching = col.gameObject;
		}
	}

	void OnTriggerExit(){
		Debug.Log ("Trigger is empty.");
		touching = null;
	}

	void PickupObj(){
		Debug.Log ("Trying to pick up...");
		if (touching != null) {
			Debug.Log ("Picking up.");

			Frysics frys = touching.GetComponent<Frysics> ();
			if(!frys.taken){
				frys.taken = true;
				discSpawners[frys.spawnerNum].GetComponent<DiscSpawner>().spawnBlade();

				Rigidbody rb = touching.GetComponent<Rigidbody>();
				rb.isKinematic = false;
				rb.useGravity = true;

			}


			//if touching something, rotate it to be parallel to the controller and attach it to the controller
			Quaternion rotation = Quaternion.LookRotation(trackedObj.GetComponent<Rigidbody>().transform.forward);
			touching.GetComponent<Rigidbody>().transform.localRotation = rotation;

			touching.GetComponent<Rigidbody>().transform.position = 
				trackedObj.GetComponent<Rigidbody>().transform.position + trackedObj.GetComponent<Rigidbody>().transform.forward * 0.18f;

			touching.GetComponents<MeshCollider>()[0].enabled = false;
			touching.GetComponents<MeshCollider>()[1].enabled = false;

			//this.GetComponent<SteamVR_RenderModel>().enabled = false;
			//GetComponent<SteamVR_RenderModel>();
			ShowRenderModel(false);


			joint.connectedBody = touching.GetComponent<Rigidbody>();
			throwing = false;
			holding = joint.connectedBody;
		} 
		else {
			Debug.Log ("Failed to pick up.");
			joint.connectedBody = null;
			holding = null;
		}
	}

	void DropObj(){
		Debug.Log ("Trying to drop...");
		if(joint.connectedBody != null){
			Debug.Log ("Dropping.");

			joint.connectedBody = null;
			//holding.GetComponents<MeshCollider>()[0].enabled = true;
			holding.GetComponents<MeshCollider>()[1].enabled = true;
			throwing = true;
			//this.GetComponent<SteamVR_RenderModel>().enabled = true;
			//GetComponent<Renderer>().enabled = true;
			ShowRenderModel(true);
		}
	}

	private void ShowRenderModel(bool hidden)
	{
		var meshRenderer = GetComponent<MeshRenderer>();
		if (meshRenderer != null)
			meshRenderer.enabled = hidden;
		foreach (var child in transform.GetComponentsInChildren<MeshRenderer>())
			child.enabled = hidden;
	}


}
