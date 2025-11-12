using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _bulletVFX;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _KnockbackTrust = 20;

    private Vector2 _fireDirection;
    private Rigidbody2D _rigidBody;
    private Gun _gun;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = _fireDirection * _moveSpeed;
    }
    public void Init(Gun gun ,Vector2 bulletSpawnPos, Vector2 mousePos)
    {
      _gun = gun;
      transform.position = bulletSpawnPos;
     _fireDirection = (mousePos - bulletSpawnPos).normalized;
    }

   private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
          return; 
        }else if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground"))
        {
            Instantiate(_bulletVFX, transform.position, transform.rotation);
            Health health = other.gameObject.GetComponent<Health>();
            health?.TakeDamage(_damageAmount);

            Knockback knockback = other.gameObject.GetComponent<Knockback>();
            knockback?.GetKnockedBack(PlayerController.Instance.transform.position, _KnockbackTrust);

            Flash flash = other.gameObject.GetComponent<Flash>();
            flash?.StartFlash();

            _gun.ReleaseBulletFromPool(this);

        }
       
    }
}