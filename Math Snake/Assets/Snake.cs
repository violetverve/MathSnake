using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    private MathUnit mathUnit;
    private bool _isAlive = true;
    
    public Rigidbody2D rb;
    public Transform segmentPrefab;
    public int initialSize = 4;
    public static event Action OnPlayerDeath;
    public int score = 0;

    [SerializeField] GameObject mathObject;


    private void OnEnable()
    {
        OnPlayerDeath += DisableMovement;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= EnableMovement;
    }

    private void Start()
    {   
        mathUnit = mathObject.GetComponent<MathUnit>();
        ResetState();
        EnableMovement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_direction != Vector2.left)
                _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_direction != Vector2.up)
                _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_direction != Vector2.right)
                _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_direction != Vector2.down)
                _direction = Vector2.up;
        }
    }

    private void FixedUpdate()
    {
        if (!_isAlive) return;
        
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
        score++;
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            Grow();
        }

        transform.position = Vector3.zero;
    } 

    private void ResetFood()
    {
        GameObject.Find("FoodSpawn").GetComponent<FoodSpawn>().ChangeFoodPosition();
        mathUnit.SetProblem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (mathUnit.CheckAnswer(other.GetComponent<Food>().GetValue()))
            {
                Grow();
            }
            else
            {
                OnPlayerDeath?.Invoke();
            }
               ResetFood();
        } 
        else if (other.CompareTag("Obstacle"))
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void DisableMovement() {
        _isAlive = false;
    }

    private void EnableMovement() {
        _isAlive = true;
    }
}
