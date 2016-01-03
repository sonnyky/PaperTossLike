using UnityEngine;
using System.Collections;

public class PaperScript : MonoBehaviour {
	
    public ShootScript shoot_script_obj;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < -0.5)
        {
			Destroy(this.gameObject);
			shoot_script_obj = Camera.main.GetComponent<ShootScript>();
            shoot_script_obj.RespawnBall();
        }
	}

}
