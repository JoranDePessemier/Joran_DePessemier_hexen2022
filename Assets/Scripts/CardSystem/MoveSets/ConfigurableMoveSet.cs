using BoardSystem;
using System.Collections.Generic;

namespace CardSystem.MoveSets
{
    internal delegate List<Position> Collector(Board board, Position playerPosition,Position hoverPosition);

    internal class ConfigurableMoveSet : MoveSet
    {
        private readonly Collector _collector;

        public ConfigurableMoveSet(Board board, Collector collector):base(board)
        {
            _collector = collector;
        }

        public override List<Position> Positions(Position playerPosition, Position hoverPosition)
        {
            return _collector(Board, playerPosition,hoverPosition);
        }
    }
}
