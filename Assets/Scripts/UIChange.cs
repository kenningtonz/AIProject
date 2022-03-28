using UnityEngine;
using UnityEngine.UI;

public class UIChange : MonoBehaviour
{
    private MapManager mapManager;
    private GameManager gameManager;
    private FoodSpawner foodSpawner;
    private AnimalSpawner animalSpawner;

    public Text animalsBorn;
    public Text animalsDied;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        gameManager = FindObjectOfType<GameManager>();
        foodSpawner = FindObjectOfType<FoodSpawner>();
        animalSpawner = FindObjectOfType<AnimalSpawner>();
    }


    //set biomes
    public void setBiome(Text biome)
    {
        if (biome.text == "Plains")
            mapManager.biome = Enums.Biomes.Plains;
        if (biome.text == "Snow")
            mapManager.biome = Enums.Biomes.Snow;
        if (biome.text == "Sand")
            mapManager.biome = Enums.Biomes.Sand;
        if (biome.text == "Forest")
            mapManager.biome = Enums.Biomes.Forest;
    }

    public void setBrushSize(Slider slider)
    {
        mapManager.brushsize = (int)slider.value;
    }

    public void setAction(Text action)
    {
        if (action.text == "Paint")
            gameManager.action = Enums.Action.Paint;
        if (action.text == "Spawn")
            gameManager.action = Enums.Action.Spawn;
        if (action.text == "Nothing")
            gameManager.action = Enums.Action.Nothing;
    }

    public void startDay(Button button)
    {
        gameManager.newDay();
        Debug.Log("startay");
        button.interactable = false;
    }

    public void setGameSpeed(Slider slider)
    {
        gameManager.gameSpeed = (int)slider.value;
    }

    public void setNumOfFood(Slider slider)
    {
        foodSpawner.numOfFood = (int)slider.value;
    }

    public void spawnAnimal()
    {
        animalSpawner.spawnAnimal = true;
    }



    // Update is called once per frame
    void Update()
    {
        animalsBorn.text = animalSpawner.animalsborn.ToString();
        animalsDied.text = animalSpawner.animalsdied.ToString();
    }
}
