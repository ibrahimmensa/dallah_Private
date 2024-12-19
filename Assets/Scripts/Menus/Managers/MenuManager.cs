using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : Singleton<MenuManager>
{
    public MainMenuHandler mainMenuHandler;
    public SettingsHandler settingsHandler;
    public GameModeHandler gameModeHandler;
    public GamePlayUIHandler gamePlayUIHandler;
    public PauseMenuHandler pauseMenuHandler;
    public SplashHandler splashHandler;
    public PopupHandler popupHandler;
    public DailyRewardsHandler dailyRewardsHandler;
    public LeaderBoardHandler leaderBoardHandler;
    public ShopHandler shopHandler;
    public WinPopupHandler winHandler;
    public LosePopupHandler loseHandler;
    public RoomHandler roomHandler;
    public SplashHandler matchMakingSplash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Setting to splash screen on gamestart
    /// </summary>
    public void onGameStart()
    {
        switchMenu();
    }

    /// <summary>
    /// disable all menus before switching to new menu according to gamestate
    /// </summary>
    public void disableAllMenus()
    {
        mainMenuHandler.gameObject.SetActive(false);
        settingsHandler.gameObject.SetActive(false);
        gameModeHandler.gameObject.SetActive(false);
        gamePlayUIHandler.gameObject.SetActive(false);
        pauseMenuHandler.gameObject.SetActive(false);
        splashHandler.gameObject.SetActive(false);
        dailyRewardsHandler.gameObject.SetActive(false);
        leaderBoardHandler.gameObject.SetActive(false);
        shopHandler.gameObject.SetActive(false);
        matchMakingSplash.gameObject.SetActive(false);
        roomHandler.gameObject.SetActive(false);
    }

    /// <summary>
    /// Switch Menus according to current gamestate
    /// </summary>
    public void switchMenu()
    {
        disableAllMenus();
        switch (SceneManager.Instance.currentState)
        {
            case GameState.GAMEMODE:
                gameModeHandler.gameObject.SetActive(true);
                break;
            case GameState.GAMEPLAY:
                gamePlayUIHandler.gameObject.SetActive(true);
                break;
            case GameState.LEADERBOARD:
                leaderBoardHandler.gameObject.SetActive(true);
                break;
            case GameState.LOOSE:
                break;
            case GameState.MAINMENU:
                mainMenuHandler.gameObject.SetActive(true);
                break;
            case GameState.MATCHMAKING:
                matchMakingSplash.gameObject.SetActive(true);
                break;
            case GameState.PAUSE:
                pauseMenuHandler.gameObject.SetActive(true);
                break;
            case GameState.SETTINGS:
                settingsHandler.gameObject.SetActive(true);
                break;
            case GameState.SHOP:
                shopHandler.gameObject.SetActive(true);
                break;
            case GameState.SPLASH:
                splashHandler.gameObject.SetActive(true);
                break;
            case GameState.WIN:
                break;
            case GameState.DAILYREWARDS:
                dailyRewardsHandler.gameObject.SetActive(true);
                break;
            case GameState.ROOMSCREEN:
                roomHandler.gameObject.SetActive(true);
                roomHandler.updateRoomsList();
                break;
        }
    }

    public void showPopup()
    {
        popupHandler.gameObject.SetActive(true);
    }

    public void showWinPopup()
    {
        winHandler.gameObject.SetActive(true);
        winHandler.piecesLeftValue.text = "+" + GameManager.Instance.outsidePiecesCount;
        int totalScore = 1 + GameManager.Instance.outsidePiecesCount;
        winHandler.TotalScoreValue.text = "+" + totalScore.ToString();
    }

    public void showLosePopup()
    {
        loseHandler.gameObject.SetActive(true);
        loseHandler.piecesLeftValue.text = "-" + GameManager.Instance.opponentOutsidePiecesCount;
        int totalScore = -1 - GameManager.Instance.opponentOutsidePiecesCount;
        loseHandler.TotalScoreValue.text = totalScore.ToString();
    }

    public void showPauseMenu()
    {
        pauseMenuHandler.gameObject.SetActive(true);
    }
}
