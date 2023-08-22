using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;


public class JSONReader : MonoBehaviour
{
    private string mainDir = "Cards";
   
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log("leggo carte");
        string folderPath = Application.streamingAssetsPath + "/" +  mainDir;

        if (Directory.Exists(folderPath))
        {
            string[] categories = Directory.GetDirectories(folderPath);

            foreach (string category in categories)
            {
                string catName = Path.GetFileName(category);



                string[] decks = Directory.GetFiles(category, "*.json");
                foreach (string deck in decks)
                {
       

                    Deck deckTmp = JsonUtility.FromJson<Deck>(File.ReadAllText(deck));
                    deckTmp.name = Path.GetFileNameWithoutExtension(deck);

                    GameManager.instance.addDeck(catName, deckTmp);
                }
                
            }
            GameManager.instance.ReadComplete();
        }
        else
        {
            Debug.LogWarning("La cartella non esiste negli Streaming Assets.");
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
