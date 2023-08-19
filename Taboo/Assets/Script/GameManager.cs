using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void CardFilledEventHandler();
public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }


    public static event CardFilledEventHandler CardsFilled;

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

    }

    // Update is called once per frame
    void Update()
    {

        if(catAndDecks.Count > 0 && !string.IsNullOrEmpty(selectedCat))
        {
            OnValidate();
        }

    }

    //Funzione per aggiornare la lista dei deck al cambiare della categoria
    private void OnValidate()
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



}