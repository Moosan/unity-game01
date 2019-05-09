using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class LazRigidBody : MonoBehaviour
{
    private Rigidbody Rigidbody { get; set; }
    public float LazVolume;
    private Vector3 Force { get; set; }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Force = new Vector3(0,LazVolume,0);
    }
    private void FixedUpdate()
    {
        Rigidbody.AddForce(Force);
    }
    public void SetLazActive(bool isActive)
    {
        Force = new Vector3(0, isActive?LazVolume:0, 0);
    }
}