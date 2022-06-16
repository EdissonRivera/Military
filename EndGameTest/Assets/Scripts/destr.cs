using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destr : MonoBehaviour
{

    private void Start()
    {
        Invoke("Destruccion", 2);
    }

    public void Destruccion()
    {
        Destroy(gameObject);
    }
}
