using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplashHandler : MonoBehaviour
{
    public Slider loadingBar;
    public TMP_Text statusText;
    public bool isMatchMakingSplash = false;
    public TMP_Text roomID;
    public GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        loadingBar.value = 0;
        StartCoroutine(startLoadingTimer());
        if (isMatchMakingSplash)
        {
            roomID.text = SceneManager.Instance.roomName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchToMainMenu()
    {
        SceneManager.Instance.switchState(GameState.MAINMENU);
    }

    public void startLoading()
    { 
    }

    IEnumerator startLoadingTimer()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 5; i++)
        {
            loadingBar.value += 20;
            yield return new WaitForSeconds(0.5f);
        }
        if (SceneManager.Instance.currentState == GameState.SPLASH)
        {
            if (!isMatchMakingSplash)
            {
                switchToMainMenu();
            }
        }
        else if (SceneManager.Instance.currentState == GameState.MATCHMAKING)
        {
            StartCoroutine(waitingForPlayerTime());
        }
    }

    public void updateStatusText(string status)
    {
        if (isMatchMakingSplash)
        { 
            statusText.text = status;
        }

    }

    public void onClickBack()
    {
        Photon.Pun.PhotonNetwork.LeaveRoom();
        SceneManager.Instance.switchState(GameState.MAINMENU);
    }

    public IEnumerator waitingForPlayerTime()
    {
        yield return new WaitForSeconds(30);
        if (this.gameObject.activeSelf)
        {
            popup.SetActive(true);
        }
    }
}
