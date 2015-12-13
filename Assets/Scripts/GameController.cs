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
	private LandController landController;
	private Level curLevel;

	public Vector2 inputCheckAccum;
	public bool levelComplete = false;

	private static float MIN_INPUT_CHECK_ACCUM = 50.0f;

	void Awake()
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
		landController = (LandController) GameObject.Find("/Universe/Land").GetComponent<LandController>();
	}

	void Start()
	{
		loadLevel(levelManager.startIndex); // load Level config
		playLevel();  // play loaded level (assumes it's been reset)
	}

	public void loadLevel(int levelIndex)
	{
		levelManager.curIndex = levelIndex;
		curLevel = levelManager.levels[levelManager.curIndex];
		if(curLevel == null) throw new UnityException("Cannot load level " + levelManager.curIndex);
	}

	public void playLevel()
	{		
		foreach(AirPlacement ap in curLevel.airPlacements) airspace.addObject((AirObject) prefabs[ap.airObject.ToString()], System.Convert.ToSingle(ap.angle));
		foreach(LandPlacement lp in curLevel.landPlacements) biomass.addObject((LandObject) prefabs[lp.landObject.ToString()], System.Convert.ToInt16(lp.index));

		float time = System.Convert.ToSingle(curLevel.time);
		if(curLevel.flag != LevelFlag.IgnoreTime && curLevel.flag != LevelFlag.InputCheck) hud.startTimer(time);

		hud.setMessage(parseMessage(curLevel.message, time));
	}

	public string parseMessage(string message, float time)
	{
		return message.Replace("{TIME}", time.ToString());
	}

	public void arrowKeysPressed(InputEvent evt)
	{
		if(curLevel.flag == LevelFlag.InputCheck && !levelComplete)
		{
			if(evt.axis.x > 0)
				inputCheckAccum.x += evt.axis.x;
			else inputCheckAccum.y -= evt.axis.x;

			if(inputCheckAccum.x > MIN_INPUT_CHECK_ACCUM && inputCheckAccum.y > MIN_INPUT_CHECK_ACCUM)
				completeLevel();
		}
	}

	public void Update()
	{
		if(levelComplete)
			return;
		
		if(curLevel.flag == LevelFlag.IgnoreTime && biomass.isComplete())
			completeLevel();
	}

	// Run on LateUpdate to ensure Hud has updated the timer
	public void LateUpdate()
	{
		if(levelComplete)
			return;
		
		// Only process levels with a default victory condition here
		if(curLevel.flag != LevelFlag.Default) return;

		else if(hud.timerRunning)
		{
			if(hud.elapsed > curLevel.time)
			{
				if(biomass.isComplete())
				{
					hud.stopTimer();
					hud.setMessage("Better late than never, but you need to do it in the time allowed. Hit R to try again.");
				}
				else hud.setMessage("Out of time! Hit R to restart level.");
			}
			else if(biomass.isComplete())
				completeLevel();
		}
	}

	public void completeLevel()
	{
		if(levelComplete)
			throw new UnityException("Completed level has called completeLevel again ... you rogue");
		levelComplete = true;
		hud.stopTimer();
		hud.setMessage(curLevel.success);
		Invoke("nextLevel", 4);
	}

	public void reset()
	{
		CancelInvoke();

		biomass.reset();
		airspace.reset();
		hud.reset();
		landController.reset();

		levelComplete = false;
		if(curLevel.flag == LevelFlag.InputCheck)
			inputCheckAccum = new Vector2(0,0);

		playLevel();
	}

	public void nextLevel()
	{
		if(++levelManager.curIndex >= levelManager.levels.Length) levelManager.curIndex = 0;
		loadLevel(levelManager.curIndex);
		reset();
	}
}


