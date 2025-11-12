using Unity.Cinemachine;
using UnityEngine;

public class playerAnimations : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moveDustVFX;
    [SerializeField] private ParticleSystem _poofDustVFX;
    [SerializeField] private float _tiltAngle = 20f;
    [SerializeField] private float _tiltSpeed = 5f;
    [SerializeField] private Transform _characterModelTransform;
    [SerializeField] private Transform _cowBoyHatTransform;
    [SerializeField] private float _cowBoyHatTiltModifier = 2f;
    [SerializeField] private float _yLandVelocityCheck = -10f;

    private Vector2 _velocityBeforePhysicsUpdate;
    private Rigidbody2D _rigidbody2D;
    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void Update()
    {
        DeTectMoveDust();
        ApplyTilt();
    }
    private void OnEnable()
    {
        PlayerController.OnJump += PlayPoofDustVFX;
    }
    private void OnDisable()
    {
        PlayerController.OnJump -= PlayPoofDustVFX;
    }
    private void FixedUpdate()
    {
        _velocityBeforePhysicsUpdate = _rigidbody2D.linearVelocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_velocityBeforePhysicsUpdate.y < _yLandVelocityCheck)
        {
            PlayPoofDustVFX();
           _impulseSource.GenerateImpulse();
        }
    }
    private void DeTectMoveDust()
    {
        if (PlayerController.Instance.checkGrounded())
        {
            if(!_moveDustVFX.isPlaying)
            {
                _moveDustVFX.Play();
            }
        }
        else
        {
            if (_moveDustVFX.isPlaying)
            {
                _moveDustVFX.Stop();
            }
        }
    }
    private void PlayPoofDustVFX()
    {
        _poofDustVFX.Play();
    }
    private void ApplyTilt()
    {
        float targetAngle;
        if(PlayerController.Instance.MoveInput.x > 0)
        {
            targetAngle = -_tiltAngle;
        }
        else if (PlayerController.Instance.MoveInput.x < 0)
        {
            targetAngle = _tiltAngle;
        }
        else
        {
            targetAngle = 0f;
        }
        //Player sprite
        Quaternion currentCharacterRotation = _characterModelTransform.rotation;
        Quaternion targetCharacterRotation = 
        Quaternion.Euler(currentCharacterRotation.eulerAngles.x, currentCharacterRotation.eulerAngles.y, targetAngle);

        _characterModelTransform.rotation = 
        Quaternion.Lerp(currentCharacterRotation,targetCharacterRotation, _tiltSpeed * Time.deltaTime);
        //Cowboy hat
        Quaternion currentHatRotation = _cowBoyHatTransform.rotation;
        Quaternion targetHatRotation =
        Quaternion.Euler(currentHatRotation.eulerAngles.x, currentHatRotation.eulerAngles.y, -targetAngle / _cowBoyHatTiltModifier);

        _cowBoyHatTransform.rotation =
        Quaternion.Lerp(currentHatRotation, targetHatRotation, _tiltSpeed * _cowBoyHatTiltModifier * Time.deltaTime);
    }
}
