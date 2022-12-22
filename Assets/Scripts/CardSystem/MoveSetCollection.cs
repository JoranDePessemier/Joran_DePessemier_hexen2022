using BoardSystem;
using CardSystem.MoveSets;
using GameSystem.Views;
using System.Collections.Generic;

namespace CardSystem
{
    public class MoveSetCollection
    {
        private Dictionary<CardType, MoveSet> _moveSets = new Dictionary<CardType, MoveSet>();

        public MoveSetCollection(Board board)
        {
            _moveSets.Add(CardType.LineAttack, new LineAttackMoveSet(board));
            _moveSets.Add(CardType.Teleport, new TeleportMoveSet(board));
            _moveSets.Add(CardType.AdjacentAttack, new AdjacentAttackMoveSet(board));
            _moveSets.Add(CardType.PushEnemies, new PushEnemiesMoveSet(board));
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
