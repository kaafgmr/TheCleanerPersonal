using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    [Range(0f,179f)]
    [SerializeField] float fovAngle;
    [SerializeField] LayerMask collisionLayer;

    public UnityEvent<Vector3> OnViewedByMe;
    public UnityEvent ImBeingViewed;
    public UnityEvent OnNothingHappening;

    Transform playerTransform;
    float playerFOV;

    bool wanderingOnce = false;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField]float maxDistanceView = 30;
#endif

    private void Start()
    {
        playerTransform = GameManager.instance.GetPlayerTransform();
        playerFOV = GameManager.instance.GetPlayerFOV();
    }

    private void FixedUpdate()
    {
        Vector3 myDir = (playerTransform.position - transform.position).normalized;

        if (ICouldBeSeenBy(playerTransform.gameObject, myDir))
        {
            Vector3 itsDir = -myDir;
            if (ImInsideItsFOV(itsDir, playerTransform.forward, playerFOV))
            {
                ImBeingViewed.Invoke();
                wanderingOnce = false;
                return;
            }

            if (IsInsideMyFOV(myDir))
            {
                OnViewedByMe.Invoke(GameManager.instance.playerPos);
                wanderingOnce = false;
                return;
            }

            if (wanderingOnce) return;

            OnNothingHappening.Invoke();
            wanderingOnce = true;
        }
        else
        {
            if (wanderingOnce) return;

            OnNothingHappening.Invoke();
            wanderingOnce = true;
        }
    }

    bool IsInsideMyFOV(Vector3 dir)
    {
        return (Vector3.Angle(dir, transform.forward) < (fovAngle * 0.5f));
    }

    bool ImInsideItsFOV(Vector3 dir, Vector3 itsForward, float itsFOV)
    {
        return (Vector3.Angle(dir, itsForward) < (itsFOV * 0.5f));
    }

    /// <summary>
    /// Calculates if "to" is inside the FOV of "from"
    /// </summary>
    /// <param name="from"> The object that has the FOV that "to" could be inside of</param>
    /// <param name="fromFOVAngle"> The FOV it self that "from" have</param>
    /// <param name="to"> The objects that could me inside "from"s FOV</param>
    /// <returns>True if "to" is inside "from"s FOV</returns>
    public bool IsInsideTheFOVOf(Transform from, float fromFOVAngle, Transform to)
    {
        Vector3 dir = (to.position - from.position).normalized;

        return (Vector3.Angle(dir, from.forward) < (fromFOVAngle * 0.5f));
    }


    bool ICouldBeSeenBy(GameObject obj, Vector3 dir)
    {
        if(Physics.Raycast(transform.position, dir, out RaycastHit hit, 1000f, collisionLayer))
        {
#if UNITY_EDITOR
            debugRay = hit.point;
            debugDir = dir;
#endif
            if (hit.collider.gameObject == obj)
            {
                return true;
            }
        }
        return false;
    }



#if UNITY_EDITOR
    Vector3 debugDir = Vector3.zero;
    Vector3 debugRay = Vector3.zero;
    private void OnDrawGizmos()
    {
        DrawFOV();

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, debugRay);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, debugDir * 100);
    }

    void DrawFOV()
    {
        float catetoOpuesto = maxDistanceView * Mathf.Tan(fovAngle * 0.5f * Mathf.Deg2Rad);
        Vector3 viewingPosR = transform.right * catetoOpuesto + transform.forward * maxDistanceView;
        Vector3 viewingPosL = transform.right * -catetoOpuesto + transform.forward * maxDistanceView;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewingPosR.normalized * maxDistanceView);
        Gizmos.DrawLine(transform.position, transform.position + viewingPosL.normalized * maxDistanceView);
    }
#endif
}