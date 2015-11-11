using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour, PointInterface {

	// 1: easy, 2: medium, 3: hard
	private static int difficulty;

	private int currentPoint;
	private int highScorePoint;

	// Get diffculty in the title scene
	public static void SetDifficulty (int difficulty) {
		GameManager.difficulty = difficulty;
	}

	// Get diffculty from the title scene
	public static int GetDifficulty () {
		return difficulty;
	}

	// Use this for initialization
	void Start () {
        LoadUiObjects();
		Debug.Log (GameManager.GetDifficulty());
		currentPoint = 0;
		highScorePoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called by other classes like
	// ExecuteEvents.Execute<PointInterface>(
    //     target: GameObject.Find("GameManager"),
    //     eventData: null,
    //     functor: (x,y)=>x.OnSuccess());
	public void OnSuccess () {
		currentPoint += 1;
		Debug.Log (currentPoint);
		highScorePoint = System.Math.Max (currentPoint, highScorePoint);
		ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("ScoreText"),
			eventData: null,
			functor: (x,y)=>x.OnChange());
		ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("HighScoreText"),
			eventData: null,
			functor: (x,y)=>x.OnChange());
	}

	// Called by other classes like
	// ExecuteEvents.Execute<PointInterface>(
	//     target: GameObject.Find("GameManager"),
	//     eventData: null,
	//     functor: (x,y)=>x.OnFailure());
	public void OnFailure () {
		currentPoint = 0;
		ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("ScoreText"),
			eventData: null,
			functor: (x,y)=>x.OnChange());
	}

	public int GetCurrentPoint () {
		return currentPoint;
	}

	public int GetHighScorePoint () {
		return highScorePoint;
	}

    private void LoadUiObjects()
    {
        GameObject UiGroup = (GameObject) Instantiate(Resources.Load("MyAssets/Prefabs/UI/UICamera")) as GameObject;
    }

}
