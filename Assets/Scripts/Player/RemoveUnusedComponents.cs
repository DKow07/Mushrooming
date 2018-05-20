using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RemoveUnusedComponents : PunBehaviour 
{

    public Component[] components;


    void Start()
    {
        if (!photonView.isMine)
        {
            foreach (Component component in components)
            {
                DestroyImmediate(component);
            }
        }
    }
}
