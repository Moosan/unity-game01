using Bike;
using UnityEngine;

namespace BikeTest
{
    //テストで作ったクラスなので直接は使わないでね
    public class RadiationLog : MonoBehaviour
    {
        private void Awake()
        {
            var rad = GetComponent<Radiation>();
            rad.OnContact += (time) => {
                Debug.Log("放射線あたってるなう");
            };
            rad.OnDestruct += (time) => {
                Debug.Log("放射線が何かを壊したなう");
            };
        }
    }
}