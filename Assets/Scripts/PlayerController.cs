using UnityEngine;

/// <summary>
/// Script related to the player and it's attributes, commands everything that should happen 
/// to the player
/// </summary>
public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// Player movement script
    /// </summary>
    public CharacterController controller;
    /// <summary>
    /// Defines if the player currently have it's suitcase
    /// </summary>
    public bool hasSuitcase = false;
    
    protected float horizontalMove = 0f;
    protected bool jump = false;

    [SerializeField] protected float runSpeed;
    [SerializeField] private GameObject badWordsSprite;

    private Animator animator;
    private GameObject badWords;
    public AudioClip audioSuitcaseGone, audioGetSuitcase;
  

   


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        /*if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            jump = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        //ADDED
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            jump = true;
        }


        controller.Movement(horizontalMove * Time.deltaTime, jump);

        
        if (badWords != null)
        {
            badWords.transform.position = new Vector3(transform.position.x + 0.6f,
                                                      transform.position.y + 0.5f);
        }

        jump = false;
    }

    private void PopBadWordsBallon()
    {
        badWords = Instantiate(badWordsSprite,
                               new Vector2(transform.position.x + 0.5f,
                                           transform.position.y + 0.5f),
                               Quaternion.identity);

        Destroy(badWords, 2);
    }

    /// <summary>
    /// Event to call whenever the player lands
    /// </summary>
    public void OnLandEvent() => animator.SetBool("isJumping", false);

    /// <summary>
    /// Event called whenever the player takes the suitcase
    /// </summary>
    public void OnSuitcasePickedUp()
    {
        animator.SetBool("hasSuitcase", true);
        hasSuitcase = true;
		AudioSource.PlayClipAtPoint(audioGetSuitcase, transform.position);
    }

    /// <summary>
    /// Event called whenever the player loses the suitcase
    /// </summary>
    public void OnSuitcaseLost()
    {
        animator.SetBool("hasSuitcase", false);
        PopBadWordsBallon();
        hasSuitcase = false;
		AudioSource.PlayClipAtPoint(audioSuitcaseGone, transform.position);        

    }
}
