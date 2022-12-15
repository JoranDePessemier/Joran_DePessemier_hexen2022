using BoardSystem;
using CardSystem.MoveSets;
using GameSystem.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class Engine
    {
        private readonly Board _board;
        private readonly MoveSetCollection _moveSetCollection;

        public Engine(Board board)
        {
            _board = board;
            _moveSetCollection = new MoveSetCollection(board);
        }

        public MoveSetCollection MoveSets
        {
            get
            {
                return _moveSetCollection;
            }
        }

        public bool Move(Position fromPosition, Position toPosition, CardView card)
        {
            if (!_board.IsValid(fromPosition))
            {
                return false;
            }
            
            if (!_board.IsValid(toPosition))
            {
                return false;
            }

            if(!_board.TryGetPieceAt(fromPosition,out PieceView piece))
            {
                return false;
            }

            if(!MoveSets.TryGetMoveSet(card.Type,out MoveSet moveSet))
            {
                return false;
            }

            if (!moveSet.Positions(fromPosition,toPosition).Contains(toPosition))
            {
                return false;
            }

            return moveSet.Execute(fromPosition, toPosition);

        }
    }

}
