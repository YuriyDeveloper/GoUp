using UnityEngine;

public class MainPlayerLedgeCollider : MonoBehaviour
{
    private bool _colliderIsDetect;
    public bool ColliderIsDetect { get => _colliderIsDetect; } 

    private void OnTriggerEnter(Collider other)
    {
        _colliderIsDetect = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _colliderIsDetect = false;
    }
}
