using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick() {
        Debug.Log("Button click!");

		// We can add something (an alert like "Do you quit the game?") before quitting the game

		// Return to the title
		Application.LoadLevel ("TitleScene");
    }
}
