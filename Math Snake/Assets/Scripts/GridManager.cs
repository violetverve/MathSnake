using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;
    private Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>();

    private void Start() {
        GenerateGrid();
    }

    public void GenerateGrid() {
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";

                var isOffset = (x + y) % 2 == 1;
                spawnedTile.Init(isOffset);

                _tiles[new Vector2Int(x, y)] = spawnedTile;
            }
        }

        _cam.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -10f);
    }

    public Tile GetTile(Vector2Int coordinates) {
        if (_tiles.ContainsKey(coordinates)) {
            return _tiles[coordinates];
        }

        return null;
    }
}
