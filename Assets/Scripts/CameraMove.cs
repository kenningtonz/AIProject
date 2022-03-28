//Final Project AI
//Kennedy Adams 100632983
//Dylan Brush 100700305
//Maija Kinnunen 100697620
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameManager gameManager;
    private int MAX_X = 29;
    private int MIN_X = -29;
    private int MAX_Y = 29;
    private int MIN_Y = -29;
    public float speed = 20f;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //gets inputs
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        
        //sets size of camera
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize += -scrollValue * 5;
   
        //moves camera
        transform.Translate(new Vector3(xAxisValue * Time.deltaTime / gameManager.gameSpeed * speed, yAxisValue * Time.deltaTime / gameManager.gameSpeed * speed, 0.0f));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X, MAX_X), Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y ), -12);


    }
}
