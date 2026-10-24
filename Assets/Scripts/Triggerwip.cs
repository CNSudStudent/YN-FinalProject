using UnityEngine;
using NaughtyAttributes;

public enum mainWeapon1ENUM
{
 Kogetsu,
 Raygust,
 SMG,    
 Handgun,
 Shotgun,
 AR,     
 Shooter
}
public class Triggerwip : MonoBehaviour
{
    public TrigonTriggerMechanaics trion;
    public TrigonTriggerMechanaics maxtrion;
    public TrigonTriggerMechanaics istriggeron;

    //Settings
    private int triggersOn;
    
    [SerializeField] public mainWeapon1ENUM mainWeapon1;
    //private bool mainWeapon1;

    private bool mainWeapon2;

    private bool mainWeapon3;

    private bool mainWeapon4;

    private bool secondaryWeapon1;

    private bool secondaryWeapon2;

    private bool secondaryWeapon3;

    private bool secondaryWeapon4;

    [Tooltip("You can find the pros and cons of each weapons if you hover over their name")]
    public bool hoveroverthisname;

    public bool Scorpian;//instant shape-shifting,weightless, but fragile
    private gameObject SCW_1;
    private gameObject SCW_2;

    public bool Kogetsu;//Max dura, highest sharpness, but fixed weight

    public bool Raygust;//very versatile,very good defense, but makes you slower

                 //Highly Reliable and fast deployment, but predeictable
    public bool SMG;

    public bool Handgun;

    public bool Shotgun;

    public bool AR;

    public bool Shooter;//infinte customization, able to fuse bullets, but extremley difficult and leaves user stationary and vulnurable while shaping cubes
    
    public bool Asteroid;

    public bool Hound;

    public bool Meteor;

    public bool Viper;

    public bool Egrett;//perfect balance, but its like a jack of all trades, master of none weapon.

    public bool Ibis;//Colossal destructive power, but very heavy and slow firing rate with great trion cost

    public bool Lighting;//Very fast bullet speed, immposible to dodge and low trion per shot but very low power

    public bool Sheild;//vital defense, can shrink to increase defese, is deployed where ever the user in point in front of them or where the user clicks,cracks/shatters under high power/consetration attacks.

    public bool Bagworm;//Hides you from enemy radar, not invisablity but in not in line of sight, you are not seen, but drains a bit of trion and cannot be used simultaneously
    
    public bool Grasshopper;

    public bool Chameleon;

    public bool Idaten;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggersOn = 0;
        Scorpian = false;
        Kogetsu = false;
        mainWeapon1 = mainWeapon1ENUM.scorpian;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("MainWeapon1") && istriggeron && trion >= 10)
        {
            MainWeapon1Config();

        }
        if(Input.GetKeyDown("MainWeapon2") && istriggeron)
        {
            mainWeapon2 = !mainWeapon2;
            mainWeapon1 = false;
            mainWeapon3 = false;
            mainWeapon4 = false;
        }
        if(Input.GetKeyDown("MainWeapon3") && istriggeron)
        {
            mainWeapon3 = !mainWeapon3;
            mainWeapon1 = false;
            mainWeapon2 = false;
            mainWeapon4 = false;
        }
        if(Input.GetKeyDown("MainWeapon4") && istriggeron)
        {
            mainWeapon4 = !mainWeapon4;
            mainWeapon1 = false;
            mainWeapon2 = false;
            mainWeapon3 = false;
        }
    }
    void MainWeapon1Config()
    {
        if(mainWeapon1 = mainWeapon1ENUM.scorpian)
        {
            Scorpian = true;
            triggersOn += 1;
        }
    }
}
