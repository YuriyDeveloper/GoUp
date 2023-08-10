using UnityEngine;

public class MainPlayerAnimatorCoordinator : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActivateBoolAnimation(string boolParameterName)
    {
        _animator.SetBool(boolParameterName, true);

        if (boolParameterName != "run")
        {
            _animator.SetBool("run", false);
        }
        if (boolParameterName != "walk")
        {
            _animator.SetBool("walk", false);
        }
        if (boolParameterName != "jumpbool")
        {
            _animator.SetBool("jumpbool", false);
        }
        if (boolParameterName != "fall")
        {
            _animator.SetBool("fall", false);
        }
        if (boolParameterName != "climbbool")
        {
            _animator.SetBool("climbbool", false);
        }
    }

    public void ActivateBoolAnimationWithoutDeleteOther(string boolParameterName)
    {
         _animator.SetBool(boolParameterName, true);
    }

    public void DeactivateBoolAnimation(string boolParameterName)
    {
        _animator.SetBool(boolParameterName, false);
    }

    public void DeactivateAllBoolAnimation()
    {
        _animator.SetBool("run", false);
        _animator.SetBool("walk", false);
        _animator.SetBool("jumpbool", false);
        _animator.SetBool("fall", false);
        _animator.SetBool("climbbool", false);
    }

    public void ActivateTriggerAnimation(string triggerParameterName)
    {
        DeactivateAllBoolAnimation();
        _animator.SetTrigger(triggerParameterName);
    }


    public void SetAnimatorSpeed(float speed)
    {
        
        _animator.speed = speed;
    }
}
