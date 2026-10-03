using UnityEngine;

public class TrigonTriggerMechanaics : MonoBehaviour
{
    public float trigon;
    public float maxtrigon = 1f;
    public bool istriggeron;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setup
        trigon = maxtrigon;
        istriggeron = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && (trigon > 0f))
        {
            istriggeron = true;
        }
        if (istriggeron = true && (trigon > 0f))
        {
            trigon -= 1f;
        }
        if (istriggeron = true && (trigon == 0f))
        {
            istriggeron = false;
        }
    }
}
