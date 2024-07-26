using System;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public string test;
    [SerializeField] List<Combination> combos = new List<Combination>();
    public static MergeManager Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    public TowerObjects Merge(Elements obj1, Elements obj2)
    {
        for (int i = 0; i < combos.Count; i++)
        {
            if ((combos[i].obj1 == obj1 && combos[i].obj2 == obj2) || (combos[i].obj1 == obj2 && combos[i].obj2 == obj1))
                return combos[i].result;
        }
        return null;
    }
}