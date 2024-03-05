using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        else if (Input.GetKeyDown(KeyCode.LeftControl)) ExitState(movement, movement.crouch);
        else if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.idle);

        if (movement.vInput < 0) movement.currentMovementSpeed = movement.walkBackSpeed;
        else movement.currentMovementSpeed = movement.walkSpeed;
    }

    public void ExitState(MovementStateManager movement,MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
