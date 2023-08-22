using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    /// <summary>
    /// Un'istanza statica di LocalizationManager accessibile da altre classi.
    /// </summary>
    public static LocalizationManager instance { get; private set; }

    [SerializeField]
    public string currentLanguage = "en"; // Imposta la lingua predefinita

    Dictionary<string, Dictionary<string, string>> translations; // Dizionario con traduzioni per lingua

    /// <summary>
    /// Inizializzazione all'avvio dell'oggetto.
    /// </summary>
    private void Awake()
    {
        Debug.Log(" [Localization MANAGER] creao istanza");
        if (instance == null)
        {
            instance = this;
        }

        // Inizializza il dizionario delle traduzioni, caricando i dati dai file o da risorse
        // per tutte le lingue supportate.
        LoadTranslations();
    }

    /// <summary>
    /// Restituisce la lingua corrente.
    /// </summary>
    /// <returns>La lingua corrente.</returns>
    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }

    /// <summary>
    /// Imposta la lingua corrente e aggiorna tutti i testi in base alla nuova lingua.
    /// </summary>
    /// <param name="newLanguage">La nuova lingua da impostare.</param>
    public void SetLanguage(string newLanguage)
    {

        currentLanguage = newLanguage;

        // Aggiorna tutti i testi in base alla nuova lingua
        UpdateAllTexts();
    }

    /// <summary>
    /// Carica le traduzioni da un file JSON.
    /// </summary>
    private void LoadTranslations()
    {
        // Ottieni il percorso completo del file JSON delle traduzioni in StreamingAssets
        string filePath = Application.streamingAssetsPath + "/" + "Localization.json";

        // Verifica se il file esiste
        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonTranslations = File.ReadAllText(filePath);


            // Parsa il JSON e popola il dizionario delle traduzioni
            translations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonTranslations);


        }
        else
        {
            Debug.LogError("File delle traduzioni non trovato: " + filePath);
        }
    }

    /// <summary>
    /// Ottiene la traduzione per una chiave specifica nella lingua corrente.
    /// </summary>
    /// <param name="key">La chiave di traduzione.</param>
    /// <returns>La traduzione corrispondente o la chiave stessa se la traduzione non è disponibile.</returns>
    public string GetTranslation(string key)
    {


        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            return translations[currentLanguage][key];
        }
        else
        {
            // Se la traduzione non è disponibile, ritorna la chiave stessa
            return key;
        }
    }

    /// <summary>
    /// Aggiorna tutti i testi del gioco in base alla lingua corrente.
    /// </summary>
    private void UpdateAllTexts()
    {
        // Ottieni una lista di tutti gli oggetti da tradurre nel gioco, il tag viene aggiunto automaticamente dalla funzione SetLocalization
        GameObject[] toTranslate = GameObject.FindGameObjectsWithTag("ToTranslate");

        foreach (GameObject obg in toTranslate)
        {
            TMP_Text tmp_Text = obg.GetComponent<TMP_Text>();


            string translationKey = obg.GetComponent<LocationKey>().key;


            string translatedText = GetTranslation(translationKey);


            tmp_Text.text = translatedText;


        }
    }

    /// <summary>
    /// Imposta il testo iniziale per un oggetto TMP_Text e aggiunge un componente LocationKey se necessario.
    /// </summary>
    /// <param name="tmp_text">L'oggetto TMP_Text da tradurre.</param>
    /// <param name="key">La chiave di traduzione.</param>
    public void SetLocalization(TMP_Text tmp_text, string key)
    {
        tmp_text.text = GetTranslation(key);

        if (tmp_text.GetComponentInParent<LocationKey>(true) == null)
        {
            tmp_text.gameObject.AddComponent<LocationKey>();
        }

        tmp_text.GetComponentInParent<LocationKey>(true).key = key;

        tmp_text.gameObject.tag = "ToTranslate";
    }
}
