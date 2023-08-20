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
}
