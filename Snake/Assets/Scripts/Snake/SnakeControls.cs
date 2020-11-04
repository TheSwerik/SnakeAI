using UnityEngine;

namespace Snake
{
    public class SnakeControls : MonoBehaviour
    {
        private Controls _controls;
        private SnakeMovement _movement;
        public Vector3 Direction { get; private set; } = Vector3.forward;

        private void Awake()
        {
            _movement = GetComponent<SnakeMovement>();
            _controls = new Controls();
            _controls.Movement.Direction.performed += v => Move(v.ReadValue<Vector2>());
        }

        private void OnEnable() { _controls.Enable(); }
        private void OnDisable() { _controls.Disable(); }
        private void OnDestroy() { _controls.Dispose(); }

        private void Move(Vector2 direction)
        {
            if (direction.x > .5 && _movement.Direction != Vector3.left) Direction = Vector3.right;
            else if (direction.x < -.5 && _movement.Direction != Vector3.right) Direction = Vector3.left;
            else if (direction.y > .5 && _movement.Direction != Vector3.back) Direction = Vector3.forward;
            else if (direction.y < -.5 && _movement.Direction != Vector3.forward) Direction = Vector3.back;
        }
    }
}