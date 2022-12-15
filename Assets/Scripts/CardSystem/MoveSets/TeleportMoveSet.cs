using BoardSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.MoveSets
{
    class TeleportMoveSet : MoveSet
    {
        public TeleportMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            return Board.Move(fromPosition, toPosition);
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            List<Position> positions = new List<Position>();

            if (!Board.TryGetPieceAt(hoverPosition,out PieceView piece))
            {
                positions.Add(hoverPosition);
            }

            return positions;
        }
    }
}
