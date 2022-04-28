using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GuardAnimetionController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float idleSpeedRange = 0.5f;
    [SerializeField] private Animator animator;
    [SerializeField] private AIPath aiPath;

    private bool moving = false;
    private bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Death"))
        {
            gameObject.GetComponentInParent<AIPath>().canMove = false;
            gameObject.GetComponentInParent<BoxCollider2D>().enabled = false;
            transform.parent.GetComponentInChildren<PolygonCollider2D>().enabled = false;
            return;
        }
        if (!facingRight && aiPath.desiredVelocity.x > 0.1f)
        {
            Flip();
        }
        else if (facingRight && aiPath.desiredVelocity.x < -0.1f)
        {
            Flip();
        }

        moving = aiPath.desiredVelocity.magnitude > idleSpeedRange;
        animator.SetBool("moving", moving);
    }

    private void Flip()
    {
        facingRight = !facingRight;

        spriteRenderer.flipX = !facingRight;
    }
}
