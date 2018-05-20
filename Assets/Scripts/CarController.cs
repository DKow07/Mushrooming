using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class CarController : PunBehaviour 
{
    public int id;
    public Transform spawnPoint;
    public GameObject leaveMushroomsPanel;
    public GameObject[] leavePanels;
    //public int delayed;
    public bool isPlayerNearby;
    public bool isLeavingMushrooms;
    private PlayerController playerController;

    public GameObject timer;
    public Slider slider;
    public int timeDelayedInSeconds;
    private int delayed;
    private int score;

    public void LeaveMushroomsInCarButtons()
    {
        Debug.Log("Zostawiam grzyby");
        if (playerController != null && photonView.isMine)
        {
            leaveMushroomsPanel.SetActive(false); //TODO tests
            //leavePanels[playerController.id - 1].SetActive(true);
            slider.value = 0;
            playerController.canFollow = false;
            playerController.motor.agent.isStopped = true;
            //playerController.AddPoints();
            timer.SetActive(true);
            StartCoroutine("LeaveMushrooms");
        }
        else
        {
            Debug.Log("PlayerController is null");
        }
    }

    void Start()
    {
        slider.maxValue = timeDelayedInSeconds;
        delayed = timeDelayedInSeconds;
        leaveMushroomsPanel.SetActive(false);
    }

    public bool canMove;

    public void StartCoroutineCar()
    {
        canMove = false;
        slider.value = 0;
        playerController.canFollow = false;
        playerController.motor.agent.isStopped = true;
        timer.SetActive(true);
        StartCoroutine("LeaveMushrooms");
    }

    private IEnumerator LeaveMushrooms()
    {
        Debug.Log("Korutyna start");
        for (int i = 0; i < timeDelayedInSeconds-1; i++)
        {
            playerController.canFollow = false;
            playerController.motor.agent.isStopped = true;
            slider.value += 1;
            delayed -= 1;

            yield return new WaitForSeconds(1);
        }

        playerController.LeaveMushroomsInCar();

        timer.SetActive(false);
       
        playerController.canFollow = true;
        playerController.motor.agent.isStopped = false;
        playerController.AddPoints();
        canMove = true;

    }


    void OnTriggerEnter(Collider collider)
    {
        GameObject player = collider.gameObject;
        if(player.gameObject.CompareTag("Player" ))
        {
            if(player.gameObject.GetComponent<PlayerController>().id == this.id)
            {
                Basket playerBasket = collider.gameObject.GetComponent<Basket>();
                Debug.Log("Player w zasięgu");

                isPlayerNearby = true;
                playerController = collider.GetComponent<PlayerController>();

                if(playerBasket != null)
                {
                    if(playerBasket.currentCountOfMushrooms > 0)
                    {
                        leaveMushroomsPanel.SetActive(true);
                        //leavePanels[playerController.id - 1].SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("Brak koszyka");
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            isPlayerNearby = false;
            playerController = null;

            Debug.Log("Papa, czekam na ciebie");
            Basket playerBasket = collider.gameObject.GetComponent<Basket>();

            if (playerBasket != null)
            {
                leaveMushroomsPanel.SetActive(false);
            }
        }
    }


}
