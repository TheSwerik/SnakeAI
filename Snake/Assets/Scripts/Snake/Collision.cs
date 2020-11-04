using UnityEngine;

namespace Snake
{
    public class Collision : MonoBehaviour
    {
        [SerializeField] private GameObject bodyPart;
        private Transform _bodyPartList;
        private SnakeMovement _movement;
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
            else if (other.CompareTag("Snake") && !other.Equals(_newBodyPartCollider) &&
                     _movement.endPosition.Equals(other.GetComponent<BodyMovement>().endPosition))
            {
                Debug.Log("DIE");
                Destroy(transform.parent.gameObject);
            }
        }

        public void MadeAnotherMove() { _newBodyPartCollider = null; }
    }
}