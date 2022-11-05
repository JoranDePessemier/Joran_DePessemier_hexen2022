using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private void Start()
    {
        BoardView boardView = FindObjectOfType<BoardView>();

        boardView.PositionClicked += OnClicked;
    }

    private void OnClicked(object sender, PositionEventArgs e)
    {
        Debug.Log(e.Position.ToString());
    }
}
