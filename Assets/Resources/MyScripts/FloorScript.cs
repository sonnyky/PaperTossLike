using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FloorScript : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Paper") {
            Debug.Log("Other Collider:" + other.name);
           
            ExecuteEvents.Execute<PointInterface>(
				target: GameObject.Find("GameManager"),
				eventData: null,
				functor: (x,y)=>x.OnFailure());
		}
	}

}
