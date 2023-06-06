using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbInteraction : MonoBehaviour //script para la bombilla que hay que coger y llevar a donde la bombilla rota
{
    public bool hasBeenChanged;
    private GameObject lightBulbToFix;
    void Start() { hasBeenChanged = false; }
    private void OnTriggerEnter(Collider obj) 
    {
        lightBulbToFix = obj.gameObject;
        if (obj.TryGetComponent(out ChangeLightBulbTask lightBulbTask)) if (!lightBulbTask.CheckIfItsDone()) StartCoroutine(isGettingChanged()); 
    }
    private void OnTriggerExit(Collider obj) { if (obj.TryGetComponent(out ChangeLightBulbTask lightBulbTask)) if (!lightBulbTask.CheckIfItsDone()) StopCoroutine(isGettingChanged()); }
    IEnumerator isGettingChanged()
    {
        yield return new WaitForSeconds(2);
        hasBeenChanged = true;
    }
    private void Update() { if (hasBeenChanged) ReplaceObj(); }
    private void ReplaceObj() 
    {//objeto padre bombilla con la task y un hijo con el modelo 3d de la bombila y el script de ReplaceObj, finalizamos la tarea del padre y movemos la lampara de la mano a donde estaba la rota y destruimos la anterior
        lightBulbToFix.GetComponent<ChangeLightBulbTask>().taskFinished = true;
        lightBulbToFix.transform.GetChild(0).GetComponent<ReplaceObj>().Replace(gameObject);
        gameObject.GetComponent<GrabObject>().enabled = false;
        this.enabled = false;
    }
}