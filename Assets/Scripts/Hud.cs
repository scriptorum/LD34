using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Spewnity;

public class Hud : MonoBehaviour 
{
	public float target = 0;
	public float elapsed = 0;
	public bool running = false;
	private Text timerText;

	void Awake()
	{
		string path = transform.GetFullPath();
		timerText = GameObject.Find(path + "/Timer").GetComponent<Text>();
	}

	void Update()
	{
		if(!running)
			return;
		
		elapsed += Time.deltaTime;
		updateHud();
	}

	public void start(float time)
	{
		target = time;
		elapsed = 0;
		running = true;
		updateHud();
	}

	public void reset()
	{
		elapsed = 0;
		target = 0;
		running = false;
		updateHud();
	}

	public void updateHud()
	{
		updateTimerText();
	}

	private void updateTimerText()
	{
		Color color = Color.white;
		string text = "";

		if(running && target > 0f)
		{
			float t = target - elapsed;
			text = String.Format("{0:0.00}", t);
			if(t < 0)
				color = Color.red;
		}
			
		timerText.text = text;
		timerText.color = color;
	}
}
