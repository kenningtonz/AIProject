using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Animals : MonoBehaviour
{
    private MapManager mapManager;
    public GameManager gameManager;
    private AnimalSpawner animalSpawner;

    public float moveTime = 5f;
    public float moveCounter;
    private float dieTimer = 0f;
    private const float MAX_DIE = 1f;
    public TileBase currentTile;
    public GameObject senseRadius;
    
    public Sprite sprite;
    public int m_fertility;
    public int m_speed;
    public int m_sense;
    public int m_belly;
    public int m_foreging;

    public Quaternion lookRotation;
    public int food;
    private int weatherSpeed = 0;
    private int weatherSense = 0;
    private int direction = 1;

    public bool foundFood = false;
    public bool readytochild = false;
    public bool riskyBoi = false;
    public bool dying = false;
    public bool leftBiome = false;

    public Enums.AnimalWarmth m_animalWarmth;

    public void init(int fertility, int speed, int sense, int belly, int foregeing, int animalWarmth)
    {
        m_fertility = fertility;
        m_speed = speed;
        m_sense = sense;
        m_belly = belly;
        m_foreging = foregeing;
        m_animalWarmth = (Enums.AnimalWarmth)animalWarmth;

    }

    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        moveCounter = moveTime;
        currentTile = mapManager.getTileData(transform.position).tiles[0];
        gameManager = FindObjectOfType<GameManager>();
        animalSpawner = FindObjectOfType<AnimalSpawner>();
    }

    public void eat()
    {
        food = food + m_foreging;
    }

    private void Update()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        dieTimer -= dieTimer - Time.deltaTime;
        if (dieTimer <= 0f && dying)
        {
            print("animal died in biome");
            animalSpawner.livinganimals.Remove(gameObject);
            Destroy(gameObject);
        }
        senseRadius.GetComponent<CircleCollider2D>().radius = m_sense + weatherSense;

        //Remove the buggy animals
        if (mapManager.getTileData(transform.position).biome == "" || mapManager.getTileData(transform.position).tiles[0] == null || currentTile == null)
        {
            animalSpawner.livinganimals.Remove(gameObject);
            Destroy(gameObject);
        }

        if (gameManager.itsDay == true)
        {
            moveCounter -= Time.deltaTime;

            if (moveCounter <= 0)
            {
                moveCounter = moveTime;
                lookRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            }
            transform.rotation = lookRotation;
            transform.position += transform.up * Time.deltaTime * (m_speed + weatherSpeed) * direction;


            if (mapManager.getTileData(transform.position).tiles[0] != currentTile)
            {
                //if (mapManager.getTileData(transform.position).biome == "" || currentTile == null)
                //{
                //    animalSpawner.livinganimals.Remove(gameObject);
                //    Destroy(gameObject);
                //}
                //else
                //{
                    currentTile = mapManager.getTileData(transform.position).tiles[0];
                    //print(mapManager.getTileData(transform.position).biome);

                    BodyCheck(m_animalWarmth, mapManager.getTileData(transform.position).weather, true);
                    if (LeavingBiome() && !leftBiome)
                    {
                        direction = -direction;
                    }
                    else if (!LeavingBiome() && leftBiome)
                    {
                        leftBiome = false;
                    }
                //}
            }

        }

        if (food > m_belly)
        {
            food = m_belly;
        }

    }

    public void newDay()
    {
        if (food > 1)
        {
            readytochild = true;
            food -= 1;
        }


        if (food == 0)
        {
            animalSpawner.livinganimals.Remove(gameObject);
            print("death by food");
            Destroy(gameObject);
        }

        food--;

    }

    public bool EatFoodOutsideBiome(Enums.Weather weather)
    {
        int threshold = 0;
        //Threshold: Can/should I get this food?
        //Is biome safe? 2
        //What is my speed/sense ratio? Can I get the food fast? 4
        //Am I starving? 6
        //5-: don't eat, 6+ eat

        //Checks if they're faster than they can see
        if (m_speed > (m_sense/ 2))
        {
            threshold += 2;
        }
        //Determines if they're hungry enough
        if (food <= 1)
        {
            threshold += 4;
        }
        //Checks if the food is in a safe space, this is the BIAS
        if (BodyCheck(m_animalWarmth, weather, false))
        {
            threshold += 6;
        }

        if (threshold <= 5)
        {
            return false;
        }
        return true;
    }
    public bool LeavingBiome()
    {
        //Is he risking it all for food?
        //If not, check if the place is safe.
        int threshold = 0;

        if (riskyBoi)
        {
            return false;
        }
        if (!BodyCheck(m_animalWarmth, mapManager.getTileData(transform.position).weather, false))
        {
            leftBiome = true;
            return true;
        }

        //DOES HE CHANGE COURSE???


        return false;
    }

    public bool BodyCheck(Enums.AnimalWarmth warmth, Enums.Weather weather, bool isEntering)
    {
        if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Cold)
            || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Hot))
        {
            if (isEntering)
            {
                weatherSense = 1;
                weatherSpeed = 1;
                dying = false;
            }
            else
            {
                return true;
            }
        }
        else if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Chilly)
                   || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Cold)
                    || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Hot)
                    || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Warm))
        {
            if (isEntering)
            {
                weatherSense = 0;
                weatherSpeed = 0;
                dying = false;
            }
            else
            {
                return true;
            }
        }
        else if ((warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Warm)
            || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Chilly))
        {
            if (isEntering)
            {
                weatherSpeed = -1;
                weatherSense = 0;
                dying = false;
            }
            else
            {
                return false;
            }
        }
        else if ((warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Warm)
          || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Chilly))
        {
            if (isEntering)
            {
                weatherSpeed = 1;
                weatherSense = 0;
                dying = false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (isEntering)
            {
                dying = true;
                dieTimer = MAX_DIE;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void SetRisk(bool _risk) => riskyBoi = _risk;
    public MapManager GetMap() => mapManager;
}
