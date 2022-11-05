using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PositionHelper
{
    private static readonly Vector2 _tileRadius = new Vector2(1,1);

    private static Position CubeSubtract(Position tile1, Position tile2)
    {
        return new Position(tile1.Q - tile2.Q, tile1.R - tile2.R, tile1.S - tile2.S);
    }

    public static int CubeDistance(Position tile1, Position tile2)
    {
        Position vector = CubeSubtract(tile1, tile2);

        return (Mathf.Abs(vector.Q) + Mathf.Abs(vector.R) + Mathf.Abs(vector.S)) / 2;
    }

    public static Position CubePosition(Vector3 worldPosition)
    {
        float Q = (Mathf.Sqrt(3) / 3 * worldPosition.x + -1f / 3f * worldPosition.z)/_tileRadius.x;
        float R = ((2f / 3f) * worldPosition.z) / _tileRadius.y;

        return CubeRound(Q,R);
    }

    public static Vector3 WorldPosition(Position cubePosition)
    {
        float x = _tileRadius.x * (Mathf.Sqrt(3) * cubePosition.Q + Mathf.Sqrt(3) / 2f * cubePosition.R);
        float z = _tileRadius.y * 3f/2f * cubePosition.R;

        return new Vector3(x, 0, z);
            
    }

    private static Position CubeRound(float fractionalQ, float fractionalR, float fractionalS)
    {
        float q = Mathf.Round(fractionalQ);
        float r = Mathf.Round(fractionalR);
        float s = Mathf.Round(fractionalS);


        float qDiff = Mathf.Abs(q - fractionalQ);
        float rDiff = Mathf.Abs(r - fractionalR);
        float sDiff = Mathf.Abs(s - fractionalS);

        if(qDiff > rDiff && qDiff > sDiff)
        {
            q = -r - s;
        }
        else if(rDiff > sDiff)
        {
            r = -q - s;
        }
        else
        {
            s = -q - r;
        }

        return new Position((int)q, (int)r,(int) s);


    }

    private static Position CubeRound(float fractionalQ, float fractionalR) => CubeRound(fractionalQ, fractionalR, -fractionalQ - fractionalR);

    
}
