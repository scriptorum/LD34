using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Air : MonoBehaviour {
	public AirborneObject cloudPrefab;
	public AirborneObject rainPrefab;
	private List<AirborneObject> contents = new List<AirborneObject>();

	void Awake () 
	{		
	}

	void Start () 
	{
		// GameController should invoke these
		addAirborneObject(cloudPrefab, 230);
		addAirborneObject(rainPrefab, 30);
		addAirborneObject(rainPrefab, 160);
	}

	void Update () {
	
	}

	public void addAirborneObject(AirborneObject prefab, float rotation)
	{
		contents.Add(AirborneObject.Create(this, prefab, rotation));
	}

	// Returns null or an airborne object that touches any part of the plot as measured in start/end angles
	public AirborneObject findCover(float angle, float sizeInDegrees)
	{
		foreach(AirborneObject ao in contents)
		{
			float range = Mathf.Abs(angle - ao.angle);
			if(range > 180)
				range = Mathf.Abs(range - 360);

//			Debug.Log("Plot:" + angle + "@" + sizeInDegrees + " AO:" + ao.angle + "@" + ao.sizeInDegrees + " range:" + range + " sz+sz/2:" +
//				((sizeInDegrees + ao.sizeInDegrees) / 2));

			if(range < (sizeInDegrees + ao.sizeInDegrees) / 2)
				return ao;
		}
		return null;
	}
}
