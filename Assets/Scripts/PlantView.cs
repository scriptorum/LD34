using UnityEngine;
using System.Collections;

public class PlantView : MonoBehaviour {
	public static float HEALTH_GAIN = 50;
	public static float MAX_HEALTH = 150;
	public static float SPAWN_HEALTH_LOSS = 20;

	public float health = 5;
	public int index = -1; // must be set by biomass
	public Biomass biomass; // must be set by biomass

	// Update is called once per frame
	void Update () {

		float modifiers = 1.0f;

		health = Mathf.Min(MAX_HEALTH, health + HEALTH_GAIN * Time.deltaTime * modifiers);

		if(health > 100)
			spawnBiomass();

		// Hack rotation
//		transform.Rotate(0, 0, -10 * Time.deltaTime);
	}

	void spawnBiomass()
	{
		if(biomass.spawn(this)) // success
			health -= SPAWN_HEALTH_LOSS;		
	}

	public static PlantView create(Biomass biomass, int index) 
	{
		// Note we need to use the biomasses initial rotation, as setting the parent causes Unity to 
		// annoying change the localRotation so that it does not move upon parenting. But -- what if 
		// want to set localRotation myself because I know what I'm doing? No, sorry, you can't do that.
		// Really annoying. So I have to give it an initial rotation to match the world rotation.
		PlantView plant = (PlantView) Instantiate(biomass.plantPrefab, Vector3.zero, biomass.transform.rotation);
		plant.index = index;
		plant.biomass = biomass;
		plant.transform.parent = biomass.transform;
		plant.transform.Rotate(0, 0, 360f / biomass.size * index);
		return plant;
	}
}