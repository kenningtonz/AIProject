//Final Project AI
//Kennedy Adams 100632983
//Dylan Brush 100700305
//Maija Kinnunen 100697620
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private FoodSpawner foodSpawner;
    private AnimalSpawner animalSpawner;
    public bool itsDay = false;
    public int gameSpeed = 1;

    private float daytimeCounter;
    private float daytimeMax;

    public Button newDayButton;
    public Toggle autoStartDay;

    public Enums.Action action ;

    // Start is called before the first frame update
    void Start()
    {
        foodSpawner = FindObjectOfType<FoodSpawner>();
        animalSpawner = FindObjectOfType<AnimalSpawner>();
        // action = Enums.Action.Nothing;
        
        daytimeCounter = daytimeMax;
    }

    public void newDay()
    {
        foodSpawner.spawnFood();
        for (int c = 0; c < animalSpawner.livinganimals.Count; c++)
        {
            animalSpawner.livinganimals[c].GetComponent<Animals>().newDay();
                }
        itsDay = true;
    }


    // Update is called once per frame
    void Update()
    {
        daytimeMax = 15f * gameSpeed;
        daytimeCounter -= Time.deltaTime;

     
     Time.timeScale = gameSpeed;


        if (foodSpawner.availableFood.Count == 0 || daytimeCounter < 0)
        {
            itsDay = false;
            newDayButton.interactable = true;
            if (autoStartDay.isOn && newDayButton.interactable == true)
            {
                newDayButton.interactable = false;
                newDay();
            }
            //Debug.Log("day end");
            daytimeCounter = daytimeMax;
        }
    }
}
