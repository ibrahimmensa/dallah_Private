using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkHandler : MonoBehaviourPunCallbacks
{

    
    // Start is called before the first frame update
    void Start()
    {
        //connectToPhoton();
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void connectToPhoton()
    {
        //menuHandler.status.text = "Status: Connecting to Photon.";
        Debug.Log("Connect to photon");
        //if(!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        //menuHandler.status.text = "Status: Joining Lobby.";
        Debug.Log("On Connected to master");
        //PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        JoinLobbyInit();
    }

    public void JoinLobbyInit()
    {
        //SceneManager.Instance.updateSplashState("Joining Lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        //createOrJoinRoom(roomName);
        //SceneManager.Instance.updateSplashState("Lobby Joined....");
        ////menuHandler.loadingScreen.SetActive(false);
        //RoomOptions option = new RoomOptions();
        //option.MaxPlayers = 2;
        //PhotonNetwork.JoinOrCreateRoom("Room1", option, TypedLobby.Default);
        //menuHandler.waitScreen.SetActive(true);
        //menuHandler.mainScreen.SetActive(false);
        //Debug.Log(PhotonNetwork.IsMasterClient);
        //menuHandler.startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        //menuHandler.startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }


    public void createRoom(string roomName, int maxPlayers)
    {
        SceneManager.Instance.updateSplashState("Creating Room....");
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.JoinOrCreateRoom(roomName, option, TypedLobby.Default);
        //Debug.Log("Room Creating");
    }

    public void createOrJoinRoom(string roomName)
    {
        Debug.Log("Joining room :" + roomName);
        SceneManager.Instance.updateSplashState("Creating Room....");
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = 2;
        option.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(roomName, option, TypedLobby.Default);
    }

    public void joinRoom(string roomName)
    {
        SceneManager.Instance.updateSplashState("Joining Room....");
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnCreatedRoom()
    {
        SceneManager.Instance.updateSplashState("Room Created....");
        Debug.Log("New room created");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
       // menuHandler.startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinedRoom()
    {
        //SceneManager.Instance.updateSplashState("Room Joined....");
        Debug.Log("Room joined successfully");
        //Debug.Log("Is Master Client: "+PhotonNetwork.IsMasterClient);
        SceneManager.Instance.updateSplashState("Waiting For Player....");
        //menuHandler.startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        //menuHandler.PlayerNumber = PhotonNetwork.PlayerList.Length;
        //menuHandler.playerCount.text = menuHandler.PlayerNumber.ToString();
        //menuHandler.waitScreen.SetActive(true);
        //menuHandler.mainScreen.SetActive(false);
        //int temp = Random.Range(0,SceneHandler.Instance.playerSpawnPoints.Length);
        //SceneHandler.Instance.player = PhotonNetwork.Instantiate(playerPrefab.name, SceneHandler.Instance.playerSpawnPoints[temp].transform.position, Quaternion.identity);
        //PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Debug.Log("Room creation failed cause: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       // Debug.Log("Player Entered Room");
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Gameplay");
        }
        //menuHandler.PlayerNumber = PhotonNetwork.PlayerList.Length;
        //menuHandler.playerCount.text = menuHandler.PlayerNumber.ToString();
    }


    public override void OnDisable()
    {
        base.OnDisable();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if (SceneManager.Instance.isGameplay)
        {
            if (!otherPlayer.IsLocal)
            {
                //GameManager.Instance.ShowWinPopup();
            }
        }
        //menuHandler.PlayerNumber = PhotonNetwork.PlayerList.Length;
        //menuHandler.playerCount.text = menuHandler.PlayerNumber.ToString();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        SceneManager.Instance.allRooms.Clear();
        SceneManager.Instance.roomsUpdated(roomList);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //PhotonNetwork.ConnectUsingSettings();
        base.OnDisconnected(cause);
    }
}
