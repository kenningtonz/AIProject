using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Animals : MonoBehaviour
{
    private MapManager mapManager;
    private float moveTime = 10f;
    private float moveCounter;
    private TileBase currentTile;
<<<<<<< Updated upstream

=======
    public GameObject senseRadius;
    
>>>>>>> Stashed changes
    public Sprite sprite;
    //public int[] attributes;
    public int m_fertility;
    public int m_speed;
    public int m_sense;
    public int m_belly;
    public int m_foreging;
    public bool readytochild = false;
    public bool nsff = false;

    private int weatherSpeed = 0;
    private int weatherSense = 0;

    public Enums.AnimalWarmth m_animalWarmth;

    public void init(int fertility, int speed, int sense, int belly, int foregeing, Enums.AnimalWarmth animalWarmth)
    {
        m_fertility = fertility;
        m_speed = speed;
        m_sense = sense;
        m_belly = belly;
        m_foreging = foregeing;
        m_animalWarmth = animalWarmth;

    }

    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        moveCounter = moveTime;
        currentTile = mapManager.getTileData(transform.position).tiles[0];
    }
    private void Update()
    {
        moveCounter -= Time.deltaTime;
<<<<<<< Updated upstream
     if (moveCounter <= 0)
        {
            moveCounter = moveTime;
            float newRotation = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, 0f, newRotation);

        }
        transform.position += transform.up * Time.deltaTime * (m_speed + weatherSpeed);
=======

        //if (moveCounter <= 0)
        //{
        //    moveCounter = moveTime;
        //    lookRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        //    Debug.Log(lookRotation);
        //}
        //    transform.rotation = lookRotation;
        //transform.position += transform.up * Time.deltaTime * (m_speed + weatherSpeed);
>>>>>>> Stashed changes

        if (mapManager.getTileData(transform.position).tiles[0] != currentTile)
        {
            Debug.Log("changed");
            currentTile = mapManager.getTileData(transform.position).tiles[0];
            BodyCheck(m_animalWarmth, mapManager.getTileData(transform.position).weather);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bpundry")
            {
            Debug.Log("collided");
            float newRotation = Random.Range(0f, 360f);
                transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
            }
    }
    public bool EatFoodOutsideBiome(Enums.Biomes biome, Enums.AnimalWarmth warmth, int speed, int sense, int food)
    {
        int threshold = 0;
        //Threshold: Can/should I get this food?
        //Is biome safe? 2
        //What is my speed/sense ratio? Can I get the food fast? 4
        //Am I starving? 6
        //5-: don't eat, 6+ eat
        if (!nsff)
        {
            threshold += 2;
        }

        if (speed > sense)
        {
            threshold += 4;
        }

        if (food <= 1)
        {
            threshold += 6;
        }

        if (threshold <= 5)
        {
            return false;
        }
        return true;
    }
    public bool TakeRisks()
    {
        //Threshold: Am I hungry enough to take risks?
        //2-: no risks, 3+ take risks
        return false;
    }

    public void BodyCheck(Enums.AnimalWarmth warmth, Enums.Weather weather)
    {
        if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Cold)
            || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Hot))
        {
            weatherSense = 1;
            weatherSpeed = 1;
            nsff = false;
        }
        else if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Chilly)
                   || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Cold)
                    || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Hot)
                    || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Warm))
        {
            weatherSense = 0;
            weatherSpeed = 0;
            nsff = false;
        }
        else if((warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Warm)
            || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Chilly))
        {
            weatherSpeed = -1;
            weatherSense = 0;
            nsff = true;
        }
        else if ((warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Warm)
          || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Chilly))
        {
            weatherSpeed = 1;
            weatherSense = 0;
            nsff = false;
        }
        else
        {
            Debug.Log(warmth);
            Debug.Log(weather);
            Destroy(gameObject);
            nsff = true;
        }
    }
}
