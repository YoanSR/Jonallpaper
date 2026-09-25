using UnityEngine;

public class JonaScript : MonoBehaviour
{

    void Update()
    {
        if (transform.position.y < -25f || transform.position.x < -100f || transform.position.x > 100f || transform.position.z < -5f)
            Destroy(gameObject);
    }
}
