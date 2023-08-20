using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void CardFilledEventHandler();
public delegate void SendCatsEventHandler();

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }


    public static event CardFilledEventHandler CardsFilled;
    public static event SendCatsEventHandler SendCats;

    [SerializeField]
    public Dictionary<string, List<Deck>> catAndDecks = new Dictionary<string, List<Deck>>();

    [SerializeField]
    public string selectedCat;

    public List<string> decks;

    private void Awake()
    {
        Debug.Log(" [GAME MANAGER] creao istanza");

        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GAMEMANAGER - avviato");
        SendCategories();
        UIManager.LoadDecks +=  OnValidate;
        
    


    }

    

    // Update is called once per frame
    void Update()
    {

    }


    


    public void OnValidate()
    {
        if (!string.IsNullOrEmpty(selectedCat) && catAndDecks.ContainsKey(selectedCat))
        {
            decks.Clear();

            foreach (Deck d in catAndDecks[selectedCat])
            {
                decks.Add(d.name);
            }


        }
        else
        {
            decks.Clear();
        }
    }

    public void addDeck(string cat, Deck deck)
    {
        if (catAndDecks.ContainsKey(cat))
        {
            catAndDecks[cat].Add(deck);
        }
        else
        {
            catAndDecks.Add(cat, new List<Deck> { deck });
        }


        CardsFilled?.Invoke();
    }
    public Dictionary<string, List<Deck>> getAll()
    {
        return catAndDecks;
    }


    public void SendCategories()
    {
        Debug.Log("INVIO LE CATEGORIE");
        SendCats?.Invoke();
    }



}