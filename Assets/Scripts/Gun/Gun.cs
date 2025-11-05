using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;
using Unity.Cinemachine;

public class Gun : MonoBehaviour
{
    public Action OnShoot;

    
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _gunFireCD = 0.5f;

    private ObjectPool<Bullet> _bulletPool;
    private static readonly int FIRE_HASH = Animator.StringToHash("fire");
    private Vector2 _mousePosition;
    private float _lastFireTime = 0f;

    private CinemachineImpulseSource _impulseSource;
    private Animator _animator;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _animator = GetComponent<Animator>();
    } 
    private void Start()
    {
        CreateBulletPool();
    }
    private void Update()
    {
        Shoot();
        RotateGun();    
    }
    private void OnEnable()
    {
       OnShoot += ShootProjectile;
       OnShoot += ResetLastFireTime;
       OnShoot += FireAnimation;
       OnShoot += TriggerImpulse;

    }
    private void OnDisable()
    {
        OnShoot -= ShootProjectile;
        OnShoot -= ResetLastFireTime;
        OnShoot -= FireAnimation;
        OnShoot -= TriggerImpulse;
    }
    public void ReleaseBulletFromPool(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
    private void CreateBulletPool()
    {
        _bulletPool = new ObjectPool<Bullet>(() =>
        {
            return Instantiate(_bulletPrefab);
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, Bullet =>
        {
            Bullet.gameObject.SetActive(false);
        }, Bullet =>
        {
            Destroy(Bullet.gameObject);
        },false);
       
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= _lastFireTime) {
           OnShoot?.Invoke();
        }
    }
    private void ResetLastFireTime()
    {
        _lastFireTime = Time.time + _gunFireCD;
    }
    private void TriggerImpulse()
    {
        _impulseSource.GenerateImpulse();
    }

    private void ShootProjectile()
    {
        Bullet newBullet = _bulletPool.Get();
        newBullet.Init(this, _bulletSpawnPoint.position, _mousePosition);
    }
    private void FireAnimation()     {
        _animator.Play(FIRE_HASH,0,0f);
    }
    private void RotateGun(){
       _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = PlayerController.Instance.transform.InverseTransformPoint(_mousePosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
