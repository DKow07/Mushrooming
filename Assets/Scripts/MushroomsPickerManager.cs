using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class MushroomsPickerManager : PunBehaviour
{

    public Text bitcoinValue;

    public GameObject standardMushroomPickerButtonBuy;
    public GameObject standardMushroomPickerButtonChoose;

    public GameObject duckMushroomPickerButtonBuy;
    public GameObject duckMushroomPickerButtonChoose;

    public GameObject penguinkMushroomPickerButtonBuy;
    public GameObject penguinMushroomPickerButtonChoose;

    public GameObject wolfMushroomPickerButtonBuy;
    public GameObject wolfMushroomPickerButtonChoose;

    public GameObject alienMushroomPickerButtonBuy;
    public GameObject alienMushroomPickerButtonChoose;

    public GameObject spriteMushroomPickerButtonBuy;
    public GameObject spriteMushroomPickerButtonChoose;

    void Start()
    {
        PlayerPrefs.SetInt("standardMushroomPicker", 1);
        PlayerPrefs.SetInt("isBuyStandardMushroomPicker", 1);
        int bitcoinValues = PlayerPrefs.GetInt("bitcoin", 0);
        bitcoinValue.text = bitcoinValues.ToString();
        CheckMushroomsPicker();
        
        CheckAvalaibleMushroomPicker();
        CheckCurrentMushroomPicker();
    }


    void CheckMushroomsPicker()
    {
        int isStandardMushroomPickerBought = PlayerPrefs.GetInt("standardMushroomPicker", 0);
        EnableItemShop(standardMushroomPickerButtonBuy, standardMushroomPickerButtonChoose, isStandardMushroomPickerBought);
    }

    public void CheckCurrentMushroomPicker()
    {
        int currentMushroomPicker = PlayerPrefs.GetInt("currentMushroomPicker", 1);
        if (currentMushroomPicker == 1)
        {
            standardMushroomPickerButtonChoose.SetActive(false);
        }
        else
        {
          //  standardMushroomPickerButtonChoose.SetActive(true);
        }

        if (currentMushroomPicker == 2)
        {
            duckMushroomPickerButtonChoose.SetActive(false);
        }
        else
        {
          //  duckMushroomPickerButtonChoose.SetActive(true);
        }

        if (currentMushroomPicker == 3)
        {
            penguinMushroomPickerButtonChoose.SetActive(false);
        }
        else
        {
          //  penguinMushroomPickerButtonChoose.SetActive(true);
        }

        if (currentMushroomPicker == 4)
        {
            wolfMushroomPickerButtonChoose.SetActive(false);
        }
        else
        {
          //  wolfMushroomPickerButtonChoose.SetActive(true);
        }


        if (currentMushroomPicker == 5)
        {
            alienMushroomPickerButtonChoose.SetActive(false);
        }
        else
        {
            //  wolfMushroomPickerButtonChoose.SetActive(true);
        }



        if (currentMushroomPicker == 6)
        {
            spriteMushroomPickerButtonChoose.SetActive(false);
        }
        else
        {
            //  wolfMushroomPickerButtonChoose.SetActive(true);
        }
    }

   public void CheckAvalaibleMushroomPicker()
    {
        int chicken = PlayerPrefs.GetInt("isBuyStandardMushroomPicker", 0);
        int duck = PlayerPrefs.GetInt("isBuyDuckMushroomPicker", 0);
        int penguin = PlayerPrefs.GetInt("isBuyPenguinMushroomPicker", 0);
        int wolf = PlayerPrefs.GetInt("isBuyWolfMushroomPicker", 0);
        int alien = PlayerPrefs.GetInt("isBuyAlienMushroomPicker", 0);
        int sprite = PlayerPrefs.GetInt("isBuySpriteMushroomPicker", 0);

        if (chicken == 1)
        {
            standardMushroomPickerButtonBuy.SetActive(false);
            standardMushroomPickerButtonChoose.SetActive(true);
        }
        else
        {
            standardMushroomPickerButtonBuy.SetActive(true);
            standardMushroomPickerButtonChoose.SetActive(false);
        }

        if (duck == 1)
        {
            duckMushroomPickerButtonBuy.SetActive(false);
            duckMushroomPickerButtonChoose.SetActive(true);
        }
        else
        {
            duckMushroomPickerButtonBuy.SetActive(true);
            duckMushroomPickerButtonChoose.SetActive(false);
        }

        if (penguin == 1)
        {
            penguinkMushroomPickerButtonBuy.SetActive(false);
            penguinMushroomPickerButtonChoose.SetActive(true);
        }
        else
        {
            penguinkMushroomPickerButtonBuy.SetActive(true);
            penguinMushroomPickerButtonChoose.SetActive(false);
        }

        if (wolf == 1)
        {
            wolfMushroomPickerButtonBuy.SetActive(false);
            wolfMushroomPickerButtonChoose.SetActive(true);
        }
        else
        {
            wolfMushroomPickerButtonBuy.SetActive(true);
            wolfMushroomPickerButtonChoose.SetActive(false);
        }

        if (alien == 1)
        {
            alienMushroomPickerButtonBuy.SetActive(false);
            alienMushroomPickerButtonChoose.SetActive(true);
        }
        else
        {
            alienMushroomPickerButtonBuy.SetActive(true);
            alienMushroomPickerButtonChoose.SetActive(false);
        }

        if (sprite == 1)
        {
            spriteMushroomPickerButtonBuy.SetActive(false);
            spriteMushroomPickerButtonChoose.SetActive(true);
        }
        else
        {
            spriteMushroomPickerButtonBuy.SetActive(true);
            spriteMushroomPickerButtonChoose.SetActive(false);
        }
    }

    void EnableItemShop(GameObject obj, GameObject objEmpty, int var)
    {
        if (var == 0)
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
