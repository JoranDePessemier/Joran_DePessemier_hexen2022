using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CardSystem
{
    internal class MoveSetHelper
    {
        private readonly Position _currentPlayerPosition;
        private Board _board;
        private List<Position> _positions = new List<Position>();

        public MoveSetHelper(Position playerPosition, Board board)
        {
            _currentPlayerPosition = playerPosition;
            _board = board;
        }

        public MoveSetHelper Right(int maxSteps)
            => Collect(new Vector2Int(1,0), maxSteps);

        public MoveSetHelper Left(int maxSteps)
            => Collect(new Vector2Int(-1,0), maxSteps);

        public MoveSetHelper UpRight(int maxSteps)
            => Collect(new Vector2Int(1, 1), maxSteps);

        public MoveSetHelper UpLeft(int maxSteps)
            => Collect(new Vector2Int(-1, 1), maxSteps);

        public MoveSetHelper DownRight(int maxSteps)
            => Collect(new Vector2Int(1, -1), maxSteps);

        public MoveSetHelper DownLeft(int maxSteps)
            => Collect(new Vector2Int(-1, -1), maxSteps);

        public MoveSetHelper Collect(Vector2Int direction, int maxSteps = int.MaxValue)
        {
            int currentStep = 0;

            Position position = new Position(_currentPlayerPosition.Q + direction.x, _currentPlayerPosition.R + direction.y);

            while(_board.IsValid(position)
                && currentStep < maxSteps)
            {
                _positions.Add(position);
                currentStep++;

                position = new Position(position.Q + direction.x, position.R + direction.y);
            }

            return this;
        }

        public List<Position> ValidPositions()
        {
            return _positions;
        }



    }
   
}

