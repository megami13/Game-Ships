using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

    public static GameInfo instance;

    public CreateShipsController shipController;
    public GameController gameController;

    public string[,] pcBoard = new string[10,10];
    public string[] playerBoard = new string[100];

    public int 
        battleship, 
        cruiser,
        submarine,
        destroyer;

    public static int battleshipSize = 4;
    public int 
        cruiserSize = 3,
        submarineSize,
        destroyerSize = 1;

    public bool 
        battleshipPlaced = false,
        cruiserPlaced = false,
        submarinePlaced = false,
        destroyerPlaced = false;

    public int pcBattleship = 2,
        pcCruiser = 3,
        pcSubmarine = 4,
        pcDestroyer = 5;

    public int pcBattleshipSize = 4,
        pcCruiserSize = 3,
        pcSubmarineSize = 2,
        pcDestroyerSize = 1;

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

        if (GameObject.Find("Create Ships Controller") != null)
        {
            shipController.GetComponent<CreateShipsController>().SetGameInfoReference(this);
        }
        
        else if (GameObject.Find("Game Controller") != null)
        {
            gameController.GetComponent<GameController>().SetGameInfoReference(this);
        }

        battleship = 1;
        cruiser = 2;
        destroyer = 4;
        submarine = 3;
        submarineSize = 2;

        pcBattleship = 1;
        //carrier = 1;
        //carrier_size = 4;
        //carrier_placed = false;

        //pc_carrier = 1;
        //pc_carrier_size = 4;
        //pc_carrier_placed = false;
        //shipController = GetComponent<CreateShipsController>();

        for (int k = 0; k < 100; k++)
        {
            playerBoard[k] = "0";
        }

        for (int k = 0; k < 10; k++)
        {
            for (int l = 0; l < 10; l++)
            {
                pcBoard[k, l] = "0";
            }
        }
         
        setPCBoard();

        //for (int i = 0; i < 10; i++)
        //{
        //    for (int j = 0; j < 10; j++)
        //    {
        //        Debug.Log(pcBoard[i, j]);
        //    }
        //    Debug.Log("");
        //}
    }

    public static GameInfo GetInstance() { return instance; }

    public void setBattleshipSizePC(int battleshipID, int size)
    {
        //if ()
    }

    public void setBattleship(int ship)
    {
        battleship = ship;
    }

    //public int getCarrierSize()
    //{
    //    return carrier_size;
    //}

    public void Update()
    {
        //if (GameObject.Find("Game Controller") != null)
        //{
        //    gameController.GetComponent<GameController>().SetGameInfoReference(this);
        //}
        gameController = (GameController)FindObjectOfType(typeof(GameController));
    }

    public void setBattleshipSize(int size)
    {
        battleshipSize = size;
    }

    public int getBattleshipSize()
    {
        return battleshipSize;
    }

    public void SetShipControllerReference(CreateShipsController controller)
    {
        shipController = controller;
    }

    //public void SetGameInfoReference(CreateShipsController controller)
    //{
    //    shipController = controller;
    //}

    public void setPCBoard()
    {
        bool isBattleshipPlaced = false, isCruiserPlaced = false, isSubmarinePlaced = false, isDestroyerPlaced = false;
        for (int i = 0; i < pcBattleship; i++)
        {
            do
            {
                createShipPC("battleship", pcBattleshipSize, i, ref isBattleshipPlaced);
            } while (isBattleshipPlaced == false);
            //Debug.Log("Battleship " + i + " placed");
        }
        for (int j = 0; j < pcCruiser; j++)
        {
            do
            {
                createShipPC("cruiser", pcCruiserSize, j, ref isCruiserPlaced);
            } while (isCruiserPlaced == false);
            //Debug.Log("Cruiser " + j + " placed");
        }
        for (int k = 0; k < pcSubmarine; k++)
        {
            do
            {
                createShipPC("submarine", pcSubmarineSize, k, ref isSubmarinePlaced);
            } while (isSubmarinePlaced == false);
            //Debug.Log("Submarine " + k + " placed");
        }
        for (int l = 0; l < pcDestroyer; l++)
        {
            do
            {
                createShipPC("destroyer", pcDestroyerSize, l, ref isDestroyerPlaced);
            } while (isDestroyerPlaced == false);
            //Debug.Log("Destroyer " + l + " placed");
        }
    }

    public void createShipPC(string shipType, int shipSize, int ship, ref bool isPlaced)
    {
        System.Random rnd = new System.Random();
        int rnd_x = rnd.Next(0, 10);
        int rnd_y = rnd.Next(0, 10);
        //int rnd_x = UnityEngine.Random.Range(0, 10);
        //int rnd_y = UnityEngine.Random.Range(0, 10);
        bool canPlace = true;

        if ((rnd_x + shipSize) < 10 && (rnd_y + shipSize) < 10)
        {
            int rnd_direction = rnd.Next(0, 2);
            if (rnd_direction == 0) // vertical
            {
                for (int j = 0; j < shipSize; j++)
                {
                    if (pcBoard[rnd_x + j, rnd_y] != "0")
                    {
                        canPlace = false;
                    }
                }

                // check area around
                for (int k = 0; k < shipSize; k++)
                {
                    if (rnd_y + 1 < 10)
                    {
                        if (pcBoard[rnd_x + k, rnd_y + 1] != "0")
                            canPlace = false;
                    }
                    if (rnd_y - 1 >= 0)
                    {
                        if (pcBoard[rnd_x + k, rnd_y - 1] != "0")
                            canPlace = false;
                    }
                }
                if (rnd_x - 1 >= 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y] != "0")
                        canPlace = false;
                }
                if (rnd_x + shipSize  < 10)
                {
                    if (pcBoard[rnd_x + shipSize , rnd_y] != "0")
                        canPlace = false;
                }

                if (rnd_x + shipSize  < 10 && rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x + shipSize , rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_x + shipSize < 10 && rnd_y - 1 > 0)
                {
                    if (pcBoard[rnd_x + shipSize , rnd_y - 1] != "0")
                        canPlace = false;
                }
                if (rnd_x - 1 > 0 && rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x - 1, rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_x - 1 > 0 && rnd_y - 1 > 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y - 1] != "0")
                        canPlace = false;
                }

                if (canPlace)
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        pcBoard[rnd_x + i, rnd_y] = shipType + ship;
                    }
                    isPlaced = true;
                }
                else
                {
                    //Debug.Log("createShipPC");
                    isPlaced = false;
                }
            }
            else if (rnd_direction == 1) // diagonal
            {
                for (int j = 0; j < shipSize; j++)
                {
                    if (pcBoard[rnd_x, rnd_y + j] != "0")
                    {
                        canPlace = false;
                    }
                }

                // check area around
                for (int k = 0; k < shipSize; k++)
                {
                    if (rnd_x + 1 < 10)
                    {
                        if (pcBoard[rnd_x + 1, rnd_y + k] != "0")
                            canPlace = false;
                    }
                    if (rnd_x - 1 >= 0)
                    {
                        if (pcBoard[rnd_x - 1, rnd_y + k] != "0")
                            canPlace = false;
                    }
                }

                if (rnd_y - 1 >= 0)
                {
                    if (pcBoard[rnd_x, rnd_y - 1] != "0")
                        canPlace = false;
                }
                if (rnd_y + shipSize < 10)
                {
                    if (pcBoard[rnd_x, rnd_y + shipSize] != "0")
                        canPlace = false;
                }

                if (rnd_y + shipSize < 10 && rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y + shipSize] != "0")
                        canPlace = false;
                }
                if (rnd_y + shipSize < 10 && rnd_x - 1 > 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y + shipSize] != "0")
                        canPlace = false;
                }
                if (rnd_y - 1 > 0 && rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y - 1] != "0")
                        canPlace = false;
                }
                if (rnd_x - 1 > 0 && rnd_y - 1 > 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y - 1] != "0")
                        canPlace = false;
                }

                if (canPlace)
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        pcBoard[rnd_x, rnd_y + i] = shipType + ship;
                    }
                    isPlaced = true;
                }
                else
                {
                    //Debug.Log("createShipPC");
                    isPlaced = false;
                }
            }
        }
        else if ((rnd_x + shipSize) < 10)
        {
            for (int i = 0; i < shipSize; i++)
            {
                if (pcBoard[rnd_x + i, rnd_y] != "0")
                {
                    canPlace = false;
                }
            }

            // check area around
            for (int k = 0; k < shipSize; k++)
            {
                if (rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x + k, rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_y - 1 >= 0)
                {
                    if (pcBoard[rnd_x + k, rnd_y - 1] != "0")
                        canPlace = false;
                }
            }
            if (rnd_x - 1 >= 0)
            {
                if (pcBoard[rnd_x - 1, rnd_y] != "0")
                    canPlace = false;
            }
            if (rnd_x + shipSize  < 10)
            {
                if (pcBoard[rnd_x + shipSize , rnd_y] != "0")
                    canPlace = false;
            }

            if (rnd_x + shipSize  < 10 && rnd_y + 1 < 10)
            {
                if (pcBoard[rnd_x + shipSize , rnd_y + 1] != "0")
                    canPlace = false;
            }
            if (rnd_x + shipSize  < 10 && rnd_y - 1 > 0)
            {
                if (pcBoard[rnd_x + shipSize , rnd_y - 1] != "0")
                    canPlace = false;
            }
            if (rnd_x - 1 > 0 && rnd_y + 1 < 10)
            {
                if (pcBoard[rnd_x - 1, rnd_y + 1] != "0")
                    canPlace = false;
            }
            if (rnd_x - 1 > 0 && rnd_y - 1 > 0)
            {
                if (pcBoard[rnd_x - 1, rnd_y - 1] != "0")
                    canPlace = false;
            }


            if (canPlace)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    pcBoard[rnd_x + i, rnd_y] = shipType + ship;
                    isPlaced = true;
                }
            }
            else
                isPlaced = false;
        }
        else if ((rnd_y + shipSize) < 10) // diagonal
        {
            for (int i = 0; i < shipSize; i++)
            {
                if (pcBoard[rnd_x, rnd_y + i] != "0")
                {
                    canPlace = false;
                }
            }

            // check area around
            for (int k = 0; k < shipSize; k++)
            {
                if (rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y + k] != "0")
                        canPlace = false;
                }
                if (rnd_x - 1 >= 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y + k] != "0")
                        canPlace = false;
                }
            }

            if (rnd_y - 1 >= 0)
            {
                if (pcBoard[rnd_x, rnd_y - 1] != "0")
                    canPlace = false;
            }
            if (rnd_y + shipSize + 1 < 10)
            {
                if (pcBoard[rnd_x, rnd_y + shipSize + 1] != "0")
                    canPlace = false;
            }

            if (rnd_y + shipSize < 10 && rnd_x + 1 < 10)
            {
                if (pcBoard[rnd_x + 1, rnd_y + shipSize] != "0")
                    canPlace = false;
            }
            if (rnd_y + shipSize < 10 && rnd_x - 1 > 0)
            {
                if (pcBoard[rnd_x - 1, rnd_y + shipSize] != "0")
                    canPlace = false;
            }
            if (rnd_y - 1 > 0 && rnd_x + 1 < 10)
            {
                if (pcBoard[rnd_x + 1, rnd_y - 1] != "0")
                    canPlace = false;
            }
            if (rnd_y - 1 > 0 && rnd_x - 1 > 0)
            {
                if (pcBoard[rnd_x - 1, rnd_y - 1] != "0")
                    canPlace = false;
            }

            if (canPlace)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    pcBoard[rnd_x, rnd_y + i] = shipType + ship;
                    isPlaced = true;
                }
            }
            else
                isPlaced = false;
        }
        else if ((rnd_x - shipSize) > 0 && (rnd_y - shipSize) > 0)
        {
            int rnd_direction = rnd.Next(0, 2);
            //int rnd_direction = UnityEngine.Random.Range(0, 2);
            if (rnd_direction == 0) // vertical
            {
                for (int j = 0; j < shipSize; j++)
                {
                    if (pcBoard[rnd_x - j, rnd_y] != "0")
                    {
                        canPlace = false;
                    }
                }

                // check area around
                for (int k = 0; k < shipSize; k++)
                {
                    if (rnd_y + 1 < 10)
                    {
                        if (pcBoard[rnd_x - k, rnd_y + 1] != "0")
                            canPlace = false;
                    }
                    if (rnd_y - 1 >= 0)
                    {
                        if (pcBoard[rnd_x - k, rnd_y - 1] != "0")
                            canPlace = false;
                    }
                }

                if (rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y] != "0")
                        canPlace = false;
                }
                if (rnd_x - shipSize  >= 0)
                {
                    if (pcBoard[rnd_x - shipSize , rnd_y] != "0")
                        canPlace = false;
                }
                //
                if (rnd_x - shipSize  > 0 && rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x - shipSize , rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_x - shipSize  > 0 && rnd_y - 1 > 0)
                {
                    if (pcBoard[rnd_x - shipSize , rnd_y - 1] != "0")
                        canPlace = false;
                }
                if (rnd_x + 1 < 10 && rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_x + 1 < 10 && rnd_y - 1 > 0)
                {
                    if (pcBoard[rnd_x + 1, rnd_y - 1] != "0")
                        canPlace = false;
                }
                //
                if (canPlace)
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        pcBoard[rnd_x - i, rnd_y] = shipType + ship;
                        isPlaced = true;
                    }
                }
                else
                {
                    isPlaced = false;
                }
            }
            else if (rnd_direction == 1) // diagonal
            {
                for (int j = 0; j < shipSize; j++)
                {
                    if (pcBoard[rnd_x, rnd_y - j] != "0")
                    {
                        canPlace = false;
                    }
                }

                // check area around
                for (int k = 0; k < shipSize; k++)
                {
                    if (rnd_x + 1 < 10)
                    {
                        if (pcBoard[rnd_x + 1, rnd_y - k] != "0")
                            canPlace = false;
                    }
                    if (rnd_x - 1 >= 0)
                    {
                        if (pcBoard[rnd_x - 1, rnd_y - k] != "0")
                            canPlace = false;
                    }
                }
                if (rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x, rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_y - shipSize - 1 >= 0)
                {
                    if (pcBoard[rnd_x, rnd_y - shipSize - 1] != "0")
                        canPlace = false;
                }

                if (rnd_y - shipSize > 0 && rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y - shipSize] != "0")
                        canPlace = false;
                }
                if (rnd_y - shipSize > 0 && rnd_x - 1 > 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y - shipSize] != "0")
                        canPlace = false;
                }
                if (rnd_y + 1 < 10 && rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_y + 1 < 10 && rnd_x - 1 > 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y - 1] != "0")
                        canPlace = false;
                }

                if (canPlace)
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        pcBoard[rnd_x, rnd_y - i] = shipType + ship;
                        isPlaced = true;
                    }
                }
                else
                    isPlaced = false;
            }
        }
        else if ((rnd_x - shipSize) > 0)
        {
            for (int i = 0; i < shipSize; i++)
            {
                if (pcBoard[rnd_x - i, rnd_y] != "0")
                {
                    canPlace = false;
                }
            }

            // check area around
            for (int k = 0; k < shipSize; k++)
            {
                if (rnd_y + 1 < 10)
                {
                    if (pcBoard[rnd_x - k, rnd_y + 1] != "0")
                        canPlace = false;
                }
                if (rnd_y - 1 >= 0)
                {
                    if (pcBoard[rnd_x - k, rnd_y - 1] != "0")
                        canPlace = false;
                }
            }

            if (rnd_y - shipSize - 1 >= 0)// && canPlace == true)
            {
                if (pcBoard[rnd_x, rnd_y - shipSize - 1] != "0")
                    canPlace = false;
            }
            if (rnd_x - shipSize  >= 0)
            {
                if (pcBoard[rnd_x - shipSize , rnd_y] != "0")
                    canPlace = false;
            }

            if (rnd_x - shipSize  > 0 && rnd_y + 1 < 10)
            {
                if (pcBoard[rnd_x - shipSize , rnd_y + 1] != "0")
                    canPlace = false;
            }
            if (rnd_x - shipSize  > 0 && rnd_y - 1 > 0)
            {
                if (pcBoard[rnd_x + shipSize , rnd_y - 1] != "0")
                    canPlace = false;
            }
            if (rnd_x + 1 < 10 && rnd_y + 1 < 10)
            {
                if (pcBoard[rnd_x + 1, rnd_y + 1] != "0")
                    canPlace = false;
            }
            if (rnd_x + 1 < 10 && rnd_y - 1 > 0)
            {
                if (pcBoard[rnd_x + 1, rnd_y - 1] != "0")
                    canPlace = false;
            }

            if (canPlace)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    pcBoard[rnd_x - i, rnd_y] = shipType + ship;
                    isPlaced = true;
                }
            }
            else
                isPlaced = false;
        }
        else if ((rnd_y - shipSize) > 0) // diagonal
        {
            for (int i = 0; i < shipSize; i++)
            {
                if (pcBoard[rnd_x, rnd_y - i] != "0")
                {
                    canPlace = false;
                }
            }

            // check area around
            for (int k = 0; k < shipSize; k++)
            {
                if (rnd_x + 1 < 10)
                {
                    if (pcBoard[rnd_x + 1, rnd_y - k] != "0")
                        canPlace = false;
                }
                if (rnd_x - 1 >= 0)
                {
                    if (pcBoard[rnd_x - 1, rnd_y - k] != "0")
                        canPlace = false;
                }
            }
            if (rnd_y + 1 < 10)// && canPlace == true)
            {
                if (pcBoard[rnd_x, rnd_y + 1] != "0")
                    canPlace = false;
            }
            if (rnd_y - shipSize - 1 >= 0)
            {
                if (pcBoard[rnd_x, rnd_y - shipSize - 1] != "0")
                    canPlace = false;
            }

            if (rnd_y - shipSize > 0 && rnd_x + 1 < 10)
            {
                if (pcBoard[rnd_x + 1, rnd_y - shipSize] != "0")
                    canPlace = false;
            }
            if (rnd_y - shipSize > 0 && rnd_x - 1 > 0)
            {
                if (pcBoard[rnd_x - 1, rnd_y - shipSize] != "0")
                    canPlace = false;
            }
            if (rnd_y + 1 < 10 && rnd_x + 1 < 10)
            {
                if (pcBoard[rnd_x + 1, rnd_y + 1] != "0")
                    canPlace = false;
            }
            if (rnd_y + 1 < 10 && rnd_x - 1 > 0)
            {
                if (pcBoard[rnd_x - 1, rnd_y - 1] != "0")
                    canPlace = false;
            }

            if (canPlace)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    pcBoard[rnd_x, rnd_y - i] = shipType + ship;
                    isPlaced = true;
                }
            }
            else
                isPlaced = false;
        }
        else
            isPlaced = false;

    }
}
