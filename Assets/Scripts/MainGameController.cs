using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class MainGameController :  PunBehaviour
{

    public float gameLengthInSeconds;
    public float currentLength;

    public bool isGameInProgress;

    public bool isGameStarting;

    public Text timerText;
    public GameObject summaryPanel;

    void Start()
    {
        currentLength = gameLengthInSeconds;
        isGameInProgress = true;
        isGameStarting = false;
    }

    public int tmp;

    void Update()
    {
        if (isGameStarting)
        {
            currentLength -= Time.deltaTime;
            if (currentLength <= 0)
            {
             
                //summaryPanel.SetActive(true);
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                /*foreach (GameObject p in players) ////////////
                {
                    if (photonView.isMine)
                    {
                        int prevValue = PlayerPrefs.GetInt("bitcoin", 0);
                        int tmp = p.GetComponent<PlayerController>().score;
                        int bitcoinValue = p.GetComponent<PlayerController>().score;
                        bitcoinValue += prevValue;
                        Debug.Log("Poprzednia wartość " + prevValue + " zebrano " + tmp + " aktualna wartość " + bitcoinValue);
                        PlayerPrefs.SetInt("bitcoin", bitcoinValue);
                    }
                }*/

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                int prevValue = PlayerPrefs.GetInt("bitcoin", 0);
                tmp = player.GetComponent<PlayerController>().score;
                int bitcoinValue = player.GetComponent<PlayerController>().score;
                bitcoinValue += prevValue;
                Debug.LogWarning("Poprzednia wartość " + prevValue + " zebrano " + tmp + " aktualna wartość " + bitcoinValue);
                PlayerPrefs.SetInt("bitcoin", bitcoinValue);

                isGameInProgress = false;
                isGameStarting = false;

                foreach (GameObject p in players)
                {
                        p.GetComponent<PlayerController>().SendScoreToEnemy();
                }

                foreach(GameObject p in players)
                {

                    if(p.GetComponent<PlayerController>().id == 1)
                    {
                        p.GetComponent<PlayerController>().SendEndMessage();
                    }
                }
                
               
            }
            UpdateTimeUI();
        }
    }

    private void UpdateTimeUI()
    {
        int minutes = (int)currentLength / 60;
        int seconds = (int)currentLength % 60;

        if (minutes < 10 || seconds < 10)
        {
            if (minutes < 10 && seconds < 10)
                timerText.text = "0" + minutes + ":0" + seconds;
            else if (seconds < 10)
                timerText.text = minutes + ":0" + seconds;
            else
                timerText.text = "0" + minutes + ":" + seconds;
        }
        if (minutes > 10 && seconds > 10)
           timerText.text = minutes + ":" + seconds;
        
    }


}
