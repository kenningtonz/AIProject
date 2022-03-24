using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject animal =  Instantiate(prefab, mousePos, Quaternion.identity);
            Debug.Log("spawn");
            animal.GetComponent<Animals>().init(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10),Enums.AnimalWarmth.Feathers);


        }
    }
}
