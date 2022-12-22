using BoardSystem;
using CardSystem;
using CardSystem.MoveSets;
using GameSystem.Helpers;
using GameSystem.Views;
using HandFactory;
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
        private PieceView _player;
        private HandView _handView;


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
                if(pieceView.Type == PieceType.Player)
                {
                    _player = pieceView;
                }

                _board.Place(PositionHelper.CubePosition(pieceView.WorldPosition), pieceView);
            }

            _boardView = FindObjectOfType<BoardView>();
            _boardView.PositionDropped += OnPositionDropped;

            _handView = FindObjectOfType<HandView>();
            _handView.CardStateSwitched += OnCardStateSwitched;
        }

        //called each time a card is dropped, even in empty space
        private void OnCardStateSwitched(object sender, CardEventArgs e)
        {
            _boardView.ActivePositions = new List<Position>();
        }

        //called only when the card drops on a tile
        private void OnPositionDropped(object sender, PositionEventArgs e)
        {
            Position dropPosition = e.Position;
            CardView dropCard = e.CardView;
            Position fromPosition = PositionHelper.CubePosition(_player.WorldPosition); //TODO: Change this to find the player

            MoveSet moveSet = _engine.MoveSets.For(dropCard.Type);
            

            _engine.Move(fromPosition,dropPosition,e.CardView);

            if (moveSet.Positions(fromPosition, dropPosition).Contains(dropPosition))
            {
                _handView.RemoveCard(e.CardView);
            }
        }

        private void OnPositionDragged(object sender, PositionEventArgs e)
        {
            CardView dropCard = e.CardView;
            Position fromPosition = PositionHelper.CubePosition(_player.WorldPosition); //TODO: Change this to find the player

            MoveSet moveSet = _engine.MoveSets.For(dropCard.Type);
            List<Position> validPositions = moveSet.Positions(fromPosition,e.Position);
            _boardView.ActivePositions = validPositions;
        }
    }

}
