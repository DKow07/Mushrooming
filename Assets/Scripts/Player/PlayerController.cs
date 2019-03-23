using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : PunBehaviour
{

    public GameObject cameraPrefab;

    public LayerMask movementMask;
    public Camera cam;
    [HideInInspector]
    public PlayerMovement motor;
    public Interactable focus;

    public GameObject gatherMushroomButton;

    [HideInInspector]
    public GameObject focusMushroom;
    [HideInInspector]
    public bool canFollow;

    public int score;
    public Text scoreText;
    [HideInInspector]
    public Basket basket;

    public int id;

    private GameObject fullBasketBlinking;

    public AudioClip movementSFX;
    public AudioClip droppingClip;
    public AudioClip trapClip;
    public AudioSource audioSource;
    

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        try
        {
            id = PhotonNetwork.playerList.Length;
            //  cam = Camera.main;
            if (photonView.isMine)
                Instantiate(cameraPrefab, transform.position, Quaternion.identity);
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            motor = GetComponent<PlayerMovement>();
            canFollow = true;
            score = 0;
            basket = GetComponent<Basket>();
            gatherMushroomButton = GameObject.FindGameObjectWithTag("GatherMushroomPanel");
            volumeTextInfo = GameObject.FindGameObjectWithTag("VolumeText").GetComponent<Text>();
            pointsTextInfo = GameObject.FindGameObjectWithTag("PointsText").GetComponent<Text>();
            cantAddToBasketPanel = GameObject.FindGameObjectWithTag("CantAddToBasketPanel");
            fullBasketBlinking = GameObject.FindGameObjectWithTag("FullBasketBlinking");
            cantAddToBasketPanel.SetActive(false);
            gatherMushroomButton.SetActive(false);
            fullBasketBlinking.SetActive(false);


            GetDataAboutPlayerMushroomPicker();
        }
        catch(Exception e)
        {

        }

    }

    public int currentMushroomPicker;
    public string currentMushroomPickerName;
    public int currentMushroomPickerBasket;
    public float currentMushroomPickerSpeed;
    public float currentMushroomPickerCutting;
    
    void GetDataAboutPlayerMushroomPicker()
    {
        currentMushroomPicker = PlayerPrefs.GetInt("currentMushroomPicker", 1);
        currentMushroomPickerName = PlayerPrefs.GetString("currentMushroomPickerName", "Rocket");
        currentMushroomPickerBasket = PlayerPrefs.GetInt("currentMushroomPickerBasket", 5);
        currentMushroomPickerSpeed = PlayerPrefs.GetFloat("currentMushroomPickerSpeed", 1f);
        currentMushroomPickerCutting = PlayerPrefs.GetFloat("currentMushroomPickerCutting", 1f);

        motor.agent.speed = motor.speed * currentMushroomPickerSpeed;
        basket.maxCountOfMushrooms = currentMushroomPickerBasket;
    }

    void Update()
    {
        try
        {
            if ((motor.agent.velocity.x != 0 || motor.agent.velocity.z != 0) && !audioSource.isPlaying)
            {
                audioSource.clip = movementSFX;
                audioSource.loop = true;
                audioSource.PlayOneShot(movementSFX);
            }
            //Debug.Log(motor.agent.velocity.ToString());

              Touch touch = Input.touches[0];
              if ((Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase.Equals(TouchPhase.Began) || (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)) && canFollow && photonView.isMine && GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainGameController>().isGameStarting)
            //if ((Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase.Equals(TouchPhase.Began)) && canFollow && photonView.isMine && GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainGameController>().isGameStarting) //left 
            {
                
                motor.agent.isStopped = false; 
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); 
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    Debug.Log("Klik " + hit.collider.name + " " + motor.agent.isStopped + " " + canFollow);
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
            }

            else if(!canFollow)
            {
                if (!motor.agent.isStopped)
                {
                    motor.agent.Stop();
                    motor.agent.isStopped = true;
                }
            }
        }
        catch(Exception ex)
        {

        }

        try
        {
            if (!UIController.isGathering)
            {
                if (carController.canMove && gatherMushroomButton.GetActive() == false && this.canFollow == false && focusMushroom == null) //leci null bo carcontroller
                {
                    this.canFollow = true;
                    this.motor.agent.isStopped = false;
                }

                if (carController.canMove && gatherMushroomButton.GetActive() == true && focusMushroom != null)
                {
                    this.canFollow = true;
                    this.motor.agent.isStopped = false;
                }

                if (carController.canMove && cantAddToBasketPanel.GetActive() == true)
                {
                    this.canFollow = true;
                    this.motor.agent.isStopped = false;
                }
            }
            else ///changes
            {
                this.canFollow = false;
                this.motor.agent.isStopped = true;
            }
        }
        catch(Exception ex)
        {

        }

        try
        {
            if (basket.canGather == false && fullBasketBlinking.GetComponent<BlinkingFullBasket>().isBlinking == false)
            {
                fullBasketBlinking.SetActive(true);
                fullBasketBlinking.GetComponent<BlinkingFullBasket>().isBlinking = true;
                fullBasketBlinking.GetComponent<BlinkingFullBasket>().blink = true;
            }
            else if (basket.canGather == true && fullBasketBlinking.GetComponent<BlinkingFullBasket>().isBlinking == true)
            {
                fullBasketBlinking.SetActive(false);
                fullBasketBlinking.GetComponent<BlinkingFullBasket>().isBlinking = false;
                fullBasketBlinking.GetComponent<BlinkingFullBasket>().blink = false;
            }

            if (mushroomControllerId != null)
            {
                if (!SynchronizationController.CanGatherMushroom(mushroomControllerId.mushroomId))
                {
                    gatherMushroomButton.SetActive(false);
                }
            }

        }
        catch(Exception e)
        {

        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }

    public void SetGatherButtonActive(bool isActive)
    {
        gatherMushroomButton.SetActive(isActive);
    }

    public CarController carController;
    public Text volumeTextInfo;
    public Text pointsTextInfo;
    public GameObject cantAddToBasketPanel;

    public MushroomController mushroomControllerId;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Mushroom" && this.gameObject.GetComponent<Basket>().canGather && IsMine())
        {
            MushroomController mushroomController = collider.gameObject.GetComponent<MushroomController>();
            mushroomControllerId = mushroomController;
            if (SynchronizationController.CanGatherMushroom(mushroomController.mushroomId) && !SynchronizationController.IsOwnTrapOnMushroom(this.id, mushroomController.mushroomId))
            {
                int currentCountOfMushrooms = GetComponent<Basket>().currentCountOfMushrooms;
                int maxCountOfMushrooms = GetComponent<Basket>().maxCountOfMushrooms;
                int mushroomsVolume = collider.gameObject.GetComponentInChildren<MushroomController>().mushroomVolume;
                int pointsOfMushroom = collider.gameObject.GetComponentInChildren<MushroomController>().mushroomPoints;

                int sum = currentCountOfMushrooms + mushroomsVolume;

                if(sum > maxCountOfMushrooms)
                {
                    gatherMushroomButton.SetActive(true);
                    cantAddToBasketPanel.SetActive(true);
                    Debug.Log("Nie możesz zebrać więcej grzybów " + sum + " / " + maxCountOfMushrooms);
                }
                else
                {
                    cantAddToBasketPanel.SetActive(false);
                    focusMushroom = collider.gameObject;
                    gatherMushroomButton.SetActive(true);
                }

                pointsTextInfo.text = "Points: " + pointsOfMushroom;
                volumeTextInfo.text = "Volume: " + mushroomsVolume;
            }
            else
            {
                //powiadomienie o niemożności zebrania grzyba, zbierany jest przez kogoś innego
            }
        }
        if(collider.gameObject.CompareTag("Car"))
        {
            CarController carController = collider.gameObject.GetComponent<CarController>();
            if(carController != null)
            {
                this.carController = carController;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Mushroom" && IsMine())
        {
            this.canFollow = true;
            this.motor.agent.isStopped = false;
            focusMushroom = null;
            gatherMushroomButton.SetActive(false);
            mushroomControllerId = null;
            if(cantAddToBasketPanel.GetActive() == true)
            {
                cantAddToBasketPanel.SetActive(false);
            }
        }
        if (collider.gameObject.CompareTag("Car"))
        {
            if (carController != null)
            {
                this.carController = null;
            }
        }
    }

    public void LeaveMushroomsInCar()
    {
        //Basket basket = GetComponent<Basket>();
        audioSource.PlayOneShot(droppingClip);
        StartCoroutine("LeaveMushrooms");
        basket.canGather = true;
        basket.mushroomsInBasket.Clear();
        basket.currentCountOfMushrooms = 0;
        basket.mushroomsInBasketText.text = basket.GetMushroomsInBasketText();
    }

    public void AddPoints(int points)
    {
        this.score += points;
        scoreText.text = this.score.ToString();
    }

    public void AddPoints()
    {

        //Basket basket = GetComponent<Basket>();

        List<Mushroom> tmpMushrooms = basket.mushroomsDataInBasket;

        foreach(Mushroom m in tmpMushrooms)
        {
            this.score += m.Points;
        }

        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + this.score.ToString();

        basket.mushroomsDataInBasket.Clear();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    public void SendMessage(int id)
    {
        this.photonView.RPC("DestroyMushroomWhereId", PhotonTargets.All, id);
        Debug.Log("Wysłałem do innych graczy");
    }

    [PunRPC]
    public void DestroyMushroomWhereId(int id)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ServerMushroomController>().DestroyMushroomWhereId(id);
        Debug.Log("usunąłem grzyba: " + id);
    }

    public void SendEndMessage()
    {
        this.photonView.RPC("EndGame", PhotonTargets.All);
        Debug.Log("Sending end game");
    }

    [PunRPC]
    public void EndGame()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainGameController>().summaryPanel.SetActive(true);
    }


    public void SendCollectingMushroom(int id)
    {
        this.photonView.RPC("SendCollectingMushroomRPC", PhotonTargets.All, id);
    }

    [PunRPC]
    public void SendCollectingMushroomRPC(int id)
    {
        SynchronizationController.AddCollectingMushroom(id);
        Debug.Log("Dodałem grzyb do listy aktualnie zbieranych grzybów");
    }

    public void SendTrappingMushroom(int pid, int mushId)
    {
        this.photonView.RPC("SendTrappingMushroomRPC", PhotonTargets.All, pid, mushId);
    }

    [PunRPC]
    public void SendTrappingMushroomRPC(int pid, int mushId)
    {
        PlayerTrap playerTrap = new PlayerTrap { MushroomId = mushId, PlayerId = pid };
        SynchronizationController.AddMushroomWithTrap(playerTrap);
        Debug.Log("Dodałem grzyb do listy grzybów z pułapkami mId " + mushId + " pId " + pid);
    }


    public void SendScoreToEnemy()
    {
        if (photonView.isMine)
        {
            Debug.LogWarning("sending score " + score);
            this.photonView.RPC("SendScoreToEnemyRPC", PhotonTargets.Others, score);
        }
    }

    [PunRPC]
    public void SendScoreToEnemyRPC(int points)
    {
        Debug.LogWarning("points " + points);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<SynchronizationController>().CreateLeaderBoard(score, points);
    }

    public bool IsMine()
    {
        return photonView.isMine;
    }

    public void PlayTrapSound()
    {
        audioSource.PlayOneShot(trapClip);
    }

}
