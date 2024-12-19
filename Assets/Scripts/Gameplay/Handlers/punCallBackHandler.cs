using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class punCallBackHandler : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if (SceneManager.Instance.isGameplay)
        {
            if (!otherPlayer.IsLocal)
            {
                Debug.Log("Player Left callback");
                GameManager.Instance.ShowWinPopup();
            }
        }
    }
}
