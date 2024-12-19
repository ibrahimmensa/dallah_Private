using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayUIHandler : MonoBehaviour
{
    public TMP_Text player1PieceCount, player2PieceCount, gameStatus, playerName, opponentName, playerPiecesLostText, opponentPiecesLostText;
    public GameObject loadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickNewPiece()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        if (GameManager.Instance.outsidePiecesCount <= 0)
            return;
        if (GameManager.Instance.currentPlayerState == TurnState.NONE || GameManager.Instance.currentPlayerState == TurnState.MOVING_PIECE || GameManager.Instance.currentPlayerState == TurnState.ADDING_NEW_PIECE)
        {
            //Debug.Log("Piece Clicked");
            GameManager.Instance.onPieceClicked();
        }
    }

    public void updateGameStatus(string status)
    {
        gameStatus.text = status;
        gameStatus.GetComponent<Animator>().SetTrigger("animate");
    }

    public void updatePiecesCount(bool isMine, int count)
    {
        if (isMine)
        {
            player1PieceCount.text = "x" + count.ToString();
        }
        else
        {
            player2PieceCount.text = "x" + count.ToString();
        }
    }

    public void updateLostPiecesCount(bool isMine, int count)
    {
        if (isMine)
        {
            playerPiecesLostText.text = "-" + count.ToString();
        }
        else
        {
            opponentPiecesLostText.text = "-" + count.ToString();
        }
    }

    public void onClickPause()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        MenuManager.Instance.showPauseMenu();
    }
}
