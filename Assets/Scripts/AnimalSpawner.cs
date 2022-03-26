using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject prefab;
<<<<<<< Updated upstream
=======
    private MapManager mapManager;
    public List<int> a_fertility;
    public List<int> a_speed;
    public List<int> a_belly;
    public List<int> a_sense;
    public List<int> a_foraging;
    public List<int> a_warmth;
    public List<GameObject> livinganimals;
    public int fertval;
    public int speedval;
    public int bellyval;
    public int senseval;
    public int forgval;
    public int warmval;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            animal.GetComponent<Animals>().init(Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10),Enums.AnimalWarmth.Feathers);


=======
            fertval = Random.Range(1, 10);
            bellyval = Random.Range(1, 10);
            senseval = 2;
            speedval = Random.Range(1, 10);
            forgval = Random.Range(1, 10);
            warmval = Random.Range(0, 3);
            animal.GetComponent<Animals>().init(fertval, speedval, senseval, bellyval, forgval, warmval);
            a_fertility.Add(fertval);
            a_belly.Add(bellyval);
            a_sense.Add(senseval);
            a_speed.Add(speedval);
            a_foraging.Add(forgval);
            a_warmth.Add(warmval);
            livinganimals.Add(animal);
>>>>>>> Stashed changes
        }
    }
}
