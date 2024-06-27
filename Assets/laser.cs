using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class laser : MonoBehaviour
{
    public float damage = 100f;
    public float range = 100;
    public float firerate = 10f;

    private float nexttimetofire;
    public GameObject fpsCam;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nexttimetofire)
        {
            nexttimetofire = Time.time + 1f / firerate;
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.take_damage(damage);
            }
        }
    }
}
