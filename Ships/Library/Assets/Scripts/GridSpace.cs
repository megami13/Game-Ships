using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    public Button button;
    public Text buttonText;
    public int buttonID;

    private GameInfo gameInfo;
    private GameController gameController;
    private CreateShipsController shipController;

    public void SetSpace()
    {
        gameController.EndTurn(buttonID);
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
    }

    public void SetShips()
    {
        shipController.placeShip(buttonID);
        buttonText.text = shipController.getInfo();
        if (shipController.getInfo() != "")
        {
            button.interactable = false;
        }
        //button.interactable = false;
    }

    public void SetGameControllerReference(GameController controller, int id)
    {
        gameController = controller;
        buttonID = id;
    }

    public void SetShipControllerReference(CreateShipsController controller, int id)
    {
        shipController = controller;
        buttonID = id;
    }
}
