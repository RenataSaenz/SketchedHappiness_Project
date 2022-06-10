using UnityEngine;

public class Movement
{
    Transform _transform;
    float _speed;
    float _jumpForce;
    Rigidbody _rb;
    [SerializeField]
    private Transform _camTransform;

    private Vector3 _velocity;

    public Movement(Transform t, float speed, float jumpForce, Rigidbody rb, Transform camTransform, Vector3 velocity)
    {
        _transform = t;
        _speed = speed;
        _jumpForce = jumpForce;
        _rb = rb;
        _camTransform = camTransform;
        _velocity = velocity;
    }
    public void Jump()
    { 
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse); 
    }

    public void Move(float v, float h)
    {
        // _velocity.Set(h, 0, v);
        // _velocity.Normalize();
        // if (h != 0 || v != 0)  _transform.forward = _velocity;
        //
        // _transform.position += _velocity * _speed * Time.deltaTime;
        
         _transform.position += _camTransform.forward * v * _speed * Time.deltaTime;
        _transform.position += _camTransform.right * h * _speed * Time.deltaTime;

    }
    public void Run(float v, float h)
    {
        _transform.position += _camTransform.forward * v * (_speed * 1.2f) * Time.deltaTime;
        _transform.position += _camTransform.right * h * (_speed * 1.2f) * Time.deltaTime;
    }

    public void Rotate()
    {
       //var yRotation = _camTransform.eulerAngles.y;
       // _transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
