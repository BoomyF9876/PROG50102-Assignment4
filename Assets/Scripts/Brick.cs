using UnityEngine;

public class Brick : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IDoDamage>(out IDoDamage damageSource))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
