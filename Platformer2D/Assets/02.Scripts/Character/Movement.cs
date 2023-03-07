using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public bool isMovable;
    public bool isDirectionChangeable;

    public int dir
    {
        get => _dir;
        set
        {
            if (value > 0)
            {
                transform.eulerAngles = Vector3.zero;
                _dir = Constants.DIRECTION_RIGHT;
            }
            else if (value < 0)
            {
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                _dir = Constants.DIRECTION_LEFT;
            }
        }
    }
    private int _dir;
    public float speed = 2.0f;
    public float h, v;
    public bool isInputValid => Mathf.Abs(h) > _minInputDelta;

    public Vector2 move;
    [SerializeField] private float _minInputDelta = 0.05f;

    private Rigidbody2D _rb;

    public void SetMove(Vector2 move)
    {
        this.move = move;
    }

    public void StopMove()
    {
        move = Vector2.zero;
        _rb.velocity = Vector2.zero;
    }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (isInputValid == false)
        {
            if (isMovable)
                move = Vector2.zero;

            return;
        }

        if (isDirectionChangeable)
        {
            dir = h > 0 ? Constants.DIRECTION_RIGHT : Constants.DIRECTION_LEFT;
        }

        if (isMovable)
        {
            move = new Vector2(h, 0.0f);
        }
    }

    private void FixedUpdate()
    {
        if (isMovable)
        {
            _rb.MovePosition(_rb.position + move * speed * Time.fixedDeltaTime);
        }
    }
}