using BoardSystem;
using System.Collections.Generic;

namespace CardSystem
{
    internal delegate List<Position> Collector(Board board, Position position);

    internal class ConfigurableMoveSet : MoveSet
    {
        private readonly Collector _collector;

        public ConfigurableMoveSet(Board board, Collector collector):base(board)
        {
            _collector = collector;
        }

        public override List<Position> Positions(Position fromPosition)
        {
            return _collector(Board, fromPosition);
        }
    }
}
