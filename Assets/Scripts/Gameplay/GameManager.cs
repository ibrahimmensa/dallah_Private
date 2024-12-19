using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public enum TurnState
{
    ADDING_NEW_PIECE,
    MOVING_PIECE,
    REMOVING_OPPONENT_PIECE,
    NONE
}

public enum Sync_State
{
    PLACINGNEWPIECE = 0,
    MOVINGPIECE = 1,
    REMOVINGOPPONENTPIECE = 2,
}

public class GameManager : Singleton<GameManager>
{
    public PlayerHandler player1, player2;
    public PlayerHandler currentPlayerTurn;
    public BoardHandler boardHandler;
    public int outsidePiecesCount;
    public int opponentOutsidePiecesCount;
    public TurnState currentPlayerState;
    public int firstTurnMoveCount = 2;
    public int TurnMoveCount = 1;
    public int moveMade = 0;
    public bool isFirstTurn = true;
    public PlayerHandler[] allPlayers;
    public Photon.Realtime.Player[] players;
    public NetworkHandler networkHandler;
    public int playerPiecesLostCount = 0;
    public int opponentPiecesLostCount = 0;
    public PieceHandler currentSelectedPiece;
    public List<CellHandler> placedPieces;

    CellHandler previousCell;
    // Start is called before the first frame update
    void Start()
    {
        // will be updated by photon
        currentPlayerTurn = player1;
        currentPlayerState = TurnState.NONE;
    }

    private void OnEnable()
    {
        playerPiecesLostCount = 0;
        opponentPiecesLostCount = 0;
        updateLostPiecesCount(true, playerPiecesLostCount);
        updateLostPiecesCount(false, opponentPiecesLostCount);
        player1 = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetComponent<PlayerHandler>();
        if (PhotonNetwork.IsMasterClient)
        {
            player1.GetComponent<PhotonView>().RPC("chooseTurn", RpcTarget.All, true);
            //currentPlayerTurn = player1;
            //player1.isTurn = true;
            //MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Your Turn");
        }
        findAllPlayers();
        //allPlayers = FindObjectsOfType<PlayerHandler>();
        //for (int i = 0; i < allPlayers.Length; i++)
        //{
        //    if (allPlayers[i].view.IsMine)
        //    {
        //        player1 = allPlayers[i];
        //    }
        //    else
        //    {
        //        player2 = allPlayers[i];
        //    }
        //}
        networkHandler = FindObjectOfType<NetworkHandler>();
        SceneManager.Instance.isGameplay = true;
        outsidePiecesCount = 12;
        MenuManager.Instance.gamePlayUIHandler.updatePiecesCount(false, outsidePiecesCount);
        MenuManager.Instance.gamePlayUIHandler.updatePiecesCount(true, outsidePiecesCount);

    }

