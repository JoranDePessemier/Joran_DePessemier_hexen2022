using BoardSystem;
using CardSystem;
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
        private Engine _engine;
        private BoardView _boardView;


        private void Start()
        {
            BoardView boardView = FindObjectOfType<BoardView>();
            _board = new Board(boardView.Size);
            _engine = new Engine(_board);

            boardView.PositionDropped += OnPositionDropped;
            boardView.PositionDragged += OnPositionDragged;

            

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

            _boardView = FindObjectOfType<BoardView>();
            _boardView.PositionDropped += OnPositionDropped;
        }

        private void OnPositionDropped(object sender, PositionEventArgs e)
        {
            Position dropPosition = e.Position;
            CardView dropCard = e.CardView;
            Position fromPosition = PositionHelper.CubePosition(FindObjectOfType<PieceView>().WorldPosition); //TODO: Change this to find the player

            MoveSet moveSet = _engine.MoveSets.For(dropCard.Type);
            

            _engine.Move(fromPosition,dropPosition,e.CardView); 
            _boardView.ActivePositions = new List<Position>();
        }

        private void OnPositionDragged(object sender, PositionEventArgs e)
        {
            CardView dropCard = e.CardView;
            Position fromPosition = PositionHelper.CubePosition(FindObjectOfType<PieceView>().WorldPosition); //TODO: Change this to find the player

            MoveSet moveSet = _engine.MoveSets.For(dropCard.Type);
            List<Position> validPositions = moveSet.Positions(fromPosition,e.Position);
            _boardView.ActivePositions = validPositions;
        }
    }

}
