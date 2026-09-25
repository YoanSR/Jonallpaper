using System;
using UnityEngine;

public class Ex5 : MonoBehaviour
{
    public MeshRenderer SphereMeshRenderer;

    public GameObject CylinderGameObject;
    public SpinningComponent CubeSpinningComponent;
    public Color ColorToApply;
    private bool _isCylinderActive;

    [SerializeField] private GameObject _jonathan;

    private void SwitchCylinder()
    {
        CylinderGameObject.SetActive(_isCylinderActive);
        _isCylinderActive = !_isCylinderActive;
    }

    private void Update()
    {
        SphereMeshRenderer.material.color = ColorToApply;

        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        Instantiate(_jonathan, this.transform);
        
        
        InvokeRepeating("SwitchCylinder", 0, 0f);
        
        CubeSpinningComponent.IsSpinning = true;
    }
}
