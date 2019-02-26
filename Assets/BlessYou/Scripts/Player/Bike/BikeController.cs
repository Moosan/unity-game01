using UnityEngine;
namespace Bike
{
    public class BikeController : MonoBehaviour
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
        private void UpdateVelocity()
        {
            if (Mathf.Abs(Rigidbody.velocity.x) > MaxSpeed) return;
            Rigidbody.AddForce(Force, ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent(collision);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEvent(other);
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
        /// 右に進みたいときはこれ
        /// </summary>
        public void OnKeyRight()
        {
            Force = transform.right * MovePower;
        }

        /// <summary>
        /// 左に進めたいときはこれ
        /// </summary>
        public void OnKeyLeft()
        {
            Force = -transform.right * MovePower;
        }

        /// <summary>
        /// プレイヤーが破壊されたときはこれ
        /// </summary>        
        public void Destrucion()
        {
            Debug.Log("Destruction!");
        }
    }
}