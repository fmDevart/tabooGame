using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public delegate void LoadDecksEventHandler();
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private GameObject title;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject voice;


    [SerializeField]
    private GameObject CardViewer;

    [SerializeField]
    private GameObject TabooWord;


    private GameObject[] generatedVoices;

    private GameObject SpawnedCard;
    private GameObject[] SpawnedTaboos = new GameObject[5];
    private List<int> AllowedIndex = new List<int>();




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameManager.ReadCompleteEvent += GenerateCategoriesUI;
        }

    }
    void Start()
    {
        Debug.Log("UIMANAGER - avviato");

        //EHI LOOK!
        //precendemente facevi: title.GetComponent<TMP_Text>().text = "Categoria";
        //Adesso devi fare:
        LocalizationManager.instance.SetLocalization(title.GetComponent<TMP_Text>(), "category");
        //Successivamente devi aggiungere la chiave (Se non presente) "category" al dizionario delle traduzioni"
        
        GameManager.CatChanged += createUI;


    }
    /// <summary>
    /// Crea l'interfaccia utente (UI) della selezione categoria/deck.
    /// </summary>
    private void createUI()
    {
        if (GameManager.instance.getSelectedCat() != null)
        {
            // EHI LOOK!
            // Precendentemente facevi: title.GetComponent<TMP_Text>().text = "Decks";
            // Adesso devi fare:
            LocalizationManager.instance.SetLocalization(title.GetComponent<TMP_Text>(), "decks");
            // Successivamente devi aggiungere la chiave (Se non presente) "decks" al dizionario delle traduzioni"
            ClearContent();
            ShowDecks();
        }
        else
        {
            // EHI LOOK!
            // Precendentemente facevi: title.GetComponent<TMP_Text>().text = "Categoria";
            // Adesso devi fare:
            LocalizationManager.instance.SetLocalization(title.GetComponent<TMP_Text>(), "category");
            // Successivamente devi aggiungere la chiave (Se non presente) "category" al dizionario delle traduzioni"
            ClearContent();
            GenerateCategoriesUI();
        }
    }


    /// <summary>
    /// Genera l'interfaccia utente delle categorie.
    /// </summary>
    private void GenerateCategoriesUI()
    {
        Debug.Log("[UI MANAGER] set categories");
        string[] categoriesToShow = new List<string>(GameManager.instance.getAll().Keys).ToArray();
       
        
        generatedVoices = new GameObject[categoriesToShow.Length];
        for (int i = 0; i < categoriesToShow.Length; i++)
        {
            Debug.Log("generate categories for " + categoriesToShow[i]);
            generatedVoices[i] = Instantiate(voice, transform);
            generatedVoices[i].transform.SetParent(this.content.transform);
            generatedVoices[i].transform.GetComponentInChildren<TMP_Text>().text = categoriesToShow[i];
            int index = i;
            generatedVoices[i].GetComponent<Button>().onClick.AddListener(() => GameManager.instance.setCat(categoriesToShow[index]));
        }
    }

    /// <summary>
    /// Cancella il contenuto dell'UI della scelta categorie/deck.
    /// </summary>
    private void ClearContent()
    {
        foreach (GameObject voic in generatedVoices)
        {
            Destroy(voic);
        }
    }

    /// <summary>
    /// Mostra i vari mazzi dopo aver scelto la categoria, premendo un mazzo parte la partita. 
    /// TODO implementare un tasto per l'avvio della partita non così. Si deve riutilizzare questo componente nelle impostazioni ergo non dover avviare sempre la partita.
    /// </summary>
    private void ShowDecks()
    {
        Debug.Log("generate Decks");
        generatedVoices = new GameObject[GameManager.instance.getDecks().Count];
        for (int i = 0; i < GameManager.instance.getDecks().Count; i++)
        {
            generatedVoices[i] = Instantiate(voice, transform);
            generatedVoices[i].transform.SetParent(this.content.transform);
            generatedVoices[i].transform.GetComponentInChildren<TMP_Text>().text = GameManager.instance.getDecks()[i].name;
            int deckIndex = i;
            generatedVoices[i].GetComponent<Button>().onClick.AddListener(() => StartMatch(deckIndex));
        }
    }

    /// <summary>
    /// Gestisce l'evento di clic su "Indietro".
    /// </summary>
    public void OnClickBack()
    {
        GameManager.instance.setCat(null);
    }

    /// <summary>
    /// Carica una carta specifica nell'UI.
    /// </summary>
    /// <param name="index">L'indice della carta da caricare nel deck selezionato.</param>
    private void LoadCard(int index)
    {
        SpawnedCard.GetComponentInChildren<TMP_Text>().text = GameManager.instance.getSelectedDeck().cards[index].obj;
        for (int i = 0; i < 5; i++)
        {
            if (SpawnedTaboos[i] != null)
            {
                Destroy(SpawnedTaboos[i]);
            }
            SpawnedTaboos[i] = Instantiate(TabooWord, transform);
            SpawnedTaboos[i].transform.SetParent(SpawnedCard.transform.GetChild(0).transform);
            SpawnedTaboos[i].GetComponent<TMP_Text>().text = GameManager.instance.getSelectedDeck().cards[index].taboos[i];
        }
    }
    /// <summary>
    /// Nasconde tutti gli elementi di UI che hanno il tag ToHideInGame.
    /// </summary>
    private void HideElementsInGame() {
        GameObject[] list = GameObject.FindGameObjectsWithTag("ToHideInGame");
        foreach(GameObject obj in list) {
            obj.SetActive(false);
        }
    }




    //TODO Spostare le successive funzioni nel manager della partita

    public void CompleteCard()
    {
        if (AllowedIndex.Count > 0)
        {
            LoadCard(GetNextCard());
        }

    }

    public void SkipCard()
    {
        if (AllowedIndex.Count > 0)
        {
            LoadCard(GetNextCard());
        }

    }

    private int GetNextCard()
    {
        int randomIndex = Random.Range(0, AllowedIndex.Count);
        int toReturn = AllowedIndex[randomIndex];
        AllowedIndex.RemoveAt(randomIndex);
        return toReturn;

    }

    //TODO questa andrebbe spostata nel game manager che istanzia il matchmanager che triggera l'evento per caricare la UI della partita o qualcosa del genere
    public void StartMatch(int deckIndex)
    {
        GameManager.instance.setDeck(deckIndex);
        ClearContent();
        title.SetActive(false);
        content.SetActive(false);

        HideElementsInGame();
        SpawnedCard = Instantiate(CardViewer, GameObject.FindWithTag("Canvas").transform);
        SpawnedCard.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        SpawnedCard.transform.SetAsFirstSibling();



        for (int i = 0; i < GameManager.instance.getSelectedDeck().cards.Length; i++)
        {
            AllowedIndex.Add(i);
        }

        LoadCard(GetNextCard());
    }
}


