using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using System;


public class World : MonoBehaviour 
{

    public int width;
    public int height;

    public int[,] canGenerate;
    public GameObject[,] worldTiles;

    public float dimension;


    public int seed;


    //-------new generator
    public float treeBoxRadius = 3f;
    public float rockBoxRadius = 0.5f;
    public float mushroomBoxRadius = 0.5f;
    private List<Circle> objectsBoxes;
    public List<GameObject> treesList;
    public List<GameObject> obstacles;
    public List<GameObject> mushrooms;

    private int mushroomsCount;
    private int obstaclesCount;
    private int treesCount;

    public int obstCount;
    public int mushCount;

    public List<Mushroom> mushroomsList;

	void Start () 
    {
        UnityEngine.Random.seed = seed;
        objectsBoxes = new List<Circle>();
        mushroomsList = new List<Mushroom>();
        //objectsBoxes.Add(new Circle(new Vector3(55.37f, 2f, 59.56f), 5));  
        //objectsBoxes.Add(new Circle(new Vector3(54.8f, 2f, 70.84f), 5));  
        objectsBoxes.Add(new Circle(new Vector3(55f, 2f, 66f), 17));  
        GenerateObstacles();
        //objectsBoxes.Add(new Circle(new Vector3(55f, 2f, 66f), 15));
        GenerateMushrooms();
        Debug.Log("Grzyby = " + mushroomsCount);
        Debug.Log("Przeszkody = " + obstaclesCount);
        Debug.Log("Drzewa = " + treesCount);
	}

    private void GenerateMushrooms()  //nie usuwają się do końca potomkowie grzyba
    {
        float x = (width - 1) * dimension;
        float z = (height - 1) * dimension;
        x = Mathf.Abs(x);
        z = Mathf.Abs(z);

        for (int i = 0; i < mushCount; i++)
        {
            bool canInstiansiate;

            do
            {
                canInstiansiate = true;

                float posX = UnityEngine.Random.Range(1, x);
                float posY = 1.85f;
                float posZ = UnityEngine.Random.Range(1, z);
                Vector3 newPos = new Vector3(posX, posY, posZ);
                Circle tmpCircle = new Circle(newPos, mushroomBoxRadius);


                if (objectsBoxes.Count > 0)
                {
                    foreach (Circle c in objectsBoxes)
                    {
                        bool hasIntersect = tmpCircle.IntersectCircles(c);
                        if (hasIntersect)
                        {
                            canInstiansiate = false;
                        }
                    }
                }

                if (canInstiansiate)
                {
                    int percentChance = UnityEngine.Random.Range(0, 100);
                    mushroomsCount++;
                    int id = ServerMushroomController.idNext++;
                    GameObject mush = null;
                    if (percentChance > 0 && percentChance <= 50) //podgrzybek
                    {
                        mush = Instantiate(mushrooms[0], newPos, Quaternion.identity);
                        objectsBoxes.Add(new Circle(newPos, mushroomBoxRadius));
                    }
                    else if (percentChance > 50 && percentChance <= 80) //kurka
                    {
                        mush = Instantiate(mushrooms[1], newPos, Quaternion.identity);
                        objectsBoxes.Add(new Circle(newPos, mushroomBoxRadius));
                    }
                    else if (percentChance > 80 && percentChance <= 90) //czubajka kania
                    {
                        mush = Instantiate(mushrooms[3], newPos, Quaternion.identity);
                        objectsBoxes.Add(new Circle(newPos, mushroomBoxRadius));
                    }
                    else if (percentChance > 90) //borowik szlachetny
                    {
                        mush = Instantiate(mushrooms[2], newPos, Quaternion.identity);
                        objectsBoxes.Add(new Circle(newPos, mushroomBoxRadius));
                    }
                    string name = "";
                    int points = 1;
                    int volume = 1;

                    if (mush != null)
                    {
                        name = mush.GetComponentInChildren<MushroomController>().mushroomName;
                        points = mush.GetComponentInChildren<MushroomController>().mushroomPoints;
                        volume = mush.GetComponentInChildren<MushroomController>().mushroomVolume;
                    }
                    else
                    {
                        Debug.Log("Mamy problem");
                    }

                    Mushroom m = new Mushroom(id, points, name, mush, volume);

                    try
                    {
                        mush.GetComponentInChildren<MushroomController>().mushroomId = id;
                    }
                    catch(Exception e)
                    {
                        Debug.Log("Gościu z dżawy Sakowicz");
                    }
                    mushroomsList.Add(m);
                }

            } while (!canInstiansiate);

        }

    }

    private void GenerateObstacles()
    {

        float x = (width-1) * dimension;
        float z = (height-1) * dimension;
        x = Mathf.Abs(x);
        z = Mathf.Abs(z);

        for (int i = 0; i < obstCount; i++ )
        {
            bool canInstiansiate;
           
            do
            {
                canInstiansiate = true;

                float posX = UnityEngine.Random.Range(1, x);
                float posY = 1.85f;
                float posZ = UnityEngine.Random.Range(1, z);
                Vector3 newPos = new Vector3(posX, posY, posZ);
                Circle tmpCircle = new Circle(newPos, treeBoxRadius);


                if (objectsBoxes.Count > 0)
                {
                    foreach (Circle c in objectsBoxes)
                    {
                        bool hasIntersect = tmpCircle.IntersectCircles(c);
                        if (hasIntersect)
                        {
                            canInstiansiate = false;
                        }
                    }
                }

                if (canInstiansiate) 
                {
                    int percentChance = UnityEngine.Random.Range(0, 100);
                    
                    if(percentChance > 0 && percentChance < 75)
                    {
                        GameObject tree = Instantiate(treesList[UnityEngine.Random.Range(0, treesList.Count)], newPos, Quaternion.identity);
                        float rnd = UnityEngine.Random.Range(0, 360);
                        tree.transform.localEulerAngles = new Vector3(tree.transform.localEulerAngles.x, rnd, tree.transform.localEulerAngles.z);
                        objectsBoxes.Add(new Circle(newPos, treeBoxRadius));
                        treesCount++;
                    }
                    else if (percentChance > 75)
                    {
                        Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Count - 1)], newPos, Quaternion.identity);
                        objectsBoxes.Add(new Circle(newPos, rockBoxRadius));
                        obstaclesCount++;
                    }
                }

            } while (!canInstiansiate);
                
        }

    }


    void OnDrawGizmos()
    {
       /* Gizmos.color = Color.red;
        foreach(Circle c in objectsBoxes)
        {
            Gizmos.DrawSphere(c.position, c.radius);
        } 
        */
    }



}
