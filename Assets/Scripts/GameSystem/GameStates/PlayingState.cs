using BoardSystem;
using CardSystem;
using CardSystem.MoveSets;
using GameSystem.Helpers;
using GameSystem.Views;
using HandFactory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    class PlayingState:State
    {

        private Board _board;
        private Engine _engine;
        private BoardView _boardView;
        private PieceView _player;
        private HandView _handView;


        public override void OnEnter()
        {
            base.OnEnter();

            AsyncOperation op = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            op.completed += InitializeScene;
        }

        private void InitializeScene(AsyncOperation obj)
        {
            //setup the board view, board and card engine
            _boardView = GameObject.FindObjectOfType<BoardView>();
            _board = new Board(_boardView.Size);
            _engine = new Engine(_board);

            //setup the events for carddropping and dragging
            _boardView.PositionDropped += OnPositionDropped;
            _boardView.PositionDragged += OnPositionDragged;

            //makes sure the board class knows about each piece
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

            //find all pieces and make sure the board class knows about them, also save the player
            PieceView[] pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (PieceView pieceView in pieceViews)
            {
                if (pieceView.Type == PieceType.Player)
                {
                    _player = pieceView;
                }

                _board.Place(PositionHelper.CubePosition(pieceView.WorldPosition), pieceView);
            }

            //setup the hand view and method for when a card is dragged or dropped, even on empty space
            _handView = GameObject.FindObjectOfType<HandView>();
            _handView.CardStateSwitched += (s, e) =>
            {
                _boardView.ActivePositions = new List<Position>();
            };
        }

            //called only when the card drops on a tile
        private void OnPositionDropped(object sender, PositionEventArgs e)
        {
            Position dropPosition = e.Position;
            CardView dropCard = e.CardView;

            //from position is the player position
            Position fromPosition = PositionHelper.CubePosition(_player.WorldPosition);

            //find the card moveset
            MoveSet moveSet = _engine.MoveSets.For(dropCard.Type);

            //only remove the card if the dropPosition is actually valid
            if (moveSet.Positions(fromPosition, dropPosition).Contains(dropPosition))
            {
                _handView.RemoveCard(e.CardView);
            }

            //execute the card move for this card
            _engine.Move(fromPosition, dropPosition, e.CardView);
        }

        //called every time a card is dragged on a tile
        private void OnPositionDragged(object sender, PositionEventArgs e)
        {
            CardView dropCard = e.CardView;
            Position fromPosition = PositionHelper.CubePosition(_player.WorldPosition);

            //find the card moveset
            MoveSet moveSet = _engine.MoveSets.For(dropCard.Type);

            //find the valid positions and set the positions as active in the board, these active positions are removed again when the card is dropped
            List<Position> validPositions = moveSet.Positions(fromPosition, e.Position);
            _boardView.ActivePositions = validPositions;
        }
    }
}
