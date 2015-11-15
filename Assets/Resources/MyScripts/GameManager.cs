﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour, PointInterface {

	// 1: easy, 2: medium, 3: hard
	private static int difficulty;
    private static Vector3 easy_pos = new Vector3(0f, -0.5f, 1),
        med_pos = new Vector3(0f, -0.5f, 0.5f),
        hard_pos = new Vector3(0f, -0.5f, 0f);

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

    public static Vector3 GetBinPosition(int difficulty)
    {
        if(difficulty == 1)
        {
            return easy_pos;
        }else if(difficulty == 2)
        {
            return med_pos;
        }else if(difficulty == 3)
        {
            return hard_pos;
        }
        else
        {
            return easy_pos;
        }
    }

	// Use this for initialization
	void Start () {
        LoadUiObjects();
		Debug.Log (GameManager.GetDifficulty());
		currentPoint = 0;
		highScorePoint = 0;
        //change position of recycle bin according to difficulty
        switch (GameManager.GetDifficulty())
        {
            case 1:
                GameObject trash_bin_easy = GameObject.Instantiate(Resources.Load("MyAssets/Prefabs/Animations/recycle_bin"), easy_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_easy.name = "recycle_bin";
                Debug.Log("Easy mode");
                break;
            case 2:
                GameObject trash_bin_medium = GameObject.Instantiate(Resources.Load("MyAssets/Prefabs/Animations/recycle_bin"), med_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_medium.name = "recycle_bin";
                Debug.Log("Medium mode");
                break;
            case 3:
                GameObject trash_bin_hard = GameObject.Instantiate(Resources.Load("MyAssets/Prefabs/Animations/recycle_bin"), hard_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_hard.name = "recycle_bin";
                Debug.Log("Hard mode");
                break;
            default:
                GameObject trash_bin_default = GameObject.Instantiate(Resources.Load("MyAssets/Prefabs/Animations/recycle_bin"), easy_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_default.name = "recycle_bin";
                Debug.Log("Default mode");
                break;
        }


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
        GameObject.Instantiate(Resources.Load("MyAssets/Prefabs/UI/UICamera"));
    }

}
