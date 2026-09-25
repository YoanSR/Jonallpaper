using UnityEngine;

public class SpinningComponent : MonoBehaviour
{
    public bool IsSpinning;
    
    private void FixedUpdate()
    {
        if (IsSpinning)
            transform.Rotate(0, 10, 0, Space.Self);
    }
}
