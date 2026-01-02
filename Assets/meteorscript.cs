using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    public GameObject Explosion;


    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);    
        Destroy(gameObject);
    }
}
