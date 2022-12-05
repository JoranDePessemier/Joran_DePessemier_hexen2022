using BoardSystem;
using System.Collections.Generic;

namespace CardSystem
{
    public abstract class MoveSet
    {
        private readonly Board _board;

        protected Board Board => _board;

        public MoveSet(Board board)
        {
            _board = board;
        }

        public abstract List<Position> Positions(Position fromPosition);

        public virtual bool Execute(Position fromPosition, Position toPosition)
        {
            _board.Take(toPosition);

            return _board.Move(fromPosition, toPosition);
        }
    }
}
