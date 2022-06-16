using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RocksObstacule : MonoBehaviour
{

    public TextMeshProUGUI textInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bomb")
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("bomba");
            Invoke("Off", 2);
        }
    }
    public void Off()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("entra player");
            textInfo.text = "Busca en la estacion de Sheriff la bomba para desbloquear el camino"; 
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textInfo.text = "INFO";
        }
    }
}
