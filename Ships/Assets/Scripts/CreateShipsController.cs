using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateShipsController : MonoBehaviour {

    public Text[] buttonList;

    public Button yourButton;

    public Text textBox;

    private string textBox_text;

    private GameInfo gameInfo;

    //public CreateShipsController shipsController;

    private string info;

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(buttonNext);
        btn.interactable = false;
    }

    private void Awake()
    {
        setShipControllerReferenceOnButtons();

        textBox.text = "Place one battleship (size: 4)";


    }

    void setShipControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetShipControllerReference(this, i);
        }
    }

    public void SetGameInfoReference(GameInfo info)
    {
        gameInfo = info;
    }

    public void buttonNext()
    {
        SceneManager.LoadScene("board_main");
    }

    public void placeShip(int buttonID)
    {
        if (gameInfo.battleshipPlaced == false)
        {
            info = "B" + gameInfo.battleship;

            gameInfo.playerBoard[buttonID] = "battleship";
            gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
            if (gameInfo.getBattleshipSize() == 0)
            {
                gameInfo.setBattleship(gameInfo.battleship - 1);

                if (gameInfo.battleship == 0)
                {
                    gameInfo.battleshipPlaced = true;
                    textBox.text = "Place two cruisers (size: 3)";
                }
            }
        }

        else if (gameInfo.cruiserPlaced == false)
        {
            info = "Cr" + gameInfo.cruiser;

            gameInfo.playerBoard[buttonID] = "cruiser";
            gameInfo.cruiserSize--;
            if (gameInfo.cruiserSize == 0)
            {
                gameInfo.cruiser--;
                if (gameInfo.cruiser != 0)
                {
                    gameInfo.cruiserSize = 3;
                }
                if (gameInfo.cruiser == 0)
                {
                    gameInfo.cruiserPlaced = true;
                    textBox.text = "Place three submarines (size: 2)";
                }
            }
        }

        else if (gameInfo.submarinePlaced == false)
        {
            info = "S" + gameInfo.submarine;

            gameInfo.playerBoard[buttonID] = "submarine";
            gameInfo.submarineSize--;
            if (gameInfo.submarineSize == 0)
            {
                gameInfo.submarine--;
                if (gameInfo.submarine != 0)
                {
                    gameInfo.submarineSize = 2;
                }
                if (gameInfo.submarine == 0)
                {
                    gameInfo.submarinePlaced = true;
                    textBox.text = "Place four destroyers (size: 1)";
                }
            }
        }

        else if (gameInfo.destroyerPlaced == false)
        {
            info = "D" + gameInfo.destroyer;

            gameInfo.playerBoard[buttonID] = "destroyer" + gameInfo.destroyer;
            gameInfo.destroyerSize--;
            if (gameInfo.destroyerSize == 0)
            {
                gameInfo.destroyer--;
                if (gameInfo.destroyer != 0)
                {
                    gameInfo.destroyerSize = 1;
                }
                if (gameInfo.destroyer == 0)
                {
                    gameInfo.destroyerPlaced = true;
                    textBox.text = "You've placed all ships!";
                    for (int i = 0; i < buttonList.Length; i++)
                    {
                        buttonList[i].GetComponentInParent<Button>().interactable = false;
                    }
                    yourButton.interactable = true;
                }
            }
        }

        else
        {

            //SceneManager.LoadScene("board_main", LoadSceneMode.Additive);
        }
    }

    public string getInfo()
    {
        return info;
    }
}
