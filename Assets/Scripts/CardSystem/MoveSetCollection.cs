using BoardSystem;
using GameSystem.Views;
using System.Collections.Generic;

namespace CardSystem
{
    public class MoveSetCollection
    {
        private Dictionary<CardType, MoveSet> _moveSets = new Dictionary<CardType, MoveSet>();

        public MoveSetCollection(Board board)
        {
            _moveSets.Add(CardType.LineAttack, new ConfigurableMoveSet(board,(b, p ) => new MoveSetHelper(b,p)
            .DownLeft()
            .DownRight()
            .Right()
            .Left()
            .UpRight()
            .UpLeft()
            .ValidPositions()
            ));
        }

        public MoveSet For(CardType type)
        {
            return _moveSets[type];
        }

        internal bool TryGetMoveSet(CardType type, out MoveSet moveSet)
        {
            return _moveSets.TryGetValue(type, out moveSet);
        }
    }
}
