﻿using UnityEngine;
namespace Bike
{
    public class BikeController : MonoBehaviour
    {
        private Rigidbody Rigidbody;

        [SerializeField]
        private float MaxSpeed;

        [SerializeField]
        private float MovePower;

        [SerializeField]
        private float zRotateThrethold;

        [SerializeField]
        private float yRotateSpeed;

        

        private EnumDirection Direction
        {
            //代入されたらBoolをTrueにする
            set
            {
                if (DirectionChanged) return;
                DirectionChanged = true;
                _Direction = value;
            }
            get
            {
                return _Direction;
            }
        }


        #region Force

        private bool DirectionChanged;

        private EnumDirection _Direction;

        #endregion


        #region Event

        //拡張機能やその他のために
        //当たり判定が来た時のイベントを発行してる
        public delegate void CollisionHandler(Collision collision);
        public event CollisionHandler OnCollisionEnterEvent = (collision) => { };

        public delegate void TriggerHandler(Collider other);
        public event TriggerHandler OnTriggerEvent = (other) => { };

        #endregion


        #region BikeAction
        //BikeControllerは拡張機能を受けとることと保存しておくことしか知らない
        private IBikeActionable BikeActionable;

        public void AttachBikeAction(IBikeActionable action)
        {
            BikeActionable?.DetachAction(this);
            BikeActionable = action;
            BikeActionable?.AttachAction(this);
        }

        #endregion


        #region Physics
        /// <summary>
        /// 最大速度を超えてない時だけ力を加える
        /// </summary>
        private void OnMove()
        {
            OnRotate();
            if (CheckSpeedRetriction()) return;
            Rigidbody.AddForce(transform.right * MovePower, ForceMode.Acceleration);
        }

        private void OnRotate()
        {
            var yangle = (int)Direction;
            var ea = transform.eulerAngles;
            transform.eulerAngles = new Vector3(ea.x, yangle, ea.z);
        }

        private float ChangeAngle(float angle)
        {
            if(angle > 180)
            {
                return ChangeAngle(angle - 360);
            }
            else if(angle < -180)
            {
                return ChangeAngle(angle + 360);
            }
            return angle;
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent(collision);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEvent(other);
        }

        private bool CheckSpeedRetriction() {
            return Rigidbody.velocity.magnitude > MaxSpeed;
        }

        private void DoRotateRestriction()
        {
            var angle = transform.eulerAngles;
            var zvalue = ChangeAngle(angle.z);
            zvalue = Mathf.Clamp(zvalue, -zRotateThrethold, zRotateThrethold);
            transform.eulerAngles = new Vector3(angle.x, angle.y, zvalue);
        }

        #endregion


        #region Unity

        void Start()
        {
            Rigidbody = this.GetComponent<Rigidbody>();
            DirectionChanged = false;
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
            if (Input.GetKey(KeyCode.W))
            {
                OnKeyUp();
            }
            if (Input.GetKey(KeyCode.S))
            {
                OnKeyDown();
            }
            DoRotateRestriction();
        }


        private void FixedUpdate()
        {
            //_Forceの値が変わってた時のみ実行
            if (DirectionChanged)
            {
                OnMove();
                DirectionChanged = false;
            }
        }

        #endregion


        #region KeyInput

        /// <summary>
        /// 右に進みたいときはこれ
        /// </summary>
        public void OnKeyRight()
        {
            Direction = EnumDirection.right;
        }

        /// <summary>
        /// 左に進めたいときはこれ
        /// </summary>
        public void OnKeyLeft()
        {
            Direction = EnumDirection.left;
        }

        /// <summary>
        /// 上に進めたいときはこれ
        /// </summary>
        public void OnKeyUp()
        {
            Direction = EnumDirection.forward;
        }
        /// <summary>
        /// 下に進みたいときはこれ
        /// </summary>
        public void OnKeyDown()
        {
            Direction = EnumDirection.back;
        }

        #endregion

        /// <summary>
        /// プレイヤーが破壊されたときはこれ
        /// </summary>        
        public void Destrucion()
        {
            Debug.Log("Destruction!");
        }

        private enum EnumDirection
        {
            forward = -90, back = 90, right = 0, left = 180
        }
        
    }
    
}