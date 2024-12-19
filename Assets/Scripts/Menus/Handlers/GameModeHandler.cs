using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeHandler : MonoBehaviour
{
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
        //Photon.Pun.PhotonNetwork.Disconnect();
    }

    public void onClickBack()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.MAINMENU);
    }

    public void onClickMultiplayer()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        //SceneManager.Instance.switchState(GameState.GAMEPLAY);
        //SceneManager.Instance.onClickMultiplayer();
        SceneManager.Instance.switchState(GameState.ROOMSCREEN);
        //Photon.Pun.PhotonNetwork.ConnectUsingSettings();

    }
}
