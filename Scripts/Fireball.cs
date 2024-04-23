using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject player;
    private GameObject dynablade;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject explodePrefab;

    [SerializeField] private GameObject boomPrefab;

    private GameObject explosion;
    private GameObject boom;
    private float grav = 8000f;
    private float zSpeed;
    private int xSpeed;
    private int direction;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        dynablade = GameObject.FindWithTag("Enemy");

        boom = Instantiate(boomPrefab) as GameObject;
        boom.SetActive(false);

        direction = dynablade.GetComponent<Flying>().shootLeft;
        zSpeed = (Random.Range(0, 500));
        xSpeed = (Random.Range(-30, 30));
        rb.velocity = new Vector3(0, 300, zSpeed * direction);
        dynablade.GetComponent<Flying>().shootLeft = -direction;
    }

    void Update()
    {
        rb.AddForce(-Vector3.up * grav * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
        {
            RaycastHit[] hit = Physics.SphereCastAll(transform.position, 20f, -transform.up);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.tag == "Player")
                {
                    player.GetComponent<RideMachine>().defeated = true;
                }
            }
            boom.transform.position = transform.position;
            boom.SetActive(true);
            explosion = Instantiate(explodePrefab) as GameObject;
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            Destroy(this.gameObject);
        }
    }
}
