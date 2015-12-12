using UnityEngine;
using System.Collections;

public class AirborneObject : MonoBehaviour {
	public int size = 1;
	private Air air;

	// Use this for initialization
	void Start () {
		Debug.Log("Shut up never warning" + air);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static AirborneObject Create(Air air, AirborneObject prefab, float rotation)
	{
		AirborneObject ab  = (AirborneObject) Instantiate(prefab, air.transform.position, air.transform.rotation);
		ab.air = air;
		ab.transform.parent = air.transform;
		ab.transform.Rotate(0, 0, rotation);
		return ab;
	}
}
