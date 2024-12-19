using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHandler : MonoBehaviour
{
    public int playerNumber;
    public int piecesOnBoard;
    public int piecesOutsideBoard;
    public int opponentPiecesDestroyed;
    public bool isTurn = false;
    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;
    public PhotonView view;

    void Update()
    {
        //Check if the left Mouse button is clicked
       

    }
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
    }

    private void OnEnable()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            //Destroy(this);
            GameManager.Instance.player2 = this;
        }
        else
        {
            GameManager.Instance.player1 = this;
        }
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        //if (!isTurn)
        //    return;
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    //Set up the new Pointer Event
        //    m_PointerEventData = new PointerEventData(m_EventSystem);
        //    //Set the Pointer Event Position to that of the mouse position
        //    m_PointerEventData.position = Input.mousePosition;

        //    //Create a list of Raycast Results
        //    List<RaycastResult> results = new List<RaycastResult>();

        //    //Raycast using the Graphics Raycaster and mouse click position
        //    m_Raycaster.Raycast(m_PointerEventData, results);

        //    //For every result returned, output the name of the GameObject on the Canvas hit by the Ray

        //    //Debug.Log("Hit " + results[0].gameObject.name);
        //    if (results[0].gameObject.tag == "cell")
        //    {
        //        Debug.Log("Clicked on the cell");
        //    }

        //}
    }

    #region pun_RPC

    [PunRPC]
    public void onOpponentSurrendered()
    {
        Debug.Log("Player Surrendered RPC");
        SceneManager.Instance.isGameplay = false;
        GameManager.Instance.ShowWinPopup();
    }

    [PunRPC]
    public void syncData(int state, int newCell, int previousCell, int opponentPiecesCount = 0)
    {
        Sync_State newState = (Sync_State)state;
        //Debug.Log("New State Got:");
        switch (newState)
        {
            case Sync_State.PLACINGNEWPIECE:
                GameManager.Instance.boardHandler.placeOpponentPiece(GameManager.Instance.boardHandler.allCells[newCell - 1]);
                MenuManager.Instance.gamePlayUIHandler.updatePiecesCount(false, opponentPiecesCount);
                GameManager.Instance.opponentOutsidePiecesCount = opponentPiecesCount;
                break;
            case Sync_State.MOVINGPIECE:
                GameManager.Instance.boardHandler.moveOpponentPiece(GameManager.Instance.boardHandler.allCells[previousCell - 1], GameManager.Instance.boardHandler.allCells[newCell - 1]);
                break;
            case Sync_State.REMOVINGOPPONENTPIECE:
                GameManager.Instance.playerPiecesLostCount++;
                GameManager.Instance.updateLostPiecesCount(true, GameManager.Instance.playerPiecesLostCount);
                GameManager.Instance.boardHandler.allCells[newCell - 1].removePiece();
                GameManager.Instance.placedPieces.Remove(GameManager.Instance.boardHandler.allCells[newCell - 1]);
                SoundManager.Instance.playSound(Sound_State.PIECEDESTROYED);
                if (GameManager.Instance.playerPiecesLostCount >= 12)
                {
                    GameManager.Instance.showLosePopup();
                }
                break;
        }
    }

    [PunRPC]
    public void chooseTurn(bool isMasterTurn)
    {
        //if (!view.IsMine)
        //    return;
        //Debug.Log("Turn Recieved");
        GameManager.Instance.boardHandler.excludingPieces.Clear();
        if (PhotonNetwork.IsMasterClient)
        {
            if (isMasterTurn)
            {
                GameManager.Instance.player1.isTurn = true;
                MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Your Turn");
                GameManager.Instance.currentPlayerTurn = GameManager.Instance.player1;
                MenuManager.Instance.gamePlayUIHandler.gameStatus.color = Color.green;
            }
            else
            {
                GameManager.Instance.player1.isTurn = false;
                MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Opponent Turn");
                GameManager.Instance.currentPlayerTurn = GameManager.Instance.player2;
                MenuManager.Instance.gamePlayUIHandler.gameStatus.color = Color.red;
            }
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            if (!isMasterTurn)
            {
                GameManager.Instance.player1.isTurn = true;
                MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Your Turn");
                GameManager.Instance.currentPlayerTurn = GameManager.Instance.player1;
                MenuManager.Instance.gamePlayUIHandler.gameStatus.color = Color.green;
            }
            else
            {
                GameManager.Instance.player1.isTurn = false;
                MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Opponent Turn");
                GameManager.Instance.currentPlayerTurn = GameManager.Instance.player2;
                MenuManager.Instance.gamePlayUIHandler.gameStatus.color = Color.red;
            }
        }
        if (GameManager.Instance.player1.isTurn && GameManager.Instance.outsidePiecesCount <= 0)
        {
            GameManager.Instance.boardHandler.ResetCellColor();
            GameManager.Instance.boardHandler.isThereACombinationOnBoard = false;
            GameManager.Instance.boardHandler.isThereACombinationOnBoard = GameManager.Instance.boardHandler.checkAllPiecesOnBoardForCombinations();
        }
    }

#endregion

}
