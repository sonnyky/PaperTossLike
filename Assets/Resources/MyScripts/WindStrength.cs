using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindStrength : MonoBehaviour, TextInterface
{

    private Text windStr;

    // Use this for initialization
    void Start()
    {
        windStr = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChange()
    {
        Debug.Log("onchange");
        ShootScript shootScript = GameObject.Find("Main Camera").GetComponent<ShootScript>();
        float currentWind = shootScript.GetCurrentWind();
        windStr.text = "WindStrength: " + currentWind.ToString();
    }

}
