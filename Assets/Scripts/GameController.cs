using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	private Dictionary<string, Object> prefabs;
	private Biomass biomass;
	private Airspace airspace;
	private Hud hud;
	private LevelManager levelManager;

	void Awake () 
	{
		prefabs = new Dictionary<string, Object>();
		prefabs.Add("Tornado", tornadoPrefab);
		prefabs.Add("Rain", rainPrefab);
		prefabs.Add("Snow", snowPrefab);
		prefabs.Add("Sunflower", sunflowerPrefab);
		prefabs.Add("Eggplant", eggplantPrefab);
		prefabs.Add("Conifer", coniferPrefab);

		biomass = (Biomass) GameObject.FindGameObjectWithTag("biomass").GetComponent<Biomass>();
		airspace = (Airspace) GameObject.FindGameObjectWithTag("airspace").GetComponent<Airspace>();
		hud = (Hud) GameObject.FindGameObjectWithTag("hud").GetComponent<Hud>();
		levelManager = (LevelManager) GameObject.Find("/Levels").GetComponent<LevelManager>();
	}
	
	void Start () 
	{
		levelManager.current = 0;
		startLevel();
	}

	public void startLevel()
	{
		Level level = levelManager.levels[levelManager.current];
		if(level == null)
			throw new UnityException("Cannot load level " + levelManager.current);
		
		foreach(AirPlacement ap in level.airPlacements)
			airspace.addObject((AirObject) prefabs[ap.airObject.ToString()], System.Convert.ToSingle(ap.angle));
		foreach(LandPlacement lp in level.landPlacements)
			biomass.addObject((LandObject) prefabs[lp.landObject.ToString()], System.Convert.ToInt16(lp.index));
		hud.start(System.Convert.ToSingle(level.time));
	}

	public void onReset()
	{
		biomass.reset();
		airspace.reset();
		hud.reset();

		startLevel();
	}
}


