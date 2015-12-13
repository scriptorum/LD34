using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Biomass : MonoBehaviour
{
	public int size = 32;

	private LandObject[] plantObjects;

	void Awake()
	{
		plantObjects = new LandObject[size];
	}

	public bool propogate(LandObject lo)
	{
		if(lo.index < 0 || lo.index >= size)
			return false;

		if(plantObjects[lo.index] == null)
			return false;

		List<int> neighbors = getNeighbors(lo.index);
		if(neighbors.Count == 0)
			return false;

		var nextIndex = -1;
		if(neighbors.Count == 1)
			nextIndex = neighbors[0];
		else
			nextIndex = neighbors[Random.Range(0, neighbors.Count)];

		if(nextIndex == lo.index)
			throw new UnityException("Attempt to respawn plant at " + nextIndex);

		addObject(lo.prefab, nextIndex);

		return true;
	}

	private List<int> getNeighbors(int index, bool empty = true)
	{
		List<int> result = new List<int>();

		int leftIndex = index - 1;
		if(leftIndex < 0)
			leftIndex = size - 1;
		if((plantObjects[leftIndex] == null) == empty)
			result.Add(leftIndex);

		int rightIndex = index + 1;
		if(rightIndex >= size)
			rightIndex = 0;
		if((plantObjects[rightIndex] == null) == empty)
			result.Add(rightIndex);

		return result;
	}

	public void addObject(LandObject prefab, int index)
	{
		if(plantObjects[index] != null)
			throw new UnityException("Cannot add a second plant to index " + index);

		plantObjects[index] = LandObject.create(this, prefab, index);
	}
}
