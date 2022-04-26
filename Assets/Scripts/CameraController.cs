using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _minAngleZ;
    [SerializeField] private float _maxAngleZ;
    [SerializeField] private float _waitTime;
    private bool _reverseTurn;
    private float _timeWaited;
    void Start()
    {
         _timeWaited = _waitTime;
    }
    void FixedUpdate()
    {
        if (_timeWaited>=_waitTime)
        {
            if (!_reverseTurn)
            {
                if (transform.rotation.eulerAngles.z <= _maxAngleZ)
                {
                    transform.Rotate(new Vector3(0,0,_rotateSpeed));
                }
                else
                {
                    _reverseTurn = true;
                    _timeWaited = 0f;
                }
            }
            else
            {
                if (transform.rotation.eulerAngles.z >= _minAngleZ)
                {
                    transform.Rotate(new Vector3(0, 0, _rotateSpeed*-1));
                }
                else
                {
                    _reverseTurn = false;
                    _timeWaited = 0f;
                }
            }
        }
        _timeWaited += Time.fixedDeltaTime;
    }
}