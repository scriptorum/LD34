using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroStartButton : MonoBehaviour 
{
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
			transition();
	}

	public void OnMouseDown()
	{
		transition();
	}

	public void transition()
	{
		SceneManager.LoadScene("Play");
	}
}
