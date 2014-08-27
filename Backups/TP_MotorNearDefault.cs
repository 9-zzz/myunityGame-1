using UnityEngine;
using System.Collections;

public class TP_Motor : MonoBehaviour {

  public static TP_Motor Instance;

  public float ForwardSpeed = 10f;
  public float BackwardSpeed = 2f;
  public float StrafingSpeed = 5f;
  public float SlideSpeed = 10f;
  public float JumpSpeed = 6f;
  public float Gravity = 21f;
  public float TerminalVelocity = 20f;
  public float SlideThreshold = 0.6f;
  public float MaxControllableSlideMagnitude = 0.4f;

  private Vector3 slideDirection;

  public Vector3 MoveVector { get; set; }
  public float VerticalVelocity { get; set; }

  public bool Infinity_Jump = false;

  void Awake()
  {
    Instance = this;
  }


  public void UpdateMotor() 
  {
    SnapAllignCharacterWithCamera();
    ProcessMotion();
  }

  void ProcessMotion()
  {
    //Transform movevector into worldspace relative to our character;s rotation
    MoveVector = transform.TransformDirection(MoveVector);

    //then normalize our movevector if our magnitude is greater than 0, fixes diagonals
    if (MoveVector.magnitude > 1)
      MoveVector = Vector3.Normalize(MoveVector);

    //Apply sliding important placement
    ApplySlide();

    //Multiply movevector by movespeed
    MoveVector *= MoveSpeed();// now a method, transmission

    //multiply  movevector by delta.time for value per second than per frame
    //MoveVector *= Time.deltaTime;//moved down, was for clarification

    //Reapply Vertical Velocity.y
    MoveVector = new Vector3(MoveVector.x, VerticalVelocity ,MoveVector.z);

    //Apply gravity
    ApplyGravity();

    //move the character in worldspace
    TP_Controller.CharacterController.Move(MoveVector * Time.deltaTime);

  }

  void ApplyGravity()
  {
    if (MoveVector.y > -TerminalVelocity)
      MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime,MoveVector.z);

    if (TP_Controller.CharacterController.isGrounded && MoveVector.y < -1)
      MoveVector = new Vector3(MoveVector.x, -1 ,MoveVector.z);
  }

  void ApplySlide()
  {
    if(!TP_Controller.CharacterController.isGrounded)//if not grounded do nothing
      return;

    slideDirection = Vector3.zero;

    RaycastHit hitInfo;

    //cast from 0,1,0 to 0,-1,0 and put out into hitInfo
    if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitInfo))
    {
			//Debug.DrawLine(transform.position + Vector3.up, Vector3.down);

      if(hitInfo.normal.y < SlideThreshold)
        slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
    }

    if (slideDirection.magnitude < MaxControllableSlideMagnitude){
      MoveVector += slideDirection;
    }else{
      MoveVector = slideDirection;

    }

  }

  public void Jump()
  {
    if(TP_Controller.CharacterController.isGrounded || Infinity_Jump)//if on the ground. important for double jump or jet
      VerticalVelocity = JumpSpeed;
    //Infinity_Jump = true;
  }

  void SnapAllignCharacterWithCamera()
  {
    if (MoveVector.x != 0 || MoveVector.z != 0)
    {
      transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
          Camera.main.transform.eulerAngles.y,
          transform.eulerAngles.z);
    }
  }

  float MoveSpeed()
  {
    var moveSpeed = 0f;//local var, need f

    switch (TP_Animator.Instance.MoveDirection)
    {
      case TP_Animator.Direction.Stationary:
        moveSpeed = 0;
        break;

      case TP_Animator.Direction.Forward:
        moveSpeed = ForwardSpeed;
        break;

      case TP_Animator.Direction.Backward:
        moveSpeed = BackwardSpeed;
        break;

      case TP_Animator.Direction.Left:
        moveSpeed = StrafingSpeed;
        break;

      case TP_Animator.Direction.Right:
        moveSpeed = StrafingSpeed;
        break;

      case TP_Animator.Direction.LeftForward:
        moveSpeed = ForwardSpeed;
        break;

      case TP_Animator.Direction.RightForward:
        moveSpeed = ForwardSpeed;
        break;

      case TP_Animator.Direction.LeftBackward:
        moveSpeed = BackwardSpeed;
        break;

      case TP_Animator.Direction.RightBackward:
        moveSpeed = BackwardSpeed;
        break;
    }

		if(slideDirection.magnitude > 0)
			moveSpeed = SlideSpeed;

    return moveSpeed;

  }
}
