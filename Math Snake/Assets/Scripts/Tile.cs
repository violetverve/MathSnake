using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Init(bool isOffset) {
        _spriteRenderer.color = isOffset ? _offsetColor : _baseColor;
    }

}
