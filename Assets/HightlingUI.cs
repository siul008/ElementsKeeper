using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HightlingUI : MonoBehaviour
{
    Image image;
    public Color alpha1;
    public Color alpha2;
    public float timeHightlight;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        image = GetComponent<Image>();
        image.color = alpha1;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > timeHightlight)
        {
            if (image.color == alpha1)
            {
                image.color = alpha2;
            }
            else
            {
                image.color = alpha1;
            }
            time = 0;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
