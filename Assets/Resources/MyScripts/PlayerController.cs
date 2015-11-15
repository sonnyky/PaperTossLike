using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool flag;
    private new Camera camera;

    void Start()
    {
        flag = false;
        camera = Camera.main;
    }

	// Use this for initialization
	void Update()
    {
        float velocity = 0.004f;
        Vector3 viewPos = camera.WorldToViewportPoint(this.transform.position);
       
        if (viewPos.x < 0f)
        {
            flag = false;
        }
        else if (viewPos.x > 1f)
        {
            flag = true;
        }


        if (flag)
        {
            transform.Translate(-velocity, 0, 0);
        } else {
            transform.Translate(velocity, 0, 0);
        }

     

        if (Input.GetKeyDown("left"))
        {
            transform.Translate(velocity, 0, 0);
        }

        if (Input.GetKeyDown("right"))
        {
            transform.Translate(-velocity, 0, 0);
        }

        if (Input.GetKeyDown("up"))
        {
            transform.Translate(0, 0, velocity);
        }

        if (Input.GetKeyDown("down"))
        {
            transform.Translate(0, 0, -velocity);
        }

    }

}
