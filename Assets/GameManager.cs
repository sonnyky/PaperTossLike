using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour, PointInterface {

	int currentPoint;

	// Use this for initialization
	void Start () {
        LoadUiObjects();
		currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called by other classes like
	// ExecuteEvents.Execute<PointInterface>(
    //     target: GameObject.Find("GameManager"),
    //     eventData: null,
    //     functor: (x,y)=>x.OnReceive());
	public void OnReceive () {
		currentPoint += 10;
		Debug.Log (currentPoint);
		ExecuteEvents.Execute<TextInterface>(
			target: GameObject.Find("StatusText"),
			eventData: null,
			functor: (x,y)=>x.OnChange());
	}

	public int GetCurrentPoint () {
		return currentPoint;
	}

    private void LoadUiObjects()
    {
        GameObject UiGroup = (GameObject) Instantiate(Resources.Load("MyAssets/Prefabs/UI/UICamera")) as GameObject;
    }

}
