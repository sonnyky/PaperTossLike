using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindStrength : MonoBehaviour, TextInterface
{

    private Text windStr;

    // This function should be "Awake," not "Start" 
    // because if it's "Start," OnChange is called by ShootScript#Start (before this#Start)
    void Awake()
    {
        windStr = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChange()
    {
        ShootScript shootScript = GameObject.Find("Shooter").GetComponent<ShootScript>();
        float currentWind = shootScript.GetCurrentWind();
        windStr.text = "WindStrength: " + currentWind.ToString();
    }

}
