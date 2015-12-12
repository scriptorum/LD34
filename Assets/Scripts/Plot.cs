using UnityEngine;
using System.Collections;

// Represents a plot of land growing one plant.
// It's not so much representing the land as it is a harness for correctly positioning the plant on the planet
public class Plot : MonoBehaviour
{
	public static float HEALTH_GAIN = 45;
	public static float MAX_HEALTH = 150;
	public static float BIRTH_THRESHOLD = 120;
	public static float MIN_HEALTH = -10;
	public static float SPAWN_HEALTH_LOSS = 20;
	public static float BABY_SCALE = 0.1f;
	// need to track separate growth meter, as a plant could be both growing and dying

	public float health = 0;
	public int index = -1;
	[HideInInspector] public Biomass biomass;

	private Plant plant;

	void Awake()
	{
		plant = (Plant) transform.GetComponentInChildren<Plant>();
		plant.transform.localScale = new Vector3(1.0f, BABY_SCALE);
	}

	void Update()
	{
		float modifiers = 1.0f;

		health = Mathf.Min(MAX_HEALTH, health + HEALTH_GAIN * Time.deltaTime * modifiers);

		if(health > BIRTH_THRESHOLD)
			spawnBiomass();

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

	public static Plot create(Biomass biomass, int index)
	{
		// Note we need to use the biomasses initial rotation, as setting the parent causes Unity to 
		// annoying change the localRotation so that it does not move upon parenting. But -- what if 
		// want to set localRotation myself because I know what I'm doing? No, sorry, you can't do that.
		// Really annoying. So I have to give it an initial rotation to match the world rotation.
		Plot plant = (Plot) Instantiate(biomass.plotPrefab, biomass.transform.position, biomass.transform.rotation);
		plant.index = index;
		plant.biomass = biomass;
		plant.transform.parent = biomass.transform;
		plant.transform.Rotate(0, 0, 360f / biomass.size * index);
		return plant;
	}
}