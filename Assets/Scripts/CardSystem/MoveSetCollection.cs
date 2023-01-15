using BoardSystem;
using CardSystem.MoveSets;
using GameSystem.Views;
using System.Collections.Generic;

namespace CardSystem
{
    public class MoveSetCollection
    {
        private Dictionary<CardType, MoveSet> _moveSets = new Dictionary<CardType, MoveSet>();


        //add the different card movesets here
        public MoveSetCollection(Board board)
        {
            _moveSets.Add(CardType.LineAttack, new LineAttackMoveSet(board));
            _moveSets.Add(CardType.Teleport, new TeleportMoveSet(board));
            _moveSets.Add(CardType.AdjacentAttack, new AdjacentAttackMoveSet(board));
            _moveSets.Add(CardType.PushEnemies, new PushEnemiesMoveSet(board));
            _moveSets.Add(CardType.StraightLineAttack, new StraightLineAttackMoveSet(board));
        }

        //other classes can ask the moveset for a specific cardttype
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
