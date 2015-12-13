using UnityEngine;
using System.Collections;

public class AirborneObject : MonoBehaviour
{
	public float sizeInDegrees = 11.25f;	// If 32 plots, each is 11.25 degrees (360/32)
	public float angle = 0;
	public bool isWet = false;
	public bool isDark = false;

//	private Air air;

	public static AirborneObject Create(Air air, AirborneObject prefab, float rotation)
	{
		AirborneObject ao = (AirborneObject) Instantiate(prefab, air.transform.position, air.transform.rotation);
//		ao.air = air;
		ao.transform.parent = air.transform;
		ao.transform.Rotate(0, 0, rotation);
		ao.angle = rotation;
		return ao;
	}
}
