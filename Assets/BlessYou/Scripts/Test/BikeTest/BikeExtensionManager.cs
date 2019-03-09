using Bike;
using UnityEngine;
namespace BikeTest
{
    //テストで作ったクラスなので直接は使わないでね
    public class BikeExtensionManager : MonoBehaviour
    {
        public BikeController bike;
        private BreakExtension BreakExtension;
        private RadiationDefenceExtension DefenceExtension;
        //いろいろ考えた結果なのでもしかしたらもっといい設計があるかもしんないんだけども
        //バイクの拡張機能は、その挙動から考えて、MonoBehaviorついてて欲しいので
        //このマネージャークラスにコンポーネントとして保管してもらった
        private void Awake()
        {
            BreakExtension = gameObject.AddComponent<BreakExtension>();
            DefenceExtension = gameObject.AddComponent<RadiationDefenceExtension>();
        }
        public void AttachBreak()
        {
            bike.AttachBikeExtension(BreakExtension);
        }
        public void AttachDefence()
        {
            bike.AttachBikeExtension(DefenceExtension);
        }
        public void Detach()
        {
            bike.AttachBikeExtension(null);
        }
    }
}