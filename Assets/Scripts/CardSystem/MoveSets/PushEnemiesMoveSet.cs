using BoardSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class PushEnemiesMoveSet : MoveSet
    {
        public PushEnemiesMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            foreach (Position position in Positions(fromPosition, toPosition))
            {
                List<Position> line = PositionHelper.CubeLine(Board, fromPosition, PositionHelper.CubeDirection(fromPosition, position), 2);

                if (line.Count < 2)
                {
                    Board.Take(line[0]);
                }
                else if(!Board.TryGetPieceAt(line[1], out PieceView piece))
                {
                    Board.Move(line[0], line[1]);
                }
            }

            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            //the selectable positions are a ring around the player
            List<Position> allPositions = new List<Position>();

            allPositions.AddRange(PositionHelper.cubeRing(Board,fromPosition, 1));

            if (!allPositions.Contains(hoverPosition))
            {
                return allPositions;
            }

            //when hovering over one of the ring positions, this position and the one next to it should be selected
            List<Position> hoverPositions = PositionHelper.cubeRing(Board,hoverPosition, 1);

            List<Position> result = new List<Position>();

            foreach (Position position in hoverPositions)
            {
                if (allPositions.Contains(position))
                {
                    result.Add(position);
                }
            }

            result.Add(hoverPosition);

            return result;
        }
    }
}
