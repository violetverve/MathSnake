using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Snake : MonoBehaviour
{

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private Direction gridMoveDirection = Direction.Right;
    private Vector2Int gridPosition;
    private List<Transform> _segments = new List<Transform>();
    private List<SnakeMovePosition> snakeMovePositionList = new List<SnakeMovePosition>();
    private List<SnakeBodyPart> snakeBodyPartList = new List<SnakeBodyPart>();


    private MathUnit mathUnit;
    private bool _isAlive = true;
    private int _score = -initialSize + 1;
    private bool _startedMoving = false;

    public Rigidbody2D rb;
    public Transform segmentPrefab;
    public static int initialSize = 4;
    public static event Action OnPlayerDeath;
    public TextMeshProUGUI scoreText;
    public ComplexityDropdown complexityDropdown;
    public SnakeColorDropdown snakeColorDropdown;

    [SerializeField] GameObject mathObject;


    private void OnEnable()
    {
        OnPlayerDeath += DisableMovement;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= EnableMovement;
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(0, 0);
    }

    private void Start()
    {
        mathUnit = mathObject.GetComponent<MathUnit>();
        complexityDropdown.SetSpeed();
        snakeColorDropdown.SetSnakeColor();
    }

    private void Update()
    {
        if (!_startedMoving && Input.anyKeyDown)
        {
            _startedMoving = true;
            ResetState();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection != Direction.Down)
            {
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_startedMoving) return;
        if (!_isAlive) return;

        SnakeMovePosition previousSnakeMovePosition = null;
        if (snakeMovePositionList.Count > 0)
        {
            Vector2Int prev = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

            previousSnakeMovePosition = snakeMovePositionList[0];
        }

        SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
        snakeMovePositionList.Insert(0, snakeMovePosition);

        Vector2Int gridMoveDirectionVector;
        switch (gridMoveDirection)
        {
            default:
            case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
            case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
            case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
            case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
        }


        gridPosition += gridMoveDirectionVector;

        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);

        UpdateSnakeBodyParts();
    }


    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void UpdateSnakeBodyParts()
    {
        int minSize = Mathf.Min(snakeBodyPartList.Count, snakeMovePositionList.Count);

        for (int i = 0; i < minSize; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
        snakeBodyPartList.Add(new SnakeBodyPart(segment));
        _score++;
        scoreText.text = $"Score: {(_score).ToString()}";
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
        mathUnit.ExecuteRandomOperation();
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
                SaveScore();
                OnPlayerDeath?.Invoke();
            }
            ResetFood();
        }
        else if (other.CompareTag("Obstacle"))
        {
            SaveScore();
            OnPlayerDeath?.Invoke();
        }
    }

    private void DisableMovement()
    {
        _isAlive = false;
    }

    private void EnableMovement()
    {
        _isAlive = true;
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", _score);
        string best = $"BestScore-{PlayerPrefs.GetInt("Speed")}-{PlayerPrefs.GetInt("Fruit")}";
        if (_score > PlayerPrefs.GetInt(best, 0))
        {
            PlayerPrefs.SetInt(best, _score);
        }
    }


    private class SnakeBodyPart
    {

        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(Transform transform)
        {
            this.transform = transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;

            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up: // Currently going Up
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        case Direction.Left: // Previously was going Left
                            angle = 0 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Right: // Previously was going Right
                            angle = 0 - 45;
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                    }
                    break;
                case Direction.Down: // Currently going Down
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        case Direction.Left: // Previously was going Left
                            angle = 180 - 45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                        case Direction.Right: // Previously was going Right
                            angle = 180 + 45;
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
                case Direction.Left: // Currently going to the Left
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = +90;
                            break;
                        case Direction.Down: // Previously was going Down
                            angle = 180 - 45;
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                        case Direction.Up: // Previously was going Up
                            angle = 45;
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
                case Direction.Right: // Currently going to the Right
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        case Direction.Down: // Previously was going Down
                            angle = 180 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Up: // Previously was going Up
                            angle = -45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

    }


    private class SnakeMovePosition
    {

        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            return previousSnakeMovePosition?.direction ?? Direction.Right;
        }

    }
}
