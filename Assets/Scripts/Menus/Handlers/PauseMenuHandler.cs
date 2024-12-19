using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickReturn()
    {
        this.gameObject.SetActive(false);
    }

    public void onClickSurrender()
    {
        SceneManager.Instance.isGameplay = false;
        GameManager.Instance.OnClickSurrender();
        this.gameObject.SetActive(false);
    }

    public void onClickHome()
    {
        SceneManager.Instance.isGameplay = false;
        GameManager.Instance.OnClickSurrender();
        this.gameObject.SetActive(false);
    }
}
