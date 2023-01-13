using BoardSystem;
using GameSystem.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameSystem.Views
{
    [CustomEditor(typeof(BoardView))]
    public class BuildBoardEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BoardView build = (BoardView)target;

            if (GUILayout.Button("Build the board"))
            {
                build.ClearBoard();
                build.Build();
            }

        }
    }

    public class PositionEventArgs : EventArgs
    {
        public Position Position { get; }

        public CardView CardView { get; }

        public PositionEventArgs(Position position, CardView cardView)
        {
            Position = position;
            CardView = cardView;
        }

    }

    public class BoardView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tilePrefab;

        [SerializeField]
        [Tooltip("Size of each tile, determines the space in between the tile world position")]
        private int _size;
        public int Size => _size;

        private List<Position> _activePositions = new List<Position>();
        
        //tiles on the boardon the board
        private Dictionary<Position, PositionView> _positionViews = new Dictionary<Position, PositionView>();

        //events for dragging and dropping cards over a tile
        public event EventHandler<PositionEventArgs> PositionDropped;
        public event EventHandler<PositionEventArgs> PositionDragged;

        private void OnEnable()
        {
            //add the positionviews to the dictionary when the scene gets enabled
            PositionView[] positionViews = GetComponentsInChildren<PositionView>();
            foreach (PositionView positionView in positionViews)
            {
                _positionViews.Add(positionView.CubePosition, positionView);
            }
        }

        public List<Position> ActivePositions
        {
            set
            {
                //first deactivate all positions
                foreach (Position position in _activePositions)
                {
                    _positionViews[position].DeActivate();
                }

                if (value == null)
                {
                    _activePositions.Clear();
                }
                else
                {
                    _activePositions = value;
                }

                //activate all positions that are supposed to be active
                foreach (Position position in value)
                {
                    _positionViews[position].Activate();
                }
            }
        }


        private void OnPositionDropped(PositionEventArgs positionEventArgs)
        {
            EventHandler<PositionEventArgs> handler = PositionDropped;
            handler?.Invoke(this, positionEventArgs);
        }

        private void OnPositionDragged(PositionEventArgs positionEventArgs)
        {
            EventHandler<PositionEventArgs> handler = PositionDragged;
            handler?.Invoke(this, positionEventArgs);
        }

        public void ClearBoard()
        {
            PositionView[] children = this.GetComponentsInChildren<PositionView>();
            for (int i = children.Length - 1; i >= 0; i--)
            {
                if (children[i] != this.transform)
                {
                    DestroyImmediate(children[i].gameObject);
                }
            }
        }

        public void Build()
        {

            #region Create the tiles by looping through the q and r values
            List<Transform> tiles = new List<Transform>();

            for (int q = -_size + 1; q < _size; q++)
            {
                for (int r = -_size + 1; r < _size; r++)
                {
                    GameObject tile = GameObject.Instantiate(_tilePrefab, this.transform);
                    tile.name = $"HexTile {q},{r},{-q - r}";
                    tile.transform.position = PositionHelper.WorldPosition(new Position(q, r));
                    tiles.Add(tile.transform);
                }
            }
            #endregion

            #region Destroy all tiles where the distance fron the center tile is bigger or equal to size
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                Position tileCubePosition = PositionHelper.CubePosition(tiles[i].position);

                if (PositionHelper.CubeDistance(new Position(0, 0), tileCubePosition) >= _size)
                {
                    DestroyImmediate(tiles[i].gameObject);
                }
            }
            #endregion

        }

        internal void ChildDropped(PositionView positionView, CardView cardView)
        {
            OnPositionDropped(new PositionEventArgs(positionView.CubePosition,cardView));
        }

        internal void ChildDragged(PositionView positionView, CardView cardView)
        {
            OnPositionDragged(new PositionEventArgs(positionView.CubePosition, cardView));
        }


    }


}

