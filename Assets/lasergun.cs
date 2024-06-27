using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasergun : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            stopshooting();
        }
    }
    void shoot()
    {
        laser.SetActive(true);
    }
    void stopshooting()
    {
        laser.SetActive(false);
    }
}
