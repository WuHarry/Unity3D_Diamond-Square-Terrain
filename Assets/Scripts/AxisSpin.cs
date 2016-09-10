using UnityEngine;
using System.Collections;

public class AxisSpin : MonoBehaviour
{

    public float spinSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * spinSpeed, Vector3.right);
    }
}
