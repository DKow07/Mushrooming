using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SynchronizationController : PunBehaviour 
{

    public static List<int> currentlyCollectingMushrooms;
    public static List<int> mushroomsWithTrap;

	void Start ()
    {
        currentlyCollectingMushrooms = new List<int>();
        mushroomsWithTrap = new List<int>();
	}

    public static void AddCollectingMushroom(int id)
    {
        currentlyCollectingMushrooms.Add(id);
    }

    public static void AddMushroomWithTrap(int id)
    {
        mushroomsWithTrap.Add(id);
    }

    public static bool CanGatherMushroom(int id)
    {
        int tmpId = currentlyCollectingMushrooms.Find(x => x == id);
        if (tmpId == id)
            return false;
        else 
            return true;

    }
}
