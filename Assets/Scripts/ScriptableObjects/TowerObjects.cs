using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerObjects : ScriptableObject
{
    public enum Types
    {
        Earth,
        Fire,
        Water,
        Wind,
        Mud,
        Steam,
        FlameThrower,
        Geyser,
        Boulder,
        Lava
    }

    public Types type;
    public int level;
    public int damage;
    public float maxHealth;
    public GameObject projectile;
    public float attackRate;
    public Sprite sprite;
    public GameObject spawnableTower;
    public Color bulletColor;
    public string description = "Default description";
}
