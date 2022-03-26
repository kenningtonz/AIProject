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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // spawnFood(3);
        }
    }

    public void spawnFood()
    {
        Debug.Log("foodspawned");
        for (int i = 0; i < numOfFood; i++)
        {
            int randX = Random.Range(-8, 20);
            int randY = Random.Range(-11, 13);
            GameObject newfood = Instantiate(prefab, new Vector3(randX, randY, 0), Quaternion.identity);
            availableFood.Add(newfood);
        }
    }

}
