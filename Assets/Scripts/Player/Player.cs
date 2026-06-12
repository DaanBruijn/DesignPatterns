using UnityEngine;

// - Base class for the PlayerObject
// - Handles the Movement and Camera
// - Daniel Bruijn

public class Player
{
    // - Variables
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;
    
    private float _sensitivity = 250f;
    
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private Rigidbody _rigidbody;

    private float _yaw;
    private float _pitch;

    public Player(Transform _playerTransform, Transform _cameraTransform, Rigidbody _rigidbody)
    {
        this._playerTransform = _playerTransform;
        this._cameraTransform = _cameraTransform;
        this._rigidbody = _rigidbody;
    }
    
    
    // - Movement
    public void Move(Vector3 direction, float speed)
    {
        Vector3 velocity = _rigidbody.linearVelocity;
        Vector3 move = direction * speed;
        
        velocity.x = move.x;
        velocity.z = move.z;
        
        _rigidbody.linearVelocity = velocity;
    }

    public void Look(float mouseX, float mouseY)
    {
        _yaw += mouseX * _sensitivity * Time.deltaTime;
        _pitch -= mouseY * _sensitivity * Time.deltaTime;
        
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        
        _playerTransform.rotation = Quaternion.Euler(0f, _yaw, 0f);
        _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    public Vector3 GetMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = _playerTransform.forward * z + _playerTransform.right * x;
        direction.y = 0f;
        
        return direction.normalized;
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(_playerTransform.position, Vector3.down, 1.1f);
    }
    
    // - Shooting
    public bool TryRayCast(out RaycastHit hit)
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        return Physics.Raycast(ray, out hit, 100f);
    }
}
