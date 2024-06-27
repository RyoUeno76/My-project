using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    public GameObject Help1;
    private void OnTriggerEnter(Collider col)
    {
        Help1.SetActive(false);
    }
}

