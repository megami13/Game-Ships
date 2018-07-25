using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLoginRegister : MonoBehaviour {

    public generalInfo generalInfo;

    public Button buttonLogin, buttonRegister;

    public InputField inputUserNameLogin;
    public InputField inputPasswordLogin;

    public InputField inputUserNameRegister;
    public InputField inputPasswordRegister;

    public Text info;

    string LoginURL = "http://localhost/Ships/Login.php";
    string CreateUserURL = "http://localhost/Ships/InsertUser.php";
    string ScoreURL = "http://localhost/Ships/SetScore.php";

    private void Awake()
    {
        generalInfo = (generalInfo)FindObjectOfType(typeof(generalInfo));
    }

    public void Login()
    {
        StartCoroutine(LoginToDB());
    }

    public void Register()
    {
        StartCoroutine(CreateUser());
        SetScore(inputUserNameRegister.text, 0);
    }

    internal void SetGeneralInfoReference(generalInfo info)
    {
        generalInfo = info;
    }

    public IEnumerator LoginToDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", inputUserNameLogin.text);
        form.AddField("passwordPost", inputPasswordLogin.text);

        WWW www = new WWW(LoginURL, form);

        yield return www;

        info.text = www.text;
        generalInfo.setUsername(inputUserNameLogin.text);
        //Debug.Log(www.text);

        if (info.text == "login success")
        {
            generalInfo.setUsername(inputUserNameLogin.text);
            Debug.Log(generalInfo.getUsername());
            SceneManager.LoadScene("menu");
        }
    }

    public IEnumerator CreateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", inputUserNameRegister.text);
        form.AddField("passwordPost", inputPasswordRegister.text);

        WWW www = new WWW(CreateUserURL, form);

        yield return www;

        info.text = www.text;
        Debug.Log(www.text);
    }

    public void SetScore(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("scorePost", score);

        WWW www = new WWW(ScoreURL, form);
    }
}
