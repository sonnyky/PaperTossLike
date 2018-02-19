using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PaperIconButton : MonoBehaviour {

    public int maxPaperNum = 3;
    public Sprite billiardBall;
    public Sprite paperCrane;
    public Sprite grenade;
    private bool iconButtonClicked;

	// Flag to select paper
	// 0: Billiard, 1: PaperCrane, 2: Granade
	private int iconNum;
    private ShootScript shooter;
	private Sprite buttonImageSprite;

	// Use this for initialization
	void Start () {
        shooter = GameObject.Find("Shooter").GetComponent<ShootScript>();
		iconNum = 0;
        iconButtonClicked = false;
		buttonImageSprite = billiardBall;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (iconButtonClicked) {
            iconNum = (iconNum + 1) % maxPaperNum;
            Debug.Log ("icon " + iconNum);

            // Load the image of button
            switch (iconNum) {
            case 0:
                buttonImageSprite = billiardBall;
                break;
            case 1:
                buttonImageSprite = paperCrane;
                break;
            case 2:
                buttonImageSprite = grenade;
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
            shooter.RespawnBall ();

            iconButtonClicked = false;
        }
	}

	public void OnClick () {
        iconButtonClicked = true;
	}
}
