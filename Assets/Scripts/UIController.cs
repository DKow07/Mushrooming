using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject leavePanel; //TODO test
    public GameObject gatherPanel;
    public Slider slider;
    public GameObject timer;

    public static bool isGathering;

    public void GatherMushroom()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        PlayerController playerController = player.gameObject.GetComponent<PlayerController>();
        if(playerController.cantAddToBasketPanel.GetActive() == false)
        { 
            gatherPanel.SetActive(false);
       
            playerController.canFollow = false;
            playerController.motor.agent.isStopped = true;
            UIController.isGathering = true;

            GameObject mushroom = playerController.focusMushroom;
            if (mushroom != null)
            {
                MushroomController mushroomController = mushroom.gameObject.GetComponent<MushroomController>();
                
               
                if (mushroomController != null)
                {
                    int id = mushroomController.mushroomId;
                    mushroomController.ShowTimerAndAddPoints(id);
                }
                else
                {
                    Debug.Log("Mushroom controller is null");
                }
            }
        }
    }

    public void LeaveMushrooms()
    {
        leavePanel.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        PlayerController playerController = player.gameObject.GetComponent<PlayerController>();
        playerController.canFollow = false;
        playerController.motor.agent.isStopped = true;
        if(playerController.id == playerController.carController.id)
        {
            playerController.carController.StartCoroutineCar();
        }
        else
        {
            playerController.canFollow = true;
            playerController.motor.agent.isStopped = false;
        }
        
    }

    public void IsGatherMushroomHovered()
    {
        PlayerController playerController = GetPlayerControllerComponent();
        playerController.canFollow = false;
    }

    public void ExitGatherButton()
    {
        PlayerController playerController = GetPlayerControllerComponent();
    }

    private PlayerController GetPlayerControllerComponent()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.gameObject.GetComponent<PlayerController>();
        return playerController;
    }

    private Basket GetBasketComponent()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Basket basket = player.gameObject.GetComponent<Basket>();
        return basket;
    }
	
    public void GoToMenu()
    {
        //SceneManager.LoadScene(0);
        Application.Quit();
    }
}
