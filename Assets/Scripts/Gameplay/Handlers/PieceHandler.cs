using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{
    public bool isPlaced = false;
    public PlayerHandler player;
    public CellHandler cell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animate(bool isAnimating)
    {
        GetComponent<Animator>().SetBool("animate", isAnimating);
    }
}
