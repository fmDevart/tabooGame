using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextCard()
    {
        UIManager.instance.CompleteCard();
    }
    public void SkipCard()
    {
        UIManager.instance.SkipCard();
    }
    public void toggleLang() {

        if (LocalizationManager.instance.currentLanguage == "it")
        {
            Debug.Log("togglo in en");
            LocalizationManager.instance.SetLanguage("en");
        }

        else if (LocalizationManager.instance.currentLanguage == "en")
        {
            Debug.Log("togglo in es");
            LocalizationManager.instance.SetLanguage("es");
        }
        else {
            Debug.Log("togglo in it");
            LocalizationManager.instance.SetLanguage("it");
        }
    }
}
