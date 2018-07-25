using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInserter : MonoBehaviour {

    public string inputUserName;
    public string inputPassword;

    string CreateUserURL = "http://localhost/Ships/InsertUser.php";

    // Use this for initialization
    void Start () {
        //CreateUser(inputUserName, inputPassword);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CreateUser(inputUserName, inputPassword));
        }
	}

    IEnumerator CreateUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);

        WWW www = new WWW(CreateUserURL, form);

        yield return www;

        Debug.Log(www.text);
    }
}
