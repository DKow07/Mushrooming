using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager : MonoBehaviour 
{

    /* type     key
     * (int)    borowik
     * (int)    podgrzybek
     * (int)    kania
     * (int)    kurka
     */


    public GameObject atlasItemBorowik;
    public GameObject atlasItemKurka;
    public GameObject atlasItemPodgrzybek;
    public GameObject atlasItemKania;

    public GameObject atlasItemBorowikEmpty;
    public GameObject atlasItemKurkaEmpty;
    public GameObject atlasItemPodgrzybekEmpty;
    public GameObject atlasItemKaniaEmpty;

    public static bool isDebugMode;


    void Start()
    {
        CheckMushroomsAtlas();
        GetMoney(3500);
    }

    public void GetMoney(int money)
    {
        PlayerPrefs.SetInt("bitcoin", money);
    }

    public static void EnableDebugMode()
    {
        isDebugMode = true;
    }

     public static void DisableDebugMode()
    {
        isDebugMode = false;
    }

    public void ChangeDebugMode()
     {
         isDebugMode = !isDebugMode;
     }

    void CheckMushroomsAtlas()
    {
        int isKaniaEnabled = PlayerPrefs.GetInt("kania", 0);
        int isPodgrzybekEnabled = PlayerPrefs.GetInt("podgrzybek", 0);
        int isBorowikEnabled = PlayerPrefs.GetInt("borowik", 0);
        int isKurkaEnabled = PlayerPrefs.GetInt("kurka", 0);

        EnableItemAtlas(atlasItemKania, atlasItemKaniaEmpty, isKaniaEnabled);
        EnableItemAtlas(atlasItemBorowik, atlasItemBorowikEmpty, isBorowikEnabled);
        EnableItemAtlas(atlasItemKurka, atlasItemKurkaEmpty, isKurkaEnabled);
        EnableItemAtlas(atlasItemPodgrzybek, atlasItemPodgrzybekEmpty, isPodgrzybekEnabled);

    }

    void EnableItemAtlas(GameObject obj, GameObject objEmpty, int var )
    {
        if (var == 1)
        {
            obj.SetActive(true);
            objEmpty.SetActive(false);
        }
        else
        {
            obj.SetActive(false);
            objEmpty.SetActive(true);
        }
    }

}
