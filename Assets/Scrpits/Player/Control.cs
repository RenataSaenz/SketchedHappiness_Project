using UnityEngine;

public class Control
{
    Animator _animator;
    string _runningParameterName;
    string _movementSpeedParameterName;
    string _groundedParameterName;
    string _jumpingParameterName;
    Movement _movement;
    private Vector3 _movementInput;

    public Control(Movement m, Animator animator, string runningParameterName, string movementSpeedParameterName, string groundedParameterName, string jumpingParameterName)
    {
        _movement = m;
        _animator = animator;
        _runningParameterName = runningParameterName;
        _movementSpeedParameterName = movementSpeedParameterName;
        _groundedParameterName = groundedParameterName;
        _jumpingParameterName = jumpingParameterName;
    }

    public void OnUpdate()
    {
        _movementInput.z = Input.GetAxis("Vertical");
        _movementInput.x = Input.GetAxis("Horizontal");

        float shift = Input.GetAxis("Shift");

        _animator.SetFloat(_movementSpeedParameterName, _movementInput.magnitude);

        if (_movementInput.z != 0 || _movementInput.x != 0)
            _movement.Move(_movementInput.z, _movementInput.x);

        if ( _movementInput.x != 0 && shift != 0)
        {
            _animator.SetBool(_runningParameterName, true);
            _movement.Run(_movementInput.z, _movementInput.x);
        }
        else
        {
            _animator.SetBool(_runningParameterName, false);
        }

        if (_movementInput.z != 0 && shift != 0)
        {
            _animator.SetBool(_runningParameterName, true);
            _movement.Run(_movementInput.z, _movementInput.x);
        }
        else
        {
            _animator.SetBool(_runningParameterName, false);
        }
        _movement.Rotate();

        KeyE();
    }

    public void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && _animator.GetBool(_groundedParameterName) == true)
        {
            _animator.SetBool(_jumpingParameterName, true);
            _movement.Jump();
            AudioManager.instance.Play(AudioManager.Types.Jump);
        }
        else
            _animator.SetBool(_jumpingParameterName, false);
    }

    public void KeyE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //EventManager.Trigger("KeyEDown");
        }
    }


}
