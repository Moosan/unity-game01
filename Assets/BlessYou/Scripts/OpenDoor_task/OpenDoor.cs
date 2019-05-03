using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private DoorController doorCont;
    // Start is called before the first frame update
    void Start()
    {
        GameObject door = GameObject.Find("Door");
        doorCont = door.GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        doorCont.OpenDoor();
    }
}
