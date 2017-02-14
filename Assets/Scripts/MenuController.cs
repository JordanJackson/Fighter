using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    public Text startPromptText;
    public float textFlashSpeed = 4.0f;
    public float colorMin = 0.6f;
    public float colorMax = 1.0f;

	// set initial color
	void Start ()
    {
        startPromptText.color = Color.white;
	}
	
	// flash color of text
	void Update ()
    {
        float colorVal = Mathf.PingPong(Time.time * textFlashSpeed, (colorMax - colorMin)) + colorMin;
        startPromptText.color = new Color(colorVal, colorVal, colorVal);
	}
}
