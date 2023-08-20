using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public delegate void LoadDecksEventHandler();
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static event LoadDecksEventHandler LoadDecks;

    [SerializeField]
    public string[] categoriesToShow;

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
    private bool LoadedDecks = false;

    private GameObject SpawnedCard;
    private GameObject[] SpawnedTaboos = new GameObject[5];
    private Deck SelectedDeck;
    private List<int> AllowedIndex = new List<int>();




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameManager.SendCats += SetCategoriesToShow;
        }

    }
    void Start()
    {
        Debug.Log("UIMANAGER - avviato");
        title.GetComponent<TMP_Text>().text = "Categoria";


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.decks.Count > 0 && LoadedDecks == false)
        {
            title.GetComponent<TMP_Text>().text = "Decks";
            ClearContent();
            ShowDecks();
            LoadedDecks = true;
        }
        else if (GameManager.instance.decks.Count == 0 && LoadedDecks == true)
        {
            title.GetComponent<TMP_Text>().text = "Categoria";

            ClearContent();
            GenerateCategoriesUI();
            LoadedDecks = false;
        }



    }



    private void SetCategoriesToShow()
    {

        Debug.Log("[UI MANAGER] set categories");
        categoriesToShow = new List<string>(GameManager.instance.getAll().Keys).ToArray();
        generatedVoices = new GameObject[categoriesToShow.Length];
        GenerateCategoriesUI();
    }

    private void GenerateCategoriesUI()
    {

        generatedVoices = new GameObject[categoriesToShow.Length];

        for (int i = 0; i < categoriesToShow.Length; i++)
        {

            Debug.Log("generate categories for " + categoriesToShow[i]);
            generatedVoices[i] = Instantiate(voice, transform);
            generatedVoices[i].transform.SetParent(this.content.transform);
            generatedVoices[i].transform.GetComponentInChildren<TMP_Text>().text = categoriesToShow[i];
            int index = i;
            generatedVoices[i].GetComponent<Button>().onClick.AddListener(() => OnClickCatUI(index));
        }

    }

    private void OnClickCatUI(int index)
    {
        Debug.Log("Selezionata categoria " + categoriesToShow[index]);
        GameManager.instance.selectedCat = categoriesToShow[index];
        LoadDecks.Invoke();

    }

    private void ClearContent()
    {
        foreach (GameObject voic in generatedVoices)
        {
            Destroy(voic);
        }

    }



    private void ShowDecks()
    {
        Debug.Log("generate Decks");

        generatedVoices = new GameObject[GameManager.instance.decks.Count];
        for (int i = 0; i < GameManager.instance.decks.Count; i++)
        {


            generatedVoices[i] = Instantiate(voice, transform);
            generatedVoices[i].transform.SetParent(this.content.transform);
            generatedVoices[i].transform.GetComponentInChildren<TMP_Text>().text = GameManager.instance.decks[i];
            int deckIndex = i;
            generatedVoices[i].GetComponent<Button>().onClick.AddListener(() => StartMatch(deckIndex));




        }

    }

    public void OnClickBack()
    {
        GameManager.instance.selectedCat = null;
        GameManager.instance.OnValidate();
    }

    public void StartMatch(int deckIndex)
    {
        ClearContent();
        title.SetActive(false);
        content.SetActive(false);
        GameObject.FindWithTag("BackBtn").gameObject.SetActive(false);
        SpawnedCard = Instantiate(CardViewer, GameObject.FindWithTag("Canvas").transform);
        SpawnedCard.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        SpawnedCard.transform.SetAsFirstSibling();

        SelectedDeck = GameManager.instance.catAndDecks.GetValueOrDefault(GameManager.instance.selectedCat)[deckIndex];

        for (int i = 0; i < SelectedDeck.cards.Length; i++)
        {
            AllowedIndex.Add(i);
        }

        LoadCard(GetNextCard());
    }

    private void LoadCard(int index)
    {

        SpawnedCard.GetComponentInChildren<TMP_Text>().text = SelectedDeck.cards[index].obj;
        for (int i = 0; i < 5; i++)
        {
            if (SpawnedTaboos[i] != null)
            {
                Destroy(SpawnedTaboos[i]);
            }
            SpawnedTaboos[i] = Instantiate(TabooWord, transform);
            SpawnedTaboos[i].transform.SetParent(SpawnedCard.transform.GetChild(0).transform);
            SpawnedTaboos[i].GetComponent<TMP_Text>().text = SelectedDeck.cards[index].taboos[i];
        }



    }

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
}


