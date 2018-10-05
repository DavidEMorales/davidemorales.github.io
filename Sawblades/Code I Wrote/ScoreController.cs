using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	public int score = 0;
	public int prevScore = 0;
	private TextMesh scoreDisplay;
	public bool isMainStump;
	public TextMesh[] otherSawbladeStumpScoreDisplays;

	// Use this for initialization
	void Start () {
		scoreDisplay = transform.GetChild(0).GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(score != prevScore){
		//}
		if(isMainStump){
			scoreDisplay.text = "" + score;
			prevScore = score;
			otherSawbladeStumpScoreDisplays[0].text = "" + score;
			otherSawbladeStumpScoreDisplays[1].text = "" + score;
			otherSawbladeStumpScoreDisplays[2].text = "" + score;
		}
	}
}
