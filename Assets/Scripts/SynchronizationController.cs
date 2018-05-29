using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using System;

public class SynchronizationController : PunBehaviour 
{

    public static List<int> currentlyCollectingMushrooms;
    public static List<PlayerTrap> mushroomsWithTrap;

	void Start ()
    {
        currentlyCollectingMushrooms = new List<int>();
        mushroomsWithTrap = new List<PlayerTrap>();
	}

    public static void AddCollectingMushroom(int id)
    {
        currentlyCollectingMushrooms.Add(id);
    }

    public static void AddMushroomWithTrap(PlayerTrap trap)
    {
        mushroomsWithTrap.Add(trap);
    }

    public static bool CanGatherMushroom(int id)
    {
        int tmpId = currentlyCollectingMushrooms.Find(x => x == id);
        if (tmpId == id)
            return false;
        else 
            return true;
    }

    public static bool IsTrapOnMushroom(int mushroomId)
    {
        PlayerTrap mId = mushroomsWithTrap.Find(x => x.MushroomId == mushroomId);
       // Debug.Log("mid "+ mId.MushroomId);
        if(mId != null && mId.MushroomId == mushroomId)
        {
            return true;
        }
        else
            return false;
    }

    public static bool IsOwnTrapOnMushroom(int playerId, int mushroomId)
    {
       // PlayerTrap mId = mushroomsWithTrap.Find(x => x.MushroomId == mushroomId);
        foreach(PlayerTrap pt in mushroomsWithTrap)
        {
            Debug.Log("pid " + pt.PlayerId + " mid " + pt.MushroomId);
            Debug.Log("p " + playerId + " m " + mushroomId);
            if(pt.MushroomId == mushroomId && pt.PlayerId == playerId)
            {
                Debug.Log("Znalazłem!");
                return true;
            }
        }
        return false;
       /* try
        {
            Debug.Log("m " + mId.MushroomId + " p " + mId.PlayerId);
        }
        catch(Exception)
        {

        }
        if(mId != null && mId.MushroomId == mushroomId && mId.PlayerId == playerId)
        {
            return true;
        }
        else
        {
            return false;
        }*/
    }
}
