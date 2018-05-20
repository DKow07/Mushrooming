using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{

    public int currentCountOfMushrooms;
    public int maxCountOfMushrooms;
    public List<GameObject> mushroomsInBasket;
    public List<Mushroom> mushroomsDataInBasket;
    [HideInInspector]
    public bool canGather;
    public Text mushroomsInBasketText;
    public int leavedTime; //czy nie powinno być w carController?


	void Start ()
    {
        canGather = true;
        currentCountOfMushrooms = 0;
        mushroomsInBasketText = GameObject.FindGameObjectWithTag("MushroomsInBasketText").GetComponent<Text>();
        if (mushroomsInBasketText != null)
            mushroomsInBasketText.text = GetMushroomsInBasketText();
        else
            Debug.Log("MushroomsInBasketText is null");
        mushroomsDataInBasket = new List<Mushroom>();
	}

    void Update()
    {
        if(currentCountOfMushrooms == maxCountOfMushrooms)
        {
            canGather = false;
        }
    }

    public string GetMushroomsInBasketText()
    {
        return currentCountOfMushrooms + "/" + maxCountOfMushrooms;
    }

    public void AddMushroomToBasket(GameObject mushroom)
    {
        if(currentCountOfMushrooms < maxCountOfMushrooms)
        {
            currentCountOfMushrooms += mushroom.GetComponentInChildren<MushroomController>().mushroomVolume;
            mushroomsInBasket.Add(mushroom);
            mushroomsInBasketText.text = GetMushroomsInBasketText();
            Debug.Log("Pomyślnie dodano grzybek");
        }
        else
        {
            canGather = false;

            Debug.Log("Pełny koszyk, nie możesz zebrać więcej grzybów");
        }
    }
	
    public void AddMushroomData(Mushroom mushroom)
    {
        mushroomsDataInBasket.Add(mushroom);
    }
}
