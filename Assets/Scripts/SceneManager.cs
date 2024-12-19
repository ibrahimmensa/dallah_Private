using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public enum GameState
{
    SPLASH,
    MAINMENU,
    SETTINGS,
    SHOP,
    LEADERBOARD,
    GAMEMODE,
    MATCHMAKING,
    GAMEPLAY,
    PAUSE,
    WIN,
    LOOSE,
    DAILYREWARDS,
    LOADINGMULTIPLAYER,
    ROOMSCREEN
}

public class SceneManager : Singleton<SceneManager>
{
    public GameState currentState;
    NetworkHandler networkHandler;
    public string roomName;
    public List<RoomInfo> allRooms = new List<RoomInfo>();
    public bool isGameplay = false;
    //public string roomName;
    //public NetworkHandler networkHandler;

    // Start is called before the first frame update

    /// <summary>
    /// Setting game to splash state on game start
    /// </summary>
    void Start()
    {
        currentState = GameState.SPLASH;
        MenuManager.Instance.onGameStart();
        //networkHandler.connectToPhoton();
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {

        networkHandler = FindAnyObjectByType<NetworkHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Switching states depending on user interaction with UI
    /// </summary>
    public void switchState(GameState newState)
    {
        currentState = newState;
        MenuManager.Instance.switchMenu();
    }

    public void onClickMultiplayer()
    {
        //networkHandler.connectToPhoton();
        //switchState(GameState.MATCHMAKING);
        //SceneManager.Instance.currentState = GameState.MATCHMAKING;
        //networkHandler.JoinLobbyInit();
    }

    public void connectToPhoton()
    {
        networkHandler.connectToPhoton();
    }

    public void onClickJoin(string roomID)
    {
        roomName = roomID;
        //networkHandler.roomName = roomID;
        networkHandler.createOrJoinRoom(roomID);
        switchState(GameState.MATCHMAKING);
        SceneManager.Instance.currentState = GameState.MATCHMAKING;
    }

    public void updateSplashState(string status)
    {
        MenuManager.Instance.matchMakingSplash.updateStatusText(status);
    }

    public void roomsUpdated(List<RoomInfo> roomList)
    {
        allRooms.Clear();
        allRooms = roomList;
        if (MenuManager.Instance.roomHandler.gameObject.activeSelf)
        {
            MenuManager.Instance.roomHandler.updateRoomsList();
        }
    }
}

