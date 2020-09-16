using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public GameObject EndGameCanvas;
    public Text WinText;

    public void GetLoser(GameTypes.Team team)
    {
        EndGameCanvas.SetActive(true);
        //team is the losing team
        switch (team)
        {
            case GameTypes.Team.Blue:
                WinText.text = "Red Team Win!";
                break;
            case GameTypes.Team.Red:
                WinText.text = "Blue Team Win!";
                break;
            default:
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
