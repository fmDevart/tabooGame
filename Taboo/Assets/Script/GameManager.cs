using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Delegate per gestire l'evento di riempimento delle carte.
/// </summary>
public delegate void CardFilledEventHandler();

/// <summary>
/// Delegate per gestire l'evento di cambio categoria.
/// </summary>
public delegate void CatChangedEventHandler();

/// <summary>
/// Delegate per gestire l'evento di lettura completa.
/// </summary>
public delegate void ReadCompleteEventHandler();

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Un'istanza statica di GameManager accessibile da altre classi.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Evento statico generato quando le carte sono riempite.
    /// </summary>
    public static event CardFilledEventHandler CardsFilled;

    /// <summary>
    /// Evento statico generato quando la lettura ? completata.
    /// </summary>
    public static event ReadCompleteEventHandler ReadCompleteEvent;

    /// <summary>
    /// Evento statico generato quando la categoria ? cambiata.
    /// </summary>
    public static event CatChangedEventHandler CatChanged;

    [SerializeField]
    public Dictionary<string, List<Deck>> catAndDecks = new Dictionary<string, List<Deck>>();

    [SerializeField]
    public string selectedCat;


    [SerializeField]
    public Deck selectedDeck;

    public List<string> decks;

    [SerializeField]
    public int seconds;

    [SerializeField]
    public bool sound;

    [SerializeField]
    public int nPass;

    [SerializeField]
    public int maxScore;

    private void Awake()
    {
        Debug.Log(" [GAME MANAGER] creao istanza");

        if (instance == null)
        {
            instance = this;
        }
    }


    /// <summary>
    /// Funzione per aggiornare la lista dei deck al cambiare della categoria.
    /// </summary>
    /*public void UpdateCats()
    {
        if (!string.IsNullOrEmpty(selectedCat) && catAndDecks.ContainsKey(selectedCat) && catAndDecks.Count > 0)
        {
            if (oldCat != selectedCat)
            {
                oldCat = selectedCat;
                decks.Clear();

                foreach (Deck d in catAndDecks[selectedCat])
                {
                    decks.Add(d.name);
                }
                CatChanged?.Invoke();
                Debug.Log("quante volte chiamo questo evento");
            }
        }
        else
        {
            ClearAll();
        }
    }*/

    /// <summary>
    /// Aggiunge un deck alla categoria specificata.
    /// </summary>
    /// <param name="cat">La categoria in cui aggiungere il deck.</param>
    /// <param name="deck">Il deck da aggiungere.</param>
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

    /// <summary>
    /// Restituisce tutti i deck raggruppati per categoria.
    /// </summary>
    /// <returns>Un dizionario contenente i deck raggruppati per categoria.</returns>
    public Dictionary<string, List<Deck>> getAll()
    {
        return catAndDecks;
    }

    /// <summary>
    /// Restituisce la lista dei deck attualmente selezionati.
    /// </summary>
    /// <returns>La lista dei deck attualmente selezionati.</returns>
    public List<Deck> getDecks()
    {
        if (selectedCat != null)
        {
            return catAndDecks[selectedCat];
        }
        return null;
    }

    /// <summary>
    /// Cancella tutte le selezioni e opzioni.
    /// </summary>
    public void ClearAll()
    {
        decks.Clear();
        selectedCat = null;
        selectedDeck = null;
        CatChanged?.Invoke();
    }

    /// <summary>
    /// Imposta la categoria selezionata.
    /// </summary>
    /// <param name="cat">La nuova categoria da impostare. Pu? essere null</param>
    public void setCat(string cat)
    {
        selectedCat = cat;
        CatChanged?.Invoke();
    }

    /// <summary>
    /// Restituisce la categoria attualmente selezionata.
    /// </summary>
    /// <returns>La categoria attualmente selezionata.</returns>
    public string getSelectedCat()
    {
        return selectedCat;
    }

    /// <summary>
    /// Imposta il deck selezionato in base all'indice dell'array dei deck disponibili in base alla categoria selezionata
    /// </summary>
    /// <param name="i">L'indice del deck da impostare.</param>
    public void setDeck(int i)
    {
        selectedDeck = catAndDecks[selectedCat][i];
    }

    /// <summary>
    /// Restituisce il deck attualmente selezionato.
    /// </summary>
    /// <returns>Il deck attualmente selezionato.</returns>
    public Deck getSelectedDeck()
    {
        return selectedDeck;
    }

    /// <summary>
    /// Notifica che la lettura ? completa. CHIAMATO SOLO DA JSONREADER
    /// </summary>
    public void ReadComplete()
    {
        ReadCompleteEvent.Invoke();
    }






    


    public void SetSeconds(int sec){seconds = sec;}
    public int GetSeconds(){return seconds;}

    public void SetSound(bool hasSound) { sound = hasSound; }
    public bool GetSound() { return sound; }

    public void SetNPass(int n) { nPass = n; }
    public int GetNPass() { return nPass; }

   
    public void SetMaxScore(int score) { maxScore = score; }
    public int GetMaxScore() { return maxScore; }

}
