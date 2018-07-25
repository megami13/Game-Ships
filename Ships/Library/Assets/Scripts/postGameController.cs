using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class postGameController : MonoBehaviour {

    public GeneralInfo GeneralInfo;

    public Text text;
    public Button buttonReturn;

    private void Start()
    {
        if (GeneralInfo.getWin())
            text.text = "You won!";
        else
            text.text = "You lost!";
    }

    private void Awake()
    {
        GeneralInfo = (GeneralInfo)FindObjectOfType(typeof(GeneralInfo));
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    internal void SetGeneralInfoReference(GeneralInfo info)
    {
        GeneralInfo = info;
    }
}
