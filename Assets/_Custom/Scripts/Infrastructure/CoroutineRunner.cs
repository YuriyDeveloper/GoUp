
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
