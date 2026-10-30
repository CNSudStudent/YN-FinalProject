using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class TrigonTriggerMechanaics : MonoBehaviour
{
    public Triggerwip weaponsMap;
    public float trion;
    public float maxtrion = 1f;
    public bool istriggeron;

    [Foldout("Trion Regen")]
    public float regenTrionPerSecond = 5f;
    public float regenInterval = 1f;
    private float trionRegenTimer = 0f;

    [Foldout("Trion Depletion")]
    public float trionDepletionPer5Seconds = 1f;
    public float depletionInterval = 5f;
    public float trionDepletionTimer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setup
        trion = maxtrion;
        istriggeron = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("WeaponToggle") && (trion > 1f))
        {
            istriggeron = true;
        }
        if (istriggeron = true && (trion > 0f))
        {
            trionDepletionTimer += Time.deltaTime;

            if (trionDepletionTimer >= depletionInterval)
            {
                ExecuteTrionDepletion();
                trionDepletionTimer -= depletionInterval;
            }
        }
        if (istriggeron = true && (trion == 0f))
        {
            istriggeron = false;
        }
        if (trion < maxtrion && !istriggeron)
        {
            trionRegenTimer += Time.deltaTime;

            if (trionRegenTimer >= regenInterval)
            {
                ExecuteTrionRegeneration();
                trionRegenTimer -= regenInterval; 
            }
        }
    }
    void ExecuteTrionRegeneration()
    {
        trion += regenTrionPerSecond;
        trion = Mathf.Min(trion, maxtrion);
    }
    void ExecuteTrionDepletion()
    {
        trion -= trionDepletionPer5Seconds;
        trion = Mathf.Min(trion, maxtrion);
    }
}
