using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RightButton : MonoBehaviour
{
    public Button Rightbutton;
    public static bool Right = false;
    // Start is called before the first frame update
    void Start()
    {
        Rightbutton.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        right();
    }
    public static void left()
    {
        Right = false;
    }
    void right()
    {
        Right = true;
        LeftButton.right();
        Debug.Log("Clicked");
        SceneManager.LoadScene("Right");
    }
}
