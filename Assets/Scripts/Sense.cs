using UnityEngine;

public class Sense : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Food")
        {
            //Checks a Neural Network to determine if the animal will change biomes to go after food.
            if (parent.GetComponent<Animals>().EatFoodOutsideBiome(collision.gameObject))
            {
                //Marks the food as a target to approach
                Vector3 targ = collision.gameObject.transform.position;
                targ.z = 0f;

                //Determines angle between self and the food and them changes course to go straight for the food.
                Vector3 objectPos = parent.transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;
                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

                parent.GetComponent<Animals>().lookRotation = q;
                //  print(parent.GetComponent<Animals>().lookRotation);

                parent.GetComponent<Animals>().moveCounter = 10;

                parent.GetComponent<Animals>().SetRisk(true);
            }
        }
    }



}
