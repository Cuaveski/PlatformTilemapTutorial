using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private int scoreValue;
    private int livesValue;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        scoreValue = 0;
        livesValue = 3;

        scoreText.text = "Score: " + scoreValue.ToString();
        livesText.text = "Lives: " + livesValue.ToString();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            scoreText.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livesText.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(scoreValue >= 4)
        {
            winTextObject.SetActive(true);
        }
        if(livesValue == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0,3), ForceMode2D.Impulse);
            }
        }
    }
}
