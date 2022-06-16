using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private ManagerControllerCharacter _mngrController;

    //prefab
    public GameObject projectile;
    public Transform refProyectile;
    public string nameArmy;
    public Transform refFireGun;
    public bool fire;
    public AudioSource managerAudio;
    public AudioClip audioProyectile;
    public AudioClip audioFire;

    private void Update()
    {
        //Debug.Log(refProyectile.gameObject.activeSelf);
    }
    public void ShootOn()
    {
        if (refProyectile.parent.gameObject.activeSelf)
        {
            Rigidbody rb = Instantiate(projectile, refProyectile.transform.position, refProyectile.transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(refProyectile.forward * 40f, ForceMode.Impulse);
            rb.AddForce(refProyectile.up * 3f, ForceMode.Impulse);
            managerAudio.clip = audioProyectile;
            managerAudio.loop = true;

            managerAudio.Play();
        }

    }


    public void FireGunOn()
    {
        if (fire == false)
        {
            if (refFireGun.gameObject.activeSelf)
            {

                refFireGun.GetChild(2).gameObject.SetActive(true);
                refFireGun.GetChild(3).gameObject.SetActive(true);
                managerAudio.clip = audioFire;
                managerAudio.Play();
                managerAudio.loop = true;
                fire = true;

            }
        }
    }

    public void FireGunOff()
    {

        if (fire == true)
        {

            refFireGun.GetChild(2).gameObject.SetActive(false);
            refFireGun.GetChild(3).gameObject.SetActive(false);

            fire = false;
        }
        managerAudio.loop = false;
        managerAudio.Stop();

    }

}
