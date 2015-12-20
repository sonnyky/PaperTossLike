using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PaperIconButton : MonoBehaviour {

    public int maxPaperNum = 3;

	// Flag to select paper
	// 0: Billiard, 1: PaperCrane, 2: Granade
	private int iconNum;

	private Sprite buttonImageSprite;

	// Use this for initialization
	void Start () {
		iconNum = 0;
		buttonImageSprite = Resources.Load<Sprite> ("MyImages/BilliardBallImage");
	}
	
	// Update is called once per frame
	void Update () {
		// Change the icon of this button
		this.GetComponent<Button> ().image.overrideSprite = buttonImageSprite;
	}

	public void OnClick () {
	    Debug.Log ("icon " + iconNum);
		iconNum = (iconNum + 1) % maxPaperNum;

        // Load the image of button
	    switch (iconNum) {
		case 0:
			buttonImageSprite = Resources.Load<Sprite> ("MyImages/BilliardBallImage");
			break;
		case 1:
			buttonImageSprite = Resources.Load<Sprite> ("MyImages/PaperPlaneImage");
			break;
		case 2:
			buttonImageSprite = Resources.Load<Sprite> ("MyImages/GranadeImage");
			break;
		}

		// Notify GameManager that the paper type has been changed
		ExecuteEvents.Execute<PaperTypeInterface>(
			target: GameObject.Find("GameManager"),
			eventData: null,
			functor: (x,y)=>x.OnPaperTypeChange(iconNum)
		);

		ShootScript shootScript = GameObject.Find ("Main Camera").GetComponent<ShootScript> ();
		shootScript.RespawnBall ();
	}
}
