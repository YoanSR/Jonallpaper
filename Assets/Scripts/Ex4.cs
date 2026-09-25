using System;
using UnityEngine;

public class Ex4 : MonoBehaviour
{

    private GameObject _createdCube;
    
    private void Destroycube()
    {
        Destroy(_createdCube);
    }

    private void CreateSpinningCube()
    {
        _createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _createdCube.AddComponent<Rigidbody>();
        _createdCube.AddComponent<SpinningComponent>();
        _createdCube.GetComponent<SpinningComponent>().IsSpinning = true;
        
        Invoke("Destroycube", 1f);
    }

    private void Start()
    {
        InvokeRepeating("CreateSpinningCube", 0f, 2f);
    }
}
