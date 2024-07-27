using System.Collections.Specialized;
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

    public enum DmgType
    {
        Single,
        AreaOfEffect,
        None
    }

    public Types type;
    public string towerName = "Defaut name";
    public DmgType dmgType;
    public int level;
    public int damage;
    public float maxHealth;
    public GameObject projectile;
    public float attackRate;
    public Sprite sprite;
    public GameObject spawnableTower;
    public Color bulletColor;
    public int price;
    public string description = "Default description";
}
