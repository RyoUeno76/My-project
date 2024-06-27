using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteText : MonoBehaviour
{
    public GameObject Help1;
    public GameObject Help2;
    private void OnTriggerEnter(Collider col)
    {
        Help1.SetActive(false);
        Help2.SetActive(true);
    }
}
