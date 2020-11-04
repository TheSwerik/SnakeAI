using System;
using UnityEngine;

namespace Snake
{
    public class Collision : MonoBehaviour
    {
        [SerializeField] private GameObject bodyPart;
        private SnakeMovement _movement;
        private Transform _bodyPartList;
        private Collider _newBodyPartCollider;

        private void Awake()
        {
            _movement = GetComponent<SnakeMovement>();
            _bodyPartList = GameObject.Find("Parts").transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Apple"))
            {
                Debug.Log("EAT APPLE");
                Destroy(other.gameObject);
                var newBodyPart = Instantiate(bodyPart, _bodyPartList);
                _movement.AddPart(newBodyPart.GetComponent<BodyMovement>());
                _newBodyPartCollider = newBodyPart.GetComponent<Collider>();
            }
            else if (other.CompareTag("Snake") && !other.Equals(_newBodyPartCollider))
            {
                Debug.Log("DIE");
                Destroy(transform.parent.gameObject);
            }
        }

        public void MadeAnotherMove() { _newBodyPartCollider = null; }
    }
}