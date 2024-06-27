using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class endlevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene("Level2");
    }
}
