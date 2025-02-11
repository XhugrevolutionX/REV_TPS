using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 8f;
    
    [SerializeField] private Transform rootCharacter;
    
    private PlayerInputs _inputs;
    private CharacterController _controller;
    private Animator _animator;
    
    private float _speedVelocity;
    private float _angleVelocity;
    void Start()
    {
        _inputs = GetComponent<PlayerInputs>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        if (_inputs.Move.magnitude >= Mathf.Epsilon)
        {
            //Player movement
            float horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;
            _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), _inputs.Move.magnitude * horizontalSpeed, ref _speedVelocity, 0.25f));
            
            //Player facing away from camera
            float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
            targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;

            float actualAngle = Mathf.SmoothDampAngle(rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);

            rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);
        }
        else
        {
            //Player Stops moving
            _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), 0f, ref _speedVelocity, 0.025f));
        }
    } 
}