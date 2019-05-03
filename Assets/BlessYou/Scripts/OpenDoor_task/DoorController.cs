using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Rigidbody door_rb;
    public bool count;
    private float Stoptime;
    public float Maxtime;
    
    // Start is called before the first frame update
    void Start()
    {
        door_rb = this.gameObject.GetComponent<Rigidbody>();
        count = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor()
    {
        if (count)
        {
            Stoptime += Time.deltaTime;
            door_rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            count = false;
            if (Stoptime > Maxtime)
            {
                door_rb.constraints = RigidbodyConstraints.FreezeAll;
                Stoptime = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        count = true;
    }
}