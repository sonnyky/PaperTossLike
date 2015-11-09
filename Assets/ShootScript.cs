using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {
    public GameObject ball;
    private GameObject clone_ball;
    private Vector3 initialBallPosition;

    private float distance_abs;
    private bool can_swipe;
    private Vector3 bin_position, vector_bin_to_ball;
    private Vector3 start, end, force;
    private Vector3 direction_on_screen;
    private Rigidbody rb;
    private float difftime, magnitude;

	// Use this for initialization
	void Start () {
        initialBallPosition = new Vector3(0f, -0.1f, 1.6f);
        clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
        can_swipe = true;
        start = new Vector3(0f, 0f, 0f);
        end = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        //Track recycle bin position
        bin_position = GameObject.Find("recycle bin").transform.position;

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

                //Get distance vector between ball and recycle bin. Only X and Z axes matters
                vector_bin_to_ball.x = bin_position.x - initialBallPosition.x;
                vector_bin_to_ball.z = bin_position.z - initialBallPosition.z;
                vector_bin_to_ball.y = 0f;

                distance_abs = Mathf.Sqrt((vector_bin_to_ball.x * vector_bin_to_ball.x) + (vector_bin_to_ball.z * vector_bin_to_ball.z));

                end.x = t.position.x;
                end.y = t.position.y;
                end.z = 0f;
               
                direction_on_screen.x = (end.x - start.x);
                direction_on_screen.y = (end.y - start.y);
                direction_on_screen.z = (end.z - start.z);

                magnitude = direction_on_screen.magnitude;

                force.x = direction_on_screen.x / -100;
                
                if(force.x > 1.2)
                {
                    force.x = 1.2f;
                }
                if (force.x < -1.2)
                {
                    force.x = -1.2f;
                }

                //Forces in y and z axes will be made constant according to position to recycle bin
                force.y = distance_abs*2;
                force.z = distance_abs*-5;

                //initialize ball prefab, using the camera rotation is not a good idea here, but first I just want to make this work
                //clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
                rb = clone_ball.GetComponent<Rigidbody>();
                Debug.Log(force.z);
                Debug.Log(force.y);
                // Debug.Log(direction_in_world);
                rb.useGravity = true;
                rb.AddForce(force, ForceMode.Impulse);

                if (can_swipe == true)
                {
                    StartCoroutine(RespawnBall());
                    can_swipe = false;
                }
            }
        }
    }
    IEnumerator RespawnBall()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
        print(Time.time);
        StopCoroutine(RespawnBall());
        can_swipe = true;
    }
   
}
