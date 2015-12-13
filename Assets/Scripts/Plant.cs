using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour {
	public float baseGrowth = 1.0f;

	public float darkGrowth = 0.0f;
	public float wetGrowth = 0.0f;

//	private Plot plot;

	public void init(Plot plot)
	{
//		this.plot = plot;
	}

	public float getModifier(AirborneObject ao)
	{
		if(ao == null)
			return baseGrowth;

		float growth = baseGrowth;
		growth += (ao.isDark ? darkGrowth : 0f);
		growth += (ao.isWet ? wetGrowth : 0f);

//		Debug.Log("Growth of " + this.name + " under " + ao.name + " is " + growth);

		return growth;
	}
}
