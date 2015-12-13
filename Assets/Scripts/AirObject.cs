using UnityEngine;
using System.Collections;

public class AirObject : MonoBehaviour
{
	public float sizeInDegrees = 11.25f;	// If 32 plots, each is 11.25 degrees (360/32)
	public bool isWet = false;
	public bool isCold = false;
	public bool isWindy = false;
	[HideInInspector] public float angle = 0;

	public static AirObject Create(Airspace airspace, AirObject prefab, float rotation)
	{
		AirObject ao = (AirObject) Instantiate(prefab, airspace.transform.position, airspace.transform.rotation);
		ao.transform.parent = airspace.transform;
		ao.transform.Rotate(0, 0, rotation);
		ao.angle = rotation;
		return ao;
	}
}
