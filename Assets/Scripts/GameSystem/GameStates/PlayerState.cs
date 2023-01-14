using BoardSystem;
using CardSystem;
using CardSystem.MoveSets;
using GameSystem.Helpers;
using GameSystem.Views;
using HandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.GameStates
{
    class PlayerState:State
    {
        public event EventHandler<EventArgs> NextPlayer;

        private PieceView _player;
        private HandView _handView;
        private BoardView _boardView;
        private Engine _engine;

        public PlayerState(PieceView player, HandView hand, BoardView boardView, Engine engine)
        {
            _player = player;
            _handView = hand;
            _engine = engine;
            _boardView = boardView;

            //setup the hand view and method for when a card is dragged or dropped, even on empty space
            _handView.CardStateSwitched += (s, e) =>
            {
                _boardView.ActivePositions = new List<Position>();
            };

            _handView.gameObject.SetActive(false);
        }

        public override void OnEnter()
        {
            _boardView.PositionDropped += OnPositionDropped;
            _boardView.PositionDragged += OnPositionDragged;

            _handView.gameObject.SetActive(true);
        }



        public override void OnExit()
        {
            _boardView.PositionDropped -= OnPositionDropped;
            _boardView.PositionDragged -= OnPositionDragged;

            _handView.gameObject.SetActive(false);
        }

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
                OnNextPlayer(EventArgs.Empty);
            }

            //execute the card move for this card
            _engine.Move(fromPosition, dropPosition, e.CardView);
        }

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

        private void OnNextPlayer(EventArgs eventArgs)
        {
            EventHandler<EventArgs> handler = NextPlayer;
            handler?.Invoke(this, eventArgs);
        }
    }
}
