using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class QueueToufue : MonoBehaviour {

    public Transform detectionSol;
    public LayerMask layerMask;

    [SerializeField] private Rigidbody2D rb;
    [Range(5, 10)]
    [SerializeField] private float speed;
    [Range(0, .3f)]
    [SerializeField] private float m_MovementSmoothing = .05f;

    [SerializeField] private float jumpHight = 1000f;
    private int jumpCount;
    public GameObject canvas;
    public GameObject canvasWin;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        canvasWin.SetActive(false);
        Time.timeScale = 1;
        jumpCount = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag.Equals("Boss"))
        {
            
            canvasWin.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            this.GetComponent<Collider2D>().enabled = false;
            Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(40, 40), 0f);
            this.transform.position = this.transform.position + movement * Time.deltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (this.transform.position.y < -3)
        {



            canvas.SetActive(true);
            Time.timeScale = 0;
        }

        float x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
           
            if (IsGrounded())
            {
                rb.AddForce(new Vector2(0f, jumpHight));
                jumpCount=1;
            }
            else
            {
                if (jumpCount < 2)
                {
                    rb.AddForce(new Vector2(0f, jumpHight));
                    jumpCount++;//ou =2
                }
            }
           
        }
        
       
        if (x < 0)
        {
            Vector3 spriteScale = this.transform.GetChild(0).localScale;
            if (spriteScale.x > 0)
            {
                this.transform.GetChild(0).localScale = new Vector3(spriteScale.x *- 1, spriteScale.y, spriteScale.z);
            }
           
        }
        if (x >= 0)
        {
            Vector3 spriteScale = this.transform.GetChild(0).localScale;
            if (spriteScale.x < 0)
            {
                this.transform.GetChild(0).localScale = new Vector3(spriteScale.x * -1, spriteScale.y, spriteScale.z);
            }

        }
        Vector3 move = transform.right * x * speed;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, move, ref move, m_MovementSmoothing);
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.8f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, layerMask);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    public void restar() {
        
        Time.timeScale =1;
        SceneManager.LoadScene("Niveau");
    }

    public void backMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Titre");
    }
}
