

using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;

    private MainPlayerClimb _mainPlayerClimb;
    private MainPlayerGeneralDetector _mainPlayerGeneralDetector;
    private MainPlayerAnimatorCoordinator _mainPlayerAnimatorCoordinator;
    private Rigidbody _rigidbody;
    private MainPlayerAccelerometer _mainPlayerAccelerometer;

    public Vector3 _movementDirection;
    private float _turnSpeed = 15;
    private float _valueForSpeed;

    private void OnEnable()
    {
        _valueForSpeed = 1.5f;
        _mainPlayerGeneralDetector = GetComponent<MainPlayerGeneralDetector>();
        _mainPlayerClimb = GetComponent<MainPlayerClimb>();
        _mainPlayerAnimatorCoordinator = GetComponent<MainPlayerAnimatorCoordinator>();
        _rigidbody = GetComponent<Rigidbody>();
        _mainPlayerAccelerometer = GetComponent<MainPlayerAccelerometer>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();
        Vector3 inputDirection = (_joystick.Horizontal * cameraRight + _joystick.Vertical * cameraForward);
        _movementDirection = inputDirection;
        Vector3 newVelocity = new Vector3(inputDirection.x, _rigidbody.velocity.y, inputDirection.z);
        if (inputDirection.magnitude >= 0.9f && _mainPlayerGeneralDetector.OnGrounded &&
            _rigidbody.velocity.y >= -1 && !_mainPlayerClimb.IsClimbing && !_mainPlayerAccelerometer.IsFalling)
        {
            _rigidbody.velocity = newVelocity / _valueForSpeed;
            Turn(inputDirection);
        }
        else if (inputDirection.magnitude > 0 && !_mainPlayerGeneralDetector.CheckOnSolidWithRayCast())
        {
            _rigidbody.velocity = new Vector3(inputDirection.x /2, _rigidbody.velocity.y, inputDirection.z /2);
        }

        if (inputDirection.magnitude > 0.9f && inputDirection.magnitude < 3 && _mainPlayerGeneralDetector.OnGrounded)
        {
            if (_mainPlayerGeneralDetector.CheckOnSolidWithCollider() || _mainPlayerGeneralDetector.CheckOnSolidWithRayCast())
            {
                if (inputDirection.magnitude < 2)
                {
                    _mainPlayerAnimatorCoordinator.SetAnimatorSpeed(0.75f);
                }
                else if (inputDirection.magnitude > 2 && inputDirection.magnitude < 2.5f)
                {
                    _mainPlayerAnimatorCoordinator.SetAnimatorSpeed(1.3f);
                }
                else if (inputDirection.magnitude > 2.5f)
                {
                    _mainPlayerAnimatorCoordinator.SetAnimatorSpeed(2);
                }
                _mainPlayerAnimatorCoordinator.ActivateBoolAnimation("walk");
            }
            else
            {
                _mainPlayerAnimatorCoordinator.DeactivateAllBoolAnimation();
            }
        }
        else if (inputDirection.magnitude >= 3 && _mainPlayerGeneralDetector.OnGrounded)
        {
            if (inputDirection.magnitude > 3 && inputDirection.magnitude < 4)
            {
                _valueForSpeed = 1.3f;
                _mainPlayerAnimatorCoordinator.SetAnimatorSpeed(0.9f);
            }
            else if (inputDirection.magnitude > 4)
            {
                _valueForSpeed = 1.1f;
                _mainPlayerAnimatorCoordinator.SetAnimatorSpeed(1.2f);
            }
            _mainPlayerAnimatorCoordinator.ActivateBoolAnimation("run");
        }
        else
        {
            _mainPlayerAnimatorCoordinator.DeactivateBoolAnimation("walk");
            _mainPlayerAnimatorCoordinator.DeactivateBoolAnimation("run");
        }
    }


    private void Turn(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }
    }
}