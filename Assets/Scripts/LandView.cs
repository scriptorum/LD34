using UnityEngine;
using System.Collections;
using Spewnity;

public class LandView : MonoBehaviour {
	public float rotateSpeed = 20.0f;

	void Awake () 
	{
	}
	
	void Update ()
	{		
	}

	public void OnRotatePlanet(InputEvent e)
	{
		transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime * e.axis.x);
	}
}
