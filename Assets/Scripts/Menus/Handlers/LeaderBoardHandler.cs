using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickBack()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.MAINMENU);
    }
}
