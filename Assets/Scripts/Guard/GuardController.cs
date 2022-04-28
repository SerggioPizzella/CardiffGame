using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    public float DamageCooldown=1;
    private float dealtDamage;
    private GameObject player;

    public void Start()
    {
        player = FindObjectOfType<CharacterController>().gameObject;
    }

    public void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= 1)
        {
            if (dealtDamage >= DamageCooldown)
            {
                player.GetComponent<CharacterController>().Health-=25;
                dealtDamage = 0;
            }
            dealtDamage += Time.deltaTime;
        }
    }

    public void startMoving()
    {
        aiPath.canMove = true;
    }
}
