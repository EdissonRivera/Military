using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombshell : MonoBehaviour
{
    public GameObject prefabExplosion;
    public void Explosion()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Bomb>().enabled = false;
        GetComponent<AudioSource>().enabled = true;
        Invoke("Destruction", 5);
    }

    public void Destruction()
    {
        Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
