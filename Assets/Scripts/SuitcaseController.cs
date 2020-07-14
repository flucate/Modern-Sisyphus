using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Script related to the suitcase and it's behaviours
/// commands everything related to the suitcase and it's logic
/// </summary>
public class SuitcaseController : MonoBehaviour
{
    [SerializeField] private int checkpointInterval = 10;
    [SerializeField] private GameObject suitcase;
    [SerializeField] private Text distanceUI;
    [SerializeField] private GameObject rightBarrier;

    private float startPosition;
    private float checkpoint;
    private float travelDistance = 0f;
    private float suitcaseLostPosition = 0f;
    private PlayerController playerController;

    protected float walkingDistance;

    /// <summary>
    /// Event that should be triggered whenever the player get the suitcase,
    /// </summary>
    [Header("Events")]
    [Space]

    public UnityEvent OnSuitcasePickedUp;
    /// <summary>
    /// Event that should be triggered whenever the player loses the suitcase,
    /// </summary>
    public UnityEvent OnSuitcaseLost;

    private void Awake()
    {
        if (OnSuitcasePickedUp == null) OnSuitcasePickedUp = new UnityEvent();
        if (OnSuitcaseLost == null) OnSuitcaseLost = new UnityEvent();
    }

    void Start()
    {
        startPosition = transform.position.x;
        playerController = gameObject.GetComponent<PlayerController>();
        checkpoint = suitcase.transform.position.x;
    }

    private void FixedUpdate()
    {
        walkingDistance = CalculateTravelDistance();

        //define new suitcase checkpoint
        if (walkingDistance >= (checkpoint + checkpointInterval) && 
            playerController.hasSuitcase == true)
        {
            checkpoint += checkpointInterval;
        }

        /*if (suitcaseLostPosition == 0f &&
            playerController.hasSuitcase == true)
        {
            suitcaseLostPosition = Random.Range(startPosition,
                                                startPosition + checkpointInterval * 3 + 1);
        }*/

        //define distance to lost the suitcase
        if (suitcaseLostPosition == 0f && playerController.hasSuitcase == true)
        {            
            suitcaseLostPosition = checkpoint + Random.Range(checkpointInterval/2, checkpointInterval + 1);          
        }

        //lose suitcase when reach suitcase lose position
        if (walkingDistance >= suitcaseLostPosition &&
            playerController.hasSuitcase == true)
        {
            RemoveSuitcase();
        }
    }

    private void RemoveSuitcase()
    {
        OnSuitcaseLost.Invoke();
        //AudioSource.PlayClipAtPoint(playerController.audioSuitcaseGone, transform.position);
        suitcaseLostPosition = 0f;

        suitcase.SetActive(true);
        suitcase.transform.position = new Vector3(startPosition,
                                                  suitcase.transform.position.y);

        if (rightBarrier.activeInHierarchy == false)
        {
            PutBarrier();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "suitcaseTag" && playerController.hasSuitcase == false)
        {
            OnSuitcasePickedUp.Invoke();
            other.gameObject.SetActive(false);
            RemoveBarrier();
        }
    }

    private void RemoveBarrier() => rightBarrier.SetActive(false);

    private void PutBarrier()
    {
        rightBarrier.SetActive(true);
        rightBarrier.transform.position = new Vector3(transform.position.x + 5f,
                                                      transform.position.y);
    }

    public float CalculateTravelDistance()
    {
        if (playerController.hasSuitcase)
        {
            travelDistance = (transform.position.x - startPosition) + 1;
            distanceUI.text = $"Distance: {(int)travelDistance}";
        }

        return travelDistance;
    }
}
