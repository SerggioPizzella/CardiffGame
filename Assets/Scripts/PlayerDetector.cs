using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetector : MonoBehaviour
{
    public float DetectionTime = 0.5f;
    private float _detectedTime = 0f;
    private GameObject _detectionBar;
    private Transform _detectionBarTransform;

    void Start()
    {
        _detectionBar = GameObject.Find("DetectionBar");
        _detectionBarTransform = _detectionBar.transform;
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _detectedTime += Time.deltaTime;
            _detectionBarTransform.localScale = Vector3.Lerp(new Vector3(0, _detectionBarTransform.localScale.y, _detectionBarTransform.localScale.z), new Vector3(0.1f, _detectionBarTransform.localScale.y, _detectionBarTransform.localScale.z), _detectedTime / DetectionTime);

        }

        if (_detectedTime >= DetectionTime)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _detectedTime = 0f;
            _detectionBarTransform.localScale = new Vector3(0, _detectionBarTransform.localScale.y, _detectionBarTransform.localScale.z);
        }
    }
}