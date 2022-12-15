using BoardSystem;
using GameSystem.Helpers;
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
        private readonly Position _hoverPosition;
        private Board _board;
        private List<Position> _maxPositions = new List<Position>();
        private List<Position> _hoveredPositions = new List<Position>();

        public MoveSetHelper(Board board, Position playerPosition, Position hoverPosition)
        {
            _currentPlayerPosition = playerPosition;
            _hoverPosition = hoverPosition;
            _board = board;
        }

        public MoveSetHelper Right(int maxSteps = int.MaxValue)
            => LineCollect(new Vector2Int(1,0), maxSteps);

        public MoveSetHelper Left(int maxSteps = int.MaxValue)
            => LineCollect(new Vector2Int(-1,0), maxSteps);

        public MoveSetHelper UpRight(int maxSteps = int.MaxValue)
            => LineCollect(new Vector2Int(0, 1), maxSteps);

        public MoveSetHelper UpLeft(int maxSteps = int.MaxValue)
            => LineCollect(new Vector2Int(-1, 1), maxSteps);

        public MoveSetHelper DownRight(int maxSteps = int.MaxValue)
            => LineCollect(new Vector2Int(1, -1), maxSteps);

        public MoveSetHelper DownLeft(int maxSteps = int.MaxValue)
            => LineCollect(new Vector2Int(0, -1), maxSteps);

        public MoveSetHelper LineCollect(Vector2Int direction, int maxSteps = int.MaxValue)
        {
            int currentStep = 0;

            Position position = new Position(_currentPlayerPosition.Q + direction.x, _currentPlayerPosition.R + direction.y);

            while(_board.IsValid(position)
                && currentStep < maxSteps)
            {
                _maxPositions.Add(position);
                currentStep++;

                position = new Position(position.Q + direction.x, position.R + direction.y);
            }

            return this;
        }

        public MoveSetHelper HpLineCollect(int maxSteps = int.MaxValue)
        {
            Position direction = PositionHelper.CubeSubtract(_currentPlayerPosition, _hoverPosition);
            Vector2Int vDirection = new Vector2Int(-(int)Math.Sign(direction.Q), -(int)Math.Sign(direction.R));

            int currentStep = 0;

            if(vDirection == Vector2Int.zero)
            {
                return this;
            }

            Position position = new Position(_currentPlayerPosition.Q + vDirection.x, _currentPlayerPosition.R + vDirection.y);

            while (_board.IsValid(position)
                && currentStep < maxSteps)
            {
                _hoveredPositions.Add(position);
                currentStep++;

                position = new Position(position.Q + vDirection.x, position.R + vDirection.y);
            }

            return this;
        }


        public List<Position> ValidPositions()
        {
            if (_maxPositions.Contains(_hoverPosition))
            {
                return _hoveredPositions;
            }
            else
            {
                return _maxPositions;
            }
        }



    }
   
}

