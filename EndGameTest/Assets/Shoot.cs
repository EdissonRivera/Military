using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private ManagerControllerCharacter _mngrController;

    //prefab
    public GameObject projectile;
    public Transform refProyectile;
    private void Update()
    {
        Debug.Log(refProyectile.gameObject.activeSelf);
    }
    public void ShootOn()
    {
        if (refProyectile.parent.gameObject.activeSelf)
        {
            Rigidbody rb = Instantiate(projectile, refProyectile.transform.position, refProyectile.transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(refProyectile.forward * 40f, ForceMode.Impulse);
            rb.AddForce(refProyectile.up * 3f, ForceMode.Impulse);
        }
    }


}
