using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    // TESTING GIT CHANGES
    // TESTING GIT PR
    [SerializeField] private GameController gameController;

    public void OnButtonClick()
    {
        gameController.StartGame();
    }
}
