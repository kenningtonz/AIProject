using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public List<GameObject> availableFood = new List<GameObject>();
    public GameObject prefab;
    public int numOfFood = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
    }

    public void spawnFood()
    {
        //spawns food
        Debug.Log("foodspawned");
        for (int i = 0; i < numOfFood; i++)
        {
            int randX = Random.Range(-28, 28);
            int randY = Random.Range(-28, 28);
            GameObject newfood = Instantiate(prefab, new Vector3(randX, randY, 0), Quaternion.identity);
            availableFood.Add(newfood);
        }
    }

}
