using UnityEngine;

namespace Snake
{
    public class SnakeMovement : BodyMovement
    {
        private Collision _collision;
        private SnakeControls _controls;
        [HideInInspector] public Vector3 Direction { get; private set; } = Vector3.forward;

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
            endPosition += Direction * WorldSettings.Instance.cellSize;
            time = 0f;
            if (nextBodyPart != null) nextBodyPart.StartNextMove(startPosition);
            _collision.MadeAnotherMove();
        }
    }
}