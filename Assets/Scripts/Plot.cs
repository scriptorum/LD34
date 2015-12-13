using UnityEngine;
using System.Collections;

// Represents a plot of land growing one plant.
// It's not so much representing the land as it is a harness for correctly positioning the plant on the planet
public class Plot : MonoBehaviour
{
	// These things are really configuration elements that should be passed to Biomass from the level creator
	public static float HEALTH_GAIN = 45; // tweaks the overall speed of plant growth
	public static float MAX_HEALTH = 150;
	public static float BIRTH_THRESHOLD = 120; // keep at least 100 + SPAWN_HEALTH_LOSS
	public static float MIN_HEALTH = -10;
	public static float SPAWN_HEALTH_LOSS = 20;
	public static float BABY_SCALE = 0.1f;
	// need to track separate growth meter, as a plant could be both growing and dying

	public float health = 0;
	public int index = -1;
	[HideInInspector] public Biomass biomass;

	private Plant plant;
	private float sizeInDegrees = 0;

	void Awake()
	{
		plant = (Plant) transform.GetComponentInChildren<Plant>();
		plant.transform.localScale = new Vector3(1.0f, BABY_SCALE); // TODO Move this to plant
		plant.init(this);
	}

	void Update()
	{
		if(biomass == null)
			return;

		float modifier = plant.getModifier(biomass.air.findCover(transform.rotation.eulerAngles.z, sizeInDegrees));			
//		Debug.Log("Plot:" + index + " Modifer:" + modifier);

		health = Mathf.Min(MAX_HEALTH, health + (HEALTH_GAIN * Time.deltaTime * modifier));

		if(health > BIRTH_THRESHOLD)
			spawnBiomass();

		if(health < MIN_HEALTH)
		{
			Debug.Log("It would appear the plant on plot " + index + " has died");
			health = -999999; // TODO Kill it
		}

		// Adjust plant height to .1 to 1.0 based on health
		float newHeight = (health >= 100f ? 1.0f : (health / 100f * (1.0f - BABY_SCALE) + BABY_SCALE));
		plant.transform.localScale = new Vector3(1.0f, newHeight);

		// Plant on the move! Plant rotation test...
		// transform.Rotate(0, 0, -10 * Time.deltaTime);
	}

	void spawnBiomass()
	{
		if(biomass.spawn(this)) // success
			health -= SPAWN_HEALTH_LOSS;
	}

	public void init(Biomass biomass, int index)
	{
		this.index = index;
		this.biomass = biomass;
		this.transform.parent = biomass.transform;
		float plotSize = 360f / biomass.size;
		float rot = plotSize * index;
		this.transform.Rotate(0, 0, rot);
		this.sizeInDegrees = plotSize;
	}

	public static Plot create(Biomass biomass, int index)
	{
		// Note we need to use the biomasses initial rotation, as setting the parent causes Unity to 
		// annoying change the localRotation so that it does not move upon parenting. But -- what if 
		// want to set localRotation myself because I know what I'm doing? No, sorry, you can't do that.
		// Really annoying. So I have to give it an initial rotation to match the world rotation.
		Plot plot = (Plot) Instantiate(biomass.plotPrefab, biomass.transform.position, biomass.transform.rotation);
		plot.init(biomass, index);
		return plot;
	}
}