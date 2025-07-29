using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;

    private bool walkingState = false;
    public Vector2 lastMovement = Vector2.zero;
    private Vector2 move = Vector2.zero;
    private Vector2 virtualMove = Vector2.zero;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    private const string _walking = "Walking";
    private const string _jump = "Jump";

    private Animator _animator;

    private Rigidbody2D playerRigidbody;


    Vector2 currentPos;
    Vector2 landingPos;
    float landingDis;
    float timeElapsed = 0f;
    bool onGround = true;
    bool jump = false;

    [SerializeField] AnimationCurve curveY;
    [SerializeField] float jumpDistance = 1f;
    void Start()
    {
        _animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.freezeRotation = true;
    }

    void Update()
    {
        InpuntHandler();
    }

    void FixedUpdate()
    {
        if(jump)
        {
            JumpHandler();
        }
        else
        {
            MovementHandler();
        }
    }

    void MovementHandler()
    {
        if (move != Vector2.zero)
        {
            move.Normalize();
            Vector2 targetPos = playerRigidbody.position + move * speed * Time.deltaTime;
            targetPos.x = Mathf.Round(targetPos.x * 100f) / 100f;
            targetPos.y = Mathf.Round(targetPos.y * 100f) / 100f;
            playerRigidbody.MovePosition(targetPos);
            playerRigidbody.rotation = 0f;
        }
    }

    void InpuntHandler()
    {
        walkingState = false;

        move = Vector2.zero;
        virtualMove = Vector2.zero;

        // Diagonales primero
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            move = new Vector2(1f, 0.5f);
            virtualMove = new Vector2(1f, 1f);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            move = new Vector2(-1f, 0.5f);
            virtualMove = new Vector2(-1f, 1f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            move = new Vector2(1f, -0.5f);
            virtualMove = new Vector2(1f, -1f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            move = new Vector2(-1f, -0.5f);
            virtualMove = new Vector2(-1f, -1f);
        }
        // Movimiento recto
        else if (Input.GetKey(KeyCode.W))
        {
            move = new Vector2(0f, 1f);
            virtualMove = move;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move = new Vector2(0f, -1f);
            virtualMove = move;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = new Vector2(1f, 0f);
            virtualMove = move;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move = new Vector2(-1f, 0f);
            virtualMove = move;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jump = true;
        }

        if (move != Vector2.zero)
        {
            lastMovement = virtualMove;
            walkingState = true;
        }

        _animator.SetFloat(_horizontal, Input.GetAxisRaw(_horizontal));
        _animator.SetFloat(_vertical, Input.GetAxisRaw(_vertical));
        _animator.SetBool(_walking, walkingState);
        _animator.SetFloat(_lastVertical, lastMovement.y);
        _animator.SetFloat(_lastHorizontal, lastMovement.x);
        _animator.SetBool(_jump, jump);
    }

    void JumpHandler()
    {
        if(onGround)
        {
            currentPos = playerRigidbody.position;
            landingPos = currentPos + move.normalized * speed;
            landingDis = Vector2.Distance(landingPos, currentPos);
            timeElapsed = 0f;
            onGround = false;
        }
        else
        {
            timeElapsed += Time.fixedDeltaTime * speed / landingDis;
            if(timeElapsed <= 1f)
            {
                currentPos = Vector2.MoveTowards(currentPos, landingPos, Time.fixedDeltaTime * speed);
                playerRigidbody.MovePosition(new Vector2(currentPos.x, currentPos.y + curveY.Evaluate(timeElapsed)));
            }
            else
            {
                jump = false;
                onGround = true;
            }
        }
    }

}

