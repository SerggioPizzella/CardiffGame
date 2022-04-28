using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier=7.5f;
    [SerializeField] private float slowSpeedMultiplier=2.0f;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private GameObject Particles;
    public float Health = 100;
    private float stabSoundLength;
    private float stabSoundPlaying;
    private float currentSoundLength;
    private float playlength;
    public AudioClip[] sounds;
    public AudioClip StabSound;
    private AudioSource source;
    private ParticleSystem _particleSystem;
    private float _currentSpeed;
    private float _horizontal;
    private float _vertical;
    private Vector3 _scale;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    void Start()
    {
        stabSoundLength = StabSound.length;
        source = GetComponent<AudioSource>();
        _particleSystem = Particles.GetComponent<ParticleSystem>();
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
                transform.localScale = new Vector3(_scale.x * -1, _scale.y, _scale.z);
                break;
            }
            case > 0:
            {
                transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
                break;
            }
        }
        _vertical = Input.GetAxis("Vertical");
        if (_vertical == 0 & _horizontal == 0)
        {
            var em = _particleSystem.emission;
            em.rateOverTime = 0;
        }
        else
        {
            var em = _particleSystem.emission;
            em.rateOverTime = 100;
            if (playlength >= currentSoundLength)
            {
                source.clip = sounds[Random.Range(0, sounds.Length)];
                currentSoundLength = source.clip.length;
                source.Play();
                playlength = -0.3f;
            }
            playlength += Time.deltaTime;
        }

        if (Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
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
        if (stabSoundPlaying >= stabSoundLength)
        {
            source.PlayOneShot(StabSound);
            stabSoundPlaying = -0.25f;
        }

        stabSoundPlaying += Time.deltaTime;
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
            collider2D.GetComponentInChildren<Animator>().SetBool("Death", true);
        }
    }
}