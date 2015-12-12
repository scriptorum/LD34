using UnityEngine;
using System.Collections;

public class Air : MonoBehaviour {
	public AirborneObject cloudPrefab;
	public AirborneObject rainPrefab;

	void Awake () 
	{
	}

	void Start () 
	{
		addAirborneObject(cloudPrefab, 230);
		addAirborneObject(rainPrefab, 30);
		addAirborneObject(rainPrefab, 160);
	}

	void Update () {
	
	}

	public void addAirborneObject(AirborneObject prefab, float rotation)
	{
		AirborneObject.Create(this, prefab, rotation);
	}
}
