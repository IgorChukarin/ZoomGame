using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerOnScript : MonoBehaviour
{

    public GameObject endScreen;
    public GameObject mainMenuButton;
    public static bool buttonIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("SetEndScreenActive", 55);
        Invoke("SetMainMenuButtonActive", 58);
        buttonIsActive = true;
    }

    void SetEndScreenActive()
    {
        endScreen.SetActive(true);    
    }

    void SetMainMenuButtonActive()
    {
        mainMenuButton.SetActive(true);
    }
}
