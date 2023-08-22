using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{


    public string[] cats;


    // Start is called before the first frame update
    void Start()
    {

        GameManager.CardsFilled += fill;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void fill()
    {

        cats = new List<string>(GameManager.instance.getAll().Keys).ToArray();


    }
}