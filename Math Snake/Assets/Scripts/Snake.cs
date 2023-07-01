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
    private Direction gridMoveDirection;
    private Vector2Int gridPosition;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;
    private bool _isAlive;

    private bool _startedMoving;

    public Rigidbody2D rb;
    public Transform segmentPrefab;
    public int initialSize;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        gridMoveDirection = Direction.Up;
        gridPosition = Vector2Int.zero;
        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        _isAlive = true;
    }


    private void OnEnable()
    {
        OnPlayerDeath += DisableMovement;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= EnableMovement;
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

    public void Grow()
    {

        Vector2Int lastMovePosition = Vector2Int.zero;
        Vector3 eulerAngles = Vector3.zero;

        if (snakeBodyPartList.Count > 0 && snakeMovePositionList.Count > 0)
        {
            lastMovePosition = snakeBodyPartList[snakeBodyPartList.Count - 1].GetGridPosition();
            eulerAngles = snakeBodyPartList[snakeBodyPartList.Count - 1].GetEulerAngles();
        }

        snakeBodyPartList.Add(new SnakeBodyPart(segmentPrefab, lastMovePosition, eulerAngles));

        GameManager.Instance.IncreaseScore(1);
    }

    private void ResetState()
    {
        for (int i = 1; i < initialSize; i++)
        {
            Grow();
        }

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.HandleFoodCollision(other.GetComponent<Food>());
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.HandleObstacleCollision();
        }
    }

    public void DisableMovement()
    {
        _isAlive = false;
    }

    public void EnableMovement()
    {
        _isAlive = true;
    }

    public void Die()
    {
        OnPlayerDeath?.Invoke();
    }

    public int GetInitialSize()
    {
        return initialSize;
    }

    private class SnakeBodyPart
    {

        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(Transform segmentPrefab, Vector2Int position, Vector3 eulerAngles)
        {
            GameObject snakeBodyGameObject = GameObject.Instantiate(segmentPrefab.gameObject);
            snakeBodyGameObject.name = "SnakeBody";
            snakeBodyGameObject.transform.position = new Vector3(position.x, position.y);
            snakeBodyGameObject.transform.eulerAngles = eulerAngles;
            transform = snakeBodyGameObject.transform;
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

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }

        public Vector3 GetEulerAngles()
        {
            return transform.eulerAngles;
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
