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

    public int animalsborn = 0;
    public int animalsdied = 0;

    public int fertval;
    public int speedval;
    public int bellyval;
    public int senseval;
    public int forgval;
    public int warmval;

    private const int MAX_FERT = 5;
    private const int MAX_SPEED = 50;
    private const int MAX_BELLY = 50;
    private const int MAX_SENSE = 50;
    private const int MAX_FORGE = 50;

    private int MAX_X = 29;
    private int MIN_X = -29;
    private int MAX_Y = 29;
    private int MIN_Y = -29;


    public Sprite coatFur;
    public Sprite coatSmooth;
    public Sprite coatFeather;
    public Sprite coatScale;

    public bool spawnAnimal = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void CheckStats()
    {
        //Ensures all the stats don't go over their capacity
        if (fertval > MAX_FERT)
            fertval = MAX_FERT;
        if (bellyval > MAX_BELLY)
            bellyval = MAX_BELLY;
        if (senseval > MAX_SENSE)
            senseval = MAX_SENSE;
        if (speedval > MAX_SPEED)
            speedval = MAX_SPEED;
        if (forgval > MAX_FORGE)
            forgval = MAX_FORGE;
    }

    private Sprite CoatType()
    {
        switch(warmval)
        {
            case 0:
                return coatFur;
            case 1:
                return coatSmooth;
            case 2:
                return coatFeather;
            case 3:
                return coatScale;
        }

        return coatFur;
    }

    private Color32 Colour()
    {
        //Setting colours according to their stat values
        int redVal = fertval;
        int greenVal = forgval;
        int blueVal = bellyval;
        int alphaVal = speedval;
        int colourMod = 3;

        if (fertval < 0)
            redVal = 0;
        if (forgval < 0)
            greenVal = 0;
        if (bellyval < 0)
            blueVal = 0;
        if (speedval < 0)
            alphaVal = 0;

        return new Color32((byte)(colourMod * alphaVal * (255 / MAX_SPEED)), (byte)(colourMod * greenVal * (255 / MAX_FORGE)), (byte)(colourMod * blueVal * (255 / MAX_BELLY)), (byte)(255 - (redVal * (100 / MAX_FERT))));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (spawnAnimal)//&& gameManager.action == Enums.Action.Spawn)
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int randX = Random.Range(MIN_X, MAX_X);
            int randY = Random.Range(MIN_Y, MAX_Y);
            GameObject animal = Instantiate(prefab, new Vector3(randX, randY, 0), Quaternion.identity);
            Debug.Log("spawn");
            animalsborn++;
            //  animal.GetComponent<Animals>().init(Random.Range(1, 10), Random.Range(1, 10), 2, Random.Range(1, 10), Random.Range(1, 10), Random.Range(0, 3));
            fertval = Random.Range(1, 4);
            bellyval = 1 + Random.Range(1, 3);
            senseval = 1 + Random.Range(1, 4);
            speedval = Random.Range(1, 6);
            forgval = Random.Range(1, 3);
            warmval = Random.Range(0, 3);

            //Setting the visuals
            animal.GetComponent<SpriteRenderer>().sprite = CoatType();
            animal.GetComponent<SpriteRenderer>().color = Colour();

            animal.GetComponent<Animals>().init(fertval, speedval, senseval, bellyval, forgval, warmval);
            //a_fertility.Add(fertval);
            //a_belly.Add(bellyval);
            //a_sense.Add(senseval);
            //a_speed.Add(speedval);
            //a_foraging.Add(forgval);
            //a_warmth.Add(warmval);
            livinganimals.Add(animal);
            spawnAnimal = false;
        }
        else if (gameManager.itsDay)
        {
            for (int c = 0; c < livinganimals.Count; c++)
            {
                GameObject parent = livinganimals[c];
                if ((parent.transform.position.x < MAX_X) && (parent.transform.position.x > MIN_X) && (parent.transform.position.y < MAX_Y) && (parent.transform.position.y > MIN_Y))
                {
                    if (parent.GetComponent<Animals>().readytochild)
                    {
                        animalsborn++;
                        //Number of babies ranges between 1 and fertility level, the higher it is, the higher the chance of more babies,
                        //(if negative) the lower, the higher the chance of no babies.
                        int numberOfBabies = Random.Range(1, parent.GetComponent<Animals>().m_fertility);

                        for (int b = 0; b < numberOfBabies; b++)
                        {
                            GameObject child = Instantiate(prefab, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
                            Debug.Log("born");
                            //Babies copy the stats of their parents and roll for a chance to increase, decrease or keep the stats.
                            fertval = parent.GetComponent<Animals>().m_fertility + Random.Range(-1, 2);
                            bellyval = parent.GetComponent<Animals>().m_belly + Random.Range(-1, 2);
                            senseval = parent.GetComponent<Animals>().m_sense + Random.Range(-1, 2);
                            speedval = parent.GetComponent<Animals>().m_speed + Random.Range(-1, 2);
                            forgval = parent.GetComponent<Animals>().m_foraging + Random.Range(-1, 2);

                            CheckStats();

                            //Checks if the baby is born in an environment that's good for them, if not, rerolls their coat type.
                            if (parent.GetComponent<Animals>().BodyCheck(parent.GetComponent<Animals>().m_animalWarmth, parent.GetComponent<Animals>().GetMap().getTileData(parent.transform.position).weather, false))
                            {
                                warmval = (int)parent.GetComponent<Animals>().m_animalWarmth;
                            }
                            else
                            {
                                warmval = Random.Range(0, 4);
                            }

                            //Setting the visuals
                            child.GetComponent<SpriteRenderer>().sprite = CoatType();
                            child.GetComponent<SpriteRenderer>().color = Colour();
                            //Spawns AI with the stuff
                            child.GetComponent<Animals>().init(fertval, speedval, senseval, bellyval, forgval, warmval);

                            //When there's too many animals, kills the oldest one.
                            if (livinganimals.Count > 80)
                            {
                                GameObject temp = livinganimals[0];
                                livinganimals.RemoveAt(0);
                                Destroy(temp);
                            }
                            livinganimals.Add(child);
                            parent.GetComponent<Animals>().readytochild = false;
                        }
                    }
                }
                else
                {
                    //livinganimals.RemoveAt(c);
                    //Destroy(parent);
                }
            }
        }
    }
}

//OLD EVOLUTION

//fertval = Random.Range(1, a_fertility.Count);
//bellyval = Random.Range(1, a_belly.Count);
//senseval = Random.Range(1, a_sense.Count);
//speedval = Random.Range(1, a_speed.Count);
//forgval = Random.Range(1, a_foraging.Count);
//warmval = Random.Range(1, a_warmth.Count);

/*
                            //Flip a coin to determine if the animal is good or bad for the gene pool
                            int targaryenCoin = Random.Range(1, 2);
                            //The world holds it's breath...
                            if (targaryenCoin == 1)
                            {
                                a_fertility.Add(fertval);
                                a_belly.Add(bellyval);
                                a_sense.Add(senseval);
                                a_speed.Add(speedval);
                                a_foraging.Add(forgval);
                                a_warmth.Add(warmval);
                            }
                            else
                            {
                                a_fertility.Remove(fertval);
                                a_belly.Remove(bellyval);
                                a_sense.Remove(senseval);
                                a_speed.Remove(speedval);
                                a_foraging.Remove(forgval);
                                a_warmth.Remove(warmval);
                            }
                            */