using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Biomass : MonoBehaviour {
	public int size = 32;
	public PlantView plantPrefab;

	private PlantView[] plants;

	void Awake () {
		plants = new PlantView[size];
	}

	public bool spawn(PlantView spawner)
	{
		if(spawner.index < 0 || spawner.index >= size)
			return false;

		if(plants[spawner.index] == null)
			return false;

		List<int> plots = getNeighbors(spawner.index);
		if(plots.Count == 0)
			return false;

		if(plots.Count == 1)
		{
			addPlant(plots[0]);
			return true;
		}

		addPlant(plots[Random.Range(0, plots.Count)]);
		return true;
	}

	private List<int> getNeighbors(int index, bool empty = true)
	{
		List<int> result = new List<int>();

		int leftIndex = index - 1;
		if(leftIndex < 0)
			leftIndex = size - 1;
		if((plants[leftIndex] == null) == empty)
			result.Add(leftIndex);

		int rightIndex = index + 1;
		if(rightIndex >= size)
			rightIndex = 0;
		if((plants[rightIndex] == null) == empty)
			result.Add(rightIndex);

		return result;
	}

	public void addPlant(int index)
	{
		if(plants[index] != null)
			throw new UnityException("Cannot add a second plant to index " + index);

		plants[index] = PlantView.create(this, index);
	}
}
