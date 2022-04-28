using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier=7.5f;
    [SerializeField] private float slowSpeedMultiplier=2.0f;
    [SerializeField] private BoxCollider2D attackCollider;
    private float _currentSpeed;
    private float _horizontal;
    private float _vertical;
    private Vector3 _scale;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    void Start()
    {
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
        if (_horizontal < 0)
        {
            transform.localScale = new Vector3(_scale.x * -1, _scale.y, _scale.z);
        }
        else if (_horizontal > 0)
        {
            transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
        }
        _vertical = Input.GetAxis("Vertical");
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
}