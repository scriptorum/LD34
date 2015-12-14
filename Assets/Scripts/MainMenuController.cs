using UnityEngine;
using System.Collections;
using Spewnity;

public class MainMenuController : MonoBehaviour
{
	public AudioClip introMusic;

	private static float SMALL = 1.5f;
	private static float LARGE = 1.51f;
	private static float ROTSPEED = 10f;
	private static float BOUNCESPEED = 0.3f;

	private float startScale = 1.0f;
	private float endScale = 1.0f;
	private float totalTime = 0f;
	private float remainingTime = 0f;

	void Start()
	{
	}

	void Update()
	{
		remainingTime -= Time.deltaTime;
		if(remainingTime <= 0)
		{
			totalTime = remainingTime = BOUNCESPEED;
			startScale = endScale;
			endScale = endScale >= LARGE ? SMALL : LARGE;
		}

		float curScale = endScale - (endScale - startScale) * (remainingTime / totalTime);
		transform.localScale = new Vector3(curScale, curScale, 1);

		transform.Rotate(new Vector3(0, 0, ROTSPEED * Time.deltaTime));
	}
}
