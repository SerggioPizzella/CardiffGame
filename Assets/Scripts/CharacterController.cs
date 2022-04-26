using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    private float _horizontal;
    private float _vertical;
    private Vector3 _scale;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _scale = transform.localScale;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
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
    }
    
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(_horizontal, _vertical);
        _rigidbody2D.velocity = movement * speedMultiplier;
    }
}