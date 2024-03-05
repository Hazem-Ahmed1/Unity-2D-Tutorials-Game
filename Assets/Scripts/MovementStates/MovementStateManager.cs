using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMovementSpeed;
    public float walkSpeed = 3, walkBackSpeed =2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public float hzInput, vInput;
    [HideInInspector] public Vector3 dir;

    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundYoffset;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    Vector3 spherePos;
    CharacterController characterController;

    MovementBaseState currentState;

    public IdleState idle = new IdleState();
    public RunState run = new RunState();
    public CrouchState crouch = new CrouchState();
    public WalkState walk = new WalkState();

    [HideInInspector] public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        SwitchState(idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        anim.SetFloat("hInput", hzInput);
        anim.SetFloat("vInput", vInput);
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove(){
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward* vInput + transform.right * hzInput;

        characterController.Move(dir.normalized * currentMovementSpeed * Time.deltaTime);
    }

    bool IsGround(){
        spherePos = new Vector3(transform.position.x,transform.position.y - groundYoffset,transform.position.z);
        if(Physics.CheckSphere(spherePos,characterController.radius - 0.05f,groundMask)) return true;
        return false;
    }

    void Gravity(){
        if(!IsGround()) velocity.y += gravity * Time.deltaTime;
        else if(velocity.y > 0) velocity.y =-2;
        characterController.Move(velocity * Time.deltaTime);
    }

    // private void OnDrawGizmos(){
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(spherePos,characterController.radius - 0.05f);
    // }
}
