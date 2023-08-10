
using System.Threading.Tasks;
using UnityEngine;

public class MainPlayerGeneralDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector3 _groundBox = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private LayerMask _solidMask;

    private MainPlayerAnimatorCoordinator _mainPlayerAnimatorCoordinator; 
    private MainPlayerClimb _mainPlayerClimb;

    private bool _canChangeOnGroundedState;
    private bool _isGrounded;

    public bool OnGrounded { get => _isGrounded; }

    private void OnEnable()
    {
        _canChangeOnGroundedState = true;
        _mainPlayerAnimatorCoordinator = GetComponent<MainPlayerAnimatorCoordinator>();
        _mainPlayerClimb = GetComponent<MainPlayerClimb>();
    }

    private void Update()
    {
        if (  CheckOnSolidWithCollider() && CheckOnSolidWithRayCast())
        {
            _isGrounded = true;
            _canChangeOnGroundedState = false;
            _mainPlayerAnimatorCoordinator.ActivateBoolAnimationWithoutDeleteOther("ongrounded");
        }
        else if (  !CheckOnSolidWithRayCast())
        {
            _isGrounded = false;
            _canChangeOnGroundedState = true;
            _mainPlayerAnimatorCoordinator.DeactivateBoolAnimation("ongrounded");
        }

        Debug.DrawRay(_groundCheckPoint.position, Vector3.down * 0.2f, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundBox);
    }

    public bool CheckOnSolidWithCollider()
    {
        if ( Physics.CheckBox(_groundCheckPoint.position, _groundBox, Quaternion.identity, _solidMask))
        {
            if (Physics.CheckBox(_groundCheckPoint.position, _groundBox, Quaternion.identity, _solidMask) && _mainPlayerClimb.IsClimbing)
            {
                return false;
            }
            return true;

        }
        else
        {
            return false;
        }
    }

    public bool CheckOnSolidWithRayCast()
    {
        return Physics.Raycast(_groundCheckPoint.position, Vector3.down, 0.2f, _solidMask);
       
    }
}
