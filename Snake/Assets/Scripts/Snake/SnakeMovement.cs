using UnityEngine;

namespace Snake
{
    public class SnakeMovement : BodyMovement
    {
        [SerializeField] private float cellSize = 1;
        private Collision _collision;
        private SnakeControls _controls;
        public Vector3 Direction { get; private set; } = Vector3.forward;

        private new void Awake()
        {
            base.Awake();
            _collision = GetComponent<Collision>();
            _controls = GetComponent<SnakeControls>();
        }

        private void Update()
        {
            if (!isMoving) return;
            if (time >= 1) StartNextMove();
            Move();
        }

        private void StartNextMove()
        {
            cachedTransform.position = startPosition = endPosition;
            Direction = _controls.Direction;
            endPosition += Direction * cellSize;
            time = 0f;
            if (nextBodyPart != null) nextBodyPart.StartNextMove(startPosition);
            _collision.MadeAnotherMove();
        }
    }
}