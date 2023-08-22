using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static MatchManager instance;
    public float sec = 0;
    public int index = 0;
    public bool inGame = true;
    public bool inPause = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //LoadSettings();
        
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        
        // Controlla se è passato un secondo
        if (sec >= 1f)
        {
            // Incrementa l'indice
            index++;

            // Resetta il timer
            sec = 0f;

            // Stampa l'indice
            Debug.Log("Indice: " + index);
        }

    }
    public void Play() {

    }
}
