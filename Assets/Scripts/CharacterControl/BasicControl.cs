using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;

    public float movingSpeed;
    [SerializeField] protected float ClimbSpeed = 5f;

    protected float horizontalInput;
    protected float verticalInput;
    protected Vector3 processedInput = new(0, 0, 0);

    public bool isInteracting;
    protected float interactInput;
    public InteractableObject interactingObject;
    public MindPowerComponent interactingMindPowerObject;

    protected float towardsY;
    [Header("ת���ٶȣ��Ƕ�/�룩")]
    public float rotateSpeed;//degree per second

    public bool alive;

    public bool isClimbing;
    public bool onRopeTopEnd;
    public bool onRopeDownEnd;
    public bool IsHoldingHands;

    public bool isInOcean;

    protected float interactTimer = 0;
    public float interactTime;

    protected int interactType;//1 = Father; 2 = Son;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        alive = true;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            isInOcean = true;
            anim.SetBool("isInOcean", true);
        }
        else
        {
            isInOcean = false;
            anim.SetBool("isInOcean", false);
        }
    }
    protected virtual void Update()
    {
        #region Character Animation & Rotation

        anim.SetBool("isClimbing", isClimbing);

        if (alive)
        {
            if (rb.velocity.x > 1)
            {
                towardsY = 270;
                anim.SetBool("isMoving", true);
            }
            else if (rb.velocity.x < -1)
            {
                towardsY = 90;
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
                anim.SetFloat("MovingSpeed", 0);
            }
            float rotateDifference = towardsY - transform.rotation.eulerAngles.y;

            if (Mathf.Abs(rotateDifference) >= 2.5)
            {
                if (rotateDifference > 0 && rotateDifference < 180 || rotateDifference < -180)
                {
                    transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                }
                else
                {
                    transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
                }
            }
        }

        #endregion
    }
    protected virtual void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.velocity = Vector3.zero;
            Climb();
        }

        if (!isClimbing && !isInOcean)
        {
            rb.useGravity = true;
        }
    }
    public void Move(float direction)
    {
        rb.velocity = new Vector3(movingSpeed * Time.deltaTime * direction, rb.velocity.y);
    }
    public void MoveInOcean(float directionX, float directionY)
    {
        rb.velocity = new Vector3(movingSpeed * Time.deltaTime * directionX, movingSpeed * Time.deltaTime * directionY);
    }
    protected void Climb()
    {
        rb.MovePosition(transform.position + ClimbSpeed * Time.deltaTime * processedInput);
    }
    public void SetAnimMoveSpeed(float count)
    {
        anim.SetFloat("MovingSpeed", count);
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactingObject = other.GetComponent<InteractableObject>();
        }
        if (other.GetComponent<MindPowerComponent>())
        {
            interactingMindPowerObject = other.GetComponent<MindPowerComponent>();
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactingObject = other.GetComponent<InteractableObject>();
        }
        if (other.GetComponent<MindPowerComponent>())
        {
            interactingMindPowerObject = other.GetComponent<MindPowerComponent>();
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactingObject = null;
        }
        if (other.GetComponent<MindPowerComponent>())
        {
            interactingMindPowerObject = null;
        }
    }
}