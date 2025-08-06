using UnityEngine;

[CreateAssetMenu(fileName = "PlantTower", menuName = "Scriptable Objects/Plant Tower")]
public class Tower : ScriptableObject
{
    [SerializeField] 
    public TowerCategories thisCategory;
    [SerializeField]
    public TowerTypes thisTower;
    [SerializeField]
    public string towerName = "Default";
    [SerializeField]
    public float health = 500;
    [SerializeField]
    public float cost = 100;
    [SerializeField]
    public float attackDmg = 1;
    [SerializeField]
    public float attackCooldown = 1;
    [SerializeField]
    public float attackRange = 1;
    [Tooltip("Put -1 for hitting all targets in area")]
    [SerializeField]
    public int numTargets = 1;
    [SerializeField]
    public bool damageOT = false;
    [SerializeField]
    public Sprite baseSprite;
    [SerializeField]
    public string towerDescription =
        "Edit description.";

    public string FormatStats()
    {
        string stats = "";

        stats += "Slot: Ground\n";
        stats += "Health: " + health + "\n";
        stats += "Attack Damage: " + attackDmg + " (Single-hit)\n";
        stats += "Attack Cooldown: " + attackCooldown + "\n";
        //stats += "Cost: " + cost + "\n";

        return stats;
    }
}
