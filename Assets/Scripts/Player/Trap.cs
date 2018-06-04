using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;
using System;

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
        try
        {
            if(GetComponent<PlayerController>().IsMine())
            {
                currentTrapCount = maxTrapCount;
                trapButton = GameObject.FindGameObjectWithTag("TrapButton");
                trapButton.SetActive(false);
                currentTrapCountText = GameObject.FindGameObjectWithTag("CurrentTrapCountText").GetComponent<Text>();
                currentTrapCountText.transform.parent = trapButton.transform;
            }
        }
        catch(Exception e)
        {

        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Mushroom") && GetComponent<PlayerController>().IsMine())
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
        if (coll.gameObject.CompareTag("Mushroom") && GetComponent<PlayerController>().IsMine())
        {
            trapButton.SetActive(false);
        }
    }

    public void DecrementTraps()
    {
        if (GetComponent<PlayerController>().IsMine())
        {
            currentTrapCount--;
            currentTrapCountText.text = currentTrapCount.ToString();
        }
    }

    public void Stun()
    {
        if(GetComponent<PlayerController>().IsMine())
        {
            Debug.Log("STUN trap");
            GetComponent<PlayerController>().canFollow = false;
            GetComponent<PlayerController>().motor.agent.isStopped = true;
            stunTimer = 5f;
            StartCoroutine("StunTimer");
        }
    }

    public float stunTimer = 5;

    void Update()
    {
        if (stunTimer <= 0)
        {
            stunTimer = 5;
            Debug.Log("Stun stopped");
            GetComponent<PlayerController>().motor.agent.isStopped = false;
            GetComponent<PlayerController>().canFollow = true;
        }
    }

    IEnumerator StunTimer()
    {

        for (int i = 0; i < 5; i++)
        {
            Debug.Log("Timer stun start");
            //  slider.value += 1;
            stunTimer -= 1;
            yield return new WaitForSeconds(1);
        }
    }
}
