using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PaperIconButton : MonoBehaviour {

    public int maxPaperNum = 3;

    private bool iconButtonClicked;

	// Flag to select paper
	// 0: Billiard, 1: PaperCrane, 2: Granade
	private int iconNum;

	private Sprite buttonImageSprite;

	// Use this for initialization
	void Start () {
		iconNum = 0;
        iconButtonClicked = false;
		buttonImageSprite = Resources.Load<Sprite> ("MyImages/BilliardBallImage");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (iconButtonClicked) {
            iconNum = (iconNum + 1) % maxPaperNum;
            Debug.Log ("icon " + iconNum);

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
            // Change the icon of this button
            this.GetComponent<Button> ().image.overrideSprite = buttonImageSprite;
            
            // Notify GameManager that the paper type has been changed
            ExecuteEvents.Execute<PaperTypeInterface> (
                target: GameObject.Find ("GameManager"),
                eventData: null,
                functor: (x,y) => x.OnPaperTypeChange (iconNum)
            );

            // Respawn the new paper
            ShootScript shootScript = GameObject.Find ("Main Camera").GetComponent<ShootScript> ();
            shootScript.RespawnBall ();

            iconButtonClicked = false;
        }
	}

	public void OnClick () {
        iconButtonClicked = true;
	}
}
