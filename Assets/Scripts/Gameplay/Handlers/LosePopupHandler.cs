using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LosePopupHandler : MonoBehaviour
{
    public TMP_Text piecesLeftValue, TotalScoreValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        SoundManager.Instance.playSound(Sound_State.LOOSE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickReturnToHome()
    {
        GameManager.Instance.disconnectFromServer();
    }
}
