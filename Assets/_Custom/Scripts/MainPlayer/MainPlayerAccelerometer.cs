
using UnityEngine;

public class MainPlayerAccelerometer : MonoBehaviour
{
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private LayerMask _solidLayerMask;

    private Rigidbody _rigidbody;
    private MainPlayerClimb _mainPlayerClimb;
    private MainPlayerAnimatorCoordinator _mainPlayerAnimatorCoordinator;
  

    private bool _isFalling;

    public bool IsFalling { get => _isFalling; }

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainPlayerClimb = GetComponent<MainPlayerClimb>();
        _mainPlayerAnimatorCoordinator = GetComponent<MainPlayerAnimatorCoordinator>();
    }

    private void FixedUpdate()
    {
        CheckFall();
        Debug.DrawRay(_rayPoint.position, Vector3.down * 1, Color.green);
    }

    private void CheckFall()
    {
        
        if (!_mainPlayerClimb.IsClimbing && !CheckRay())
        {
            _mainPlayerAnimatorCoordinator.ActivateBoolAnimation("fall");
            _isFalling = true;
        }
        else
        {
            _mainPlayerAnimatorCoordinator.DeactivateBoolAnimation("fall");
            _isFalling = false;
        }
    }

    public bool CheckRay()
    {

        return Physics.Raycast(_rayPoint.position, Vector3.down, 1, _solidLayerMask);
    }

}
