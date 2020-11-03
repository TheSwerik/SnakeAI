using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float cellSize = 1;
    [SerializeField] private float speed = 2;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private bool isMoving = true;
    private Vector3 _endPosition;
    private Vector3 _startPosition;
    private float _time = 1;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _endPosition = _transform.position;
    }

    private void Update()
    {
        if (!isMoving) return;
        if (_time >= 1) StartNextMove();
        Move();
    }

    private void StartNextMove()
    {
        _transform.position = _startPosition = _endPosition;
        _endPosition += direction * cellSize;
        _time = 0f;
    }

    private void Move()
    {
        _time += Time.deltaTime * speed;
        _transform.position = Vector3.Lerp(_startPosition, _endPosition, _time);
    }
}