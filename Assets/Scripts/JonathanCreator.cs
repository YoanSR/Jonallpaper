using System;
using NaughtyAttributes;
using UnityEngine;

public class JonathanCreator : MonoBehaviour
{
    [SerializeField] private GameObject _jonaPrefab;

    [Button]
    public void CreateJonathan()
    {
        GameObject jonathan = Instantiate(_jonaPrefab,transform.position,Quaternion.identity); 
        jonathan.name = "Jonathan";
        JonaActions.JonathanCreated?.Invoke();
    }
    
    
    public void Awake()
    {
        Singleton();
    }

    public static JonathanCreator Instance{ get; private set; }
    void Singleton()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
