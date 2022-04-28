using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier=7.5f;
    [SerializeField] private float slowSpeedMultiplier=2.0f;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private GameObject ParticlesLeft;
    [SerializeField] private GameObject ParticlesRight;
    [SerializeField] private GameObject ParticlesUp;
    [SerializeField] private GameObject ParticlesDown;
    private ParticleSystem particleSystemRight;
    private ParticleSystem particleSystemLeft;
    private ParticleSystem particleSystemUp;
    private ParticleSystem particleSystemDown;
    private float _currentSpeed;
    private float _horizontal;
    private float _vertical;
    private Vector3 _scale;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    void Start()
    {
        particleSystemLeft = ParticlesLeft.GetComponent<ParticleSystem>();
        particleSystemRight = ParticlesRight.GetComponent<ParticleSystem>();
        particleSystemUp = ParticlesUp.GetComponent<ParticleSystem>();
        particleSystemDown = ParticlesDown.GetComponent<ParticleSystem>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "AttackAnimation")
        {
            Attacking();
        }
        else
        {
            Walking();
        }
        _horizontal = Input.GetAxis("Horizontal");
        switch (_horizontal)
        {
            case < 0:
            {
                var emissionLeft = particleSystemLeft.emission;
                emissionLeft.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                var emissionRight = particleSystemRight.emission;
                emissionRight.rateOverTime = new ParticleSystem.MinMaxCurve(100, 100);
                transform.localScale = new Vector3(_scale.x * -1, _scale.y, _scale.z);
                break;
            }
            case > 0:
            {
                var emissionLeft = particleSystemLeft.emission;
                emissionLeft.rateOverTime = new ParticleSystem.MinMaxCurve(100, 100);
                var emissionRight = particleSystemRight.emission;
                emissionRight.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
                break;
            }
            default:
            {
                var emissionLeft = particleSystemLeft.emission;
                emissionLeft.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                var emissionRight = particleSystemRight.emission;
                emissionRight.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                break;
            }
        }
        _vertical = Input.GetAxis("Vertical");
        switch (_vertical)
        {
            case < 0:
            {
                var emissionUp = particleSystemUp.emission;
                emissionUp.rateOverTime = new ParticleSystem.MinMaxCurve(100, 100);
                var emissionDown = particleSystemDown.emission;
                emissionDown.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                break;
            }
            case > 0:
            {
                var emissionUp = particleSystemUp.emission;
                emissionUp.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                var emissionDown = particleSystemDown.emission;
                emissionDown.rateOverTime = new ParticleSystem.MinMaxCurve(100, 100);
                break;

            }
            default:
            {
                var emissionUp = particleSystemUp.emission;
                emissionUp.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                var emissionDown = particleSystemDown.emission;
                emissionDown.rateOverTime = new ParticleSystem.MinMaxCurve(0, 0);
                break;
            }
        }
        _animator.SetBool("Attacking", Input.GetKey(KeyCode.Space));
    }

    private void Walking()
    {
        attackCollider.enabled = false;
        _currentSpeed = speedMultiplier;
    }

    private void Attacking()
    {
        attackCollider.enabled = true;
        _currentSpeed = slowSpeedMultiplier;
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(_horizontal, _vertical);
        _rigidbody2D.velocity = movement * _currentSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Guard"))
        {
            Destroy(collider2D.gameObject);
        }
    }
}