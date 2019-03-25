using UnityEngine;
namespace Bike
{
    public class BikeController : MonoBehaviour
    {
        private Rigidbody Rigidbody;


        #region SerializedFields

        [SerializeField]
        private float MaxSpeed;

        [SerializeField]
        private float MovePower;

        [SerializeField]
        private float zRotateThrethold;

        [SerializeField]
        private float yRotateSpeed;

        #endregion


        #region Direction
        private EnumDirection TargetDirection
        {
            //代入されたらBoolをTrueにする
            set
            {
                if (_DirectionChanged) return;
                _DirectionChanged = true;
                _Direction = value;
            }
            get
            {
                return _Direction;
            }
        }

        private bool _DirectionChanged;

        private EnumDirection _Direction;

        private enum EnumDirection
        {
            forward = 270, back = 90, right = 0, left = 180, forright = 315, bacright = 45, bacleft = 135, forleft = 225
        }

        private void DirectionReset()
        {
            _DirectionChanged = false;
        }
        private bool IsDirectionChanged()
        {
            return _DirectionChanged;
        }

        #endregion


        #region Event

        //拡張機能やその他のために
        //当たり判定が来た時のイベントを発行してる
        public delegate void CollisionHandler(Collision collision);
        public event CollisionHandler OnCollisionEnterEvent = (collision) => { };

        public delegate void TriggerHandler(Collider other);
        public event TriggerHandler OnTriggerEvent = (other) => { };

        #endregion


        #region BikeExtension
        //BikeControllerは拡張機能を受けとることと保存しておくことしか知らない
        private IBikeExtension BikeExtension;

        public void AttachBikeExtension(IBikeExtension extension)
        {
            BikeExtension?.DetachExtension(this);
            BikeExtension = extension;
            BikeExtension?.AttachExtension(this);
        }

        #endregion


        #region Physics
        /// <summary>
        /// 最大速度を超えてない時だけ力を加える
        /// </summary>
        private void OnMove()
        {
            if (!IsDirectionChanged()) return;
            if (CheckSpeedRetriction()) return;
            Rigidbody.AddForce(transform.right * MovePower, ForceMode.Acceleration);
            DirectionReset();
        }

        //ここめっちゃくそい
        private void OnRotate()
        {
            //もし入力に変化が無いならやめる
            if (!IsDirectionChanged()) return;

            //現在角を取得
            var nowAngle = transform.eulerAngles.y;

            var targetAngle = (int)TargetDirection;
            //目標角と現在角の差分を取得
            var deltaAngle = targetAngle - nowAngle;

            //差分の大きさがこのフレームでの変化量未満なら、目的の角度にして回転やめる、このフレームから動き始める
            if (Mathf.Abs(deltaAngle) < yRotateSpeed * Time.deltaTime) {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
                return;
            }

            //現在角に、目標角に近づく値を足す、Mathf.Sign()のとこで、右回りか左回りか指定してる
            nowAngle = nowAngle + Mathf.Sign(ChangeAngle(deltaAngle)) * Time.deltaTime * yRotateSpeed;

            //新しい現在角を直接代入
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, nowAngle, transform.eulerAngles.z);

            //入力に対しての処理を行ったので入力の状態をリセットする
            //このおかげで、回転の処理が行われたフレームでは移動の処理は行われない
            DirectionReset();
        }

        //オイラー角のZ成分が閾値(Threshold)を超えないようにクランプしてる
        private void DoRotateRestriction()
        {
            var zvalue = Mathf.Clamp(ChangeAngle(transform.eulerAngles.z), -zRotateThrethold, zRotateThrethold);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zvalue);
        }


        //最大速度超えてたらtrue
        private bool CheckSpeedRetriction()
        {
            return Rigidbody.velocity.magnitude > MaxSpeed;
        }

        #endregion


        #region PhysicsCallback
        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent(collision);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEvent(other);
        }

        #endregion


        #region UnityCallback

        void Start()
        {
            Rigidbody = this.GetComponent<Rigidbody>();
            _DirectionChanged = false;
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
            OnRotate();
            OnMove();
        }

        #endregion


        #region KeyInput

        /// <summary>
        /// 右に進みたいときはこれ
        /// </summary>
        public void OnKeyRight()
        {
            TargetDirection = EnumDirection.right;
        }

        /// <summary>
        /// 左に進めたいときはこれ
        /// </summary>
        public void OnKeyLeft()
        {
            TargetDirection = EnumDirection.left;
        }

        /// <summary>
        /// 上に進めたいときはこれ
        /// </summary>
        public void OnKeyUp()
        {
            TargetDirection = EnumDirection.forward;
        }
        /// <summary>
        /// 下に進みたいときはこれ
        /// </summary>
        public void OnKeyDown()
        {
            TargetDirection = EnumDirection.back;
        }

        #endregion

        /// <summary>
        /// プレイヤーが破壊されたときはこれ
        /// </summary>        
        public void Destrucion()
        {
            Debug.Log("Destruction!");
        }

        //角度を-180から180の間の値にあわせる
        private float ChangeAngle(float angle)
        {
            if (angle > 180)
            {
                return ChangeAngle(angle - 360);
            }
            else if (angle < -180)
            {
                return ChangeAngle(angle + 360);
            }
            return angle;
        }
    }
}