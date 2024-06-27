using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulleto : MonoBehaviour
{
    public float damage = 30;
    public void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("L");
        playermanager target = collision.transform.gameObject.GetComponent<playermanager>();
        if(collision.gameObject.tag == "Player")
        {
            target.take_damage(damage);
            Debug.Log("W");
        }
        Destroy(gameObject);
    }
}
 