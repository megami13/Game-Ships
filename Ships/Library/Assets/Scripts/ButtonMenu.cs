using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour {

    public GeneralInfo GeneralInfo;

    public Button butonStart, buttonScore, buttonQuit;

    int score;

    private void Awake()
    {
        GeneralInfo = (GeneralInfo)FindObjectOfType(typeof(GeneralInfo));
        score = GeneralInfo.getScore();
    }

    private void Update()
    {
        score = GeneralInfo.getScore();
        //GeneralInfo.setScore(score);
        //text.text = Convert.ToString(score);
    }

    public void startGame()
    {
        SceneManager.LoadScene("place_ships");
        //int score = GeneralInfo.getScore();
        //GeneralInfo.setScore(score + 1);
        //GeneralInfo.ChangeScore(score + 1);
        //Debug.Log(GeneralInfo.getScore());
    }

    public void viewScore()
    {
        SceneManager.LoadScene("scoreScreen");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void SetGeneralInfoReference(GeneralInfo info)
    {
        GeneralInfo = info;
    }
}
