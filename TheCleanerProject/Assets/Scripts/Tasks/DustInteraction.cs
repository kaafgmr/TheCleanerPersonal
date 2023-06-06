using System.Collections;
using UnityEngine;

public class DustInteraction : MonoBehaviour
{
    public bool hasBeenVacuumed;
    void Start()
    {
        hasBeenVacuumed = false;   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out VacuumTask vacuumTask))
        {
            if (!vacuumTask.CheckIfItsDone())
            {
                StartCoroutine(isVacuuming());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out VacuumTask vacuumTask))
        {
            if (!vacuumTask.CheckIfItsDone())
            {
                StopCoroutine(isVacuuming());
            }
        }
    }

    IEnumerator isVacuuming()
    {
        yield return new WaitForSeconds(2);
        hasBeenVacuumed = true;
        Destroy(gameObject);
    }
}
