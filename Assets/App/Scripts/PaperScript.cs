using UnityEngine;
using System.Collections;

public class PaperScript : MonoBehaviour {
	
    public ShootScript shooter;
	// Use this for initialization
	void Start () {
        shooter = GameObject.Find("Shooter").GetComponent<ShootScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < -0.5)
        {
			Destroy(this.gameObject);
            shooter.RespawnBall();
        }
	}

}
