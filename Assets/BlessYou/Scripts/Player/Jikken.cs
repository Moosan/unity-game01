using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jikken : MonoBehaviour
{
    private Rigidbody rb;
    public float sin;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionStay(Collision other)
    {
        Vector3 normal = Vector3.zero;
        //法線ベクトルの取得
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.thisCollider.name == name)
            {
                normal = contact.normal;
                Debug.Log(normal);
                sin = normal.y;
            }
        }
    }
}
