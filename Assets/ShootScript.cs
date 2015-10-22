using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {
    public GameObject ball;
    private GameObject clone_ball;
    private Vector3 initialBallPosition;
    private Vector3 start, end, force;
    private Vector3 direction_on_screen;
    private Rigidbody rb;
    private float starttime, difftime, magnitude;
	// Use this for initialization
	void Start () {
        initialBallPosition = new Vector3(0f, -0.1f, 1.6f);
       
        start = new Vector3(0f, 0f, 0f);
        end = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            if (t.phase == TouchPhase.Began)
            {
                start.x = t.position.x;
                start.y = t.position.y;
                start.z = 0;
                starttime = Time.time;
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                end.x = t.position.x;
                end.y = t.position.y;
                end.z = 0f;


               
                direction_on_screen.x = (end.x - start.x);
                direction_on_screen.y = (end.y - start.y);
                direction_on_screen.z = (end.z - start.z);

                magnitude = direction_on_screen.magnitude;

                force.x = direction_on_screen.x / -100;
                force.y = magnitude / 180;
                force.z = magnitude / -200;

                //initialize ball prefab, using the camera rotation is not a good idea here, but first I just want to make this work
                clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
                rb = clone_ball.GetComponent<Rigidbody>();
                Debug.Log(force);
               // Debug.Log(direction_in_world);
                rb.AddForce(force, ForceMode.Impulse);
                
            }
        }
    }
}
