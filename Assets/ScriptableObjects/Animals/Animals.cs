using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Animals : MonoBehaviour
{
    private MapManager mapManager;
    private GameManager gameManager;
    private AnimalSpawner animalSpawner;

    public float moveTime = 5f;
    public float moveCounter;
    private TileBase currentTile;
    public GameObject senseRadius;

    public Sprite sprite;
    //public int[] attributes;
    public int m_fertility;
    public int m_speed;
    public int m_sense;
    public int m_belly;
    public int m_foreging;


    public Quaternion lookRotation;
    public Vector3 direction;
    public int food;

    private int weatherSpeed = 0;
    private int weatherSense = 0;
    public bool foundFood = false;
    public bool readytochild = false;
    private Rigidbody2D rb;
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

    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        moveCounter = moveTime;
        currentTile = mapManager.getTileData(transform.position).tiles[0];
        gameManager = FindObjectOfType<GameManager>();
        animalSpawner = FindObjectOfType<AnimalSpawner>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void eat()
    {
        food = food + m_foreging;
        print(food);
    }

    private void Update()
    {
        senseRadius.GetComponent<CircleCollider2D>().radius = m_sense + weatherSense;
        if (gameManager.itsDay == true)
        {
            moveCounter -= Time.deltaTime;

            if (moveCounter <= 0)
            {
                moveCounter = moveTime;
                lookRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                //Debug.Log(lookRotation);
            }
            // rb.AddForce(transform.up * Time.deltaTime * (m_speed + weatherSpeed ) * 100);
          //  rb.MoveRotation(lookRotation);
          //  rb.MovePosition(rb.position * Time.deltaTime * (m_speed + weatherSpeed));
           transform.rotation = lookRotation;
           transform.position += transform.up * Time.deltaTime * (m_speed + weatherSpeed);
            

            if (mapManager.getTileData(transform.position).tiles[0] != currentTile)
            {
              
                currentTile = mapManager.getTileData(transform.position).tiles[0];
                BodyCheck(m_animalWarmth, mapManager.getTileData(transform.position).weather);
            }

        }

        if(food > m_belly)
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

    public void BodyCheck(Enums.AnimalWarmth warmth, Enums.Weather weather)
    {
        if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Cold)
            || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Hot))
        {
            weatherSense = 1;
            weatherSpeed = 1;
        }
        else if ((warmth == Enums.AnimalWarmth.Fur && weather == Enums.Weather.Chilly)
                   || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Cold)
                    || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Hot)
                    || (warmth == Enums.AnimalWarmth.Scales && weather == Enums.Weather.Warm))
        {
            weatherSense = 0;
            weatherSpeed = 0;
        }
        else if ((warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Warm)
            || (warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Chilly))
        {
            weatherSpeed = -1;
            weatherSense = 0;
        }
        else if ((warmth == Enums.AnimalWarmth.Feathers && weather == Enums.Weather.Warm)
          || (warmth == Enums.AnimalWarmth.Smooth && weather == Enums.Weather.Chilly))
        {
            weatherSpeed = 1;
            weatherSense = 0;
        }
        else
        {
            //print("death by biome");
           // animalSpawner.livinganimals.Remove(gameObject);
          //  Destroy(gameObject);
        }
    }
}
