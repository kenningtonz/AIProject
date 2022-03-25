using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject prefab;
    private MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mapManager.action == Enums.Action.Spawn)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject animal =  Instantiate(prefab, new Vector3(mousePos.x,mousePos.y,0), Quaternion.identity);
            Debug.Log("spawn");
            animal.GetComponent<Animals>().init(Random.Range(1,10), Random.Range(1, 10), 2, Random.Range(1, 10), Random.Range(1, 10), Random.Range(0, 3));


        }
    }
}
