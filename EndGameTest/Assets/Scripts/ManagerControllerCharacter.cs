using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ManagerControllerCharacter : MonoBehaviour
{
    //component
    [SerializeField]
    private CharacterController _charController;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ManagerJoystick _mngrJoystick;
    [SerializeField]
    private ManagerContRot _mngrContRot;
    private Transform meshPlayer;

    //AnimationReverse
    private Animation animShootWalk;

    //move
    private float inputX;
    private float inputZ;
    private Vector3 vel_movement;
    private Vector3 pos_Rotation;
    [SerializeField]
    private float moveSpeed=0.1f;
    private float gravity;
    private bool _shoot;

    //Statistics
    public float _health;
    public Image img_Health;
    public TextMeshProUGUI text_Health;
    public int coint;
    public TextMeshProUGUI textCoint;



    //Inventory
    public Inventory inventory;
    public GameObject Hand;
    public Shoot army;
    void Start()
    {
        moveSpeed = 0.1f;
        gravity = 0.5f;
        meshPlayer = _charController.transform.GetChild(0);
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        if (item.Name == "Weapon")
        {
            Hand.transform.GetChild(0).gameObject.SetActive(true);
            Hand.transform.GetChild(1).gameObject.SetActive(false);
            Hand.transform.GetChild(2).gameObject.SetActive(false);
            army.nameArmy = "Weapon";
        }

        if (item.Name == "Axe")
        {
            Hand.transform.GetChild(0).gameObject.SetActive(false);
            Hand.transform.GetChild(1).gameObject.SetActive(true);
            Hand.transform.GetChild(2).gameObject.SetActive(false);
            army.nameArmy = "Axe";

        }

        if (item.Name == "FireGun")
        {
            Hand.transform.GetChild(0).gameObject.SetActive(false);
            Hand.transform.GetChild(1).gameObject.SetActive(false);
            Hand.transform.GetChild(2).gameObject.SetActive(true);
            army.nameArmy = "FireGun";

        }


        //goItem.SetActive(true);

        //goItem.transform.parent = Hand.transform;
        //goItem.transform.position = Hand.transform.position;
    }

    void Update()
    {
        inputX = _mngrJoystick.inputHorizontal();
        inputZ = _mngrJoystick.inputVertical();

        if (inputX == 0 && inputZ == 0)
        {
            _animator.SetBool("Walk", false);
        }
        else
        {
            _animator.SetBool("Walk", true);
        } 
    }


    private void FixedUpdate()
    {
        if (_charController.isGrounded)
        {
            vel_movement.y = 0;
        }
        else
        {
            vel_movement.y -= gravity * Time.deltaTime;
        }
        //Movement
        vel_movement = new Vector3(inputX * moveSpeed, vel_movement.y, inputZ * moveSpeed);
        
        _charController.Move(vel_movement);
        //mesh rotate

        if ((_mngrContRot.inputHorizontal() != 0 || _mngrContRot.inputVertical() != 0))
        {
            //Debug.Log("VELOCIDAD " + _mngrContRot.inputHorizontal() + "aa" + _mngrContRot.inputVertical());
            pos_Rotation = new Vector3(_mngrContRot.inputHorizontal(), 0, _mngrContRot.inputVertical());
            meshPlayer.rotation = Quaternion.LookRotation(pos_Rotation);
            _animator.SetBool("Shoot", true);
            moveSpeed = 0.05f;
            _shoot = true;
            //Shoot();

        }
        else
        {
            _shoot = false;
            _animator.SetBool("Shoot", false);
            moveSpeed = 0.1f;
        }

        if (_shoot == false)
        {
            if (inputX != 0 || inputZ != 0)
            {
                Vector3 lookDir = new Vector3(vel_movement.x, 0, vel_movement.z);
                meshPlayer.rotation = Quaternion.LookRotation(lookDir);
                moveSpeed = 0.1f;
            }
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "House" || other.tag == "Sheriff")
        {
            Debug.Log("entro a la casa");
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (other.tag == "bulletEnemy")
        {
            TakeDamage(10);
            Debug.Log("disparoEnemy");
        }

        if(other.tag == "Coint")
        {
            coint++;
            textCoint.text = coint.ToString();
            Destroy(other.gameObject);
        }

        if (other.tag == "PotionHealth")
        {
            PotionHealth();
            Destroy(other.gameObject);
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "House" || other.tag =="Sheriff")
        {
            Debug.Log("entro a la casa");
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            img_Health.fillAmount = _health / 100;
            text_Health.text = "Health: " + _health.ToString();
            if (_health <= 0)
            {
                //Instantiate(Coint, transform.position, Quaternion.identity);
                Invoke(nameof(DestroyEnemy), 0.5f);
            }
        }
    }


    public void PotionHealth()
    {
        if (_health < 100)
        {
            _health = _health + 20;
            text_Health.text = "Health: " + _health.ToString();


        }

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }


}
