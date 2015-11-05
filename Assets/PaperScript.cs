using UnityEngine;
using System.Collections;

public class PaperScript : MonoBehaviour {

	public float timeToDestory = 3.0f;
	private float time;

	// Use this for initialization
	void Start () {
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > timeToDestory) {
			Destroy (this.gameObject);
		}
	}

}
