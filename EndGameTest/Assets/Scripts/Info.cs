using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Info : MonoBehaviour
{
    public TextMeshProUGUI tmInfo;
    public string textInfo;
    public string textInfoExit;
    public bool instance;
    public GameObject objOn;
    public bool destroy;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("entra player");
            tmInfo.text = textInfo;
            if (instance)
            {
                objOn.SetActive(true);
            }
            if (destroy)
            {
                Destroy(objOn);
            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tmInfo.text = textInfoExit;
        }
    }
}
