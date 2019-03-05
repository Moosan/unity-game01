using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderJump : MonoBehaviour
{
    private Rigidbody rb;
    public bool key;
    public float up;
    public float jumpspan;
    private float jumpbase;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        key = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* jumpbase += Time.deltaTime;
        if(jumpbase >= jumpspan){
            key = true;
            jumpbase = 0;
        }*/
    }
    private void OnCollisionStay(Collision othor)
    {
        Vector3 housen = Vector3.zero;

        foreach (ContactPoint contact in othor.contacts)
        {
            if (contact.thisCollider.name == name)
            {
                housen = contact.normal;
                Debug.Log(housen);
            }
        }
        if (housen.y > 0)
        {
            key = true;
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, up, 0), ForceMode.Impulse);
                key = false;
            }
        }
    }
}