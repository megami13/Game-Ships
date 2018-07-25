using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text[] buttonList;

    //public Text textBox;

    private string playerSide;

    string info;

    public static GameController instance;

    public GameInfo gameInfo;
    public generalInfo generalInfo;

    public Text textBox;
    public Text textBoxBattleship, textBoxCruiser, textBoxSubmarine, textBoxDestroyer;

    //public CreateShipsController shipController;

    private string[,] pcBoard = new string[10, 10];
    private string[] playerBoard = new string[100];

    private string[] used = new string[100];

    private int 
        pcBattleship = 1, 
        pcCruiser = 2, 
        pcSubmarine = 3, 
        pcDestroyer = 4,

        pcBattleshipSize = 4,
 
        pcCruiser1Size = 3, pcCruiser2Size = 3,
        pcSubmarine1Size = 2, pcSubmarine2Size = 2, pcSubmarine3Size = 2,
        pcDestroyer1Size = 1, pcDestroyer2Size = 1, pcDestroyer3Size = 1, pcDestroyer4Size = 1,

        playerBattleship = 1,
        playerCruiser = 2,
        playerSubmarine = 3,
        playerDestroyer = 4,

        playerBattleshipSize = 4,

        playerCruiser1Size = 3, playerCruiser2Size = 3,
        playerSubmarine1Size = 2, playerSubmarine2Size = 2, playerSubmarine3Size = 2,
        playerDestroyer1Size = 1, playerDestroyer2Size = 1, playerDestroyer3Size = 1, playerDestroyer4Size = 1;

    private bool 
        pcBattleshipAlive = true,
        pcCruiser1Alive = true, pcCruiser2Alive = true,
        pcSubmarine1Alive = true, pcSubmarine2Alive = true, pcSubmarine3Alive = true,
        pcDestroyer1Alive = true, pcDestroyer2Alive = true, pcDestroyer3Alive = true, pcDestroyer4Alive = true,

        playerBattleshipAlive = true,
        playerCruiser1Alive = true, playerCruiser2Alive = true,
        playerSubmarine1Alive = true, playerSubmarine2Alive = true, playerSubmarine3Alive = true,
        playerDestroyer1Alive = true, playerDestroyer2Alive = true, playerDestroyer3Alive = true, playerDestroyer4Alive = true;

    private void Awake()
    {
        setGameControllerReferenceOnButtons();
        gameInfo = (GameInfo)FindObjectOfType(typeof(GameInfo));
        generalInfo = (generalInfo)FindObjectOfType(typeof(generalInfo));
        playerSide = "player";
        textBox.text = "";

        textBoxBattleship.text = "Battleship: " + pcBattleship;
        textBoxCruiser.text = "Cruiser: " + pcCruiser;
        textBoxSubmarine.text = "Submarine: " + pcSubmarine;
        textBoxDestroyer.text = "Destroyer: " + pcDestroyer;

        this.pcBoard = gameInfo.pcBoard;
        this.playerBoard = gameInfo.playerBoard;
    }

    void setGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this, i);
        }
    }

    public void SetGameInfoReference(GameInfo info)
    {
        gameInfo = info;
    }

    public void SetGeneralInfoReference(generalInfo info)
    {
        generalInfo = info;
    }

    public string GetPlayerSide()
    {
        return info;
    }

    public void EndTurn(int buttonID)
    {
        int x = buttonID - (buttonID / 10) * 10;
        int y = buttonID / 10;

        if (pcBoard[x, y] == "0")
        {
            info = "";
            textBox.text = "Miss";
        }

        else if (pcBoard[x, y] == "battleship" + "0")
        {
            info = "X";
            textBox.text = "You hit an enemy ship!";
            pcBattleshipSize--;

            if (pcBattleshipSize == 0)
            {
                pcBattleship--;
                if (pcBattleship == 0)
                {
                    textBoxBattleship.text = "Battleship: " + pcBattleship;
                    textBox.text = "Enemy ship is down!";
                    pcBattleshipAlive = false;
                }
            }
        }
        else if (pcBoard[x, y].Contains("cruiser"))
        {
            if (pcBoard[x, y].Contains("0"))
            {
                checkShip("cruiser", ref pcCruiser1Size, ref pcCruiser, ref pcCruiser1Alive, "X", textBoxCruiser, textBox);
            }
            else if (pcBoard[x, y].Contains("1"))
            {
                checkShip("cruiser", ref pcCruiser2Size, ref pcCruiser, ref pcCruiser2Alive, "X", textBoxCruiser, textBox);
            }
        }
        else if (pcBoard[x, y].Contains("submarine"))
        {
            if (pcBoard[x, y].Contains("0"))
            {
                checkShip("submarine", ref pcSubmarine1Size, ref pcSubmarine, ref pcSubmarine1Alive, "X", textBoxSubmarine, textBox);
            }
            else if (pcBoard[x, y].Contains("1"))
            {
                checkShip("submarine", ref pcSubmarine2Size, ref pcSubmarine, ref pcSubmarine2Alive, "X", textBoxSubmarine, textBox);
            }
            else if (pcBoard[x, y].Contains("2"))
            {
                checkShip("submarine", ref pcSubmarine3Size, ref pcSubmarine, ref pcSubmarine3Alive, "X", textBoxSubmarine, textBox);
            }
        }
        else if (pcBoard[x, y].Contains("destroyer"))
        {
            if (pcBoard[x, y].Contains("0"))
            {
                checkShip("destroyer", ref pcDestroyer1Size, ref pcDestroyer, ref pcDestroyer1Alive, "X", textBoxDestroyer, textBox);
            }
            else if (pcBoard[x, y].Contains("1"))
            {
                checkShip("destroyer", ref pcDestroyer2Size, ref pcDestroyer, ref pcDestroyer2Alive, "X", textBoxDestroyer, textBox);
            }
            if (pcBoard[x, y].Contains("2"))
            {
                checkShip("destroyer", ref pcDestroyer3Size, ref pcDestroyer, ref pcDestroyer3Alive, "X", textBoxDestroyer, textBox);
            }
            if (pcBoard[x, y].Contains("3"))
            {
                checkShip("destroyer", ref pcDestroyer4Size, ref pcDestroyer, ref pcDestroyer4Alive, "X", textBoxDestroyer, textBox);
            }
        }


        if (!pcBattleshipAlive && !pcCruiser1Alive && !pcCruiser2Alive && !pcSubmarine1Alive && !pcSubmarine2Alive &&
            !pcSubmarine3Alive && !pcDestroyer1Alive && !pcDestroyer2Alive && !pcDestroyer3Alive && !pcDestroyer4Alive)
        {
            GameOver();
        }

        ChangeSides();
        if (playerSide == "pc")
        {
            PcTurn();
        }
    }

    void checkShip(string shipType, ref int shipSize, ref int shipCount, ref bool alive, string info_s, Text textBox1, Text textBox)
    {
        info = info_s;
        textBox.text = "You hit an enemy ship!";
        shipSize--;

        if (shipSize == 0)
        {
            shipCount--;
            textBox1.text = shipType + ": " + shipCount;
            textBox.text = "Enemy ship is down!";
            alive = false;

            if (shipCount == 0)
            {
                //alive = false;
            }
        }
    }

    void checkShipPlayer(string shipType, ref int shipSize, ref int shipCount, ref bool alive)
    {
        shipSize--;

        if (shipSize == 0)
        {
            shipCount--;
            alive = false;

            if (shipCount == 0)
            {
                //alive = false;
            }
        }
    }

    void GameOver()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }

        int score = generalInfo.getScore();

        if (playerSide == "player")
        {
            score++;
            generalInfo.setScore(score);
            generalInfo.ChangeScore(score);
            Debug.Log(generalInfo.getScore());
        }
        else
        {
            generalInfo.setScore(score - 1);
            generalInfo.ChangeScore(score - 1);
            Debug.Log(generalInfo.getScore());
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "player") ? "pc" : "player";
    }

    void PcTurn()
    {
        System.Random rnd = new System.Random();
        int n;
        List<string> list = new List<string>(used);
        do
        {
            n = rnd.Next(0, 100);
        } while (list.Contains(Convert.ToString(n)));
        
        //Debug.Log("Index: " + n);

        if (playerBoard[n] == "battleship" + "0")
        {
            playerBattleshipSize--;

            if (playerBattleshipSize == 0)
            {
                playerBattleship--;
                if (playerBattleship == 0)
                {
                    playerBattleshipAlive = false;
                }
            }
        }
        else if (playerBoard[n].Contains("cruiser"))
        {
            if (playerBoard[n].Contains("0"))
            {
                checkShipPlayer("cruiser", ref playerCruiser1Size, ref playerCruiser, ref playerCruiser1Alive);
            }
            else if (playerBoard[n].Contains("1"))
            {
                checkShipPlayer("cruiser", ref playerCruiser2Size, ref playerCruiser, ref playerCruiser2Alive);
            }
        }
        else if (playerBoard[n].Contains("submarine"))
        {
            if (playerBoard[n].Contains("0"))
            {
                checkShipPlayer("submarine", ref playerSubmarine1Size, ref playerSubmarine, ref playerSubmarine1Alive);
            }
            else if (playerBoard[n].Contains("1"))
            {
                checkShipPlayer("submarine", ref playerSubmarine2Size, ref playerSubmarine, ref playerSubmarine2Alive);
            }
            else if (playerBoard[n].Contains("2"))
            {
                checkShipPlayer("submarine", ref playerSubmarine3Size, ref playerSubmarine, ref playerSubmarine3Alive);
            }
        }
        else if (playerBoard[n].Contains("destroyer"))
        {
            if (playerBoard[n].Contains("0"))
            {
                checkShipPlayer("destroyer", ref playerDestroyer1Size, ref playerDestroyer, ref playerDestroyer1Alive);
            }
            else if (playerBoard[n].Contains("1"))
            {
                checkShipPlayer("destroyer", ref playerDestroyer2Size, ref playerDestroyer, ref playerDestroyer2Alive);
            }
            if (playerBoard[n].Contains("2"))
            {
                checkShipPlayer("destroyer", ref playerDestroyer3Size, ref playerDestroyer, ref playerDestroyer3Alive);
            }
            if (playerBoard[n].Contains("3"))
            {
                checkShipPlayer("destroyer", ref playerDestroyer4Size, ref playerDestroyer, ref playerDestroyer4Alive);
            }
        }

        if (!playerBattleshipAlive && !playerCruiser1Alive && !playerCruiser2Alive && !playerSubmarine1Alive && !playerSubmarine2Alive &&
            !playerSubmarine3Alive && !playerDestroyer1Alive && !playerDestroyer2Alive && !playerDestroyer3Alive && !playerDestroyer4Alive)
        {
            GameOver();
        }


        ChangeSides();
    }
}
