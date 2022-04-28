using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;

    public void startMoving()
    {
        aiPath.canMove = true;
    }
}
