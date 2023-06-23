using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;

    private Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>();

    private void Start() {
        GenerateGrid();
    }

    public void GenerateGrid() {
        for (int x = -_width/2; x < _width/2 + 1; x++) {
            for (int y = -_height/2 - 1; y < _height/2; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";

                var isOffset = Math.Abs(x + y) % 2 == 1;
                spawnedTile.Init(isOffset);

                _tiles[new Vector2Int(x, y)] = spawnedTile;
            }
        }
    }

    public Tile GetTile(Vector2Int coordinates) {
        if (_tiles.ContainsKey(coordinates)) {
            return _tiles[coordinates];
        }

        return null;
    }
}
