using JetBrains.Annotations;
using UnityEngine;

namespace Snake
{
    public class SnakeMovement : BodyMovement
    {
        [SerializeField] private float cellSize = 1;
        [SerializeField] private Vector3 direction = Vector3.forward;
        private Collision _collision;

        private new void Awake()
        {
            base.Awake();
            _collision = GetComponent<Collision>();
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
            endPosition += direction * cellSize;
            time = 0f;
            if (nextBodyPart != null) nextBodyPart.StartNextMove(startPosition);
            _collision.MadeAnotherMove();
        }
    }
}