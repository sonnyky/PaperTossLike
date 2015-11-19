using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour, TextInterface {

	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnChange () {
		Debug.Log("onchange");
		GameManager gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		int currentPoint = gameManager.GetCurrentPoint ();
        Debug.Log("Inside game manager : " + currentPoint);
		scoreText.text = "Score: " + currentPoint.ToString ();
	}

}
