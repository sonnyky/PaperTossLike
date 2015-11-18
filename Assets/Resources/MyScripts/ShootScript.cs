using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShootScript : MonoBehaviour {
    public GameObject ball;
    private GameObject clone_ball;
    private Vector3 initialBallPosition;
    private Vector3 constantWind;
    private int game_difficulty;

    private float distance_abs, initial_velocity, initial_distance_to_trajectory;

    //constant velocity calculation
    private Vector3 real_world_velocity;
    private float tangent_swipe_on_screen;

    private float ball_launch_angle;

    private bool can_swipe;
    private Vector3 bin_position, vector_bin_to_ball;
    private Vector3 wind_position;
    private Vector3 start, end, force;
    private Vector3 direction_on_screen;
    private Rigidbody rb;
    private float difftime, magnitude;

    private float diff_mouse_x, diff_mouse_y;

	// Use this for initialization
	void Start () {
        initialBallPosition = new Vector3(0f, -0.1f, 1.6f);
        constantWind = new Vector3(Random.Range(-1F, 1F), 0f, 0f);
        ExecuteEvents.Execute<TextInterface>(
                target: GameObject.Find("windStr"),
                eventData: null,
                functor: (x, y) => x.OnChange());
        real_world_velocity = new Vector3(0f, 0f, 0f);
        ball_launch_angle = 70.0f;
        //Check current game difficulty
        game_difficulty = GameManager.GetDifficulty();

        //Get current distance from ball to bin trajectory
        initial_distance_to_trajectory = initialBallPosition.z - GameManager.GetBinPosition(game_difficulty).z;

        clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
        can_swipe = true;
        start = new Vector3(0f, 0f, 0f);
        end = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        //Track recycle bin position
        bin_position = GameObject.Find("recycle_bin").transform.position;

        //This is just for checking the angles
        if (Input.GetMouseButtonDown(0))
        {
            start.x = Input.mousePosition.x;
            start.y = Input.mousePosition.y;
            start.z = 0;

        }
        if (Input.GetMouseButtonUp(0))
        {
            end.x = Input.mousePosition.x;
            end.y = Input.mousePosition.y;
            end.z = 0f;
            diff_mouse_x = end.x - start.x;
            diff_mouse_y = end.y - start.y;
            tangent_swipe_on_screen = Mathf.Atan(diff_mouse_y / diff_mouse_x);

            if(tangent_swipe_on_screen < 0)
            {
                tangent_swipe_on_screen = Mathf.Deg2Rad * 180 + tangent_swipe_on_screen;
            }

            Debug.Log(Mathf.Rad2Deg * tangent_swipe_on_screen); //Angle of user swipe

            vector_bin_to_ball.x = bin_position.x - initialBallPosition.x;
            vector_bin_to_ball.z = bin_position.z - initialBallPosition.z;
            vector_bin_to_ball.y = 0f;

            distance_abs = (initial_distance_to_trajectory * 1/Mathf.Sin(tangent_swipe_on_screen)) + 0.01f;
            Debug.Log("Distance to bin trajectory : " + distance_abs);
            Debug.Log("gravity :" + Physics.gravity.y);
            Debug.Log("Initial ball height : " + initialBallPosition.y);

            initial_velocity = initialVelocity(distance_abs, Mathf.Abs(Physics.gravity.y), 0.4f, Mathf.Deg2Rad*ball_launch_angle);
            Debug.Log(initial_velocity);

            real_world_velocity.z = Mathf.Sin(tangent_swipe_on_screen) * (initial_velocity * Mathf.Cos(Mathf.Deg2Rad * ball_launch_angle)) * -1;
            real_world_velocity.x = (Mathf.Cos(tangent_swipe_on_screen)) * (initial_velocity * Mathf.Cos(Mathf.Deg2Rad * ball_launch_angle)) * -1;
            real_world_velocity.y = (initial_velocity * Mathf.Sin(Mathf.Deg2Rad * ball_launch_angle));
            //TODO change this to force
            force.x = ball.GetComponent<Rigidbody>().mass * real_world_velocity.x;
            force.y = ball.GetComponent<Rigidbody>().mass * real_world_velocity.y;
            force.z = ball.GetComponent<Rigidbody>().mass * real_world_velocity.z;
            rb = clone_ball.GetComponent<Rigidbody>();
            Debug.Log(force.z);
            Debug.Log(force.y);
            // Debug.Log(direction_in_world);
            force.x += constantWind.x;
            Debug.Log("WIND");
            Debug.Log(constantWind.x);
            constantWind.x = Random.Range(-1F, 1F);
            ExecuteEvents.Execute<TextInterface>(
               target: GameObject.Find("windStr"),
               eventData: null,
               functor: (x, y) => x.OnChange());
            

            rb.useGravity = true;
            rb.AddForce(force, ForceMode.Impulse);

            StartCoroutine(RespawnBall());
            can_swipe = false;
        }

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
				if (can_swipe == true)
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
	                
	                if(force.x > 1.5)
	                {
	                    force.x = 1.5f;
	                }
	                if (force.x < -1.5)
	                {
	                    force.x = -1.5f;
	                }

	                //Forces in y and z axes will be made constant according to position to recycle bin
	                force.y = distance_abs*5;
	                force.z = distance_abs*-2;
                    force.x += constantWind.x;
                    constantWind.x = Random.Range(-1F, 1F);
                    ExecuteEvents.Execute<TextInterface>(
                       target: GameObject.Find("windStr"),
                       eventData: null,
                       functor: (x, y) => x.OnChange());

                    //initialize ball prefab, using the camera rotation is not a good idea here, but first I just want to make this work
                    //clone_ball = GameObject.Instantiate(ball, initialBallPosition, this.transform.rotation) as GameObject;
                    rb = clone_ball.GetComponent<Rigidbody>();
	                Debug.Log(force.z);
	                Debug.Log(force.y);
	                // Debug.Log(direction_in_world);
	                rb.useGravity = true;
	                rb.AddForce(force, ForceMode.Impulse);

                    StartCoroutine(RespawnBall());
                    can_swipe = false;
                }
            }
        }
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
