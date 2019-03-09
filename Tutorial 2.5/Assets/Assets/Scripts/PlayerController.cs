using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public int count;
    private int lives;

    public bool onGround;

    private bool levelOneComplete;

    public Text countText;
    public Text winText;
    public Text livesText;
    Animator anim;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetLivesText();
        SetCountText();
        levelOneComplete = false;
        anim = GetComponent<Animator>();
        onGround = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (onGround == false) {
            anim.SetInteger("State", 2);
        }
        if (onGround == true)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                anim.SetInteger("State", 1);
            }
            else {
                anim.SetInteger("State", 0);
            }
        }

        Vector3 charScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            charScale.x = -0.25f;
        }
        else {
            charScale.x = 0.25f;
        }
        transform.localScale = charScale;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground")
        {
            onGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";
        }
        if (count == 4 && levelOneComplete == false)
        {
            transform.position = new Vector2(75f , -2.4f);
            lives = 3;
            SetLivesText();
            levelOneComplete = true;
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You Lose!";
            gameObject.SetActive(false);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2 (0 , jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
