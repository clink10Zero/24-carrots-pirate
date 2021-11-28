using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBumpDetection : MonoBehaviour
{


      GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("QT"))
        {

            Enemy.GetComponent<PolygonCollider2D>().enabled = false;
            if (tag.Equals("Boss"))
            {
                Enemy.tag = "Boss";
            }
            else {
                Enemy.tag = "Untagged";
            }
            

            GetComponent<Collider2D>().enabled = false;
            Enemy.GetComponent<SpriteRenderer>().flipY = true;
            
            Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(40, 40), 0f);
            Enemy.transform.position = Enemy.transform.position + movement * Time.deltaTime;
        }

    }
}
