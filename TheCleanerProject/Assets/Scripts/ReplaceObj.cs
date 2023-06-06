using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceObj : MonoBehaviour
{
    public void Replace(GameObject newObj) //codigo en la bombilla que hay que reparar
    {
        newObj.transform.position = transform.position;
        Destroy(gameObject);
    }
}
