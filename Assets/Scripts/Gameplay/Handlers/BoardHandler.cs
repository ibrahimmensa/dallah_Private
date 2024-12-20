using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardHandler : MonoBehaviour
{
    public List<CellHandler> allCells;
    public List<CellHandler> AvailableCells;
    public List<CellHandler> excludingPieces;
    public List<CellHandler> animatedPieces;
    public int[] centerPiecesIndex;
    public GameObject opponentPiecePrefab;
    public GameObject playerPiecePrefab;
    public GameObject cellPrefab;
    public GameObject cellParent;
    public int NumberOfPiecesToRemove = 0;
    public bool isThereACombinationOnBoard = false;

    List<CellHandler> leftAdjacentPieces, rightAdjacentPieces, topAdjacentPieces, bottomAdjacentPieces;
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
        init();
    }

    private void init()
    {
        GameObject tempCell;
        CellHandler tempCellHandler;
        for (int i = 0; i < 36; i++)
        {
            tempCell = Instantiate(cellPrefab, cellParent.transform);
            tempCellHandler = tempCell.GetComponent<CellHandler>();
            tempCellHandler.cellNumber = i + 1;
            tempCellHandler.isFill = false;
            tempCellHandler.piece = null;
            tempCellHandler.IsAvailable(false);
            tempCellHandler.GetComponent<Button>().onClick.AddListener(tempCellHandler.onClicked);
            allCells.Add(tempCellHandler);
        }
    }

    public void checkCenterPiecesForFirstMove()
    {
        for (int i = 0; i < centerPiecesIndex.Length; i++)
        {
            if (!allCells[centerPiecesIndex[i]].isFill)
            {
                allCells[centerPiecesIndex[i]].IsAvailable(true);
            }
        }
    }

    public void ResetCellColor()
    {
        foreach (CellHandler cell in allCells)
        {
            cell.IsAvailable(false);
        }
    }

    public bool checkCellStatusForCombination(CellHandler currentCell)
    {
        CellHandler upperCell, lowerCell, leftCell, rightCell;
        int upperCellIndex, lowerCellIndex, rightCellIndex, leftCellIndex;
        bool isMakingCombination = false;
        //bool isThereACombination = checkAllPiecesOnBoardForCombinations();
        //ResetCellColor();
        //Check Upper Piece
        if (currentCell.cellNumber > 6)
        {
            upperCellIndex = currentCell.cellNumber - 6;
            upperCell = allCells[upperCellIndex - 1];
            if (!upperCell.isFill)
            {
                if ((adjacentPiecesOnTop(upperCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesInRow(upperCell, GameManager.Instance.player1) + 1) == 3)
                {
                    if (adjacentPiecesOnTop(upperCell, GameManager.Instance.player1) + adjacentPiecesInRow(upperCell, GameManager.Instance.player1) < 4)
                    {
                        //upperCell.IsAvailable(true);
                        isMakingCombination = true;
                    }
                }
                
            }
        }
        //Check Lower Piece
        if (currentCell.cellNumber < 31)
        {
            lowerCellIndex = currentCell.cellNumber + 6;
            lowerCell = allCells[lowerCellIndex - 1];
            if (!lowerCell.isFill)
            {
                if ((adjacentPiecesOnBottom(lowerCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesInRow(lowerCell, GameManager.Instance.player1) + 1) == 3)
                {
                    if (adjacentPiecesOnBottom(lowerCell, GameManager.Instance.player1) + adjacentPiecesInRow(lowerCell, GameManager.Instance.player1) < 4)
                    {
                        isMakingCombination = true;
                    }
                }
                
            }
        }
        //Check Left Piece
        if (currentCell.cellNumber % 6 != 1)
        {
            leftCellIndex = currentCell.cellNumber - 1;
            leftCell = allCells[leftCellIndex - 1];
            if (!leftCell.isFill)
            {
                if ((adjacentPiecesInColumn(leftCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesOnLeft(leftCell, GameManager.Instance.player1) + 1) == 3)
                {
                    if (adjacentPiecesInColumn(leftCell, GameManager.Instance.player1) + adjacentPiecesOnLeft(leftCell, GameManager.Instance.player1) < 4)
                    {
                        isMakingCombination = true;
                    }
                }
                
            }
        }
        //Check Right Piece
        if (currentCell.cellNumber % 6 != 0)
        {
            rightCellIndex = currentCell.cellNumber + 1;
            rightCell = allCells[rightCellIndex - 1];
            if (!rightCell.isFill)
            {
                if ((adjacentPiecesInColumn(rightCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesOnRight(rightCell, GameManager.Instance.player1) + 1) == 3)
                {
                    if (adjacentPiecesInColumn(rightCell, GameManager.Instance.player1) + adjacentPiecesOnRight(rightCell, GameManager.Instance.player1) < 4)
                    {
                        isMakingCombination = true;
                    }
                }
            }
        }
        return isMakingCombination;
    }


    public bool checkCellStatusForMovingPiece(CellHandler currentCell)
    {
        CellHandler upperCell, lowerCell, leftCell, rightCell;
        int upperCellIndex, lowerCellIndex, rightCellIndex, leftCellIndex;
        bool canMove = false;
        //bool isThereACombination = checkAllPiecesOnBoardForCombinations();
        //ResetCellColor();
        //Check Upper Piece
        if (currentCell.cellNumber > 6)
        {
            upperCellIndex = currentCell.cellNumber - 6;
            upperCell = allCells[upperCellIndex - 1];
            if (!upperCell.isFill)
            {
                if (GameManager.Instance.outsidePiecesCount <= 0)
                {
                    if (isThereACombinationOnBoard)
                    {
                        if ((adjacentPiecesOnTop(upperCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesInRow(upperCell, GameManager.Instance.player1) + 1) == 3)
                        {
                            if (adjacentPiecesOnTop(upperCell, GameManager.Instance.player1) + adjacentPiecesInRow(upperCell, GameManager.Instance.player1) < 4)
                            {
                                upperCell.IsAvailable(true);
                                canMove = true;
                            }
                        }
                    }
                    else
                    {
                        upperCell.IsAvailable(true);
                        canMove = true;
                    }
                    // change color to green for available
                    
                }
                else
                {
                    if ((adjacentPiecesOnTop(upperCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesInRow(upperCell, GameManager.Instance.player1) + 1) == 3)
                    {
                        if (adjacentPiecesOnTop(upperCell, GameManager.Instance.player1) + adjacentPiecesInRow(upperCell, GameManager.Instance.player1) < 4)
                        {
                            upperCell.IsAvailable(true);
                            canMove = true;
                        }
                    }
                }
            }
        }
        //Check Lower Piece
        if (currentCell.cellNumber < 31)
        {
            lowerCellIndex = currentCell.cellNumber + 6;
            lowerCell = allCells[lowerCellIndex - 1];
            if (!lowerCell.isFill)
            {
                if (GameManager.Instance.outsidePiecesCount <= 0)
                {
                    if (isThereACombinationOnBoard)
                    {
                        if ((adjacentPiecesOnBottom(lowerCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesInRow(lowerCell, GameManager.Instance.player1) + 1) == 3)
                        {
                            if (adjacentPiecesOnBottom(lowerCell, GameManager.Instance.player1) + adjacentPiecesInRow(lowerCell, GameManager.Instance.player1) < 4)
                            {
                                lowerCell.IsAvailable(true);
                                canMove = true;
                            }
                        }
                    }
                    else
                    {
                        // change color to green for available
                        lowerCell.IsAvailable(true);
                        canMove = true;
                    }
                }
                else
                {
                    if ((adjacentPiecesOnBottom(lowerCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesInRow(lowerCell, GameManager.Instance.player1) + 1) == 3)
                    {
                        if (adjacentPiecesOnBottom(lowerCell, GameManager.Instance.player1) + adjacentPiecesInRow(lowerCell, GameManager.Instance.player1) < 4)
                        {
                            lowerCell.IsAvailable(true);
                            canMove = true;
                        }
                    }
                }
            }
        }
        //Check Left Piece
        if (currentCell.cellNumber % 6 != 1)
        {
            leftCellIndex = currentCell.cellNumber - 1;
            leftCell = allCells[leftCellIndex - 1];
            if (!leftCell.isFill)
            {
                if (GameManager.Instance.outsidePiecesCount <= 0)
                {
                    if (isThereACombinationOnBoard)
                    {
                        if ((adjacentPiecesInColumn(leftCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesOnLeft(leftCell, GameManager.Instance.player1) + 1) == 3)
                        {
                            if (adjacentPiecesInColumn(leftCell, GameManager.Instance.player1) + adjacentPiecesOnLeft(leftCell, GameManager.Instance.player1) < 4)
                            {
                                leftCell.IsAvailable(true);
                                canMove = true;
                            }
                        }
                    }
                    else
                    {
                        // change color to green for available
                        leftCell.IsAvailable(true);
                        canMove = true;
                    }
                }
                else
                {
                    if ((adjacentPiecesInColumn(leftCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesOnLeft(leftCell, GameManager.Instance.player1) + 1) == 3)
                    {
                        if (adjacentPiecesInColumn(leftCell, GameManager.Instance.player1) + adjacentPiecesOnLeft(leftCell, GameManager.Instance.player1) < 4)
                        {
                            leftCell.IsAvailable(true);
                            canMove = true;
                        }
                    }
                }
            }
        }
        //Check Right Piece
        if (currentCell.cellNumber % 6 != 0)
        {
            rightCellIndex = currentCell.cellNumber + 1;
            rightCell = allCells[rightCellIndex - 1];
            if (!rightCell.isFill)
            {
                if (GameManager.Instance.outsidePiecesCount <= 0)
                {
                    if (isThereACombinationOnBoard)
                    {
                        if ((adjacentPiecesInColumn(rightCell, GameManager.Instance.player1) + 1) == 3 || (adjacentPiecesOnRight(rightCell, GameManager.Instance.player1) + 1) == 3)
                        {
                            if (adjacentPiecesInColumn(rightCell, GameManager.Instance.player1) + adjacentPiecesOnRight(rightCell, GameManager.Instance.player1) < 4)
                            {
                                rightCell.IsAvailable(true);
                                canMove = true;
                            }
                        }
                    }
                    else
                    {
                        // change color to green for available
                        rightCell.IsAvailable(true);
                        canMove = true;
                    }
                }
                else
                {
                    if ((adjacentPiecesInColumn(rightCell, GameManager.Instance.player1) + 1)  == 3 || (adjacentPiecesOnRight(rightCell, GameManager.Instance.player1) + 1) == 3)
                    {
                        if (adjacentPiecesInColumn(rightCell, GameManager.Instance.player1) + adjacentPiecesOnRight(rightCell, GameManager.Instance.player1) < 4)
                        {
                            rightCell.IsAvailable(true);
                            canMove = true;
                        }
                    }
                }
            }
        }
        return canMove;
    }

    public void movePiece(CellHandler previousCell, CellHandler newCell)
    {
        SoundManager.Instance.playSound(Sound_State.PIECEMOVED);
        newCell.addPiece(playerPiecePrefab);
        previousCell.removePiece();
        //previousCell.isFill = false;
        GameManager.Instance.player1.view.RPC("syncData", RpcTarget.Others, (int)Sync_State.MOVINGPIECE, newCell.cellNumber, previousCell.cellNumber, GameManager.Instance.outsidePiecesCount);
        if (IsCellMakingCombination(newCell, newCell.piece.player))
        {
            //check for number of compbinations made
            //int count = checkForNumberOfCombinations(newCell, newCell.piece.player);
            //Debug.Log("Total Combination made is: " + count);
            NumberOfPiecesToRemove++;
            if (checkAllPiecesOnBoardForCombinations())
            {
                //NumberOfPiecesToRemove++;
                ResetCellColor();
                GameManager.Instance.currentPlayerState = TurnState.NONE;
            }
            else
            {
                GameManager.Instance.currentPlayerState = TurnState.REMOVING_OPPONENT_PIECE;
                MenuManager.Instance.gamePlayUIHandler.updateGameStatus("Remove Opponent Piece");
                GameManager.Instance.currentPlayerState = TurnState.REMOVING_OPPONENT_PIECE;
                ResetCellColor();
                getRemoveableOpponentPieces();
            }
            //GameManager.Instance.changeTurn();
        }
        else
        {
            ResetCellColor();
            GameManager.Instance.currentPlayerState = TurnState.NONE;
            GameManager.Instance.changeTurn();
        }
    }

    public void moveOpponentPiece(CellHandler previousCell, CellHandler newCell)
    {
        SoundManager.Instance.playSound(Sound_State.PIECEMOVED);
        newCell.addPiece(opponentPiecePrefab);
        previousCell.removePiece();
    }

    public void placeNewPiece(CellHandler cell)
    {
        cell.addPiece(playerPiecePrefab);
    }

    public void placeOpponentPiece(CellHandler cell)
    {
        cell.addPiece(opponentPiecePrefab);
    }

    public void getRemoveableOpponentPieces()
    {
        //Debug.Log("Checking for opponents cell");
        foreach (CellHandler cell in allCells)
        {
            if (cell.isFill)
            {
                if (cell.piece.player == GameManager.Instance.player2)
                {
                    cell.IsAvailable(true);
                    //Debug.Log("Available Cells: " + cell.cellNumber);
                }
            }
        }
    }

    public void checkCellStatusForPlacingNewPiece(PlayerHandler player)
    {
        AvailableCells.Clear();
        if (checkAllPiecesOnBoardForCombinations())
        {
            ResetCellColor();
            GameManager.Instance.currentPlayerState = TurnState.NONE;
            return;
        }
        foreach (CellHandler cell in allCells)
        {
            if (!cell.isFill)
            {
                if (adjacentPiecesInColumn(cell, player) <= 1 && adjacentPiecesInRow(cell, player) <= 1 && !isMakingSquares(cell, player) && !cell.isFill)
                {
                    cell.IsAvailable(true);
                }
            }
        }
    }

    public bool checkAllPiecesOnBoardForCombinations()
    {
        foreach (CellHandler cell in animatedPieces)
        {
            cell.piece.animate(false);
        }
        animatedPieces.Clear();
        foreach (CellHandler cell in GameManager.Instance.placedPieces)
        {
            if (!excludingPieces.Contains(cell))
            {
                if (checkCellStatusForCombination(cell))
                {
                    cell.piece.animate(true);
                    animatedPieces.Add(cell);
                    Debug.Log("Combination on Cell: " + cell.cellNumber);
                    //cell.piece.GetComponent<Animator>().SetBool("animate", true);
                    //ResetCellColor();
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsCellMakingCombination(CellHandler cell, PlayerHandler player)
    {
        //if (isCombinationsAbove(cell, player) || isCombinationsBelow(cell, player) || isCombinationLeft(cell, player) || isCombinationRight(cell, player))
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        if ((adjacentPiecesInColumn(cell, player) + 1) == 3)
        {
            //Debug.Log("Combination is Made");
            excludingPieces.Add(cell);
            excludingPieces.AddRange(getAdjacentPiecesOnBottom(cell,player));
            excludingPieces.AddRange(getAdjacentPiecesOnTop(cell, player));
            return true;
        }
        if ((adjacentPiecesInRow(cell, player) + 1) == 3)
        {
            //Debug.Log("Combination is Made");
            excludingPieces.Add(cell);
            excludingPieces.AddRange(getAdjacentPiecesOnLeft(cell, player));
            excludingPieces.AddRange(getAdjacentPiecesOnRight(cell, player));
            return true;
        }
        return false;
    }

    public bool isCombinationsAbove(CellHandler cell, PlayerHandler player)
    {
        if (cell.cellNumber <= 12)
        {
            return false;
        }
        else
        {
            if (allCells[cell.cellNumber - 7].isFill && allCells[cell.cellNumber - 7].piece.player == player)
            {
                if (allCells[cell.cellNumber - 13].isFill && allCells[cell.cellNumber - 13].piece.player == player)
                {
                    return true;
                }
                    return false;
            }else
                return false;
        }
    }

    public bool isCombinationsBelow(CellHandler cell, PlayerHandler player)
    {
        if (cell.cellNumber > 24)
        {
            return false;
        }
        else
        {
            if (allCells[cell.cellNumber + 5].isFill && allCells[cell.cellNumber + 5].piece.player == player)
            {
                if (allCells[cell.cellNumber + 11].isFill && allCells[cell.cellNumber + 11].piece.player == player)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
    }

    public bool isCombinationRight(CellHandler cell, PlayerHandler player)
    {
        if (cell.cellNumber % 6 == 5 && cell.cellNumber % 6 == 0)
        {
            return false;
        }
        else
        {
            if (allCells[cell.cellNumber].isFill && allCells[cell.cellNumber].piece.player == player)
            {
                if (allCells[cell.cellNumber + 1].isFill && allCells[cell.cellNumber + 1].piece.player == player)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
    }

    public bool isCombinationLeft(CellHandler cell, PlayerHandler player)
    {
        if (cell.cellNumber % 6 == 1 && cell.cellNumber % 6 == 2)
        {
            return false;
        }
        else
        {
            if (allCells[cell.cellNumber - 2].isFill && allCells[cell.cellNumber - 2].piece.player == player)
            {
                if (allCells[cell.cellNumber - 3].isFill && allCells[cell.cellNumber - 3].piece.player == player)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
    }

    public int adjacentPiecesInColumn(CellHandler cell, PlayerHandler player)
    {
        Debug.Log("Adjacent pieces in column: " + adjacentPiecesOnBottom(cell, player) + adjacentPiecesOnTop(cell, player));
        return (adjacentPiecesOnBottom(cell, player) + adjacentPiecesOnTop(cell, player));
    }

    public int adjacentPiecesInRow(CellHandler cell, PlayerHandler player)
    {
        Debug.Log("Adjacent Pieces in a row:" + adjacentPiecesOnRight(cell, player) + adjacentPiecesOnLeft(cell, player));
        return (adjacentPiecesOnLeft(cell, player) + adjacentPiecesOnRight(cell, player));
    }

    public int adjacentPiecesOnLeft(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        leftAdjacentPieces = new List<CellHandler>();
        leftAdjacentPieces.Clear();
        if (cell.cellNumber % 6 == 1)
            return 0;
        nextCellIndex = cell.cellNumber - 1;
        do
        {
            if (nextCellIndex == 0)
                break;
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                Debug.Log("Cell Number:" + nextCellIndex);
                leftAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex--;
            }
            else
            {
                break;
            }
        } while ((nextCellIndex - 1) % 6 != 5);
        //Debug.Log("Pieces On Left: " + count);
        return count;

    }

    public List<CellHandler> getAdjacentPiecesOnLeft(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        leftAdjacentPieces = new List<CellHandler>();
        leftAdjacentPieces.Clear();
        if (cell.cellNumber % 6 == 1)
            return leftAdjacentPieces;
        nextCellIndex = cell.cellNumber - 1;
        do
        {
            if (nextCellIndex == 0)
                break;
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                leftAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex--;
            }
            else
            {
                break;
            }
        } while ((nextCellIndex - 1) % 6 != 5);
        return leftAdjacentPieces;

    }

    public int adjacentPiecesOnRight(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        rightAdjacentPieces = new List<CellHandler>();
        rightAdjacentPieces.Clear();
        if ((cell.cellNumber) % 6 == 0)
            return 0;
        nextCellIndex = cell.cellNumber + 1;
        do 
        {           
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                rightAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex++;
            }
            else
            {
                break;
            }
        }while ((nextCellIndex - 1) % 6 != 0) ;
        //Debug.Log("Pieces On Right: " + count);
            return count;
    }

    public List<CellHandler> getAdjacentPiecesOnRight(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        rightAdjacentPieces = new List<CellHandler>();
        rightAdjacentPieces.Clear();
        if ((cell.cellNumber) % 6 == 0)
            return rightAdjacentPieces;
        nextCellIndex = cell.cellNumber + 1;
        do
        {
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                rightAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex++;
            }
            else
            {
                break;
            }
        } while ((nextCellIndex - 1) % 6 != 0);
        return rightAdjacentPieces;
    }

    public int adjacentPiecesOnTop(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        topAdjacentPieces = new List<CellHandler>();
        topAdjacentPieces.Clear();
        if (cell.cellNumber <= 6)
            return 0;
        nextCellIndex = cell.cellNumber - 6;
        do
        {
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                topAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex -= 6;
            }
            else
            {
                break;
            }
        } while (nextCellIndex > 0);
        //Debug.Log("Pieces On Top: " + count);
        return count;
    }

    public List<CellHandler> getAdjacentPiecesOnTop(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        topAdjacentPieces = new List<CellHandler>();
        topAdjacentPieces.Clear();
        if (cell.cellNumber <= 6)
            return topAdjacentPieces;
        nextCellIndex = cell.cellNumber - 6;
        do
        {
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                topAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex -= 6;
            }
            else
            {
                break;
            }
        } while (nextCellIndex > 0);
        return topAdjacentPieces;
    }

    public int adjacentPiecesOnBottom(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        bottomAdjacentPieces = new List<CellHandler>();
        bottomAdjacentPieces.Clear();
        if (cell.cellNumber >= 31 )
            return 0;
        nextCellIndex = cell.cellNumber + 6;
        do
        {
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                bottomAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex += 6;
            }
            else
            {
                break;
            }
        } while (nextCellIndex <= 36);
        //Debug.Log("Pieces On Bottom: " + count);
        return count;
    }

    public List<CellHandler> getAdjacentPiecesOnBottom(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        //CellHandler nextCell;
        int nextCellIndex = 0;
        bottomAdjacentPieces = new List<CellHandler>();
        bottomAdjacentPieces.Clear();
        if (cell.cellNumber >= 31)
            return bottomAdjacentPieces;
        nextCellIndex = cell.cellNumber + 6;
        do
        {
            if (allCells[nextCellIndex - 1].isFill && allCells[nextCellIndex - 1].piece.player == player)
            {
                bottomAdjacentPieces.Add(allCells[nextCellIndex - 1]);
                count++;
                nextCellIndex += 6;
            }
            else
            {
                break;
            }
        } while (nextCellIndex <= 36);
        return bottomAdjacentPieces;
    }

    public bool isMakingSquares(CellHandler cell, PlayerHandler player)
    {
        if (adjacentPiecesOnTop(cell, player) >= 1 && adjacentPiecesOnRight(cell, player) >= 1)
        {
            //Checking TopRight Diagnol
            int cellIndex = cell.cellNumber - 5;
            CellHandler temp = allCells[cellIndex - 1];
            if (temp.isFill && temp.piece.player == player)
            {
                return true;
            }
        }
        if (adjacentPiecesOnTop(cell, player) >= 1 && adjacentPiecesOnLeft(cell, player) >= 1)
        {
            //Checking TopLeft Diagnol
            int cellIndex = cell.cellNumber - 7;
            CellHandler temp = allCells[cellIndex - 1];
            if (temp.isFill && temp.piece.player == player)
            {
                return true;
            }
        }
        if (adjacentPiecesOnBottom(cell, player) >= 1 && adjacentPiecesOnRight(cell, player) >= 1)
        {
            //Checking BottomRight Diagnol
            int cellIndex = cell.cellNumber + 7;
            CellHandler temp = allCells[cellIndex - 1];
            if (temp.isFill && temp.piece.player == player)
            {
                return true;
            }
        }
        if (adjacentPiecesOnBottom(cell, player) >= 1 && adjacentPiecesOnLeft(cell, player) >= 1)
        {
            //Checking BottomLeft Diagnol
            int cellIndex = cell.cellNumber + 5;
            CellHandler temp = allCells[cellIndex - 1];
            if (temp.isFill && temp.piece.player == player)
            {
                return true;
            }
        }
        return false;
    }

    public int checkForNumberOfCombinationsInOneDirection(CellHandler cell, PlayerHandler player, bool checkColumn)
    {
        int count = 0;
        if (checkColumn)
        {
            if ((adjacentPiecesInColumn(cell, player) + 1) % 3 == 0)
            {
                count += (adjacentPiecesInColumn(cell, player) + 1) / 3;
                if (count > 0)
                {
                    List<CellHandler> AllCellsOfCombinationOnBottom = getAdjacentPiecesOnBottom(cell, player);
                    List<CellHandler> AllCellsOfCombinationOnTop = getAdjacentPiecesOnTop(cell, player);
                    if (AllCellsOfCombinationOnBottom.Count > 0)
                    {
                        foreach (CellHandler temp in AllCellsOfCombinationOnBottom)
                        {
                            count += checkForNumberOfCombinationsInOneDirection(temp, player, false);
                        }
                    }
                    if (AllCellsOfCombinationOnTop.Count > 0)
                    {
                        foreach (CellHandler temp in AllCellsOfCombinationOnTop)
                        {
                            count += checkForNumberOfCombinationsInOneDirection(temp, player, false);
                        }
                    }
                }
                
            }
        }
        else
        {
            if ((adjacentPiecesInRow(cell, player) + 1) % 3 == 0)
            {
                count += (adjacentPiecesInRow(cell, player) + 1) / 3;
                if (count > 0)
                {
                    List<CellHandler> AllCellsOfCombinationOnLeft = getAdjacentPiecesOnLeft(cell, player);
                    List<CellHandler> AllCellsOfCombinationOnRight = getAdjacentPiecesOnRight(cell, player);
                    if (AllCellsOfCombinationOnLeft.Count > 0)
                    {
                        foreach (CellHandler temp in AllCellsOfCombinationOnLeft)
                        {
                            count += checkForNumberOfCombinationsInOneDirection(temp, player, true);
                        }
                    }
                    if (AllCellsOfCombinationOnRight.Count > 0)
                    {
                        foreach (CellHandler temp in AllCellsOfCombinationOnRight)
                        {
                            count += checkForNumberOfCombinationsInOneDirection(temp, player, true);
                        }
                    }
                }
            }
        }
        Debug.Log("Combinations count from function is :" + count);

        return count;
    }

    public int checkForNumberOfCombinations(CellHandler cell, PlayerHandler player)
    {
        int count = 0;
        count += checkForNumberOfCombinationsInOneDirection(cell, player, true);
        count += checkForNumberOfCombinationsInOneDirection(cell, player, false);
        //if ((adjacentPiecesInColumn(cell, player) + 1) % 3 == 0)
        //{
        //    count += (adjacentPiecesInColumn(cell, player) + 1) / 3;
        //    List<CellHandler> AllCellsOfCombinationOnBottom = getAdjacentPiecesOnBottom(cell, player);
        //    List<CellHandler> AllCellsOfCombinationOnTop = getAdjacentPiecesOnTop(cell, player);
        //    //foreach (CellHandler temp in AllCellsOfCombinationOnBottom)
        //    //{
        //    //    if ((adjacentPiecesInRow(temp, player) + 1) % 3 == 0)
        //    //    {
        //    //        count += (adjacentPiecesInRow(temp, player) + 1 % 3);
                    
        //    //    }
        //    //}

        //    //foreach (CellHandler temp in AllCellsOfCombinationOnTop)
        //    //{
        //    //    if ((adjacentPiecesInRow(temp, player) + 1) % 3 == 0)
        //    //    {
        //    //        count += (adjacentPiecesInRow(temp, player) + 1 % 3);
        //    //    }
        //    //}
        //}
        //if ((adjacentPiecesInRow(cell, player) + 1) % 3 == 0)
        //{
        //    count += (adjacentPiecesInRow(cell, player) + 1) / 3;
        //    List<CellHandler> AllCellsOfCombinationOnLeft = getAdjacentPiecesOnLeft(cell, player);
        //    List<CellHandler> AllCellsOfCombinationOnRight = getAdjacentPiecesOnRight(cell, player);
        //    //foreach (CellHandler temp in AllCellsOfCombinationOnLeft)
        //    //{
        //    //    count += checkForNumberOfCombinations(temp, player);
        //    //}

        //    //foreach (CellHandler temp in AllCellsOfCombinationOnRight)
        //    //{
        //    //    count += checkForNumberOfCombinations(temp, player);
        //    //}
        //}
        //Debug.Log("Total Count of combination is: " + count);
        return count;
    }

    // Debuging Code
    #region Debuging_Functions
    public void checkSingleCellCombination(CellHandler cell, PlayerHandler player)
    {
        //Debug.Log("Adjacent Pieces above : " + adjacentPiecesOnTop(cell, player));
        //Debug.Log("Adjacent Pieces below : " + adjacentPiecesOnBottom(cell, player));
        //Debug.Log("Adjacent Pieces right : " + adjacentPiecesOnRight(cell, player));
        //Debug.Log("Adjacent Pieces Left : " + adjacentPiecesOnLeft(cell, player));

    }
    #endregion
}
