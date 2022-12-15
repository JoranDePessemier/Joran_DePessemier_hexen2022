using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class AdjacentAttackMoveSet : MoveSet
    {
        public AdjacentAttackMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            throw new NotImplementedException();
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            throw new NotImplementedException();
        }
    }
}
