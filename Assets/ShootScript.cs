using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {
    public GameObject ball;
    private GameObject clone_ball;
    private Vector3 initialBallPosition;
    private Vector3 start, end, direction;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        initialBallPosition = new Vector3(0.41f, -5.44f, -6.67f);
       
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
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                end.x = t.position.x;
                end.y = t.position.y;
                end.z = 0.5f;

                direction = (end - start)/100;

                direction.x = direction.x*-1;
                direction.y = 0.2f;
                direction.z = -0.5f;

                //initialize ball prefab, using the camera rotation is not a good idea here, but first I just want to make this work
               clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
                rb = clone_ball.GetComponent<Rigidbody>();
                Debug.Log(start);
                Debug.Log(end);
                Debug.Log(direction);
                rb.AddForce(direction, ForceMode.Impulse);
                
            }
        }
    }
}
