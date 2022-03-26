using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private FoodSpawner foodSpawner;
    private AnimalSpawner animalSpawner;
    public bool itsDay = false;
    public int gameSpeed = 1;

    public Button newDayButton;

    public Enums.Action action ;

    // Start is called before the first frame update
    void Start()
    {
        foodSpawner = FindObjectOfType<FoodSpawner>();
        animalSpawner = FindObjectOfType<AnimalSpawner>();
        action = Enums.Action.Nothing;
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
     Time.timeScale = gameSpeed;


        if (foodSpawner.availableFood.Count == 0)
        {
            itsDay = false;
            newDayButton.interactable = true;
            //Debug.Log("day end");

        }
    }
}
