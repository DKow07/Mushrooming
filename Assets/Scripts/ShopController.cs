using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    /*
     * Standard Mushroom Picker id = 1  isBuyStandardMushroomPicker
     * Kaczka id = 2 isBuyDuckMushroomPicker
     * Pingwin id = 3 isBuyPenguinMushroomPicker
     * Wolf id = 4 isBuyWolfMushroomPicker
     * Alien id = 5 isBuyAlienMushroomPicker
     * Sprite id = 6 isBuySpriteMushroomPicker
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */

    public void ChooseStandardMushroomPicker()
    {
        SetMushroomPicker(1, "Kurczak", 5, 1f, 1f);
        GetComponent<MushroomsPickerManager>().standardMushroomPickerButtonChoose.SetActive(false);
    }

    //Kaczka
    public void ChooseDuckMushroomPicker()
    {
        SetMushroomPicker(2, "Kaczka", 5, 1.2f, 0.9f);
        GetComponent<MushroomsPickerManager>().duckMushroomPickerButtonChoose.SetActive(false);
    }

    public void BuyDuckMushroomPicker()
    {
        int bitcoin = PlayerPrefs.GetInt("bitcoin", 0);
        int cost = 150;
        if(bitcoin > cost)
        {
            bitcoin -= cost;
            PlayerPrefs.SetInt("bitcoin", bitcoin);
            PlayerPrefs.SetInt("isBuyDuckMushroomPicker", 1);
            GetComponent<MushroomsPickerManager>().CheckAvalaibleMushroomPicker();
            ActualizeScore();
        }
        else
            Debug.Log("Posiadasz za mało pieniędzy by kupić kaczkę");
    }

    //Pingwin
    public void ChoosePenguinMushroomPicker()
    {
        SetMushroomPicker(3, "Pingwin", 6, 0.8f, 1.5f);
        GetComponent<MushroomsPickerManager>().penguinMushroomPickerButtonChoose.SetActive(false);
    }

    public void BuyPenguinMushroomPicker()
    {
        int bitcoin = PlayerPrefs.GetInt("bitcoin", 0);
        int cost = 150;
        if (bitcoin > cost)
        {
            bitcoin -= cost;
            PlayerPrefs.SetInt("bitcoin", bitcoin);
            PlayerPrefs.SetInt("isBuyPenguinMushroomPicker", 1);
            GetComponent<MushroomsPickerManager>().CheckAvalaibleMushroomPicker();
            ActualizeScore();
        }
        else
            Debug.Log("Posiadasz za mało pieniędzy by kupić pingwina");
    }

    //Wilk
    public void ChooseWolfMushroomPicker()
    {
        SetMushroomPicker(4, "Wilk", 8, 1.5f, 1.5f);
        GetComponent<MushroomsPickerManager>().wolfMushroomPickerButtonChoose.SetActive(false);
    }

    public void BuyWolfMushroomPicker()
    {
        int bitcoin = PlayerPrefs.GetInt("bitcoin", 0);
        int cost = 150;
        if (bitcoin > cost)
        {
            bitcoin -= cost;
            PlayerPrefs.SetInt("bitcoin", bitcoin);
            PlayerPrefs.SetInt("isBuyWolfMushroomPicker", 1);
            GetComponent<MushroomsPickerManager>().CheckAvalaibleMushroomPicker();
            ActualizeScore();
        }
        else
            Debug.Log("Posiadasz za mało pieniędzy by kupić wilka");
    }

    //Kosmita
    public void ChooseAlienMushroomPicker()
    {
        SetMushroomPicker(5, "Kosmita", 9, 3f, 0.5f);
        GetComponent<MushroomsPickerManager>().alienMushroomPickerButtonChoose.SetActive(false);
    }

    public void BuyAlienMushroomPicker()
    {
        int bitcoin = PlayerPrefs.GetInt("bitcoin", 0);
        int cost = 750;
        if (bitcoin > cost)
        {
            bitcoin -= cost;
            PlayerPrefs.SetInt("bitcoin", bitcoin);
            PlayerPrefs.SetInt("isBuyAlienMushroomPicker", 1);
            GetComponent<MushroomsPickerManager>().CheckAvalaibleMushroomPicker();
            ActualizeScore();
        }
        else
            Debug.Log("Posiadasz za mało pieniędzy by kupić wilka");
    }

    //Sprite
    public void ChooseSpriteMushroomPicker()
    {
        SetMushroomPicker(6, "Sprite",15, 2f, 2f);
        GetComponent<MushroomsPickerManager>().spriteMushroomPickerButtonChoose.SetActive(false);
    }

    public void BuySpriteMushroomPicker()
    {
        int bitcoin = PlayerPrefs.GetInt("bitcoin", 0);
        int cost = 1000;
        if (bitcoin > cost)
        {
            bitcoin -= cost;
            PlayerPrefs.SetInt("bitcoin", bitcoin);
            PlayerPrefs.SetInt("isBuySpriteMushroomPicker", 1);
            GetComponent<MushroomsPickerManager>().CheckAvalaibleMushroomPicker();
            ActualizeScore();
        }
        else
            Debug.Log("Posiadasz za mało pieniędzy by kupić wilka");
    }

    private void ActualizeScore()
    {
        Text text = GetComponent<MushroomsPickerManager>().bitcoinValue;
        text.text = PlayerPrefs.GetInt("bitcoin", 0).ToString();
        GetComponent<MushroomsPickerManager>().CheckCurrentMushroomPicker();
    }

    public void SetMushroomPicker(int id, string name, int basket, float speed, float cutting)
    {
        PlayerPrefs.SetInt("currentMushroomPicker", id);
        PlayerPrefs.SetString("currentMushroomPickerName", name);
        PlayerPrefs.SetInt("currentMushroomPickerBasket", basket);
        PlayerPrefs.SetFloat("currentMushroomPickerSpeed", speed);
        PlayerPrefs.SetFloat("currentMushroomPickerCutting", cutting);
        GetComponent<MushroomsPickerManager>().CheckAvalaibleMushroomPicker();
        GetComponent<MushroomsPickerManager>().CheckCurrentMushroomPicker();
    }

}
