using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    float inputX, inputZ;
 
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        if (inputX != 0)
            rotate();
        if (inputZ != 0)
            rotate();
    }
    private void rotate()
    {
        transform.Rotate(new Vector3(0f, inputX * Time.deltaTime * 2, 0f));
        transform.Rotate(Vector3.up * inputZ * Time.deltaTime);
    }

}
