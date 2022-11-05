using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Position
{
    private readonly int _q;
    private readonly int _r;
    private readonly int _s;

    public int Q => _q;
    public int R => _r;
    public int S => _s;

    public Position(int q, int r, int s)
    {
        _q = q;
        _r = r;
        _s = s;
    }

    public Position(int q, int r) : this(q, r, -q - r) { }


    public override string ToString()
    {
        return $"Posion(Q: {_q}, R: {_r}, S: {_s})";
    }


}
