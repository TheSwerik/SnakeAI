using System;
using UnityEngine;

namespace Snake
{
    public class SnakeControls : MonoBehaviour
    {
        private SnakeMovement _movement;
        private Controls _controls;

        private void Awake()
        {
            _movement = GetComponent<SnakeMovement>();
            _controls = new Controls();
            _controls.Movement.Direction.performed += v => Move(v.ReadValue<Vector2>());
        }

        private void Move(Vector2 direction)
        {
            if (direction.x > .5 && _movement.direction != Vector3.left) _movement.direction = Vector3.right;
            else if (direction.x < -.5 && _movement.direction != Vector3.right) _movement.direction = Vector3.left;
            else if (direction.y > .5 && _movement.direction != Vector3.back) _movement.direction = Vector3.forward;
            else if (direction.y < -.5 && _movement.direction != Vector3.forward) _movement.direction = Vector3.back;
        }

        private void OnEnable() { _controls.Enable(); }
        private void OnDisable() { _controls.Disable(); }
        private void OnDestroy() { _controls.Dispose(); }
    }
}