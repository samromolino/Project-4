using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluebox : MonoBehaviour
{
    private GameObject player;

    private GameObject controller;

    private bool spawning = true;

    private float grav = 10f;
      
    private Rigidbody rb;

    private float rotateSpeedMax;

    private float xSpeed;
    private float ySpeed;
    private float zSpeed;

    private void Start() 
    {
        rotateSpeedMax = 360f;
        xSpeed = (Random.Range(-rotateSpeedMax, rotateSpeedMax));
        ySpeed = (Random.Range(-rotateSpeedMax, rotateSpeedMax));
        zSpeed = (Random.Range(-rotateSpeedMax, rotateSpeedMax));
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        controller = GameObject.FindWithTag("Controller");
    }

    private void Update()
    {
        if (spawning)
        {
            rb.AddForce(-Vector3.up * grav);
            transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<RideMachine>().acceleration += 200f;
            player.GetComponent<RideMachine>().maxSpeed += 2f;
            player.GetComponent<RideMachine>().turnMobility += 5f;
            player.GetComponent<RideMachine>().gravStart *= .92f;
            player.GetComponent<RideMachine>().boostChargeMax += 10f;
            player.GetComponent<RideMachine>().boostChargeRate += 10f;
            controller.GetComponent<GameController>().collectedBoxes++;
            Destroy(this.gameObject);
        }
        else
        {
            rb.velocity = Vector3.zero;
            Vector3 currentEuler = transform.rotation.eulerAngles;
            float xRad = Mathf.Cos(currentEuler.x * Mathf.Deg2Rad) * 2;
            float zRad = Mathf.Cos(currentEuler.z * Mathf.Deg2Rad) * 2;
            float tiltHeight = xRad + zRad;
            currentEuler.x = 0f;
            currentEuler.z = 0f;
            transform.rotation = Quaternion.Euler(currentEuler);
            float height;
            if (other.tag != "Building")
            {
                height = -3f;
            }
            else
            {
                height = 7f;
            }
            transform.position = new Vector3(transform.position.x, other.gameObject.transform.localScale.y + height, transform.position.z);
            spawning = false;
        }
    }
}
