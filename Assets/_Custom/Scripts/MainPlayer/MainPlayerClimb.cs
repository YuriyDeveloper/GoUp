
using System.Collections;
using UnityEngine;

public class MainPlayerClimb : MonoBehaviour
{
    [SerializeField] private MainPlayerLedgeCollider _ledgeCollider1;
    [SerializeField] private MainPlayerLedgeCollider _ledgeCollider2;
    [SerializeField] private MainPlayerLedgeCollider _ledgeCollider3;

    private MainPlayerGeneralDetector _mainPlayerGeneralDetector;
    private Rigidbody _rigidbody;
    private MainPlayerAnimatorCoordinator _mainPlayerAnimatorCoordinator;

    private bool _isClimbing;
    private bool _canClimbTransformPositionY;
    private bool _canClimbTransformPositionZ;
    private bool _startTwoClimbStep;

    private float _climpTransformPositionPorgressY;
    private float _climpTransformPositionPorgressZ;
    private float _durationTransformPositionY;
    private float _durationTransformPositionZ;
    private float _additionalSpeedToTransformPositionY = 1; 
    private float _additionalSpeedToTransformPositionZ = 1;

    public bool IsClimbing { get => _isClimbing;  } 

    private void OnEnable()
    {
        _canClimbTransformPositionY = false;
        _canClimbTransformPositionZ = false;
        _isClimbing = false;
        _mainPlayerGeneralDetector = GetComponent<MainPlayerGeneralDetector>();
        _rigidbody = GetComponent<Rigidbody>();
        _mainPlayerAnimatorCoordinator = GetComponent<MainPlayerAnimatorCoordinator>();
    }

    private void FixedUpdate()
    {
        CheckCanClimb();
        if (_canClimbTransformPositionY)
        {
            ClimbTransformPositionY();
        }
        if (_canClimbTransformPositionZ)
        {
            ClimbTransformPositionZ();
        }
    }

    private void OnDrawGizmos()
    {
        if (_canClimbTransformPositionY)
        {
            DrawArrow.ForGizmo(transform.position,  Vector3.up * 1.5f, Color.green, 0.25f);
        }
        if (_canClimbTransformPositionZ)
        {
            DrawArrow.ForGizmo(transform.position, transform.forward * 1.5f, Color.blue, 0.25f);
        }
    }

    private void CheckCanClimb()
    {
        if (!_ledgeCollider1.ColliderIsDetect && _ledgeCollider2.ColliderIsDetect &&
            !_isClimbing && !_mainPlayerGeneralDetector.CheckOnSolidWithRayCast())
        {
            Climb();
        }
    }

    private void Climb()
    {
        _isClimbing = true;
        _rigidbody.isKinematic = true;
        _mainPlayerAnimatorCoordinator.DeactivateAllBoolAnimation();
        _mainPlayerAnimatorCoordinator.ActivateTriggerAnimation("climb");
        _mainPlayerAnimatorCoordinator.ActivateBoolAnimation("climbbool");
        StartCoroutine(ToClimbProcess());
    }

    private IEnumerator ToClimbProcess()
    {
        _canClimbTransformPositionY = true;
        _canClimbTransformPositionZ = true;
        _additionalSpeedToTransformPositionY = 1.45f;
        _durationTransformPositionY = 0.7f;
        _durationTransformPositionZ = 0.3f;
        yield return new WaitForSeconds(0.5f);
        _canClimbTransformPositionZ = true;
        _additionalSpeedToTransformPositionZ = 0.75f;

        if (_ledgeCollider3.ColliderIsDetect)
        {
           
            _durationTransformPositionZ = 2; 
            Debug.Log("Z " + _durationTransformPositionZ);
        }
        else
        {
             _durationTransformPositionZ = 0.2f;
            Debug.Log("Z " + _durationTransformPositionZ);
        }

        yield return new WaitForSeconds(0.5f);
        _canClimbTransformPositionY = true;
        _additionalSpeedToTransformPositionY = 1.2f;
        _durationTransformPositionY = 0.3f;
        _rigidbody.isKinematic = false;
        _isClimbing = false;
    }

    private void ClimbTransformPositionY()
    {
        
        if (_climpTransformPositionPorgressY < _durationTransformPositionY)
        {
            _climpTransformPositionPorgressY += Time.deltaTime;
            transform.position += transform.up * Time.deltaTime * _additionalSpeedToTransformPositionY;
        }
        else
        {
            _climpTransformPositionPorgressY = 0;
            _canClimbTransformPositionY = false;
        }
     
    }

    private void ClimbTransformPositionZ()
    {
        if (_climpTransformPositionPorgressZ < _durationTransformPositionZ)
        {
            _climpTransformPositionPorgressZ += Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * _additionalSpeedToTransformPositionZ;
        }
        else
        {
            _climpTransformPositionPorgressZ = 0;
            _canClimbTransformPositionZ = false;
        }
    }



}
