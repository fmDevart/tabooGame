using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckContainer : MonoBehaviour
{
    [SerializeField]
    public GameObject toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.CatChanged += Spawn;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn() 
    {
        Clear();
        for (int i = 0; i < GameManager.instance.getDecks().Count; i++)
        {
            
            float xOffset = 1.5f * i;
            Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y , transform.position.z);

            GameObject child = Instantiate(toSpawn, spawnPosition, Quaternion.identity, transform); 
            child.GetComponent<DeckSelector>().setIndex(i);
        }

    }

    private void Clear() 
    {
       
        // Ottieni tutti i figli del GameObject corrente.
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // Distruggi tutti i figli.
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
  
    }
}
