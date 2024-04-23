using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    private float time = 0;

    void Update()
    {
        if (time >= 5) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
