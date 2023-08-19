using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelector : MonoBehaviour
{
    [SerializeField]
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager.instance.setDeck(this.index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setIndex(int index) {
        this.index = index;
    }
}
