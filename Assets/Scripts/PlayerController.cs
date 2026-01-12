
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public float jumpForce = 15f;
    public Transform groundCheck; // Assign an empty GameObject at Dino's feet
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Game State")]
    public int score = 0;
    public Text scoreTextDisplay1; // Drag UI Text here
    public Text scoreTextDisplay2; // Drag 2nd UI Text here
    public GameObject gameOverPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverPanel.SetActive(false);
        UpdateScore(0);
    }

    void Update()
    {
        // 1. Check if on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 2. Check Input (Keyboard OR Kinect)
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);
        if (KinectInput.Instance != null && KinectInput.Instance.jumpDetected) jumpInput = true;

        // 3. Jump Action
        if (jumpInput && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    // Collision Logic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameOver();
        }
        else if (collision.CompareTag("Star") || collision.CompareTag("Apple"))
        {
            UpdateScore(100);
            Destroy(collision.gameObject); // Remove item
        }
    }

    void UpdateScore(int add)
    {
        score += add;
        string text = "Score: " + score;
        if (scoreTextDisplay1) scoreTextDisplay1.text = text;
        if (scoreTextDisplay2) scoreTextDisplay2.text = text;
    }

    void GameOver()
    {
        Debug.Log("Dead");
        Time.timeScale = 0; // Pause Game
        gameOverPanel.SetActive(true);
    }
}