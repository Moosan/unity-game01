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
        if (Input.GetKey(KeyCode.Space) && key)
        {
            rb.AddForce(new Vector3(0, up, 0), ForceMode.Impulse);
            key = false;
        }
    }
    private void OnCollisionEnter(Collision othor)
    {
        Vector3 housen = Vector3.zero;

        foreach (ContactPoint contact in othor.contacts)
        {
            if (contact.thisCollider.name == name)
            {
                housen = contact.normal;
                Debug.Log(housen);
                if (contact.normal.y > 0)
                {
                    key = true;
                }
            }



        }
    }
}