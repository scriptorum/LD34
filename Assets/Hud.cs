using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour 
{
	public float timer = 0;
	public bool running = false;

	void Update()
	{
		if(!running)
			return;
		
		timer += Time.deltaTime;
		updateHud();
	}

	public void start(float timer)
	{
		timer = 0;
		running = true;
		updateHud();
	}

	public void reset()
	{
		timer = 0;
		running = false;
		updateHud();
	}

	public void updateHud()
	{
		// Update GUI here
	}
}
