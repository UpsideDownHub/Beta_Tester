using UnityEngine;
using System.Collections;

public class SpinningBall : MonoBehaviour
{
    public float spinx;
    public float spiny;
    public float spinz;

    private void FixedUpdate()
    {
        transform.Rotate(spinx, spiny, spinz);
    }
}
