using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {


    public Transform left;
    public Transform right;
    public bool re;
    //回転時間
    public float turn = 0.5f;
    public GameObject hito;
    
	// Use this for initialization
	void Start () {
        hito = GameObject.Find("peple");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 center = hito.transform.position;
        center = center + new Vector3(0, 0, 1);
        Vector3 leftposi = left.position - center;
        Vector3 rightposi = right.position - center;
        Debug.Log(center);
        if(Input.GetKey(KeyCode.A)){
            transform.position = Vector3.Slerp(leftposi, rightposi, 2f);
        }
    }
}
