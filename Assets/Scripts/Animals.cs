using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Animals : MonoBehaviour
{
    private MapManager mapManager;
    public GameManager gameManager;
    private AnimalSpawner animalSpawner;


    public GameObject circle;
    public float moveTime = 5f;
    public float moveCounter;
    private float dieTimer = 0f;
    private const float MAX_DIE = 5f;
    private const float MAX_HUNGER = 5f;
    private float hunger = MAX_HUNGER;

    public TileBase currentTile;
    public Vector3 previousTile;
    public GameObject senseRadius;
    
    public Sprite sprite;
    public int m_fertility;
    public int m_speed;
    public int m_sense;
    public int m_belly;
    public int m_foraging;

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
        m_foraging = foregeing;
        m_animalWarmth = (Enums.AnimalWarmth)animalWarmth;

        circle.transform.localScale = new Vector3(sense * 2, sense * 2, sense * 2);
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
        //eating scales off their foraging level
        food += m_foraging;
        SetRisk(false);
        hunger = MAX_HUNGER;

        //Caps food using belly as the capacity
        if (food > m_belly)
        {
            food = m_belly;
        }
    }

    private void Update()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        //Destroys animals that spend too much time in the wrong biome.
        dieTimer -= Time.deltaTime;
        hunger -= Time.deltaTime;
        if (dieTimer <= 0f && dying)
        {
            print("animal died in biome");
            animalSpawner.livinganimals.Remove(gameObject);
            animalSpawner.animalsdied++;
            Destroy(gameObject);
        }
        if (hunger < 0 && food < 2)
        {
            SetRisk(true);
        }
        senseRadius.GetComponent<CircleCollider2D>().radius = m_sense + weatherSense;

        //Remove the buggy animals
        if (mapManager.getTileData(transform.position).biome == "" || mapManager.getTileData(transform.position).weather == Enums.Weather.Death || currentTile == null)
        {
            animalSpawner.livinganimals.Remove(gameObject);
            animalSpawner.animalsdied++;
            Destroy(gameObject);
        }

        //Allows animals to move during the daytime.
        if (gameManager.itsDay == true)
        {
            moveCounter -= Time.deltaTime;

            if (moveCounter <= 0)
            {
                moveCounter = moveTime;
                lookRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            }

            //If the animal walks into a new biome, callthis
            if (mapManager.getTileData(transform.position).tiles[0] != currentTile)
            {
                //Setting speed according to the new biome.
                BodyCheck(m_animalWarmth, mapManager.getTileData(transform.position).weather, true);


                if (LeavingBiome() && leftBiome && !riskyBoi)
                {
                    if (previousTile != null && previousTile.x != transform.position.x && previousTile.y != transform.position.y)
                    {
                        //Target is the last position
                        Vector2 targ = previousTile;

                        //Measures distance between current and previous position
                        targ.x = targ.x - transform.position.x;
                        targ.y = targ.y - transform.position.y;

                        //Gets angle between current and previous position, sets variables that system uses for this.
                        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90;
                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        lookRotation = q;
                        moveCounter = 10;
                    }
                    else
                    {
                        SetRisk(true);
                        leftBiome = false;
                    }
                }
                else if (!LeavingBiome() && leftBiome)
                {
                    //Once they left, returns to status quo
                    currentTile = mapManager.getTileData(transform.position).tiles[0];
                    leftBiome = false;
                }
                else
                {
                    //Standard action if nothing is wrong
                    currentTile = mapManager.getTileData(transform.position).tiles[0];
                }
            }
            else
            {
                //Remembers this place just in case.
                previousTile = mapManager.PositionGetter();
            }

            transform.rotation = lookRotation;
            transform.position += transform.up * Time.deltaTime * (m_speed + weatherSpeed) * direction;
        }
        //
    }

    public void newDay()
    {
        //If animals have more than one food they are able to have a baby, then eat one food.
        if (food > 1)
        {
            readytochild = true;
            food -= 1;
        }

        //If animals end the day without eating, they die.
        if (food <= 0)
        {
            animalSpawner.livinganimals.Remove(gameObject);
            print("death by food");
            animalSpawner.animalsdied++;
            Destroy(gameObject);
        }

        food--;

    }

    public bool EatFoodOutsideBiome(GameObject snack)
    {
        int threshold = 0;
        //Threshold: Can/should I get this food?
        //Is biome safe? 2
        //What is my speed/sense ratio? Can I get the food fast? 4
        //Am I starving? 6
        //5-: don't eat, 6+ eat

        //Divides distance by speed to find the time it will take to get the snack, if it exceeds the death timer, they will not pursue.
        if (MAX_DIE < (((snack.transform.position.x + snack.transform.position.y) - (transform.position.x + transform.position.y))/ m_speed))
        {
            threshold += 2;
        }
        //Determines if they're hungry enough(not enough to have a baby)
        if (food <= 1)
        {
            threshold += 4;
        }
        //Checks if the food is in a safe space, this is the BIAS
        if (BodyCheck(m_animalWarmth, snack.GetComponent<Food>().weather, false))
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

        //If theyre currently taking risks, they will go for the food and remain in the biome
        if (riskyBoi)
        {
            return false;
        }
        //If theyre not taking risks and the biome is bad for them, returns true and actives the bool
        else if (!BodyCheck(m_animalWarmth, mapManager.getTileData(transform.position).weather, false))
        {
            leftBiome = true;
            return true;
        }

        //DOES HE CHANGE COURSE???


        return false;
    }

    public bool BodyCheck(Enums.AnimalWarmth warmth, Enums.Weather weather, bool isEntering)
    {
        //Biome against coat, identifies with specialist buff(specialists survive in less environments but have a better buff in their preferred biome)
        if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Cold)
            || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Hot))
        {
            if (isEntering)
            {
                //Applies buffs
                weatherSense = 1;
                weatherSpeed = 1;
                dying = false;
            }
            else
            {
                //Returns true to tell the system this biome is good.
                return true;
            }
        }
        //Biome against coat, identifies with standard(no buffs or debuffs)
        else if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Chilly)
                   || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Cold)
                    || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Hot)
                    || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Warm))
        {
            if (isEntering)
            {
                //Removes buffs or debuffs
                weatherSense = 0;
                weatherSpeed = 0;
                dying = false;
            }
            else
            {
                //Returns true to tell system biome is good.
                return true;
            }
        }
        //Biome against coat, identifies with debuff.
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
        //Biome against coat, identifies with buff
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
        //Remaining biomes are deadly to their respective coats.
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
