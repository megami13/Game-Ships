using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour {

    public generalInfo generalInfo;

    public Button butonStart, buttonScore, buttonQuit;

    private void Awake()
    {
        generalInfo = (generalInfo)FindObjectOfType(typeof(generalInfo));
    }

    public void startGame()
    {
        SceneManager.LoadScene("place_ships");
    }

    public void viewScore()
    {
        Debug.Log(generalInfo.getScore());
        generalInfo.ChangeScore(5);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void SetGeneralInfoReference(generalInfo info)
    {
        generalInfo = info;
    }
}
