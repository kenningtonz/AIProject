using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private List<GameObject> availableFood = new List<GameObject>();
    public GameObject prefab;
    //public int numOfFood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spawnFood(3);
        }
    }

    public void spawnFood(int numOfFood)
    {
        for (int i = 0; i < numOfFood; i++)
        {
            availableFood.Add(prefab);
            int randX = Random.Range(-9, 20);
            int randY = Random.Range(-11, 13);
            Instantiate(availableFood[i], new Vector3(randX, randY, 0), Quaternion.identity);
        }
    }

}
