using BoardSystem;
using GameSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class MeteorMoveSet : MoveSet
    {
        public MeteorMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            foreach(Position position in Positions(fromPosition, toPosition))
            {
                Board.Take(position);
            }

            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            List<Position> positions = PositionHelper.cubeRing(Board, hoverPosition, 1);
            positions.Add(hoverPosition);

            return positions;
        }
    }
}
