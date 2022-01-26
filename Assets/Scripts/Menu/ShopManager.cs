using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public GameObject[] carModels;
    public int currentCarIndex;

    public CarBlueprint[] cars;

    public Button buyButton;

    public TextMeshProUGUI coinText;

    void Start()
    {

        foreach(CarBlueprint car in cars)
        {
            if(car.price == 0)
            {
                car.isUnlocked = true;
            }
            else
            {
                car.isUnlocked = PlayerPrefs.GetInt(car.name, 0) == 0 ? false : true;
            }
        }

        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);

        foreach(var car in carModels)
        {
            car.SetActive(false);
        }

        carModels[currentCarIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void NextCar()
    {
        carModels[currentCarIndex].SetActive(false);

        currentCarIndex++;

        if (currentCarIndex == carModels.Length)
            currentCarIndex = 0;

        carModels[currentCarIndex].SetActive(true);

        if(cars[currentCarIndex].isUnlocked)
            PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void PreviousCar()
    {
        carModels[currentCarIndex].SetActive(false);

        currentCarIndex--;

        if (currentCarIndex < 0)
            currentCarIndex = carModels.Length - 1;

        carModels[currentCarIndex].SetActive(true);

        if (cars[currentCarIndex].isUnlocked)
            PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    private void UpdateUI()
    {
        CarBlueprint c = cars[currentCarIndex];

        if (c.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);

            buyButton.GetComponentInChildren<Text>().text = "BUY - " + c.price;

            if(c.price < PlayerPrefs.GetInt("NumberOfCoins", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }

    public void UnlockCar()
    {
        CarBlueprint c = cars[currentCarIndex];

        PlayerPrefs.SetInt(c.name, 1);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);

        c.isUnlocked = true;

        PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins") - c.price);

        coinText.text = PlayerPrefs.GetInt("NumberOfCoins", 0) + "";
    }
}
