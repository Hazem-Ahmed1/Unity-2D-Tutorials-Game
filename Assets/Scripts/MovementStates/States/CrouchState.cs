using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Crouch", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(movement.dir.magnitude <0.1f) ExitState(movement, movement.idle);
            else ExitState(movement, movement.walk);
        }
        if (movement.vInput < 0) movement.currentMovementSpeed = movement.crouchBackSpeed;
        else movement.currentMovementSpeed = movement.crouchSpeed;
    }
    public void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Crouch", false);
        movement.SwitchState(state);
    }
}
