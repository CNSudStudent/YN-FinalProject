using UnityEngine;

public class TrigonTriggerMechanaics : MonoBehaviour
{
    public float trion;
    public float maxtrion = 1f;
    public bool istriggeron;
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
        if (Input.GetKeyDown(KeyCode.Z) && (trion > 0f))
        {
            istriggeron = true;
        }
        if (istriggeron = true && (trion > 0f))
        {
            trion -= 1f;
        }
        if (istriggeron = true && (trion == 0f))
        {
            istriggeron = false;
        }
    }
}
