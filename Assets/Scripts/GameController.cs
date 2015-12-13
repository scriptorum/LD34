using UnityEngine;
using System.Collections;
using Spewnity;

public class GameController : MonoBehaviour 
{
	[Header("Prefabs")]
	public AirObject tornadoPrefab;
	public AirObject rainPrefab;
	public AirObject snowPrefab;

	public LandObject sunflowerPrefab;
	public LandObject eggplantPrefab;
	public LandObject coniferPrefab;

	private Biomass biomass;
	private Airspace airspace;
	private Hud hud;

	void Awake () 
	{
		biomass = (Biomass) GameObject.FindGameObjectWithTag("biomass").GetComponent<Biomass>();
		airspace = (Airspace) GameObject.FindGameObjectWithTag("airspace").GetComponent<Airspace>();
		hud = (Hud) GameObject.FindGameObjectWithTag("hud").GetComponent<Hud>();
	}
	
	void Start () 
	{
		startLevel();
	}

	public void startLevel()
	{
		biomass.addObject(sunflowerPrefab, 0);
		biomass.addObject(eggplantPrefab, 10);
		biomass.addObject(coniferPrefab, 18);

		airspace.addObject(tornadoPrefab, 230);
		airspace.addObject(snowPrefab, 30);
		airspace.addObject(rainPrefab, 160);

		hud.start(5.0f); // 5 second clock
	}

	public void onReset()
	{
		biomass.reset();
		airspace.reset();
		hud.reset();

		startLevel();
	}
}
