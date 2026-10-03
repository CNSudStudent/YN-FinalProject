using UnityEngine;
using NaughtyAttributes;

public class Trigger : MonoBehaviour
{
    public TrigonTriggerMechanaics trigon;
    public TrigonTriggerMechanaics maxtrigon;
    public TrigonTriggerMechanaics istriggeron;
    [Tooltip("You can find the pros and cons of each weapons if you hover over their name")]
    public bool hoveroverthisname;

    [Foldout("Attacker")]
    [Tooltip("Instant shpeshifting, weightless, but fragile")]
    public bool Scorpian;//instant shape-shifting,weightless, but fragile

    [Foldout("Attacker")]
    [Tooltip("Max dura, highest sharpness, but fixed weight")]
    public bool Kogetsu;//Max dura, highest sharpness, but fixed weight

    [Foldout("Attacker")]
    [Tooltip("very versatile,very good defense, but makes you slower")]
    public bool Raygust;//very versatile,very good defense, but makes you slower

    [Foldout("Gunner")]
    [Tooltip("Very good fire rate but takes up alot of trion")]
    //Highly Reliable and fast deployment, but predeictable
    public bool SMG;

    [Foldout("Gunner")]
    [Tooltip("High mobility, but short range and lower accuracy")]
    public bool Handgun;

    [Foldout("Gunner")]
    [Tooltip("Very good close range power and very hard to dodge, but very short effective range and slow fire rate")]
    public bool Shotguns;

    [Foldout("Gunner")]
    [Tooltip("Exelent balance, but requires both hands")]
    public bool AR;

    [Foldout("Gunner")]
    [Tooltip("infinte customization, able to fuse bullets, but extremley difficult and leaves user stationary and vulnurable while shaping cubes, and needs mastery for more prescise shots")]
    public bool Shooter;//infinte customization, able to fuse bullets, but extremley difficult and leaves user stationary and vulnurable while shaping cubes
    
    [Foldout("Bullet Types")]
    [Tooltip("High base damage/speed, but no specialtraits")]
    public bool Asteroid;

    [Foldout("Bullet Types")]
    [Tooltip("Homing capablity,but predicted curature")]
    public bool Hound;

    [Foldout("Bullet Types")]
    [Tooltip("Large AoE explosive damage, but could damage yourself or allies")]
    public bool Meteor;

    [Foldout("Bullet Types")]
    [Tooltip("Custom unpredictable paths, but requires preprograming")]
    public bool Viper;

    [Foldout("Sniper")]
    [Tooltip("perfect balance, but its like a jack of all trades, master of none weapon.")]
    public bool Egrett;//perfect balance, but its like a jack of all trades, master of none weapon.

    [Foldout("Sniper")]
    [Tooltip("Colossal destructive power, but very heavy and slow firing rate with great trion cost")]
    public bool Ibis;//Colossal destructive power, but very heavy and slow firing rate with great trion cost

    [Foldout("Sniper")]
    [Tooltip("Very fast bullet speed, immposible to dodge and low trion per shot but very low power")]
    public bool Lighting;//Very fast bullet speed, immposible to dodge and low trion per shot but very low power

    [Foldout("Support or Trap")]
    [Tooltip("vital defense, can shrink to increase defese, is deployed where ever the user in point in front of them or where the user clicks,cracks/shatters under high power/consetration attacks.")]
    public bool Sheild;//vital defense, can shrink to increase defese, is deployed where ever the user in point in front of them or where the user clicks,cracks/shatters under high power/consetration attacks.

    [Foldout("Support or Trap")]
    [Tooltip("Hides you from enemy radar, not invisablity but in not in line of sight, you are not seen, but drains a bit of trion and cannot be used simultaneously")]
    public bool Bagworm;//Hides you from enemy radar, not invisablity but in not in line of sight, you are not seen, but drains a bit of trion and cannot be used simultaneously
    
    [Foldout("Support or Trap")]
    [Tooltip("High Mobility, but requires precise foot work and can be hijacked or used by enimes if they read your positioning")]
    public bool Grasshopper;

    [Foldout("Support or Trap")]
    [Tooltip("Full invis, but drains lots of trion and cannot attack while on,")]
    public bool Chameleon;

    [Foldout("Support or Trap")]
    [Tooltip("lets you teleport in a fixed distance and in you line of sight but very predictable")]
    public bool Idaten;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
