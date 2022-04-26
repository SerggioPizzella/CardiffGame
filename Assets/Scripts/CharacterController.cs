using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    private float _horizontal;
    private float _vertical;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }
    
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(_horizontal, _vertical);
        _rigidbody2D.velocity = movement * speedMultiplier;
    }
}