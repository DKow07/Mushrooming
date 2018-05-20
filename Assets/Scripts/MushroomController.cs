using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using System;

public class MushroomController : PunBehaviour
{

    public GameObject timer;
    public Slider slider;
    public int timeDelayedInSeconds;
    private int delayed;

    private GameObject player;
    private PlayerController playerController;
    [HideInInspector]
    Mushroom mushroomData;

    public int mushroomPoints;
    public string mushroomName;
    public int mushroomId; //unique
    public int mushroomVolume;


    void Start()
    {
        delayed = timeDelayedInSeconds;
        mushroomData = new Mushroom(mushroomId, mushroomPoints, mushroomName, this.gameObject, mushroomVolume); //prawdopodobnie do usunięcia
       // mushroomId = ServerMushroomController.idNext;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (photonView.isMine)
            {
                timeDelayedInSeconds = Convert.ToInt32(Mathf.Floor(timeDelayedInSeconds * p.GetComponent<PlayerController>().currentMushroomPickerCutting));
            }
        }
                
    }

    void Update()
    {
       // if (id == null)
            //id = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ServerMushroomController>().mushromsList.Find(xs => xs.Obj == this.gameObject).ID;
        
        if(delayed <= 0)
        {

            CheckNotification();

            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.gameObject.GetComponent<PlayerController>();
            playerController.SetGatherButtonActive(false);
            playerController.canFollow = true;
            UIController.isGathering = false;
            Basket basket = player.gameObject.GetComponent<Basket>();
            if(basket != null)
            {
                basket.AddMushroomToBasket(this.gameObject);
                basket.AddMushroomData(new Mushroom(mushroomId, mushroomPoints, mushroomName, this.gameObject, mushroomVolume));
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SendMessage(id);

            //Destroy(this.gameObject);
            Destroy(timer);
        }
    }
    int id;

    public void ShowTimerAndAddPoints(int id)
    {
        this.id = id;
        timer.SetActive(true);
        slider.maxValue = timeDelayedInSeconds;
        StartCoroutine("TimerStart");
    }

    IEnumerator TimerStart()
    {
        for (int i = 0; i < timeDelayedInSeconds; i++ )
        {
            slider.value += 1;
            delayed -= 1;
            yield return new WaitForSeconds(1);
        } 
    }

    private List<Mushroom> mushroomsList;

    private void DestroyGatheredMushroom(int id)
    {
        Mushroom m = mushroomsList.Find(x => x.ID == id);
        Destroy(m.Obj);
    }

    private void CheckNotification()
    {
        if(mushroomName == "Podgrzybek")
        {
            int isAddedToAtlas = PlayerPrefs.GetInt("podgrzybek", 0);
            if(isAddedToAtlas == 0)
            {
                PlayerPrefs.SetInt("podgrzybek", 1);
                GameObject.FindGameObjectWithTag("PreferencesManager").GetComponent<NotificationManager>().ShowMushroomNotification();
            }
        }
        else if (mushroomName == "Borowik")
        {
            int isAddedToAtlas = PlayerPrefs.GetInt("borowik", 0);
            if (isAddedToAtlas == 0)
            {
                PlayerPrefs.SetInt("borowik", 1);
                GameObject.FindGameObjectWithTag("PreferencesManager").GetComponent<NotificationManager>().ShowMushroomNotification();
            }
        }
        else if (mushroomName == "Kania")
        {
            int isAddedToAtlas = PlayerPrefs.GetInt("kania", 0);
            if (isAddedToAtlas == 0)
            {
                PlayerPrefs.SetInt("kania", 1);
                GameObject.FindGameObjectWithTag("PreferencesManager").GetComponent<NotificationManager>().ShowMushroomNotification();
            }
        }
        else if (mushroomName == "Kurka")
        {
            int isAddedToAtlas = PlayerPrefs.GetInt("kurka", 0);
            if (isAddedToAtlas == 0)
            {
                PlayerPrefs.SetInt("kurka", 1);
                GameObject.FindGameObjectWithTag("PreferencesManager").GetComponent<NotificationManager>().ShowMushroomNotification();
            }
        }
    }

}
