using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom
{


    private int points;
    private string name;
    private int id;
    private int volume;
    private GameObject gameObject;

    public Mushroom(int id, int points, string name, GameObject gameObject, int volume)
    {
        this.id = id;
        this.name = name;
        this.points = points;
        this.gameObject = gameObject;
        this.volume = volume;
    }

    public int Points { get { return points; } set { points = value; } }
    public string Name { get { return name; } set { name = value; } }
    public int ID { get { return id; } set { id = value; } }
    public GameObject Obj { get { return gameObject; } set { gameObject = value; } }
    public int Volume { get { return volume; } set { volume = value; } }

}
