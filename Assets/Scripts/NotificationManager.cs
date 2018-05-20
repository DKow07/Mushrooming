using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class NotificationManager : PunBehaviour 
{

    public GameObject newMushroomAlert;
    public float delay;
    public bool isShow;
    private float timer;

    void Update()
    {
        if(isShow)
        {
            timer += Time.deltaTime;
            if(timer >= delay)
            {
                timer = 0;
                isShow = false;
                newMushroomAlert.SetActive(false);
            }
        }
    }

    public void ShowMushroomNotification()
    {
        isShow = true;
        newMushroomAlert.SetActive(true);
    }

}
