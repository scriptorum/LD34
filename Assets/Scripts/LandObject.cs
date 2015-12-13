using UnityEngine;
using System.Collections;

// Represents a plot of land growing one plant.
// It's not so much representing the land as it is a harness for correctly positioning the plant on the planet
public class LandObject : MonoBehaviour
{
	// These things are really configuration elements that should be passed to Biomass from the level creator
	public static float HEALTH_GAIN = 45; // tweaks the overall speed of plant growth
	public static float MAX_HEALTH = 150;
	public static float BIRTH_THRESHOLD = 120; // keep at least 100 + SPAWN_HEALTH_LOSS
	public static float MIN_HEALTH = -10;
	public static float SPAWN_HEALTH_LOSS = 20;
	public static float BABY_SCALE = 0.1f;

	public bool isPlant = true; // Eventually support mountains? Might have to change class name to SurfaceObject, and allow terrain features below it (e.g.water)

	[Header("Plant Data")]
	public float health = 0; // need to track separate growth meter, as a plant could be both growing and dying
	public float baseGrowth = 1.0f;
	public float darkGrowth = 0.0f;
	public float wetGrowth = 0.0f;

	[HideInInspector] public int index = -1;
	[HideInInspector] public LandObject prefab;

	private Biomass biomass;
	private Airspace airspace;
	private Transform view;
	private float sizeInDegrees = 0;

	void Awake()
	{
		biomass = (Biomass) GameObject.FindGameObjectWithTag("biomass").GetComponent<Biomass>();
		airspace = (Airspace) GameObject.FindGameObjectWithTag("airspace").GetComponent<Airspace>();

		view = (Transform) transform.GetChild(0); // should be only one child!
		view.transform.localScale = new Vector3(1.0f, BABY_SCALE); // TODO Move this to plant
	}

	void Update()
	{		
		if(biomass == null)
			throw new UnityException("Cannot find biomass");
		if(airspace == null)
			throw new UnityException("Cannot find airspace");

		float modifier = getGrowthModifier(airspace.findCover(transform.rotation.eulerAngles.z, sizeInDegrees));	
		health = Mathf.Min(MAX_HEALTH, health + (HEALTH_GAIN * Time.deltaTime * modifier));

		if(health > BIRTH_THRESHOLD)
			propogateBiomass();

		else if(health < MIN_HEALTH)
		{
			Debug.Log("It would appear the plant on plot " + index + " has died");
			health = MIN_HEALTH; // TODO Kill it
		}

		// Adjust plant height to .1 to 1.0 based on health
		float newHeight = (health >= 100f ? 1.0f : (health / 100f * (1.0f - BABY_SCALE) + BABY_SCALE));
		view.transform.localScale = new Vector3(1.0f, Mathf.Max(0f, newHeight));

		// Plant on the move! Plant rotation test...
		// transform.Rotate(0, 0, -10 * Time.deltaTime);
	}

	void propogateBiomass()
	{
		if(biomass.propogate(this)) // success
			health -= SPAWN_HEALTH_LOSS;
	}

	public float getGrowthModifier(AirObject ao)
	{
		if(ao == null)
			return baseGrowth;

		float growth = baseGrowth;
		growth += (ao.isDark ? darkGrowth : 0f);
		growth += (ao.isWet ? wetGrowth : 0f);

		return growth;
	}

	public void init(LandObject prefab, int index)
	{
		this.index = index;
		this.transform.parent = biomass.transform;
		this.prefab = prefab;
		sizeInDegrees = 360f / biomass.size;
		float rot = sizeInDegrees * index;
		this.transform.Rotate(0, 0, rot);
	}

	public static LandObject create(Biomass biomass, LandObject prefab, int index)
	{
		// Note we need to use the biomasses initial rotation, as setting the parent causes Unity to 
		// annoying change the localRotation so that it does not move upon parenting. But -- what if 
		// want to set localRotation myself because I know what I'm doing? No, sorry, you can't do that.
		// Really annoying. So I have to give it an initial rotation to match the world rotation.
		LandObject lo = (LandObject) Instantiate(prefab, biomass.transform.position, biomass.transform.rotation);
		lo.init(prefab, index);
		return lo;
	}
}