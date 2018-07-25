using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour {

    public GeneralInfo GeneralInfo;
    public Text text;
    int score;
    public Button returnToMenuB;

    internal void SetGeneralInfoReference(GeneralInfo info)
    {
        GeneralInfo = info;
    }

    private void Awake()
    {
        GeneralInfo = (GeneralInfo)FindObjectOfType(typeof(GeneralInfo));
    }

    private void Update()
    {
        score = GeneralInfo.getScore();
        text.text = Convert.ToString(score);
    }

    private void Start()
    {
        if (GeneralInfo.getUsername() == "user")
        {
            score = GeneralInfo.getScore();
            text.text = Convert.ToString(score);
        }
        else
        {
            StartCoroutine(GeneralInfo.getScoreFromSql());
            score = GeneralInfo.getScore();
            text.text = Convert.ToString(score);
        }
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
