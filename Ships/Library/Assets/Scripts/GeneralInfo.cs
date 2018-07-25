using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInfo : MonoBehaviour {

    public static GeneralInfo instance;

    public ButtonLoginRegister loginController;
    public ButtonMenu menuController;
    public ScoreMenu scoreController;
    public GameController gameController;
    public postGameController PostGameController;

    private string username;
    private int score;

    private bool gameWon;

    string ScoreURL = "http://localhost/Ships/Score.php";
    string FindScoreURL = "http://localhost/Ships/FindScore.php";

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (GameObject.Find("Login Controller") != null)
        {
            loginController.GetComponent<ButtonLoginRegister>().SetGeneralInfoReference(this);
        }

        else if (GameObject.Find("Menu Controller") != null)
        {
            menuController.GetComponent<ButtonMenu>().SetGeneralInfoReference(this);
        }

        else if (GameObject.Find("Score Controller") != null)
        {
            scoreController.GetComponent<ScoreMenu>().SetGeneralInfoReference(this);
        }
        else if (GameObject.Find("Game Controller") != null)
        {
            gameController.GetComponent<GameController>().SetGeneralInfoReference(this);
        }
        else if (GameObject.Find("Post Game Controller") != null)
        {
            PostGameController.GetComponent<postGameController>().SetGeneralInfoReference(this);
        }
    }

    public void ChangeScore(int newScore)
    {
        if (username == "user")
        {
            score = newScore;
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("usernamePost", this.username);
            form.AddField("scorePost", newScore);

            WWW www = new WWW(ScoreURL, form);
        }
    }
    
    public IEnumerator getScoreFromSql()
    {
        if (username == "user")
        {

        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("usernamePost", this.username);

            WWW www = new WWW(FindScoreURL, form);

            yield return www;

            score = Convert.ToInt32(www.text);
            Debug.Log(score);
        }
    }

    public string getUsername()
    {
        return username;
    }

    public void setUsername(string username)
    {
        this.username = username; 
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public void setWin(bool win)
    {
        this.gameWon = win;
    }

    public bool getWin()
    {
        return gameWon;
    }
}
