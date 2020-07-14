using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public enum CutscenesIds
    {
        CutsceneOne = 1
    }

    [SerializeField] private GameObject goddessObject;
    [SerializeField] private float animationSpeed;
    [SerializeField] private float animationDuration;

    private float initialPosition;
    private GameObject goddess;
    private PlayerController player;
    private readonly Dictionary<CutscenesIds, float> cutscenesDuration = new Dictionary<CutscenesIds, float>()
    {
        {CutscenesIds.CutsceneOne, 7f }
    };

    public readonly Dictionary<CutscenesIds, bool> isCutsceneActive = new Dictionary<CutscenesIds, bool>()
    {
        {CutscenesIds.CutsceneOne, false }
    };
    private Vector2 GoddessPosition { get => goddess.transform.position; set => goddess.transform.position = value; }
    private Vector2 PlayerPosition { get => player.transform.position; }

    private void Awake()
    {
        isCutsceneActive[CutscenesIds.CutsceneOne] = true;

        goddess = Instantiate(goddessObject);
        goddessObject.SetActive(false);
        player = gameObject.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = GoddessPosition.x;
        player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCutsceneActive[CutscenesIds.CutsceneOne])
        {
            if (Time.time < animationDuration)
            {
                GoddessPosition = Vector2.Lerp(GoddessPosition,
                                              new Vector2(PlayerPosition.x + 4f, GoddessPosition.y),
                                              Time.deltaTime * animationSpeed);
            }
            else if (Time.time >= animationDuration && Time.time < cutscenesDuration[CutscenesIds.CutsceneOne])
            {
                GoddessPosition = Vector2.Lerp(GoddessPosition,
                                              new Vector2(initialPosition, GoddessPosition.y),
                                              Time.deltaTime * animationSpeed);
            }
            else if (Time.time > cutscenesDuration[CutscenesIds.CutsceneOne])
            {
                isCutsceneActive[CutscenesIds.CutsceneOne] = false;
                Destroy(goddess);
                player.enabled = true;
            }
        }
    }
}
