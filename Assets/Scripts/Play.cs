using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void OnClick()
    {
        if(RightButton.Right || LeftButton.Left) 
        {
            play();
        } else
        {
            SceneManager.LoadScene("Oops");
        }

    }

    void play()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("First Level");
    }
}