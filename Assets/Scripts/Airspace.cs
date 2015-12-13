using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Airspace : MonoBehaviour
{
	private List<AirObject> contents = new List<AirObject>();

	public void addObject(AirObject prefab, float rotation)
	{
		contents.Add(AirObject.Create(this, prefab, rotation));
	}

	// Returns null or an air object that covers any part of the land object as measured in start/end angles
	public AirObject findCover(float angle, float sizeInDegrees)
	{
		foreach(AirObject ao in contents)
		{
			float range = Mathf.Abs(angle - ao.angle);
			if(range > 180) range = Mathf.Abs(range - 360);

			if(range < (sizeInDegrees + ao.sizeInDegrees) / 2) return ao;
		}
		return null;
	}

	public void reset()
	{
		foreach(AirObject ao in contents)
			Destroy(ao.gameObject);
		contents.Clear();
	}
}
