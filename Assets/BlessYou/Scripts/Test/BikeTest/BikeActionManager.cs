using Bike;
using UnityEngine;
namespace BikeTest
{
    //テストで作ったクラスなので直接は使わないでね
    public class BikeActionManager : MonoBehaviour
    {
        public BikeController bike;
        private BreakAction BreakAction;
        private RadiationDefenceAction DefenceAction;
        //いろいろ考えた結果なのでもしかしたらもっといい設計があるかもしんないんだけども
        //バイクの拡張機能は、その挙動から考えて、MonoBehaviorついてて欲しいので
        //このマネージャークラスにコンポーネントとして保管してもらった
        private void Awake()
        {
            BreakAction = gameObject.AddComponent<BreakAction>();
            DefenceAction = gameObject.AddComponent<RadiationDefenceAction>();
        }
        public void AttachBreak()
        {
            bike.AttachBikeAction(BreakAction);
        }
        public void AttachDefence()
        {
            bike.AttachBikeAction(DefenceAction);
        }
        public void Detach()
        {
            bike.AttachBikeAction(null);
        }
    }
}