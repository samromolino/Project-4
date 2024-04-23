using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject t;

    void FixedUpdate()
    {
        transform.LookAt(target.transform);
        float carMove = Mathf.Abs(Vector3.Distance(transform.position, t.transform.position));
        transform.position = Vector3.Lerp(transform.position, t.transform.position, carMove * Time.deltaTime);
    }
}
