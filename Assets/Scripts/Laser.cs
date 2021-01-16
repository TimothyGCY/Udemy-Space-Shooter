using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int _laserSpd = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _laserSpd * Time.deltaTime);

        if (transform.position.y > 7) {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            } else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
