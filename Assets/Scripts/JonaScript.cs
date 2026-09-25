using NaughtyAttributes;
using UnityEngine;

public class JonaScript : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField,MinMaxSlider(-1,1)] private Vector2 _xLaunchDirectionRange;
    [SerializeField,MinMaxSlider(-1,1)] private Vector2 _yLaunchDirectionRange;
    [SerializeField,MinMaxSlider(-1,1)] private Vector2 _zLaunchDirectionRange;
    [SerializeField,MinMaxSlider(0,5000)] private Vector2 _lauchStrengthRange;
    

    void Start()
    {
        float xRange = JonaUtility.EvaluateVector2(_xLaunchDirectionRange,(float)Random.Range(0,100) / 100 );
        float yRange = JonaUtility.EvaluateVector2(_yLaunchDirectionRange,(float)Random.Range(0,100) / 100 );
        float zRange = JonaUtility.EvaluateVector2(_zLaunchDirectionRange,(float)Random.Range(0,100) / 100 );
        Vector3 launchDirection = new Vector3(xRange,yRange,zRange);
        _rigidBody.AddForce(launchDirection * JonaUtility.EvaluateVector2(_lauchStrengthRange,(float)Random.Range(0,100) / 100 ));
    }
    
    void Update()
    {
        if (transform.position.y < -25f || transform.position.x < -100f || transform.position.x > 100f || transform.position.z < -5f)
            Destroy(gameObject);
    }
}
