using BoardSystem;
using GameSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class StraightLineAttackMoveSet : MoveSet
    {
        public StraightLineAttackMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            foreach (Position position in Positions(fromPosition,toPosition))
            {
                Board.Take(position);
            }

            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            List<Position> allPositions = PositionHelper.cubeCircle(Board, fromPosition, 3);

            if (allPositions.Contains(hoverPosition))
            {
                List<Position> hoverPositions = PositionHelper.cubeLineDraw(fromPosition, hoverPosition);
                hoverPositions.Remove(fromPosition);

                return hoverPositions;
            }
            else
            {
                return allPositions;
            }

            
        }
    }
}
