using UnityEngine;

public class ObjectsPositionCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ObjectsToOrder _objects = other.GetComponent<ObjectsToOrder>();
        if (_objects != null)
        {
            _objects.SetIsOnRealPosition();
        }
    }
}
