using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour, TextInterface {

	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnChange () {
		GameManager gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		int highScorePoint = gameManager.GethighScore();
		scoreText.text = "HighScore: " + highScorePoint.ToString ();
	}
}
