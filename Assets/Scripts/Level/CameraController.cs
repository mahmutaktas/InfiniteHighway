using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    [SerializeField]
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
        offset.z = -12.5f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = newPos;
    }
}
