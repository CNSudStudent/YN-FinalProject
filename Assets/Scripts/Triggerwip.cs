using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public enum mainWeapon1ENUM
{
 kogetsu,
 raygust,
 scorpian,
 smg,    
 handgun,
 shotgun,
 a4,     
 shooter
}
public class Triggerwip : MonoBehaviour
{
    public TrigonTriggerMechanaics trion;
    public TrigonTriggerMechanaics maxtrion;
    public TrigonTriggerMechanaics istriggeron;

    public InputActionMap weaponsMap;

    //Settings
    private int triggersOn;
    
    public GameObject Weapons;
    public GameObject SCW_1;
    public GameObject SCW_2;
    public GameObject Kogetsu;
    [SerializeField] public mainWeapon1ENUM mainWeapon1;
    private bool mainWeapon1On;

    private bool mainWeapon2On;

    private bool mainWeapon3On;

    private bool mainWeapon4On;

    private bool secondaryWeapon1On;

    private bool secondaryWeapon2On;

    private bool secondaryWeapon3On;

    private bool secondaryWeapon4On;

    [Tooltip("You can find the pros and cons of each weapons if you hover over their name")]
    public bool hoveroverthisname;

    /*public bool Scorpian;//instant shape-shifting,weightless, but fragile
    private GameObject SCW_1;
    private GameObject SCW_2;

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

    public bool Idaten;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggersOn = 0;
        mainWeapon1On = false;
        mainWeapon2On = false;
        mainWeapon3On = false;
        mainWeapon4On = false;
        secondaryWeapon1On = false;
        secondaryWeapon2On = false;
        secondaryWeapon3On = false;
        secondaryWeapon4On = false;
        mainWeapon1 = mainWeapon1ENUM.scorpian;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("MainWeapon1") && istriggeron)
        {
            mainWeapon1On = true;
            MainWeaponConfig();
            

        }
        if(Input.GetKeyDown("MainWeapon2") && istriggeron)
        {
           /* mainWeapon2 = !mainWeapon2;
            mainWeapon1 = false;
            mainWeapon3 = false;
            mainWeapon4 = false;
            */
        }
        if(Input.GetKeyDown("MainWeapon3") && istriggeron)
        {
           /* mainWeapon3 = !mainWeapon3;
            mainWeapon1 = false;
            mainWeapon2 = false;
            mainWeapon4 = false;
            */
        }
        if(Input.GetKeyDown("MainWeapon4") && istriggeron)
        {
           /* mainWeapon4 = !mainWeapon4;
            mainWeapon1 = false;
            mainWeapon2 = false;
            mainWeapon3 = false;
            */
        }
    }
    void MainWeaponConfig()
    {
        if(!istriggeron)
        {
            Weapons.SetActive(false);
        }
        else if(mainWeapon2On || mainWeapon3On || mainWeapon4On)
        {
            mainWeapon1On = false;
        }
        else if(mainWeapon1 == mainWeapon1ENUM.kogetsu)
        {
            Kogetsu.SetActive(true);
            triggersOn += 1;
        }
    }
}
