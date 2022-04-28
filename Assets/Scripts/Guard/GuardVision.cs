using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GuardVision : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI alertMesh;
    [SerializeField] private float detectionTime = 0.5f;
    [SerializeField] private UnityEvent guardCaughtPlayer;
    private AIPath aiPath;
    private bool caught;

    private float _detectedTime;

    void Start()
    {
        _detectedTime = 0;
        alertMesh.text = "";
        alertMesh.color = Color.yellow;
        aiPath = GetComponentInParent<AIPath>();
    }

    void Update()
    {
        if (aiPath.canMove)
        {
            alertMesh.color = Color.red;
            alertMesh.text = "!!!";
            caught = true;
            guardCaughtPlayer.Invoke();
        }
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _detectedTime += Time.deltaTime;
        }

        if (_detectedTime > detectionTime)
        {
            alertMesh.color = Color.red;
            alertMesh.text = "!!!";
            caught = true;
            guardCaughtPlayer.Invoke();
        }
        else if (_detectedTime >= detectionTime / 3 * 2)
        {
            alertMesh.text = "!!";
        }
        else if (_detectedTime >= detectionTime / 3)
        {
            alertMesh.text = "!";
        }
        else
        {
            alertMesh.text = "";
        }
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        if(caught)
            return;
        if (collider2D.CompareTag("Player"))
        {
            _detectedTime = 0f;
            alertMesh.text = "";
            alertMesh.color = Color.yellow;
        }
    }
}
