using BoardSystem;
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
            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            return new List<Position>();
        }
    }
}
