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
        //setShipControllerReference();

        //gameInfo = GetComponent<GameInfo>();

        textBox.text = "Place one battleship (size: 4)";


    }

    void setShipControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetShipControllerReference(this, i);
        }
    }

    //void setShipControllerReference()
    //{
    //    gameInfo.GetComponent<GameInfo>().SetShipControllerReference(this);
    //}

    //public void SetGameControllerReference(GameController controller)
    //{
    //    gameController = controller;
    //}

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

            if (gameInfo.getBattleshipSize() == 4) //dodane
            {
                gameInfo.playerBoard[buttonID] = "battleship";
                gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
            }
            //gameInfo.playerBoard[buttonID] = "battleship";
            //gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);

            else if (gameInfo.getBattleshipSize() == 3)
            {
                if (buttonID - 1 >= 0 && gameInfo.playerBoard[buttonID - 1] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else if (buttonID - 10 >= 0 && gameInfo.playerBoard[buttonID - 10] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else if (buttonID + 10 < 100 && gameInfo.playerBoard[buttonID + 10] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else if (buttonID + 1 < 100 && gameInfo.playerBoard[buttonID + 1] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else
                {
                    info = "";
                }

            }

            else if (gameInfo.getBattleshipSize() > 0)
            {
                if (buttonID - 2 > 0 && gameInfo.playerBoard[buttonID - 1] == "battleship" &&
                    gameInfo.playerBoard[buttonID - 2] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else if (buttonID - 20 >= 0 && gameInfo.playerBoard[buttonID - 10] == "battleship" &&
                    gameInfo.playerBoard[buttonID - 20] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else if (buttonID + 20 < 100 && gameInfo.playerBoard[buttonID + 10] == "battleship" &&
                    gameInfo.playerBoard[buttonID + 20] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else if (buttonID + 2 < 100 && gameInfo.playerBoard[buttonID + 1] == "battleship" &&
                    gameInfo.playerBoard[buttonID + 2] == "battleship")
                {
                    gameInfo.playerBoard[buttonID] = "battleship";
                    gameInfo.setBattleshipSize(gameInfo.getBattleshipSize() - 1);
                }
                else
                {
                    info = "";
                }
            }

            if (gameInfo.getBattleshipSize() == 0)
            {
                //gameInfo.battleship--;
                gameInfo.setBattleship(gameInfo.battleship - 1);
                //if (gameInfo.battleship != 0)
                //{
                //    gameInfo.setBattleshipSize(4);
                //}
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
            //info = Convert.ToString(gameInfo.cruiserSize);

            if (gameInfo.cruiserSize == 3) //dodane
            {
                gameInfo.playerBoard[buttonID] = "cruiser";
                gameInfo.cruiserSize--;
            }
            //gameInfo.playerBoard[buttonID] = "cruiser";
            //gameInfo.cruiserSize--;

            else if (gameInfo.cruiserSize == 2)
            {
                if (buttonID - 1 >= 0 && gameInfo.playerBoard[buttonID - 1] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else if (buttonID - 10 >= 0 && gameInfo.playerBoard[buttonID - 10] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else if (buttonID + 10 < 100 && gameInfo.playerBoard[buttonID + 10] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else if (buttonID + 1 < 100 && gameInfo.playerBoard[buttonID + 1] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else
                {
                    info = "";
                }

            }

            else if (gameInfo.cruiserSize > 0)
            {
                if (buttonID - 2 > 0 && gameInfo.playerBoard[buttonID - 1] == "cruiser" &&
                    gameInfo.playerBoard[buttonID - 2] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else if (buttonID - 20 >= 0 && gameInfo.playerBoard[buttonID - 10] == "cruiser" &&
                    gameInfo.playerBoard[buttonID - 20] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else if (buttonID + 20 < 100 && gameInfo.playerBoard[buttonID + 10] == "cruiser" &&
                    gameInfo.playerBoard[buttonID + 20] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else if (buttonID + 2 < 100 && gameInfo.playerBoard[buttonID + 1] == "cruiser" &&
                    gameInfo.playerBoard[buttonID + 2] == "cruiser")
                {
                    gameInfo.playerBoard[buttonID] = "cruiser";
                    gameInfo.cruiserSize--;
                }
                else
                {
                    info = "";
                }
            }


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
            //info = Convert.ToString(gameInfo.submarineSize);

            if (gameInfo.submarineSize == 2) //dodane
            {
                gameInfo.playerBoard[buttonID] = "submarine";
                gameInfo.submarineSize--;
            }

            //gameInfo.playerBoard[buttonID] = "submarine";
            //gameInfo.submarineSize--;

            else if (gameInfo.submarineSize == 1)
            {
                if (buttonID - 1 >= 0 && gameInfo.playerBoard[buttonID - 1] == "submarine")
                {
                    gameInfo.playerBoard[buttonID] = "submarine";
                    gameInfo.submarineSize--;
                }
                else if (buttonID - 10 >= 0 && gameInfo.playerBoard[buttonID - 10] == "submarine")
                {
                    gameInfo.playerBoard[buttonID] = "submarine";
                    gameInfo.submarineSize--;
                }
                else if (buttonID + 10 < 100 && gameInfo.playerBoard[buttonID + 10] == "submarine")
                {
                    gameInfo.playerBoard[buttonID] = "submarine";
                    gameInfo.submarineSize--;
                }
                else if (buttonID + 1 < 100 && gameInfo.playerBoard[buttonID + 1] == "submarine")
                {
                    gameInfo.playerBoard[buttonID] = "submarine";
                    gameInfo.submarineSize--;
                }
                else
                {
                    info = "";
                }

            }

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
            //info = Convert.ToString(gameInfo.destroyerSize);

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
