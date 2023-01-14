using BoardSystem;
using CardSystem;
using GameSystem.GameStates;
using GameSystem.Helpers;
using GameSystem.Views;
using HandFactory;
using System;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private Board _board;
        private Engine _engine;
        private BoardView _boardView;
        private StateMachine _stateMachine;

        private int _currentPlayerNumber;
        private int _maxPlayerCount;


        private void Start()
        {
            _stateMachine = new StateMachine();

            //setup the board view, board and card engine
            _boardView = FindObjectOfType<BoardView>();
            _board = new Board(_boardView.Size);
            _engine = new Engine(_board);

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
            PieceView[] pieceViews = FindObjectsOfType<PieceView>();
            HandView[] handViews = FindObjectsOfType<HandView>();

            int counter = 0;

            foreach (PieceView pieceView in pieceViews)
            {
                

                if(pieceView.Type == PieceType.Player)
                {
                    
                    HandView hand = handViews[counter];

                    PlayerState playerState = new PlayerState(pieceView, hand, _boardView, _engine);
                    _stateMachine.Register($"Player{counter}", playerState);
                    playerState.NextPlayer += OnNextPlayer;

                    counter++;
                    
                }

                _board.Place(PositionHelper.CubePosition(pieceView.WorldPosition), pieceView);
            }

            _maxPlayerCount = counter;

            _stateMachine.InitialState = "Player0";

        }

        private void OnNextPlayer(object sender, EventArgs e)
        {
            _currentPlayerNumber++;
            
            if (_currentPlayerNumber >= _maxPlayerCount)
            {
                _currentPlayerNumber = 0;
            }

            _stateMachine.MoveTo($"Player{_currentPlayerNumber}");
        }
    }

}
