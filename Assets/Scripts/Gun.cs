using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 100f;
    public float range = 100;
    public float firerate = 10f;
    public float boxhealth = 10f;
    public int currentammo = 0;
    public int maxammo = 30;
    public float reloadTime = 3f;
    public bool isReloading = false;

    private float nexttimetofire;
    public GameObject fpsCam;
    public GameObject MuzzleFlash;
    public Transform MuzzlePosition;
    public GameObject text;
    private Text Ammo;
    private Text _ammoText;
    public GameObject impactEffect;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if(currentammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nexttimetofire)
        {
            nexttimetofire = Time.time + 1f / firerate;
            Shoot();
        }
    }
    public int getAmmo()
    {
        return currentammo;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        currentammo = maxammo;
        isReloading = false;
    }
    void Shoot()
    {
        currentammo--;
        GameObject Flash = Instantiate(MuzzleFlash, MuzzlePosition);
        Destroy(Flash, 0.1f);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.take_damage(damage);
                Instantiate(impactEffect,
                hit.point + (hit.normal * .01f),
                Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(impactEffect);
            }
        }
    }
}

