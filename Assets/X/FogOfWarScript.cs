using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class FogOfWarScript : PunBehaviour {

    public GameObject fogOfWarPlane;
    public Transform player;
    public LayerMask fogLayer;
    public float radius= 10f;
    public float radiusSqr { get { return radius* radius; } }

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;

    public GameObject fogOfWarPrivate;

    void Start ()
    {
            GameObject prefab = Instantiate(fogOfWarPlane, new Vector3(-35f, 5f, -35f), Quaternion.identity);
            prefab.tag = "FogOfWarTag";
            fogOfWarPrivate = GameObject.FindGameObjectWithTag("FogOfWarTag");
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            fogOfWarPlane = GameObject.FindGameObjectWithTag("FogOfWarTag");
            Initialize();
	}
	
	void Update ()
    {
        Ray r = new Ray(transform.position, player.position - transform.position);
        RaycastHit hit;
        if(Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for(int i =0; i<vertices.Length; i++)
            {
                Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if(dist < radiusSqr)
                {
                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqr);
                    colors[i].a = alpha;

                }
            }
            UpdateColor();
        }	
	}

    void Initialize()
    {
        mesh = fogOfWarPrivate.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        for(int i =0; i< colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        mesh.colors = colors;

    }
}
