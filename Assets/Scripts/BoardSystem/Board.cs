using GameSystem.Helpers;
using GameSystem.Views;
using System;
using System.Collections.Generic;

namespace BoardSystem
{
    public class PiecePlacedEventArgs : EventArgs
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

        //is there a piece on a certain tile
        public bool TryGetPieceAt(Position position, out PieceView piece)
        {
            return _pieces.TryGetValue(position, out piece);
        }

        //is the tile actually on the board
        public bool IsValid(Position position)
        {
            return !(PositionHelper.CubeDistance(new Position(0, 0), position) >= _size);
        }

        //spawn in a piece on a certain position, check if there is already a piece there first (called by gameloop in the beginning of the game)
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

        //move a piece from a certain position to another => check if there is a piece on the fromposition first
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

            if (!_pieces.TryGetValue(fromPosition, out PieceView piece))
            {
                return false;
            }

            _pieces.Remove(fromPosition);
            _pieces[toPosition] = piece;

            OnPieceMoved(new PieceMovedEventArgs(piece, fromPosition, toPosition));

            return true;
        }

        //remove a piece from a certain position => check if there is a piece on the position first
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

            if (!_pieces.TryGetValue(fromPosition, out PieceView piece))
            {
                return false;
            }

            _pieces.Remove(fromPosition);

            OnPieceTaken(new PieceTakenEventArgs(piece, fromPosition));

            return true;
        }


        // these events are for the BoardView class => the boardview should know which pieces to remove

        public void OnPiecePlaced(PiecePlacedEventArgs eventArgs)
        {
            EventHandler<PiecePlacedEventArgs> handler = PiecePlaced;
            handler?.Invoke(this, eventArgs);
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


}
