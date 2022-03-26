using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private FoodSpawner foodSpawner;
    public bool itsDay = false;
    public int gameSpeed = 1;

    public Button newDayButton;

    public Enums.Action action ;

    // Start is called before the first frame update
    void Start()
    {
        foodSpawner = FindObjectOfType<FoodSpawner>();
        action = Enums.Action.Nothing;
    }

    public void newDay()
    {
        itsDay = true;
       
        foodSpawner.spawnFood();
    }


    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = gameSpeed;


        if (foodSpawner.availableFood.Count == 0)
        {
            itsDay = false;
            newDayButton.interactable = true;
            //Debug.Log("day end");

        }
    }
}
