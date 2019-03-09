using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    public float speedx;
    public float speedy;
    public float speedz;
    public bool jump;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        jump = false;
	}
	
	// Update is called once per frame
	void Update () {
        buturi();
	}
    void buturi()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speedx, rb.velocity.y, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(speedx * -1, rb.velocity.y, 0);
        }
        if(Input.GetKey(KeyCode.W)){
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedz);
        }
        if(Input.GetKey(KeyCode.S)){
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedz * -1);
        }
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            jumphantei();
            if (jump == true)
            {
                rb.AddForce(new Vector3(0, speedy, 0), ForceMode.Impulse);
                jump = false;
            }
        }*/
    }
    void jumphantei()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance - (0.5f * CosToTan(Vector3.Dot(Vector3.up, hit.normal)) + 0.5f) <= 0.05f)
            {

                jump = true;
            }
        }
    }
    float CosToTan(float cos){
        return Mathf.Sqrt(1 / (cos * cos) - 1);
    }
}
