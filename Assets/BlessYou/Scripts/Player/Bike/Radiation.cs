using UnityEngine;

namespace Bike
{
    //放射線
    [RequireComponent(typeof(Collider))]
    public class Radiation : MonoBehaviour
    {
        [SerializeField]
        private float LifeTime;
        private float DeltaTime = 0.01f;
        private float time = 0f;

        public delegate void RadiationEventHandler(float time);
        public event RadiationEventHandler OnContact = (time) => { };
        public event RadiationEventHandler OnDestruct = (time) => { };

        private bool IsStop;

        //放射線って触れたら即死なのか、少しずつダメージ食らってくのかわかんなかったので
        //どっちでもすぐ対応できそうな感じにした
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag != "Player") return;
            if (IsStop) return;
            time += DeltaTime;

            //timeの変更を通知してる
            OnContact(time);

            if (time < LifeTime) return;

            //破壊するタイミングを通知してる
            OnDestruct(LifeTime);

            other.gameObject.GetComponent<BikeController>().Destrucion();

            time = 0f;
            //DeltaTime = 0f;
        }

        private void OnTriggerEnter(Collider other)
        {
            IsStop = false;
        }

        //放射線は放射線の止め方を公開してる
        public void Stop()
        {
            IsStop = true;
        }
    }
}