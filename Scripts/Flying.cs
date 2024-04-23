using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb; 
    
    [SerializeField]
    private float speed = 100;

    [SerializeField] 
    private GameObject fireballPrefab;

    private GameObject controller;

    private GameObject player;

    private GameObject fireball;

    private float timer = 0f;

    public int shootLeft = 1;

    public float xPos;

    private

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        controller = GameObject.FindWithTag("Controller");
    }
    void Update()
    {
        xPos = transform.position.x;
        if (transform.position.x > 500 || transform.position.x < -500)
        {
            speed = -speed;
            transform.Rotate(0, transform.rotation.y + 180, 0);
        }
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

        timer += Time.deltaTime;
        if (timer >= 1)
        {
            fireball = Instantiate(fireballPrefab) as GameObject;
            fireball.transform.position = transform.position;
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            controller.GetComponent<GameController>().dynaDown = true;
            Destroy(this.gameObject);
        }
    }
}
