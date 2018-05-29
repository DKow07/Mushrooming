using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class Trap : PunBehaviour 
{

    public GameObject trapButton;
    public int currentTrapCount;
    public int maxTrapCount;
    public TrapType trapType;
    public Text currentTrapCountText;

    public enum TrapType { Stun }
    
    void Start()
    {
        currentTrapCount = maxTrapCount;
        trapButton = GameObject.FindGameObjectWithTag("TrapButton");
        trapButton.SetActive(false);
        currentTrapCountText = GameObject.FindGameObjectWithTag("CurrentTrapCountText").GetComponent<Text>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Mushroom"))
        {
            Debug.Log("Trap enter");
            MushroomController mushroomController = coll.gameObject.GetComponentInChildren<MushroomController>();
            if(currentTrapCount > 0)
            {
                if(!SynchronizationController.IsOwnTrapOnMushroom(GetComponent<PlayerController>().id, mushroomController.mushroomId))
                    trapButton.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Mushroom"))
        {
            trapButton.SetActive(false);
        }
    }

    public void DecrementTraps()
    {
        currentTrapCount--;
        currentTrapCountText.text = currentTrapCount.ToString();
    }






}
