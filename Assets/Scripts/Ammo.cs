using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public GameObject player;
    public int ammo;
    private float nexttimetofire;
    public float firerate = 10f;
    public bool isReloading = false;
    public int currentammo = 0;
    public int maxammo = 30;
    public float reloadTime = 3f;
    //[SerializeField]
    public Text _ammoText;
    public Text reloadText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Gun>();
        _ammoText.text = "Reloading";
        _ammoText.text = 30.ToString();
        reloadText.gameObject.SetActive(true);
        _ammoText.gameObject.SetActive(true);
    }
    IEnumerator Reload()
    {
        reloadText.gameObject.SetActive(true);
        _ammoText.gameObject.SetActive(false);
        isReloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        currentammo = maxammo;
        isReloading = false;
        _ammoText.text = currentammo.ToString();
        _ammoText.gameObject.SetActive(true);
        reloadText.gameObject.SetActive(false);
    }
    public void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (currentammo <= 0)
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
    public void Shoot()
    {
        currentammo--;
        _ammoText.text = currentammo.ToString();
    }
    // Update is called once per frame
}
 