using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour 
{
	public string success = "You did it!";
	public string message = "";
	public float time = 10.0f;
	public LevelFlag flag = LevelFlag.Default;
	public AirPlacement[] airPlacements;
	public LandPlacement[] landPlacements;
}

[System.Serializable]
public struct AirPlacement
{
	public AirEnum airObject;
	public float angle;
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

public enum AirEnum
{
	Tornado,
	Rain,
	Snow
}

public enum LevelFlag
{
	Default,
	InputCheck,
	IgnoreTime
}