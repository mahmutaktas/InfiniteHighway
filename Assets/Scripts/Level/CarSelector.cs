using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public GameObject[] carModels;
    public int currentCarIndex;

    GameObject player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);

        foreach (var car in carModels)
        {
            car.SetActive(false);
        }

        carModels[currentCarIndex].SetActive(true);
        carModels[currentCarIndex].transform.SetParent(player.transform);
        player.GetComponent<PlayerController>().animator = carModels[currentCarIndex].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
