using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressureplate2 : MonoBehaviour
{

    [SerializeField]
    GameObject door;

    bool isOpened = false;
    //private void OnTriggerEnter(Collider col)
    //{
        
    //}
    //private void OnTriggerExit(Collider col)
    //{
    //    if (isOpened)
    //    {
    //        isOpened = false;
    //        door.transform.position += new Vector3(0, 0, 0);
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (!isOpened)
        {
            isOpened = true;
            door.transform.position += new Vector3(0, 4, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isOpened)
        {
            isOpened = false;
            door.transform.position += new Vector3(0, -4, 0);
        }
    }
}
