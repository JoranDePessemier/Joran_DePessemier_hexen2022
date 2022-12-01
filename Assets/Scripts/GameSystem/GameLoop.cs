using BoardSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private Board _board;
        private void Start()
        {
            BoardView boardView = FindObjectOfType<BoardView>();
            boardView.PositionClicked += OnPositionDropped;

            _board = new Board(boardView.Size);

            _board.PiecePlaced += (s, e) =>
            {
                e.Piece.Placed(PositionHelper.WorldPosition(e.ToPosition));
            };

            _board.PieceMoved += (s, e) =>
            {
                e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));
            };

            _board.PieceTaken += (s, e) =>
            {
                e.Piece.Taken();
            };

            PieceView[] pieceViews = FindObjectsOfType<PieceView>();
            foreach (PieceView pieceView in pieceViews)
            {
                _board.Place(PositionHelper.CubePosition(pieceView.WorldPosition), pieceView);
            }


        }

        private void OnPositionDropped(object sender, PositionEventArgs e)
        {
            Debug.Log(e.Position);
        }
    }

}
