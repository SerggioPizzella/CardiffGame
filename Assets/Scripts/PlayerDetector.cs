using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("GameOver");
    }
}