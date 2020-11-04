using UnityEngine;

namespace Snake
{
    public class BodyMovement : MonoBehaviour
    {
        [SerializeField] protected float speed = 2;
        [SerializeField] protected bool isMoving = true;
        private Renderer _renderer;
        protected Transform cachedTransform;
        protected Vector3 endPosition;
        protected BodyMovement nextBodyPart;
        protected Vector3 startPosition;
        protected float time = 1;

        protected void Awake()
        {
            cachedTransform = transform;
            endPosition = cachedTransform.position;
            nextBodyPart = null;
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (!isMoving) return;
            Move();
        }

        public void StartNextMove(Vector3 newEndPosition)
        {
            cachedTransform.position = startPosition = endPosition;
            endPosition = newEndPosition;
            time = 0f;
            if (nextBodyPart != null) nextBodyPart.StartNextMove(startPosition);
            if (!_renderer.enabled) _renderer.enabled = true;
        }

        protected void Move()
        {
            time += Time.deltaTime * speed;
            cachedTransform.position = Vector3.Lerp(startPosition, endPosition, time);
        }

        public void AddPart(BodyMovement bodyMovement)
        {
            if (nextBodyPart == null)
            {
                nextBodyPart = bodyMovement;
                nextBodyPart.StartAt(cachedTransform.position, endPosition, time);
            }
            else
            {
                nextBodyPart.AddPart(bodyMovement);
            }
        }

        private void StartAt(Vector3 startPos, Vector3 endPos, float currentTime)
        {
            endPosition = startPos;
            StartNextMove(endPos);
            _renderer.enabled = false;
            time = currentTime;
        }
    }
}