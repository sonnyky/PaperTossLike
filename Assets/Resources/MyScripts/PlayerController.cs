using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool flag;
    public new Camera camera;

    void Start()
    {
        flag = false;
    }

	// Use this for initialization
	void Update()
    {
        float velocity = 0.004f;
        Vector3 viewPos = camera.WorldToViewportPoint(this.transform.position);

        if (viewPos.x < 0.05f)
        {
            flag = true;
        }
        else if (viewPos.x > 0.9f)
        {
            flag = false;
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
