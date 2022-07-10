using UnityEngine;
using UnityEngine.InputSystem;

namespace Lesson_4_7.GameEntityComponents
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _thisBody;
        private PlayerStats _playerStats;
        private float _verticalAxis, _horizontalAxis;

        private void Start()
        {
            _thisBody = GetComponent<Rigidbody>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void FixedUpdate()
        {
            if (!_playerStats.InputEnabled)
            {
                return;
            }
            MovementLogic();
        }

        private void MovementLogic()
        {
            if (!_playerStats.PhotonView.isMine)
            {
                return;
            }

            //Vector3 inputAxis = new Vector3(_horizontalAxis, 0, _verticalAxis);
            //Vector3 rotation = _thisBody.rotation.eulerAngles;
            //Vector3 velocity = _thisBody.velocity;
            //float bodyRotationAsRadian = _thisBody.rotation.eulerAngles.y * Mathf.Deg2Rad;

            //velocity.x = Mathf.Sin(bodyRotationAsRadian) * inputAxis.x;

            //velocity.z = Mathf.Cos(bodyRotationAsRadian) * inputAxis.z;

            //velocity *= _playerStats.Acceleration * Time.fixedDeltaTime;
            //velocity = Vector3.ClampMagnitude(velocity, _playerStats.MaxVelocity);


            //_thisBody.velocity += velocity;
            //rotation.x = 0;
            //rotation.z = 0;
            //_thisBody.rotation = Quaternion.Euler(rotation);

            Vector3 inputAxis = new Vector3(_horizontalAxis, 0, _verticalAxis);
            inputAxis *= _playerStats.Acceleration * Time.fixedDeltaTime;

            Vector3 velocity = _thisBody.velocity;
            velocity += inputAxis;

            velocity = Vector3.ClampMagnitude(velocity, _playerStats.MaxVelocity);

            _thisBody.velocity = velocity;
            Vector3 rotation = _thisBody.rotation.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;
            _thisBody.rotation = Quaternion.Euler(rotation);
        }

        private void OnMove(InputValue input)
        {
            _verticalAxis = input.Get<float>();
        }

        private void OnStrafe(InputValue input)
        {
            _horizontalAxis = input.Get<float>();
        }
    }
}
