using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField] SpawnerScriptV2 spawner;
    public void CallSpawnerChangeText()
    {
        spawner.SetDayCounterText();
    }
}
