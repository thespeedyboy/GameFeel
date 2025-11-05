using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float _moveX = 1f;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void SetCurrentDirection(float currentDirection)
    {
        _moveX = currentDirection;
    }
    private void Move()
    {
        Vector2 Movement = new Vector2(_moveX * speed, _rigidBody.linearVelocity.y);
        _rigidBody.linearVelocity = Movement;
    }
}
