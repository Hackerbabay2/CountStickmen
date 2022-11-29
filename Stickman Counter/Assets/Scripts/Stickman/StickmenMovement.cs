using UnityEngine;

public class StickmenMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sensitivity = 5;
    [SerializeField] private UnitsManager _unitsManager;

    private float _xDirection;
    private float _startX;
    private Vector3 _elemetaryX;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _elemetaryX = Input.mousePosition;
            _startX = transform.position.x;
        }

        if (Input.GetMouseButton(0))
        {
            _xDirection = (_elemetaryX.x - Input.mousePosition.x) / _sensitivity;
            transform.position = new Vector3(_startX - _xDirection,transform.position.y,transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.D))
            _unitsManager.RemoveEndUnit();
        float barrier = Mathf.Clamp(4.5f - _unitsManager.GetSpiralRadius(),-4.5f,4.5f);

        if (transform.position.x > barrier)
            transform.position = new Vector3(barrier,transform.position.y,transform.position.z);
        else if (transform.position.x < -barrier)
            transform.position = new Vector3(-barrier,transform.position.y,transform.position.z);
        transform.position += Vector3.forward * _speed * Time.deltaTime;
    }
}