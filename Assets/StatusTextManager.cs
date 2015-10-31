using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusTextManager : MonoBehaviour, TextInterface {

	public Text statusText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnChange () {
		Debug.Log("onchange");
		GameManager gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		int currentPoint = gameManager.GetCurrentPoint ();
		statusText.text = currentPoint.ToString ();
	}

}
