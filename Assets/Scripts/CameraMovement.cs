using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _startPositionCamera;
    [SerializeField] private Transform _endPositionCamera;

    private bool _isStartMoving;
    private bool _isEndMoving;

    private float _timer;

    public void Initialization()
    {
        _timer = 0;
        _isStartMoving = false;
        _isEndMoving = false;
        transform.position = _endPositionCamera.position;
        _isStartMoving = true;

    }

    private void Update()
    {
        if (_isStartMoving)
        {
            _timer += Time.deltaTime;
            if (_timer < 1)
            {
                LerpToPosition(_timer, _endPositionCamera.position, _startPositionCamera.position);
            }
            else
            {
                _timer = 0;
                _isStartMoving = false;
            }

        }

        if (_isEndMoving)
        {
            _timer += Time.deltaTime * 0.7f;
            if (_timer < 1)
            {
                LerpToPosition(_timer, _startPositionCamera.position, _endPositionCamera.position);
            }
            else
            {
                _timer = 0;
                _isEndMoving = false;
            }

        }

    }
    private void LerpToPosition(float timer, Vector3 startPosition, Vector3 endPosition)
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, timer);
    }

    public void MovingCameraToPlayer()
    {
        _isEndMoving = true;
    }

 



    



}
