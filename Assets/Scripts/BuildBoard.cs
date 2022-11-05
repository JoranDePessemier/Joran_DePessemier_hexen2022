using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildBoard))]
public class BuildBoardEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BuildBoard build = (BuildBoard)target;

        if (GUILayout.Button("Build the board"))
        {
            build.Clear();
            build.Build();
        }

    }
}

public class BuildBoard:MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;


    [SerializeField]
    private int _size;


    public void Clear()
    {
        Transform[] children = this.GetComponentsInChildren<Transform>();
        for (int i = children.Length - 2; i >= 0; i--)
        {
            if(children[i] != this.transform)
            {
                DestroyImmediate(children[i].gameObject);
            }
        }
    }

    public void Build()
    {
        List<Transform> tiles = new List<Transform>();
        
        for (int q = -_size+1; q < _size; q++)
        {
            for (int r = -_size+1; r < _size; r++)
            {
                GameObject tile = GameObject.Instantiate(_tilePrefab, this.transform);
                tile.name = $"HexTile {q},{r},{-q - r}";
                tile.transform.position = PositionHelper.WorldPosition(new Position(q, r));
                tiles.Add(tile.transform);
            }
        }

        for (int i = tiles.Count -1; i >= 0 ; i--)
        {
            Position tileCubePosition = PositionHelper.CubePosition(tiles[i].position);

            if(PositionHelper.CubeDistance(new Position(0,0),tileCubePosition) >= _size)
            {
                DestroyImmediate(tiles[i].gameObject);
            }
        }


    }
}
