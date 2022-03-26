using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject prefab;
    private GameManager gameManager;


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


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void spawnAnimals()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) )//&& gameManager.action == Enums.Action.Spawn)
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int randX = Random.Range(-9, 20);
            int randY = Random.Range(-11, 13);
            GameObject animal = Instantiate(prefab, new Vector3(randX, randY, 0), Quaternion.identity);
            Debug.Log("spawn");
            //  animal.GetComponent<Animals>().init(Random.Range(1, 10), Random.Range(1, 10), 2, Random.Range(1, 10), Random.Range(1, 10), Random.Range(0, 3));
            fertval = Random.Range(1, 10);
            bellyval = Random.Range(1, 10);
            senseval = 7;
            speedval = Random.Range(1, 10);
            forgval = Random.Range(1, 3);
            warmval = Random.Range(0, 3);
            animal.GetComponent<Animals>().init(fertval, speedval, senseval, bellyval, forgval, warmval);
            a_fertility.Add(fertval);
            a_belly.Add(bellyval);
            a_sense.Add(senseval);
            a_speed.Add(speedval);
            a_foraging.Add(forgval);
            a_warmth.Add(warmval);
            livinganimals.Add(animal);




        }
        else if (gameManager.itsDay)
        {
            for (int c = 0; c < livinganimals.Count; c++)
            {
                GameObject parent = livinganimals[c];
                if (parent.GetComponent<Animals>().readytochild)
                {
                    GameObject child = Instantiate(prefab, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
                    Debug.Log("born");
                    fertval = Random.Range(1, a_fertility.Count);
                    bellyval = Random.Range(1, a_belly.Count);
                    senseval = Random.Range(1, a_sense.Count);
                    speedval = Random.Range(1, a_speed.Count);
                    forgval = Random.Range(1, a_foraging.Count);
                    warmval = Random.Range(1, a_warmth.Count);
                    child.GetComponent<Animals>().init(fertval, speedval, senseval, bellyval, forgval, warmval);
                    a_fertility.Add(fertval);
                    a_belly.Add(bellyval);
                    a_sense.Add(senseval);
                    a_speed.Add(speedval);
                    a_foraging.Add(forgval);
                    a_warmth.Add(warmval);
                    livinganimals.Add(child);
                    parent.GetComponent<Animals>().readytochild = false;
                }
            }
        }
    }
}

