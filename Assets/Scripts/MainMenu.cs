﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameController.GetComponent<GameController>();

    }

    public void StartGame()
    {
        gameController.StartWaves();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}