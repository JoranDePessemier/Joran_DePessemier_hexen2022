using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class LineAttackMoveSet : MoveSet
    {
        public LineAttackMoveSet(Board board) : base(board)
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
            return new MoveSetHelper(Board, fromPosition, hoverPosition)
                .Left()
                .DownLeft()
                .UpLeft()
                .Right()
                .UpRight()
                .DownRight()
                .HpLineCollect()
                .ValidPositions();
        }
    }
}
