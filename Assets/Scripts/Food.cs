using UnityEngine;

public class Food : MonoBehaviour
{

    private FoodSpawner foodSpawner;

    private void Start()
    {
        foodSpawner = FindObjectOfType<FoodSpawner>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Animal")
        {
            collision.gameObject.GetComponent<Animals>().eat();
           // collision.gameObject.GetComponent<Animals>().food = collision.gameObject.GetComponent<Animals>().food + collision.gameObject.GetComponent<Animals>().m_foreging;
            foodSpawner.availableFood.Remove(gameObject);
            collision.gameObject.GetComponent<Animals>().moveCounter = collision.gameObject.GetComponent<Animals>().moveTime;
            Destroy(gameObject);
            
            print("yum");
        }
    }

}
