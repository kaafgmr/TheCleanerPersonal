using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateInteraction : MonoBehaviour
{
    void Start()
    {
        WashTheDishesTask._instance.plates.Add(this);
    }

    void Update()
    {
        
    }
}
