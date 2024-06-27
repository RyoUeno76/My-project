using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftButton : MonoBehaviour
{
    public Button Leftbutton;
    public static bool Left = false;
    // Start is called before the first frame update
    void Start()
    {
        Leftbutton.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        left();
    }

    public static void right()
    {
        Left = false;
    }
    void left()
    {
        Left = true;
        RightButton.left();
        Debug.Log("Clicked");
        SceneManager.LoadScene("Left");
    }    
}
 