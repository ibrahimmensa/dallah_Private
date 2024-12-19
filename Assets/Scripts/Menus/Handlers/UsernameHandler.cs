using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;

public class UsernameHandler : MonoBehaviour
{
    public TMP_Text userName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickSave()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        PlayerPrefs.SetString("UserName", userName.text);
        this.gameObject.SetActive(false);
        Photon.Pun.PhotonNetwork.NickName = userName.text;
    }
}
