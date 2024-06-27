using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class quit : MonoBehaviour
{
    public Button Quitbutton;
    // Start is called before the first frame update
    void Start()
    {
        Quitbutton.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        Quit();
    }
    void Quit()
    {
        Application.Quit();
    }
}