    public void findAllPlayers()
    {
        players = PhotonNetwork.PlayerList;
        foreach (Photon.Realtime.Player player in players)
        {
            if (player.IsLocal)
            {
                MenuManager.Instance.gamePlayUIHandler.playerName.text = player.NickName;
            }
            else
            {
                MenuManager.Instance.gamePlayUIHandler.opponentName.text = player.NickName;
            }
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPieceClicked()
    {
        if (!player1.isTurn)
            return;
        currentPlayerState = TurnState.ADDING_NEW_PIECE;
        if (isFirstTurn)
        {
            moveMade++;
            if (moveMade == firstTurnMoveCount)
            {
                moveMade = 0;
                isFirstTurn = false;
            }
            boardHandler.checkCenterPiecesForFirstMove();
        }
        else
        {
            boardHandler.checkCellStatusForPlacingNewPiece(currentPlayerTurn);
        }
    }

    public void onCellWithPieceClicked(CellHandler cell)
    {
        if ((GameManager.Instance.currentPlayerState == TurnState.NONE || GameManager.Instance.currentPlayerState == TurnState.MOVING_PIECE || GameManager.Instance.currentPlayerState == TurnState.ADDING_NEW_PIECE) && !isFirstTurn)
        {
            boardHandler.ResetCellColor();
            previousCell = cell;
            if (boardHandler.checkCellStatusForMovingPiece(cell))
            {
                currentPlayerState = TurnState.MOVING_PIECE;
            }
            else
            {
                
            }
        }
        // player wants to move a piece in board
    }

    public void onCellClicked(CellHandler cell)
    {
        // player is either placing new piece or moving a selected piece
        if (currentPlayerState == TurnState.ADDING_NEW_PIECE && cell.isAvailableForInteraction)
        {
            placedPieces.Add(cell);
            boardHandler.placeNewPiece(cell);
            outsidePiecesCount--;
            MenuManager.Instance.gamePlayUIHandler.updatePiecesCount(true, outsidePiecesCount);
            boardHandler.checkSingleCellCombination(cell, player1);
            currentPlayerState = TurnState.NONE;
            boardHandler.ResetCellColor();
            player1.view.RPC("syncData", RpcTarget.Others, (int)Sync_State.PLACINGNEWPIECE, cell.cellNumber, 0, outsidePiecesCount);
            if (!isFirstTurn)
            {
                //player1.isTurn = false;
                changeTurn();
                //MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Opponent Turn");
            }
        }
        else if (currentPlayerState == TurnState.MOVING_PIECE && cell.isAvailableForInteraction)
        {
            placedPieces.Remove(previousCell);
            placedPieces.Add(cell);
            boardHandler.movePiece(previousCell, cell);
            //currentPlayerState = TurnState.NONE;
            //boardHandler.ResetCellColor();
            
        }
    }

    internal void ShowWinPopup()
    {
        //throw new NotImplementedException();
        MenuManager.Instance.showWinPopup();
    }

    public void showLosePopup()
    {
        MenuManager.Instance.showLosePopup();
    }

    public void updateStatsOnNetwork()
    { 
    }

    public void changeTurn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1.GetComponent<PhotonView>().RPC("chooseTurn", RpcTarget.All, false);
        }
        else
        {
            player1.GetComponent<PhotonView>().RPC("chooseTurn", RpcTarget.All, true);
        }
    }

    public void syncDataWrapper(RpcTarget target, Sync_State state, int newCell, int previousCell, int pieceCount)
    {
        player1.view.RPC("syncData", target, (int)state, newCell, previousCell, pieceCount);
    }

    public void OnClickSurrender()
    {
        player1.view.RPC("onOpponentSurrendered", RpcTarget.Others);
        showLosePopup();
    }
   
    public void disconnectFromServer()
    {
        MenuManager.Instance.gamePlayUIHandler.loadingScreen.SetActive(true);
        SceneManager.Instance.isGameplay = false;
        PhotonNetwork.LeaveRoom(false);
        //PhotonNetwork.LeaveLobby();
        //PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        Debug.Log("Disconnecting Photon");
        StartCoroutine(waitForDisconnection());
    }

    IEnumerator waitForDisconnection()
    {
        yield return null;
        Debug.Log("Disconnecting Coroutin Enter");
        while (PhotonNetwork.IsConnected)
        {
            Debug.Log("Still Connected");
            yield return new WaitForSeconds(2);
        }
        MenuManager.Instance.gamePlayUIHandler.loadingScreen.SetActive(false);
        SceneManager.Instance.currentState = GameState.SPLASH;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void updateLostPiecesCount(bool isMine, int count)
    {
        MenuManager.Instance.gamePlayUIHandler.updateLostPiecesCount(isMine, count);
    }

    public void animateCell(PieceHandler piece)
    {
        //currentSelectedPiece = piece;
        //currentSelectedPiece.GetComponent<Animator>().SetBool("animate", true);
    }

    public void stopAnimatingCell()
    {
        //if (currentSelectedPiece != null)
        //{
        //    //currentSelectedPiece.GetComponent<Animator>().SetBool("animate", false);
        //}
    }
}
