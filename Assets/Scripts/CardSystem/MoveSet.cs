using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem
{
    public abstract class MoveSet
    {
        private readonly Board _board;

        public MoveSet(Board board)
        {
            _board = board;
        }

        public abstract List<Position> Positions(Position fromPosition);

        public virtual bool Execute(Position fromPosition, Position toPosition)
        {
            _board.Take(fromPosition);

            return _board.Move(fromPosition, toPosition);
        }
    }
}
