using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUIEvents : MonoBehaviour
{
    private Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void Merge()
    {
        CraftingManager.Instance.Merge();
        anim.SetTrigger("Reset");
    }
}
