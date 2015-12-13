using UnityEngine;
using System.Collections;
using Spewnity;

public class GameController : MonoBehaviour 
{
	private Biomass biomass;

	void Awake () 
	{
		biomass = (Biomass) GameObject.FindGameObjectWithTag("biomass").GetComponent<Biomass>();
	}
	
	void Start () 
	{
		biomass.addPlant(0);
		biomass.addPlant(10);
		biomass.addPlant(18);
	}
}
