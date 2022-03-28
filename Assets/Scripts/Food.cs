//Final Project AI
//Kennedy Adams 100632983
//Dylan Brush 100700305
//Maija Kinnunen 100697620
using UnityEngine;
using UnityEngine.Tilemaps;


public class Food : MonoBehaviour
{
    private FoodSpawner foodSpawner;
    private MapManager mapManager;
    public Enums.Weather weather;
    

    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        foodSpawner = FindObjectOfType<FoodSpawner>();
        weather = mapManager.getTileData(transform.position).weather;

    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Animal")
        {
            //makes animal eat
            collision.gameObject.GetComponent<Animals>().eat();
           
            foodSpawner.availableFood.Remove(gameObject);
            collision.gameObject.GetComponent<Animals>().moveCounter = collision.gameObject.GetComponent<Animals>().moveTime;
            Destroy(gameObject);

            print("yum");
        }
    }
}
