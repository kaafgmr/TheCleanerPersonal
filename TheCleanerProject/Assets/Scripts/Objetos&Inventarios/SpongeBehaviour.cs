using UnityEngine;

public class SpongeBehaviour : MonoBehaviour
{
    bool collidedWithWindow;

    private void Start()
    {
        collidedWithWindow = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out WindowInteraction window)) return;

        collidedWithWindow = true;

        if (collidedWithWindow)
        {
            //markus things
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collidedWithWindow = false;
    }
}