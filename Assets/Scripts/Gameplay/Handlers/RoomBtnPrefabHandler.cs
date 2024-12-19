using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomBtnPrefabHandler : MonoBehaviour
{
    public TMP_Text roomName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickRoom()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.onClickJoin(roomName.text);
    }
}
