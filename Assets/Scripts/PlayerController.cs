using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private bool isRootMotion = true;
    [SerializeField] private Transform rootCharacter;

    private PlayerInputs _inputs;
    private CharacterController _controller;
    private Animator _animator;
    private Camera _mainCamera;
    
    private float _speedVelocity;
    private float _angleVelocity;
    private float _horizontalSpeed;
    
    
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] float gravity = -9.81f;
    private Vector3 _spherePos;
    private Vector3 _velocity;



    void Start()
    {
        _inputs = GetComponent<PlayerInputs>();
        _controller = GetComponentInChildren<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _mainCamera = Camera.main;
        
    }

    void Update()
    {
        _horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;

        if (isRootMotion)
        {
            _animator.applyRootMotion = true;
            if (_inputs.Move.magnitude >= Mathf.Epsilon)
            {
                //Player movement animation
                _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), _inputs.Move.magnitude * _horizontalSpeed, ref _speedVelocity, 0.25f));

                RotatePlayerWithCamera();
            }
            else
            {
                //Player stops moving
                _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), 0f, ref _speedVelocity, 0.025f));
            }
        }
        else
        {
            //Non RootMotion Movement
            _animator.applyRootMotion = false;
            if (_inputs.Move.magnitude >= Mathf.Epsilon)
            {
                //Player movement animation
                _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), _inputs.Move.magnitude * _horizontalSpeed, ref _speedVelocity, 0.25f));

                RotatePlayerWithCamera();
                GetDirectionAndMove();
            }
            else
            {
                //Player stops moving
                _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), 0f, ref _speedVelocity, 0.025f));
            }
            
            Gravity();
        }
    }
    private void RotatePlayerWithCamera()
    {
        //Player facing away from camera
        float targetAngle = _mainCamera.transform.rotation.eulerAngles.y;
        targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;

        float actualAngle = Mathf.SmoothDampAngle(rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);

        rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);
    }

    private void GetDirectionAndMove()
    {
        //Non RootMotion movement
        Vector3 cameraForward = _mainCamera.transform.forward;
        Vector3 cameraRight = _mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 direction = cameraForward * _inputs.Move.y + cameraRight * _inputs.Move.x;
        _controller.Move(direction * (_horizontalSpeed * Time.deltaTime));
    }
    
    bool IsGrounded()
    {
        _spherePos = new Vector3(_controller.transform.position.x, _controller.transform.position.y - groundYOffset, _controller.transform.position.z);
        if(Physics.CheckSphere(_spherePos, _controller.radius - 0.05f, groundMask)) return true;
        return false;
    }
    
    void Gravity()
    {
        if(!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
        else if(_velocity.y < 0) _velocity.y = -2;
        
        _controller.Move(_velocity * Time.deltaTime);
    }
}