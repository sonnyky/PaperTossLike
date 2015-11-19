using UnityEngine;
using System.Collections;

public class PaperScript : MonoBehaviour {

	public float timeToDestory = 3.0f;
	private float time;
    public ShootScript shoot_script_obj;
	// Use this for initialization
	void Start () {
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//time += Time.deltaTime;
		if (time > timeToDestory) {
			//Destroy (this.gameObject);
		}

        if(this.transform.position.y < -0.5)
        {
            shoot_script_obj = Camera.main.GetComponent<ShootScript>();
            shoot_script_obj.RespawnBall();
            Destroy(this.gameObject);
            
        }
	}

}
