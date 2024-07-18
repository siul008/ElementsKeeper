using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerObjects : ScriptableObject
{
    public enum Types
    {
        Dirt,
        Fire,
        Water,
        Light
    }

    public Types type;
    public int level;
    public int damage;
    public Sprite sprite;
}
