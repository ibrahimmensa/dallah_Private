using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellHandler : MonoBehaviour
{
    public int cellNumber;
    public bool isFill = false;
    public bool isSelected = false;
    public PieceHandler piece;
    public bool isAvailableForInteraction;
    //public Color32 cellColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        //cellColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsAvailable(bool isAvailable)
    {
        if (isAvailable)
        {
            GetComponent<Image>().color = Color.green;
            isAvailableForInteraction = true;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            isAvailableForInteraction = false;
        }
    }

    public void IsOccupied(PieceHandler piece)
    {
        this.piece = piece;
        isFill = true;
        IsAvailable(false);
    }

    public void onClicked()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        if (!GameManager.Instance.player1.isTurn)
            return;
        if (isFill && (GameManager.Instance.currentPlayerState == TurnState.NONE || GameManager.Instance.currentPlayerState == TurnState.MOVING_PIECE || GameManager.Instance.currentPlayerState == TurnState.ADDING_NEW_PIECE) && GameManager.Instance.currentPlayerTurn == piece.player)
        {
            //GameManager.Instance.stopAnimatingCell();
            //GameManager.Instance.animateCell(piece);
            GameManager.Instance.onCellWithPieceClicked(this);
        }
        else if (!isFill && GameManager.Instance.currentPlayerState == TurnState.ADDING_NEW_PIECE && isAvailableForInteraction)
        {
            //Debug.Log("Placing new Piece in  Cell Number" + cellNumber);
            GameManager.Instance.onCellClicked(this);
        }
        else if (!isFill && GameManager.Instance.currentPlayerState == TurnState.MOVING_PIECE && isAvailableForInteraction)
        {
            GameManager.Instance.onCellClicked(this);
        }
        else if (isFill && GameManager.Instance.currentPlayerState == TurnState.REMOVING_OPPONENT_PIECE && isAvailableForInteraction && piece.player != GameManager.Instance.player1)
        {
            removePiece();
            GameManager.Instance.opponentPiecesLostCount++;
            GameManager.Instance.updateLostPiecesCount(false, GameManager.Instance.opponentPiecesLostCount);
            GameManager.Instance.syncDataWrapper(Photon.Pun.RpcTarget.Others, Sync_State.REMOVINGOPPONENTPIECE, cellNumber, 0, GameManager.Instance.outsidePiecesCount);
            GameManager.Instance.boardHandler.NumberOfPiecesToRemove--;
            if (GameManager.Instance.boardHandler.NumberOfPiecesToRemove == 0)
            {
                GameManager.Instance.boardHandler.ResetCellColor();
                GameManager.Instance.currentPlayerState = TurnState.NONE;
                GameManager.Instance.changeTurn();
            }
            if (GameManager.Instance.opponentPiecesLostCount >= 12)
            {
                GameManager.Instance.ShowWinPopup();
            }
            SoundManager.Instance.playSound(Sound_State.PIECEDESTROYED);
        }
        
    }

    public void addPiece(GameObject piecePrefab)
    {
        GameObject tempPiece;
        PieceHandler tempPieceHandler;
        tempPiece = Instantiate(piecePrefab, this.gameObject.transform);
        tempPieceHandler = tempPiece.GetComponent<PieceHandler>();
        tempPieceHandler.cell = this;
        tempPieceHandler.player = GameManager.Instance.currentPlayerTurn;
        isFill = true;
        piece = tempPieceHandler;
    }

    public void removePiece()
    {
        piece.GetComponent<Animator>().SetTrigger("remove");
        //Destroy(piece.gameObject);
        isFill = false;
        StartCoroutine(removePieceWithDelay());
        //piece = null;
        //SoundManager.Instance.playSound(Sound_State.PIECEDESTROYED);
    }

    IEnumerator removePieceWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(piece.gameObject);
        isFill = false;
        piece = null;
    }
}
