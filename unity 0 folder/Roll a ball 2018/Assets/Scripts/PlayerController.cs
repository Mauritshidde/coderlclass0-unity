using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text JumpText;
    public Text winText;
    public float jumpForce;
    public float SuperJumpForce;
    public GameObject player;

    private Rigidbody rb;

    private int count;
    private int jumpcount;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpcount = 0;
        count = 0;
        SetCountText ();
        winText.text = "";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        SetJumpText();

        if (player.transform.position.y <= -12.37)
        {
            transform.position = new Vector3(0, 0.75f, 0);
        }





    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);

            count += 1;
            jumpcount += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 14)
        {
            winText.text = "You Win!";
        }
    }
    void SetJumpText()
    {
        JumpText.text = "Jumps: " + jumpcount.ToString();
    }
    
    void OnCollisionStay(Collision collision)
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcount >= 1)
        {
                if (collision.gameObject.tag == "Ground")
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jumpcount -= 1;
                    
                }
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (jumpcount >= 3)
                {
                    rb.AddForce(Vector3.up * SuperJumpForce, ForceMode.Impulse);
                    jumpcount -= 3;
                }
            }
        }

        if (collision.gameObject.tag == "Kill")
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            transform.position = new Vector3(110, 22f, 40);
        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
    }
}