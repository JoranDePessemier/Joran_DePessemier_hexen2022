using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Helpers
{
    public static class PositionHelper
    {
        private static readonly Vector2 _tileRadius = new Vector2(1,1);

        private static readonly Vector2Int[] _directionVectors = 
        { 
            new Vector2Int(1,0),
            new Vector2Int(1,-1),
            new Vector2Int(0,-1),
            new Vector2Int(-1,0),
            new Vector2Int(-1,1),
            new Vector2Int(0,1)
        };

        #region Transforming between cube and worldpositions
        public static Position CubePosition(Vector3 worldPosition)
        {
            float Q = (Mathf.Sqrt(3) / 3 * worldPosition.x + -1f / 3f * worldPosition.z) / _tileRadius.x;
            float R = ((2f / 3f) * worldPosition.z) / _tileRadius.y;

            return CubeRound(Q, R);
        }

        public static Vector3 WorldPosition(Position cubePosition)
        {
            float x = _tileRadius.x * (Mathf.Sqrt(3) * cubePosition.Q + Mathf.Sqrt(3) / 2f * cubePosition.R);
            float z = _tileRadius.y * 3f / 2f * cubePosition.R;

            return new Vector3(x, 0, z);

        }

        #endregion

        #region Basic operations

        public static Position CubeSubtract(Position position1, Position position2)
        {
            return new Position(position1.Q - position2.Q, position1.R - position2.R, position1.S - position2.S);
        }

        public static Position CubeAdd(Position position1, Position position2)
        {
            return new Position(position1.Q + position2.Q, position1.R + position2.R, position1.S + position2.S);
        }

        public static Position CubeScale(Position position, int factor)
        {
            return new Position(position.Q * factor, position.R * factor);
        }

        public static Position CubeNeighbor(Position position, int direction)
        {
            return CubeAdd(position,new Position(_directionVectors[direction].x,_directionVectors[direction].y));
        }

        private static Position CubeRound(float fractionalQ, float fractionalR, float fractionalS)
        {
            float q = Mathf.Round(fractionalQ);
            float r = Mathf.Round(fractionalR);
            float s = Mathf.Round(fractionalS);


            float qDiff = Mathf.Abs(q - fractionalQ);
            float rDiff = Mathf.Abs(r - fractionalR);
            float sDiff = Mathf.Abs(s - fractionalS);

            if (qDiff > rDiff && qDiff > sDiff)
            {
                q = -r - s;
            }
            else if (rDiff > sDiff)
            {
                r = -q - s;
            }
            else
            {
                s = -q - r;
            }

            return new Position((int)q, (int)r, (int)s);
        }

        public static Vector2Int CubeDirection(Position startPosition, Position toPosition)
        {
            Position direction = PositionHelper.CubeSubtract(startPosition, toPosition);
            
            return new Vector2Int(-(int)Math.Sign(direction.Q), -(int)Math.Sign(direction.R));
        }

        private static Position CubeRound(float fractionalQ, float fractionalR)
            => CubeRound(fractionalQ, fractionalR, -fractionalQ - fractionalR);

        private static Position CubeRound(Position position)
            => CubeRound(position.Q, position.S);

        #endregion

        #region Distance between positions
        public static int CubeDistance(Position tile1, Position tile2)
        {
            Position vector = CubeSubtract(tile1, tile2);

            return (Mathf.Abs(vector.Q) + Mathf.Abs(vector.R) + Mathf.Abs(vector.S)) / 2;
        }

        #endregion

        #region Line selecting
        public static List<Position> CubeLine(Board board,Position startingPosition,Vector2Int direction, int maxSteps = int.MaxValue)
        {
            List<Position> results = new List<Position>();

            int currentStep = 0;

            Position position = new Position(startingPosition.Q + direction.x, startingPosition.R + direction.y);

            while (board.IsValid(position)
                && currentStep < maxSteps)
            {
                results.Add(position);
                currentStep++;

                position = new Position(position.Q + direction.x, position.R + direction.y);
            }

            return results;
        }

        public static List<Position> LeftLine(Board board, Position startingPosition, int maxSteps = int.MaxValue)
            => CubeLine(board, startingPosition, _directionVectors[3], maxSteps);
        public static List<Position> RightLine(Board board, Position startingPosition, int maxSteps = int.MaxValue)
            => CubeLine(board, startingPosition, _directionVectors[0], maxSteps);
        public static List<Position> UpLeftLine(Board board, Position startingPosition, int maxSteps = int.MaxValue)
            => CubeLine(board, startingPosition, _directionVectors[5], maxSteps);
        public static List<Position> UpRightLine(Board board, Position startingPosition, int maxSteps = int.MaxValue)
            => CubeLine(board, startingPosition, _directionVectors[4], maxSteps);
        public static List<Position> DownLeftLine(Board board, Position startingPosition, int maxSteps = int.MaxValue)
            => CubeLine(board, startingPosition, _directionVectors[2], maxSteps);
        public static List<Position> DownRightLine(Board board, Position startingPosition, int maxSteps = int.MaxValue)
            => CubeLine(board, startingPosition, _directionVectors[1], maxSteps);
        #endregion

        #region Ring selecting

        public static List<Position> cubeRing(Board board,Position centerPosition, int radius)
        {
            List<Position> results = new List<Position>();

            Position hex = CubeAdd(centerPosition, CubeScale(new Position(_directionVectors[4].x, _directionVectors[4].y), radius));

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    if (board.IsValid(hex))
                    {
                        results.Add(hex);
                    }
                    hex = CubeNeighbor(hex, i);
                }
            }

            return results;
        }

        #endregion

        #region Circle selecting

        public static List<Position> cubeCircle(Board board, Position centerPosition, int radius)
        {
            List<Position> result = new List<Position>();

            for (int i = 0; i < radius+1; i++)
            {
                result.AddRange(cubeRing(board, centerPosition, i));
            }

            return result;
        }

        #endregion

        #region Straight Line Selecting

        private static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        private static Vector2 CubeLerp(Position a, Position b, float t)
        {
            return new Vector2(Lerp(a.Q, b.Q, t), Lerp(a.R, b.R, t));
        }


        public static List<Position> cubeLineDraw(Position fromPosition, Position toPosition)
        {
            int distance = CubeDistance(fromPosition, toPosition);

            List<Position> result = new List<Position>();

            for (int i = 0; i <= distance; i++)
            {
                Vector2 cubeLerp = CubeLerp(fromPosition, toPosition, 1f / distance * i);

                result.Add(CubeRound(cubeLerp.x,cubeLerp.y));
            }

            return result;
        }

        #endregion



    }

}
