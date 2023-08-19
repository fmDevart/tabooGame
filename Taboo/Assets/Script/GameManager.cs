using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void CardFilledEventHandler();
public delegate void CatChangedEventHandler();
public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }


    public static event CardFilledEventHandler CardsFilled;
    public static event CatChangedEventHandler CatChanged;


    [SerializeField]
    public Dictionary<string, List<Deck>> catAndDecks = new Dictionary<string, List<Deck>>();

    [SerializeField]
    public string selectedCat;

    private string oldCat;

    [SerializeField]
    public Deck selectedDeck;

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

        UpdateCats();

    }

    //Funzione per aggiornare la lista dei deck al cambiare della categoria
    private void UpdateCats()
    {
        if (!string.IsNullOrEmpty(selectedCat) && catAndDecks.ContainsKey(selectedCat) && catAndDecks.Count > 0)
        {
            if (oldCat != selectedCat) {
                oldCat = selectedCat;
                decks.Clear();

                foreach (Deck d in catAndDecks[selectedCat])
                {
                    decks.Add(d.name);
                }
                CatChanged?.Invoke();


            }
            

        }
        else
        {
            ClearAll();
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
    
    public List<Deck> getDecks()
    {
        return catAndDecks[selectedCat];
    }
    public void ClearAll() 
    {
        decks.Clear();
        selectedCat = null;
        selectedDeck = null;
        CatChanged?.Invoke();
    }
    public void setDeck(int i) {

        selectedDeck = catAndDecks[selectedCat][i];
    }

    


}