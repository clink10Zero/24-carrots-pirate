using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QT_death : MonoBehaviour
{


      GameObject QT;
    // Start is called before the first frame update
    void Start()
    {
        QT = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {

            QT.GetComponent<PolygonCollider2D>().enabled = false;
            
            

            GetComponent<Collider2D>().enabled = false;
            QT.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            
            Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(40, 40), 0f);
            QT.transform.position = QT.transform.position + movement * Time.deltaTime;
        }

    }
}
