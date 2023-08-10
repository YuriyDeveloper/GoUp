
using UnityEngine;

public class MainPlayerJump : MonoBehaviour
{
    private MainPlayerAnimatorCoordinator _mainPlayerAnimatorCoordinator;
    private Rigidbody _rigidbody;
    private MainPlayerGeneralDetector _mainPlayerGeneralDetector;
    private MainPlayerMovement _mainPlayerMovement;

    private bool _isJumping;

    private float _jumpForce = 35;

    public bool IsJumping { get => _isJumping; }
    private void OnEnable()
    {
        _mainPlayerGeneralDetector = GetComponent<MainPlayerGeneralDetector>();
        _mainPlayerAnimatorCoordinator = GetComponent<MainPlayerAnimatorCoordinator>();
        _rigidbody = GetComponent<Rigidbody>();
        _mainPlayerMovement = GetComponent<MainPlayerMovement>();
    }

    private void Update()
    {
        Jump();
       
    }

    private void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && _mainPlayerGeneralDetector.CheckOnSolidWithCollider()  && _mainPlayerGeneralDetector.CheckOnSolidWithRayCast())
        {
           // Time.timeScale = 0.3f;
         //   Debug.Log("JUMP");
            _isJumping = true;
            _mainPlayerAnimatorCoordinator.DeactivateAllBoolAnimation();
            _mainPlayerAnimatorCoordinator.ActivateTriggerAnimation("jump");
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else
        {
            _isJumping = false; 
        }
    }

   

}
