using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Rigidbody;

    [SerializeField]
    private float MaxSpeed;

    [SerializeField]
    private float MovePower;


    private Vector3 Force
    {
        //代入されたらBoolをTrueにする
        set
        {
            if (ForceChanged) return;
            ForceChanged = true;
            _Force = value;
        }
        get
        {
            return _Force;
        }
    }


    #region Force

    private bool ForceChanged;

    private Vector3 _Force;

    #endregion


    #region Physics

    /// <summary>
    /// 最大速度を超えてない時だけ力を加える
    /// </summary>
    private void UpdateVelocity()
    {
        if (Mathf.Abs(Rigidbody.velocity.x) > MaxSpeed) return;
        Rigidbody.AddForce(Force, ForceMode.Acceleration);
    }

    #endregion


    #region Unity

    void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody>();
        Force = new Vector3();
        ForceChanged = false;
    }
    

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            OnKeyRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnKeyLeft();
        }
    }


    private void FixedUpdate()
    {
        //_Forceの値が変わってた時のみ実行
        if (ForceChanged)
        {
            UpdateVelocity();
            ForceChanged = false;
        }
    }

    #endregion


    /// <summary>
    /// 右に進みたいときはこれを呼び出す
    /// </summary>
    public void OnKeyRight()
    {
        Force = transform.right * MovePower;
    }


    /// <summary>
    /// 左に進めたいときはこれを呼び出す
    /// </summary>
    public void OnKeyLeft()
    {
        Force = -transform.right * MovePower;
    }
}
