using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float _moveX = 1f;
    private bool _canMove = true;

    private Rigidbody2D _rigidBody;
    private Knockback _knockback;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnEnable()
    {
        _knockback.OnKnockbackStart += canMoveFalse;
        _knockback.OnKnockbackEnd += canMoveTrue;
    }
    private void OnDisable()
    {
        _knockback.OnKnockbackStart -= canMoveFalse;
        _knockback.OnKnockbackEnd -= canMoveTrue;
    }

    public void SetCurrentDirection(float currentDirection)
    {
        _moveX = currentDirection;
    }
    private void canMoveTrue()
    {
               _canMove = true;
    }
    private void canMoveFalse()
    {
        _canMove = false;
        
    }
    private void Move()
    {
        if (!_canMove) { return; }

        Vector2 Movement = new Vector2(_moveX * speed, _rigidBody.linearVelocity.y);
        _rigidBody.linearVelocity = Movement;
    }
}
