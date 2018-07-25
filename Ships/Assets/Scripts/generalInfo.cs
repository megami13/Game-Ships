using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalInfo : MonoBehaviour {

    public static generalInfo instance;

    public ButtonLoginRegister loginController;
    public ButtonMenu menuController;
    public ScoreMenu scoreController;
    public GameController gameController;

    private string username;
    private int score;

    string ScoreURL = "http://localhost/Ships/Score.php";

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
    }

    public void ChangeScore(int newScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", this.username);
        form.AddField("scorePost", newScore);

        WWW www = new WWW(ScoreURL, form);
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
}
