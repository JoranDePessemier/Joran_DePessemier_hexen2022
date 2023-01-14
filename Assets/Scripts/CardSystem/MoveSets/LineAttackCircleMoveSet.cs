using BoardSystem;
using GameSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class LineAttackCircleMoveSet : MoveSet
    {
        public LineAttackCircleMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            foreach (Position position in Positions(fromPosition, toPosition))
            {
                if(!position.Equals(toPosition))
                {
                    Board.Take(position);
                }

            }

            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            List<Position> allPositions = new List<Position>();
            List<Position> hoverPositions = new List<Position>();

            allPositions.AddRange(PositionHelper.LeftLine(Board, fromPosition));
            allPositions.AddRange(PositionHelper.RightLine(Board, fromPosition));
            allPositions.AddRange(PositionHelper.UpLeftLine(Board, fromPosition));
            allPositions.AddRange(PositionHelper.UpRightLine(Board, fromPosition));
            allPositions.AddRange(PositionHelper.DownLeftLine(Board, fromPosition));
            allPositions.AddRange(PositionHelper.DownRightLine(Board, fromPosition));

            hoverPositions.AddRange(PositionHelper.cubeRing(Board, hoverPosition, 1));
            hoverPositions.Add(hoverPosition);

            if (allPositions.Contains(hoverPosition))
            {
                return hoverPositions;
            }
            else
            {
                return allPositions;
            }

            
        }
    }
}
