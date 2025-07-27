using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;

    private bool walkingState = false;
    public Vector2 lastMovement = Vector2.zero;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    private const string _walking = "Walking";

    private Animator _animator;

    // Direcciones isométricas (normalizadas para velocidad constante)
    private Vector2 isoUp = new Vector2(1f, 0.5f).normalized;
    private Vector2 isoDown = new Vector2(-1f, -0.5f).normalized;
    private Vector2 isoLeft = new Vector2(-1f, 0.5f).normalized;
    private Vector2 isoRight = new Vector2(1f, -0.5f).normalized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        walkingState = false;

        //if (Mathf.Abs(Input.GetAxisRaw(_horizontal)) > 0.5f)
        //{
        //    this.transform.Translate(new Vector3(Input.GetAxisRaw(_horizontal) * speed * Time.deltaTime, 0, 0));
        //    walkingState = true;
        //    lastMovement = new Vector2(Input.GetAxisRaw(_horizontal), 0);
        //}

        //if (Mathf.Abs(Input.GetAxisRaw(_vertical)) > 0.5f)
        //{
        //    this.transform.Translate(new Vector3(0, Input.GetAxisRaw(_vertical) * speed * Time.deltaTime, 0));
        //    walkingState = true;
        //    lastMovement = new Vector2(0, Input.GetAxisRaw(_vertical));
        //}

        float inputX = Input.GetAxisRaw(_horizontal);
        float inputY = Input.GetAxisRaw(_vertical);

        Vector2 movement = Vector2.zero;

        //if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        //{
        //    walkingState = true;

        //    // Combina input en espacio isométrico
        //    movement = (isoLeft * inputX + isoDown * inputY).normalized;

        //    this.transform.Translate(movement * speed * Time.deltaTime);

        //    lastMovement = movement;
        //}

        // Detectar combinaciones manualmente

        Vector2 move = Vector2.zero;
        Vector2 virtualMove = Vector2.zero;

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
            move.Normalize();
            transform.Translate(move * speed * Time.deltaTime);
            lastMovement = virtualMove;
            walkingState = true;
        }


        _animator.SetFloat(_horizontal, Input.GetAxisRaw(_horizontal));
        _animator.SetFloat(_vertical, Input.GetAxisRaw(_vertical));
        _animator.SetBool(_walking, walkingState);
        _animator.SetFloat(_lastVertical, lastMovement.y);
        _animator.SetFloat(_lastHorizontal, lastMovement.x);
    }
}
