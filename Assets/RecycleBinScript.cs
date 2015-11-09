using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RecycleBinScript : MonoBehaviour {

	// To prevent Unity from counting twice on a TriggerEnter, it prepares some wait time
	public int waitFrames = 2;
	private bool enable;
	private int count;

	// Use this for initialization
	void Start () {
		enable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (enable) {
			count = 0;
		} else {
			count += 1;
			if (count > waitFrames) {
				enable = true;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Paper" && enable) {
            Debug.Log("Collided with a recycle bin");
			Destroy(other.gameObject);
            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            this.gameObject.GetComponentInChildren<AudioSource>().Play();
			ExecuteEvents.Execute<PointInterface>(
				target: GameObject.Find("GameManager"),
				eventData: null,
				functor: (x,y)=>x.OnSuccess());

			enable = false;
		}
	}

}
