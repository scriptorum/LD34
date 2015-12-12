using UnityEngine;
using System.Collections;
using Spewnity;

public class LandView : MonoBehaviour {
	public float rotateSpeed = 20.0f;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void OnRotatePlanet(InputEvent e)
	{
		transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime * e.axis.x);
//		Debug.Log(e.axis.x);
	}
}
