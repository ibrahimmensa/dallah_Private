using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject userNameScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.Instance.connectToPhoton();
        }
        if (PlayerPrefs.GetString("UserName") == "" || PlayerPrefs.GetString("UserName") == null)
        {
            Debug.Log("UserName: " + PlayerPrefs.GetString("UserName"));
            userNameScreen.gameObject.SetActive(true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("UserName");
        }
    }

    /// <summary>
    /// On Click Play Button on Main Menu
    /// </summary>
    public void onClickPlay()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.GAMEMODE);
        //Photon.Pun.PhotonNetwork.Disconnect();
    }

    /// <summary>
    /// LeaderBoard functionality will be implemented later
    /// </summary>
    public void onClickLeaderBoard()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.LEADERBOARD);
        //MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// INApps will be implemented later
    /// </summary>
    public void onClickNoAds()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// Seasons will be implemented later
    /// </summary>
    public void OnClickSeasons()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// Missions will be implemented later
    /// </summary>
    public void OnClickMissions()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// SHop will be implemented later
    /// </summary>
    public void onClickShop()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.SHOP);
        //MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// Daily rewards will be implemented Later
    /// </summary>
    public void onClickDailyRewards()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.DAILYREWARDS);
        //MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// Rewarded Ads will not be implemented in demo
    /// </summary>
    public void onClickMoreCoins()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        MenuManager.Instance.showPopup();
    }

    /// <summary>
    /// Open up settings menu
    /// </summary>
    public void onClickSettings()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.SETTINGS);
    }
}
