using BoardSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.MoveSets
{
    public class CircleRotateMoveSet : MoveSet
    {
        public CircleRotateMoveSet(Board board) : base(board)
        {
        }

        public override bool Execute(Position fromPosition, Position toPosition)
        {
            int distance = PositionHelper.CubeDistance(fromPosition, toPosition);

            List<Position> ring = PositionHelper.cubeRing(Board, fromPosition, distance, true);
            List<Position> toPositions = new List<Position>();
            List<Position> fromPositions = new List<Position>();
            
            for (int i = ring.Count - 1; i >= 0; i--)
            {
                if (Board.TryGetPieceAt(ring[i] ,out PieceView piece))
                {
                    fromPositions.Add(ring[i]);
                    if (i != 0)
                    {
                        toPositions.Add(ring[i - 1]);
                    }
                    else
                    {
                        toPositions.Add(ring[ring.Count - 1]);
                    }
                }
            }

            int counter = toPositions.Count - 1;

            while(toPositions.Count != 0)
            {
                if (Board.Move(fromPositions[counter], toPositions[counter]))
                {
                    toPositions.RemoveAt(counter);
                    fromPositions.RemoveAt(counter);
                    counter = toPositions.Count - 1;
                }
                else if(!Board.IsValid(toPositions[counter]))
                {
                    Board.Take(fromPositions[counter]);
                    toPositions.RemoveAt(counter);
                    fromPositions.RemoveAt(counter);
                    counter = toPositions.Count - 1;
                }
                else
                {
                    counter--;
                }
            }



            return true;
        }

        public override List<Position> Positions(Position fromPosition, Position hoverPosition)
        {
            int distance = PositionHelper.CubeDistance(fromPosition, hoverPosition);

            return PositionHelper.cubeRing(Board, fromPosition, distance);
        }
    }
}

