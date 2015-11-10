using UnityEngine;
using System.Collections;

public class ToGameButtonScript : MonoBehaviour {

	// 1: easy, 2: medium, 3: hard
	public int difficulty;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick (int difficulty) {
		// Set the difficulyt of the game into GameManager as a static variable
		GameManager.SetDifficulty(difficulty);

		// Invoke GameScene.unity
		Application.LoadLevel ("GameScene");
	}

}
