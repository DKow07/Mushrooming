using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.UI;

public class PUNManager : PunBehaviour
{
	public GameObject joinRoomBtn;
	public Text status;

    public GameObject startingBlackPanel;

	public GameObject playerPrefab;
    public GameObject chickenPrefab;
    public GameObject wolfPrefab;
    public GameObject duckPrefab;
    public GameObject penguinPrefab;
	public GameObject Plane;
	RoomOptions roomOption;

    public bool isDebugMode;

    public GameObject[] cars;

	public override void OnJoinedRoom ()
	{
		Debug.Log ("Connected to Room");
		joinRoomBtn.SetActive (false);
		Plane.SetActive (true);
		status.text = "Connected to room";


        Vector3 position = cars[PhotonNetwork.playerList.Length-1].GetComponent<CarController>().spawnPoint.position;

		//PhotonNetwork.Instantiate (playerPrefab.name, Vector3.zero, Quaternion.identity, 0); 

        int idPrefab = PlayerPrefs.GetInt("currentMushroomPicker", 1);

        if(idPrefab == 1)
            PhotonNetwork.Instantiate(chickenPrefab.name, position, Quaternion.identity, 0); 
        else if(idPrefab == 2)
            PhotonNetwork.Instantiate(duckPrefab.name, position, Quaternion.identity, 0);
        else if (idPrefab == 3)
            PhotonNetwork.Instantiate(penguinPrefab.name, position, Quaternion.identity, 0);
        else if (idPrefab == 4)
            PhotonNetwork.Instantiate(wolfPrefab.name, position, Quaternion.identity, 0); 
        else
            PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity, 0); 
        
	}

	void Awake ()
	{
		PhotonNetwork.ConnectUsingSettings ("0.1");
		roomOption = new RoomOptions ()
		{ IsOpen = true, MaxPlayers = 4, IsVisible = true };
	}
	void Start ()
	{
		joinRoomBtn.SetActive (false);
        Debug.Log(PhotonNetwork.playerList.Length);
    } 
	void Update ()
	{
		if(!PhotonNetwork.connected)
		{
			
			status.text=PhotonNetwork.connectionStateDetailed.ToString();
		}
        //Debug.Log(PhotonNetwork.playerList.Length);
        if (PhotonNetwork.playerList.Length >= 2|| isDebugMode)
        {
            startingBlackPanel.SetActive(false);
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainGameController>().isGameInProgress)
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainGameController>().isGameStarting = true;
        }
    }
	public override void OnJoinedLobby ()
	{
		joinRoomBtn.SetActive (true);
		status.text = "Connected";
		//if(PhotonNetwork.lobby==null)
		Debug.Log ("Joined lobby: " + PhotonNetwork.lobby.Name);
		status.text = "Connecting to room..";
		PhotonNetwork.JoinRandomRoom ();
	}
	public void JoinRoom ()
	{
		Debug.Log ("join room");
		status.text = "Connecting to room..";
		PhotonNetwork.JoinRandomRoom ();
	} 
	// This callback is called when random room join fails. Thus creating a new room
	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null, roomOption, null);
	}

}