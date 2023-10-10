using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Vector2 positionPlayerOne = new Vector2(-8f, 0);
    private Vector2 positionPlayerTwo = new Vector2(8f, 0);

    private Vector2 positionPlayer;

    private float positionPlayerY;
    private float velocity = 4f;

    private float limit = 3.5f;

    private bool isPlayerOne;

    public bool iAActive;
    public bool IAActive
    {
        get { return iAActive; }
        set { iAActive = value; }
    }

    private Transform ballTransform;

    public Transform BallTransform
    {
        get { return ballTransform; }
        set { ballTransform = value; }
    }

    void Awake()
    {
        isPlayerOne = gameObject.tag == "Player" ? true : false;
    }
    void Start()
    {
        transform.position = gameObject.tag == "Player" ? positionPlayerOne : positionPlayerTwo;
        positionPlayer = transform.position;
    }

    void Update()
    {
        float customVelocity = velocity * Time.deltaTime;

        if (isPlayerOne && !IAActive)
        {
            PlayerOneController(customVelocity);
        }
        else if (!IAActive)
        {
            ChangeMode();
            PlayerTwoController(customVelocity);
        }
        else
        {
            ChangeMode();
            BallTransform = GameObject.Find("bola").transform;
            IaController();
        }
    }

    void PlayerOneController(float customVelocity)
    {
        if (transform.position.y < limit && Input.GetKey(KeyCode.W))
        {
            positionPlayerY += customVelocity;
        }
        else if (transform.position.y > -limit && Input.GetKey(KeyCode.S))
        {
            positionPlayerY -= customVelocity;
        }

        MovePlayer();
    }

    void PlayerTwoController(float customVelocity)
    {
        if (transform.position.y < limit && Input.GetKey(KeyCode.UpArrow))
        {
            positionPlayerY += customVelocity;
        }
        else if (transform.position.y > -limit && Input.GetKey(KeyCode.DownArrow))
        {
            positionPlayerY -= customVelocity;
        }

        MovePlayer();
    }

    void ChangeMode()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            IAActive = false;
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            IAActive = true;
        }
    }

    void IaController()
    {
        positionPlayerY = Mathf.Lerp(positionPlayerY, BallTransform.position.y, 0.03f);

        if (positionPlayerY < -limit)
        {
            positionPlayerY = -limit;
        }
        else if (positionPlayerY > limit)
        {
            positionPlayerY = limit;
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        positionPlayer.y = positionPlayerY;
        transform.position = positionPlayer;
    }
}
