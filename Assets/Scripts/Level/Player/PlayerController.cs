using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpSpeed;

    private PlayerInputController _playerInputController;
    private Rigidbody _rigidBody;
    private bool _jumpTriggered;

    void Awake()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _rigidBody = GetComponent<Rigidbody>();

        _playerInputController.OnJumpButtonPressed += JumpButtonPressed;
    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(
            _playerInputController.MovementInputVector.x,
            0,
            _playerInputController.MovementInputVector.y)
            *_speed;
        velocity.y = _rigidBody.linearVelocity.y;
        
        if(_jumpTriggered)
        {
            velocity.y = _jumpSpeed;
            _jumpTriggered = false;
        }
        _rigidBody.linearVelocity = velocity;
    }

    private void JumpButtonPressed()
    {
        _jumpTriggered = true;

    }
}
