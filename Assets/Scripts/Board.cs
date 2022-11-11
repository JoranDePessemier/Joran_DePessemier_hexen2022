using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PiecePlacedEventArgs:EventArgs
{
    public PieceView Piece { get; }
    public Position ToPosition { get; }

    public PiecePlacedEventArgs(PieceView piece, Position toPosition)
    {
        Piece = piece;
        ToPosition = toPosition;
    }
}
public class PieceMovedEventArgs : EventArgs
{
    public PieceView Piece { get; }
    public Position FromPosition { get; }
    public Position ToPosition { get; }

    public PieceMovedEventArgs(PieceView piece, Position fromPosition, Position toPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
        ToPosition = toPosition;
    }
}
public class PieceTakenEventArgs : EventArgs
{
    public PieceView Piece { get; }
    public Position FromPosition { get; }

    public PieceTakenEventArgs(PieceView piece, Position fromPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
    }
}


public class Board
{
    private Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

    private readonly int _size;

    public event EventHandler<PiecePlacedEventArgs> PiecePlaced;
    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;

    public Board(int size)
    {
        _size = size;
    }

    public bool TryGetPieceAt(Position position, out PieceView piece)
    {
        return _pieces.TryGetValue(position, out piece);
    }

    public bool IsValid(Position position)
    {
        return !(PositionHelper.CubeDistance(new Position(0, 0), position) >= _size);
    }

    public bool Place(Position position, PieceView piece)
    {
        if (piece == null)
        {
#if UNITY_EDITOR || DEBUG
            throw new ArgumentException($"{piece} is not an actual piece, dumbass");
#else
            return false;
#endif
        }

        if (!IsValid(position))
        {
            return false;
        }

        if (_pieces.ContainsKey(position))
        {
            return false;
        }

        if (_pieces.ContainsValue(piece))
        {
            return false;
        }

        _pieces[position] = piece;

        OnPiecePlaced(new PiecePlacedEventArgs(piece, position));

        return true;
    }

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValid(toPosition))
        {
            return false;
        }

        if (_pieces.ContainsKey(toPosition))
        {
            return false;
        }

        if(!_pieces.TryGetValue(fromPosition,out PieceView piece))
        {
            return false;
        }

        _pieces.Remove(fromPosition);
        _pieces[toPosition] = piece;

        OnPieceMoved(new PieceMovedEventArgs(piece, fromPosition, toPosition));

        return true;
    }

    public bool Take(Position fromPosition)
    {
        if (!IsValid(fromPosition))
        {
            return false;
        }

        if (!_pieces.ContainsKey(fromPosition))
        {
            return false;
        }

        if(!_pieces.TryGetValue(fromPosition, out PieceView piece))
        {
            return false;
        }

        _pieces.Remove(fromPosition);

        OnPieceTaken(new PieceTakenEventArgs(piece, fromPosition));

        return true;
    }

    public void OnPiecePlaced(PiecePlacedEventArgs eventArgs)
    {
        EventHandler<PiecePlacedEventArgs> handler = PiecePlaced;
        handler?.Invoke(this,eventArgs);
    }
    public void OnPieceMoved(PieceMovedEventArgs eventArgs)
    {
        EventHandler<PieceMovedEventArgs> handler = PieceMoved;
        handler?.Invoke(this, eventArgs);
    }
    public void OnPieceTaken(PieceTakenEventArgs eventArgs)
    {
        EventHandler<PieceTakenEventArgs> handler = PieceTaken;
        handler?.Invoke(this, eventArgs);
    }
}

