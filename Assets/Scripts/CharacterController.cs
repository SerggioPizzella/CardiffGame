using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        GetComponent<Rigidbody2D>().velocity = movement * speedMultiplier;
    }
}