﻿using UnityEngine;
using System.Collections;
using Spewnity;

public class GameController : MonoBehaviour 
{
	[Header("Prefabs")]
	public AirObject tornadoPrefab;
	public AirObject rainPrefab;
	public AirObject snowPrefab;
	public LandObject sunflowerPrefab;

	private Biomass biomass;
	private Airspace airspace;

	void Awake () 
	{
		biomass = (Biomass) GameObject.FindGameObjectWithTag("biomass").GetComponent<Biomass>();
		airspace = (Airspace) GameObject.FindGameObjectWithTag("airspace").GetComponent<Airspace>();
	}
	
	void Start () 
	{
		biomass.addObject(sunflowerPrefab, 0);
		biomass.addObject(sunflowerPrefab, 10);
		biomass.addObject(sunflowerPrefab, 18);

		airspace.addObject(tornadoPrefab, 230);
		airspace.addObject(snowPrefab, 30);
		airspace.addObject(rainPrefab, 160);
	}
}
