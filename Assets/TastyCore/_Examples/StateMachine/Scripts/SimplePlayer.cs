using UnityEngine;

namespace TastyCore._Examples.StateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class SimplePlayer : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        
        private CharacterController _controller;
        private Vector3 _velocity;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _controller.Move(move * Time.deltaTime * _speed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}