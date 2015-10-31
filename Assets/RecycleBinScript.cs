using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RecycleBinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Paper") {
            Debug.Log("Collided");
			Destroy(other.gameObject);
           
			ExecuteEvents.Execute<PointInterface>(
				target: GameObject.Find("GameManager"),
				eventData: null,
				functor: (x,y)=>x.OnReceive());
		}
	}

}
