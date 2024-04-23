using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideMachine : MonoBehaviour
{
    [SerializeField] public float acceleration = 5800f;
    [SerializeField] public float maxSpeed = 90f;
    [SerializeField] private float brakePower = 2f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider box;
    [SerializeField] public float turnMobility = 60f;
    [SerializeField] public float gravStart = 1800f;
    private GameObject controller;

    private float currentYaw;
    private float currentPitch;
    private Vector3 ogBoxSize;
    private Vector3 ogBoxCen;
    private float length = 3f;
    private RaycastHit Hit;
    private float lastX = 0;
    private float grav;
    private float boostCurrent = 0;

    public float boostChargeMax;
    public float boostChargeRate;


    public bool defeated = false;


    void Start()
    {
        ogBoxCen = box.center;
        ogBoxSize = box.size;
        currentPitch = 0;
        currentYaw = 0;
        grav = gravStart;
        boostChargeMax = 300f;
        boostChargeRate = 200f;
        controller = GameObject.FindWithTag("Controller");
    }




    void Update()
    {
        if (!defeated)
        {
            Movement();

            Steering();
        }
        else
        {
            isDead();
        }
    }



    private void Movement()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = transform.forward * boostCurrent;
            box.size = ogBoxSize;
            box.center = ogBoxCen;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.magnitude <= maxSpeed)
            {
                rb.AddForce(transform.forward * acceleration * Time.deltaTime);
            }
            else
            {
                rb.AddForce(transform.forward * -acceleration * Time.deltaTime);
            }
            boostCurrent = 0;
            box.size = ogBoxSize;
            box.center = ogBoxCen;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (isNotGrounded())
            {
                var localVelocity = transform.InverseTransformDirection(rb.velocity);
                if (localVelocity.z > 0)
                {
                    rb.AddForce(transform.forward * -maxSpeed * brakePower * Time.deltaTime);
                }
            }
            else
            {
                if (boostCurrent <= boostChargeMax)
                {
                    boostCurrent += boostChargeRate * Time.deltaTime;
                }
            }
            box.size = new Vector3(1, 2, 1);
            box.center = new Vector3(0, 0, 0);
            rb.AddForce(-transform.up * 20);
        }
    }



    private void Steering()
    {
        float turnH = Input.GetAxis("Horizontal");
        float turnV = Input.GetAxis("Vertical");

        currentYaw = turnH * turnMobility;

        if (isNotGrounded())
        {
            currentPitch = turnV * turnMobility;
            grav += gravStart * Time.deltaTime;
            rb.AddForce(-Vector3.up * grav * Time.deltaTime);
        }
        else
        {
            grav = gravStart;
            rb.AddForce(-Vector3.up * grav * Time.deltaTime);
            currentPitch = 0;
        }

        Vector3 currentEuler = transform.rotation.eulerAngles;

        currentEuler.y += currentYaw * Time.deltaTime;

        if (currentEuler.x <= 320 && currentEuler.x >= 40)
        {
            if (lastX >= 300f)
            {
                currentEuler.x = 321;
            }
            else if (lastX <= 60f)
            {
                currentEuler.x = 39;
            }
        }
        else
        {
            currentEuler.x += currentPitch * Time.deltaTime;
        }
        currentEuler.z = 0;
        transform.rotation = Quaternion.Euler(currentEuler);

        lastX = currentEuler.x;
    }



    private bool isNotGrounded()
    {
        if(Physics.BoxCast(box.bounds.center, transform.localScale * 0.5f, -Vector3.up, out Hit, transform.rotation, length))
        {
            return false;
        }
        return true;
    }

    private void isDead()
    {
        rb.velocity = Vector3.zero;
        transform.Rotate(0,0, 120 * Time.deltaTime);
        controller.GetComponent<GameController>().defeated = true;
    }
}
