using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMushroomController : MonoBehaviour {


    public static int idNext;

	// Use this for initialization
	void Start () {
        idNext = 0;
	}

    public List<Mushroom> mushromsList;

	void Update ()
    {
        mushromsList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<World>().mushroomsList;
	}

    public void DestroyMushroomWhereId(int id)
    {
        Mushroom m = mushromsList.Find(x => x.ID == id);
        GameObject go = m.Obj;
        Destroy(go);
    }
}
