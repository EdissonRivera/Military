using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public Transform initialPosition;
    public Transform finPosition;
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(pos.x,pos.y,pos.z));
        
            if (transform.position.x > finPosition.position.x+5)
            {
            transform.position = new Vector3(initialPosition.position.x,transform.position.y,transform.position.z);
            }

            if(transform.position.x < initialPosition.position.x - 5)
            {
                transform.position = new Vector3(initialPosition.position.x, transform.position.y, transform.position.z);


            }

    }
}
