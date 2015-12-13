using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Biomass : MonoBehaviour
{
	public int size = 32;

	private LandObject[] contents;
	private int contentCount = 0;

	void Awake()
	{
		contents = new LandObject[size];
	}

	public bool propogate(LandObject lo)
	{
		if(lo.index < 0 || lo.index >= size)
			return false;

		if(contents[lo.index] == null)
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
		if((contents[leftIndex] == null) == empty)
			result.Add(leftIndex);

		int rightIndex = index + 1;
		if(rightIndex >= size)
			rightIndex = 0;
		if((contents[rightIndex] == null) == empty)
			result.Add(rightIndex);

		return result;
	}

	public bool isComplete()
	{
		return contentCount >= size;
	}

	public void addObject(LandObject prefab, int index)
	{
		if(contents[index] != null)
			throw new UnityException("Cannot add a second plant to index " + index);

		contents[index] = LandObject.create(this, prefab, index);
		contentCount++;
	}

	public void reset()
	{
		for(int i = 0; i < size; i++)
		{
			if(contents[i] != null)
			{
				Destroy(contents[i].gameObject);
				contents[i] = null;
			}
		}
		contentCount = 0;
	}
}
