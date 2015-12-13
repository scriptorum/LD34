﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Place a collider2D on the button object
// Define the name of the scene to transition to
// And any additional key strokes that should cause the transition
public class TransitionToScene : MonoBehaviour 
{
	public string sceneName;
	public KeyCode[] keys;
	public LoadSceneMode mode = LoadSceneMode.Single;

	void Update()
	{
		foreach(KeyCode key in keys)
		{
			if(Input.GetKeyDown(key))
			{
				transition();
				break;
			}
		}
	}

	public void OnMouseDown()
	{
		transition();
	}

	public void transition()
	{
		SceneManager.LoadScene(sceneName, mode);
	}
}