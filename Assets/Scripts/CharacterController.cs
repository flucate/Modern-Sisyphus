using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Character movement script, should be used for, and has, everything related to character movement
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 80.0f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private AudioClip jumpSound;

    private const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector2 m_Velocity = Vector2.zero;

    /// <summary>
    /// Event that should be triggered whenever the player lands, 
    /// calls animator and updates needed booleans
    /// </summary>
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 
                                                            k_GroundedRadius, 
                                                            m_WhatIsGround);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                m_Grounded = true;
                if (wasGrounded == false)
                    OnLandEvent.Invoke();
            }
        }
    }

    /// <summary>
    /// Manages this GameObject movement. Flips the sprite if needed and provides air control if enabled
    /// </summary>
    /// <param name="move">The speed at which the player should move</param>
    /// <param name="jump">Defines if the player is jumping or not</param>
    public void Movement(float move, bool jump)
    {
        if (m_Grounded || m_AirControl)
        {
            Vector2 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity,
                                                        targetVelocity,
                                                        ref m_Velocity,
                                                        m_MovementSmoothing);

            if (move > 0 && m_FacingRight == false)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight == true)
            {
                Flip();
            }
        }

        if (m_Grounded && jump)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);

            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    /// <summary>
    /// Flips the player sprite according to it's input
    /// </summary>
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
