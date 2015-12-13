using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour 
{
	public string title = "Title";
	public float time = 10.0f;
	public AirPlacement[] airPlacements;
	public LandPlacement[] landPlacements;
}

[System.Serializable]
public struct AirPlacement
{
	public AirEnum airObject;
	public float angle;
}

public enum AirEnum
{
	Tornado,
	Rain,
	Snow
}

[System.Serializable]
public struct LandPlacement
{
	public LandEnum landObject;
	public float index;
}

public enum LandEnum
{
	Sunflower,
	Eggplant,
	Conifer
}