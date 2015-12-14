using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Spewnity;

public class Hud : MonoBehaviour 
{
	public AudioClip messageSnd;
	public float target = 0;
	public float elapsed = 0;
	public bool timerRunning = false;

	private Text timerText;
	private Text messageText;

	void Awake()
	{
		string path = transform.GetFullPath();
		timerText = GameObject.Find(path + "/Timer").GetComponent<Text>();
		messageText = GameObject.Find(path + "/Message").GetComponent<Text>();
		reset();
	}

	void Update()
	{
		if(!timerRunning)
			return;
		
		elapsed += Time.deltaTime;
		updateTimerText();
	}

	public void startTimer(float time)
	{
		target = time;
		elapsed = 0;
		timerRunning = true;
		updateTimerText();
		clearMessage();
	}

	public void stopTimer()
	{
		timerRunning = false;

		updateTimerText();
	}

	public void reset()
	{
		elapsed = 0;
		target = 0;
		timerRunning = false;
		updateTimerText();
		clearMessage();
	}

	public void clearMessage()
	{
		setMessage("");
	}

	public void setMessage(string msg)
	{
		if(msg != "" && msg != messageText.text)
			SoundManager.instance.RandomizeSfx(messageSnd);

		messageText.text = msg;
	}

	private void updateTimerText()
	{
		Color color = Color.white;
		string text = "";

		if(target > 0f)
		{
			float t = target - elapsed;
			text = String.Format("{0:0.00}", t);
			if(t < 0)
				color = Color.red;
			else if (!timerRunning)
				color = Color.green; // timer paused - use green, probably for victory
		}
			
		timerText.text = text;
		timerText.color = color;
	}
}
