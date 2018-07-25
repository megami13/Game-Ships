using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class ButtonLoginRegister : MonoBehaviour {

    public GeneralInfo GeneralInfo;

    public Button buttonLogin, buttonRegister;

    public InputField inputUserNameLogin;
    public InputField inputPasswordLogin;

    public InputField inputUserNameRegister;
    public InputField inputPasswordRegister;

    public string passwordToEdit = "";

    public Text info;

    string LoginURL = "http://localhost/Ships/Login.php";
    string CreateUserURL = "http://localhost/Ships/InsertUser.php";
    string ScoreURL = "http://localhost/Ships/SetScore.php";
    string FindScoreURL = "http://localhost/Ships/FindScore.php";

    void OnGUI()
    {
        //passwordToEdit = GUI.PasswordField(new Rect(140, 271, 160, 30), passwordToEdit, "*"[0], 25);
    }

    private void Awake()
    {
        GeneralInfo = (GeneralInfo)FindObjectOfType(typeof(GeneralInfo));
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

    internal void SetGeneralInfoReference(GeneralInfo info)
    {
        GeneralInfo = info;
    }

    public IEnumerator LoginToDB()
    {
        string pass = getHashSha256(inputPasswordLogin.text); //inputPasswordLogin.text);
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", inputUserNameLogin.text);
        form.AddField("passwordPost", pass);

        WWW www = new WWW(LoginURL, form);

        yield return www;

        info.text = www.text;
        GeneralInfo.setUsername(inputUserNameLogin.text);
        //Debug.Log(www.text);

        if (inputUserNameLogin.text == "user" && inputPasswordLogin.text == "password")
        {
            info.text = "login success";
            GeneralInfo.setUsername(inputUserNameLogin.text);
            Debug.Log(GeneralInfo.getUsername());
            GeneralInfo.setScore(0);
            //StartCoroutine(getScoreFromSql());
            //getScoreFromSql();
            SceneManager.LoadScene("menu");
        }

        if (info.text == "login success")
        {
            GeneralInfo.setUsername(inputUserNameLogin.text);
            Debug.Log(GeneralInfo.getUsername());
            //StartCoroutine(getScoreFromSql());
            //getScoreFromSql();
            SceneManager.LoadScene("menu");
        }
    }

    public IEnumerator CreateUser()
    {
        string pass = getHashSha256(inputPasswordRegister.text);
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", inputUserNameRegister.text);
        form.AddField("passwordPost", pass);

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

    public static string getHashSha256(string text)
    {
        SHA256 sha256 = SHA256Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] hash = sha256.ComputeHash(bytes);

        return GetStringFromHash(hash);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }
        return result.ToString();
    }
}
