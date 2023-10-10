using UnityEngine;
using UnityEngine.SceneManagement;

public class BAllController : MonoBehaviour
{
    private Rigidbody2D RB;
    private Vector2 direction;
    private float velocityBall = 2.0f;

    private float limite = 10f;
    private float delay = 2f;

    private bool isInitGame = false;

    public AudioClip boing;

    void Awake()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0 && !isInitGame)
        {
            ResumeGame();
            isInitGame = true;
        }

        if (transform.position.x > limite || transform.position.x < -limite)
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("Game");
        isInitGame = false;
        delay = 2f;
    }

    private void ResumeGame()
    {
        float randomDirectionX = Random.Range(-1, 2);
        float randomDirectionY = Random.Range(-1, 2);

        while (randomDirectionX == 0)
        {
            randomDirectionX = Random.Range(-1, 2);
        }

        direction.x = randomDirectionX;
        direction.y = randomDirectionY;


        RB.velocity = direction * velocityBall;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(boing, transform.position);
    }
}
