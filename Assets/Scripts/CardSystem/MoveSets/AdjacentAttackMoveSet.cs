﻿using BoardSystem;
using GameSystem.Helpers;
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
            foreach (Position position in Positions(fromPosition, toPosition))
            {
                Board.Take(position);
            }

            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            //the selectable positions are a ring around the player
            List<Position> allPositions = new List<Position>();

            allPositions.AddRange(PositionHelper.cubeRing(Board,fromPosition, 1));

            if (!allPositions.Contains(hoverPosition))
            {
                return allPositions;
            }

            //when hovering over one of the ring positions, this position and the one next to it should be selected
            List<Position> hoverPositions = PositionHelper.cubeRing(Board,hoverPosition, 1);

            List<Position> result = new List<Position>();

            foreach(Position position in hoverPositions)
            {
                if (allPositions.Contains(position))
                {
                    result.Add(position);
                }
            }

            result.Add(hoverPosition);

            return result;

        }
    }
}
