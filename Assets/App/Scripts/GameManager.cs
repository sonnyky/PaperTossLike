using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour, PointInterface, PaperTypeInterface {

	// Game Difficulty as selected in the title screen. 1: easy, 2: medium, 3: hard
	private static int difficulty;

    public  GameObject uiCamera;
    public  GameObject recycleBin;
    public  GameObject windmill;

    private static ILogger logger = Debug.unityLogger;
    private static string  kTAG   = "TinkerApp_PaperToss";

    private static Vector3 easy_pos = new Vector3(0f, -0.5f, 1),
        med_pos = new Vector3(0f, -0.5f, 0.5f),
        hard_pos = new Vector3(0f, -0.5f, 0f);
    private static Vector3 wind_pos = new Vector3(0.23f, -0.42f, 1.45f);

	private int currentScore;
	private int highScore;

    // The type of paper (0: BilliardBall, 1: PaperCrane, 2: Granade)
	private int paperType;

    // This static method is reachable from anywhere so we can launch the game with the correct difficulty settings.
    public static void SetDifficulty (int difficulty) {
		GameManager.difficulty = difficulty;
	}

    /// <summary>
    /// Returns the game difficulty settings. Static so it can be accessed anywhere.
    /// </summary>
    /// <returns>int. 1: easy, 2: medium, 3: hard </returns>
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

        //Instantiate Windmill Object
        GameObject windMill = GameObject.Instantiate(windmill, wind_pos, Quaternion.Euler(0f, 130f, 0f)) as GameObject;
        windMill.name = "Windmill";
        windMill.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        currentScore = 0;
		highScore = 0;

		paperType = 0;

        //change position of recycle bin according to difficulty
        switch (GameManager.GetDifficulty())
        {
            case 1:
                GameObject trash_bin_easy = GameObject.Instantiate(recycleBin, easy_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_easy.name = "recycle_bin";
                break;
            case 2:
                GameObject trash_bin_medium = GameObject.Instantiate(recycleBin, med_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_medium.name = "recycle_bin";
                break;
            case 3:
                GameObject trash_bin_hard = GameObject.Instantiate(recycleBin, hard_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_hard.name = "recycle_bin";
                break;
            default:
                GameObject trash_bin_default = GameObject.Instantiate(recycleBin, easy_pos, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trash_bin_default.name = "recycle_bin";
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
		currentScore += 1;
		Debug.Log (currentScore);
        
        highScore = System.Math.Max (currentScore, highScore);
		ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("UIScreen").transform.Find("ScoreText").gameObject,
			eventData: null,
			functor: (x,y)=>x.OnChange());
		ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("UIScreen").transform.Find("HighScoreText").gameObject,
			eventData: null,
			functor: (x,y)=>x.OnChange());
	}

	// Called by other classes like
	// ExecuteEvents.Execute<PointInterface>(
	//     target: GameObject.Find("GameManager"),
	//     eventData: null,
	//     functor: (x,y)=>x.OnFailure());
	public void OnFailure () {
		currentScore = 0;
        
        ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("UIScreen").transform.Find("ScoreText").gameObject,
			eventData: null,
			functor: (x,y)=>x.OnChange());
	}

	public void OnPaperTypeChange(int paperType) {
		this.paperType = paperType;
	}

	public int GetCurrentScore () {
		return this.currentScore;
	}

	public int GethighScore () {
		return this.highScore;
	}
	
	public int GetPaperType () {
		return this.paperType;
	}
}
