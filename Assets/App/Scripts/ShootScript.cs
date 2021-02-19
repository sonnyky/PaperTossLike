using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShootScript : MonoBehaviour {

    public GameObject billiardBall;
    public GameObject grenade;
    public GameObject paperCrane;

    private GameObject ball;
    private bool mBallExists = false;
    private GameObject clone_ball;
    private GameObject gameManager;

    private GameManager gameManagerScript;

    private Vector3 initialBallPosition;
    private Vector3 constantWind;
    private int game_difficulty;

    private float distance_abs, initial_velocity, initial_distance_to_trajectory;

    //constant velocity calculation
    private Vector3 real_world_velocity;
    private float tangent_swipe_on_screen;

    private float ball_launch_angle;

    private bool can_swipe; // Whether the player can swipe or not

    private Vector3 vector_bin_to_ball;
    private Vector3 wind_position;
    private Vector3 start, end, force;
    private Vector3 direction_on_screen;
    private Rigidbody rb;
    private float difftime, magnitude;

    private float diff_mouse_x, diff_mouse_y;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager> ();

        initialBallPosition = new Vector3(0f, -0.1f, 1.6f);
        /*
        constantWind = new Vector3(Random.Range(0f, 0.05f), 0f, 0f);
        ExecuteEvents.Execute<TextInterface>(
                target: GameObject.Find("windStr"),
                eventData: null,
                functor: (x, y) => x.OnChange());
                */
        real_world_velocity = new Vector3(0f, 0f, 0f);
        ball_launch_angle = 60.0f;
		force = new Vector3 (0f, 0f, 0f);
        constantWind = new Vector3 (0f, 0f, 0f);

        //Check current game difficulty
        game_difficulty = GameManager.GetDifficulty();

        //Get current distance from ball to bin trajectory
        initial_distance_to_trajectory = initialBallPosition.z - GameManager.GetBinPosition(game_difficulty).z;

        RespawnBall();

        can_swipe = true;
        start = new Vector3(0f, 0f, 0f);
        end = new Vector3(0f, 0f, 0f);
	}
	
    public void BallDisabled()
    {
        mBallExists = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (mBallExists)
        {
            if (clone_ball.transform.position.z < 1.3f) ApplyWind();
        }

        //This is just for checking the angles
        if (Input.GetMouseButtonDown (0)) {
            start.x = Input.mousePosition.x;
            start.y = Input.mousePosition.y;
            start.z = 0;

        }
        if (Input.GetMouseButtonUp (0)) {
            end.x = Input.mousePosition.x;
            end.y = Input.mousePosition.y;
            end.z = 0f;
            diff_mouse_x = end.x - start.x;
            diff_mouse_y = end.y - start.y;

            if (diff_mouse_x == 0) {
                tangent_swipe_on_screen = Mathf.PI / 2.0f;
            } else {
                tangent_swipe_on_screen = Mathf.Atan (diff_mouse_y / diff_mouse_x);
            }

            if (tangent_swipe_on_screen < 0) {
                tangent_swipe_on_screen = Mathf.Deg2Rad * 180 + tangent_swipe_on_screen;
            }

            //Calculate for to throw paper ball object. Add force to the gameobject
            ThrowObject (tangent_swipe_on_screen);
            can_swipe = false;
        }

        if (Input.touchCount > 0) {
            Touch t = Input.touches [0];
            if (t.phase == TouchPhase.Began) {
                start.x = t.position.x;
                start.y = t.position.y;
                start.z = 0;
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) {
                if (can_swipe == true) {
	              
                    end.x = t.position.x;
                    end.y = t.position.y;
                    end.z = 0f;
                    tangent_swipe_on_screen = (end.y - start.y) / (end.x - start.x);
                    if (tangent_swipe_on_screen < 0) {
                        tangent_swipe_on_screen = Mathf.Deg2Rad * 180 + tangent_swipe_on_screen;
                    }
                    ThrowObject (tangent_swipe_on_screen);
                    
                    can_swipe = false;
                }
            }
        }
    }

    private void ApplyWind()
    {
        clone_ball.GetComponent<Rigidbody>().AddForce(constantWind, ForceMode.Force);
    }

    private void ThrowObject(float tangent_swipe_on_screen)
    {
        distance_abs = (initial_distance_to_trajectory * 1 / Mathf.Sin(tangent_swipe_on_screen)) + 0.01f;

        initial_velocity = initialVelocity(distance_abs, Mathf.Abs(Physics.gravity.y), 0.4f, Mathf.Deg2Rad * ball_launch_angle);

        real_world_velocity.z = Mathf.Sin(tangent_swipe_on_screen) * (initial_velocity * Mathf.Cos(Mathf.Deg2Rad * ball_launch_angle)) * -1;
        real_world_velocity.x = (Mathf.Cos(tangent_swipe_on_screen)) * (initial_velocity * Mathf.Cos(Mathf.Deg2Rad * ball_launch_angle)) * -1;
        real_world_velocity.y = (initial_velocity * Mathf.Sin(Mathf.Deg2Rad * ball_launch_angle));

        force = new Vector3 (0f, 0f, 0f);
        force.x = ball.GetComponent<Rigidbody>().mass * real_world_velocity.x;
        force.y = ball.GetComponent<Rigidbody>().mass * real_world_velocity.y;
        force.z = ball.GetComponent<Rigidbody>().mass * real_world_velocity.z;
        rb = clone_ball.GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.AddForce(force, ForceMode.Impulse);
    }

    private float initialVelocity(float distance, float gravity, float drop_height, float launch_angle)
    {
        float calc_result;
        float upper_half = distance * distance * gravity;
        float lower_half = (distance * Mathf.Sin(2 * launch_angle)) + (2 * drop_height * Mathf.Cos(launch_angle) * Mathf.Cos(launch_angle));
        calc_result = Mathf.Sqrt(upper_half / lower_half);
        return calc_result;
    }

    public float GetCurrentWind()
    {
        return constantWind.x;
    }

    private int RandomizeNumberSign()
    {
        int sign = 1;
        int random_number = Random.Range(1, 3);
        if(random_number == 2)
        {
            sign = -1;
        }
        return sign;
    }
	
    public void RespawnBall()
    {
		can_swipe = false;

		// Delete all the game objects tagged "Paper"
		GameObject[] paperObjects = GameObject.FindGameObjectsWithTag ("Paper");
		foreach (GameObject paperObject in paperObjects) {
			Destroy(paperObject);
		}

        // Initial position of the paper 
		Quaternion ballQuaternion = new Quaternion ();

		switch (gameManagerScript.GetPaperType ()) {
		case 0:
			ball = billiardBall;
			ballQuaternion = this.transform.rotation;
			break;
		case 1:
			ball = paperCrane;
			ballQuaternion = Quaternion.Euler(0f, 270f, 0f);
			break;
		case 2:
            ball = grenade;
            ballQuaternion = this.transform.rotation;
			break;
		}
		clone_ball = GameObject.Instantiate(ball, initialBallPosition, ballQuaternion) as GameObject;
        mBallExists = true;
        constantWind.x = ((float)Random.Range(1, 10) / 25) * RandomizeNumberSign();
        ExecuteEvents.Execute<TextInterface>(
            target: GameObject.Find("windStr"),
            eventData: null,
            functor: (x, y) => x.OnChange());

        can_swipe = true;
    }
   
}
