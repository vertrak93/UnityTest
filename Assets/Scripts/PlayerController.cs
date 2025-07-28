using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;

    private bool walkingState = false;
    public Vector2 lastMovement = Vector2.zero;

    private Vector2 move = Vector2.zero;           // ✅ Agregá esta línea
    private Vector2 virtualMove = Vector2.zero;    // ✅ Agregá esta línea

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    private const string _walking = "Walking";

    private Animator _animator;

    private Rigidbody2D playerRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
         //playerRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
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

        if (move != Vector2.zero)
        {
            // move.Normalize();
            // transform.Translate(move * speed * Time.deltaTime);

            lastMovement = virtualMove;
            walkingState = true;
        }

        _animator.SetFloat(_horizontal, Input.GetAxisRaw(_horizontal));
        _animator.SetFloat(_vertical, Input.GetAxisRaw(_vertical));
        _animator.SetBool(_walking, walkingState);
        _animator.SetFloat(_lastVertical, lastMovement.y);
        _animator.SetFloat(_lastHorizontal, lastMovement.x);
    }
    
    void FixedUpdate()
    {
        if (move != Vector2.zero)
        {
            move.Normalize();
            Vector2 targetPos = playerRigidbody.position + move * speed * Time.deltaTime;
            targetPos.x = Mathf.Round(targetPos.x * 100f) / 100f;
            targetPos.y = Mathf.Round(targetPos.y * 100f) / 100f;
            playerRigidbody.MovePosition(targetPos);

            // 🔁 Mantener rotación fija si Unity la modifica
            playerRigidbody.rotation = 0f;
        }
    }
}
