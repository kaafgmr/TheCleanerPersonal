using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    public bool screaming = false;

    public virtual void Scream()
    {
        Transform ScreamPoint = GameManager.instance.ScreamerPoint;
        transform.position = ScreamPoint.position;
        transform.rotation = ScreamPoint.rotation;
        screaming = true;
    }
    public virtual void AnimationHashes()
    {

    }

    public virtual void LoseGame()
    {
        PlayerPrefs.SetInt("Win", 0);
        MenuControl.instance.LoadScene("GameFinished");
    }
    public abstract void GhostCounter();
    public abstract void GhostAction(Vector3 otherPos);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PointingMovement PM))
        Scream();
    }
}
