using UnityEngine;
namespace Bike
{
    //壊す拡張機能
    public class BreakAction : MonoBehaviour, IBikeActionable
    {
        public void AttachAction(BikeController bike)
        {
            bike.OnCollisionEnterEvent += OnBreak;
        }
        public void DetachAction(BikeController bike)
        {
            bike.OnCollisionEnterEvent -= OnBreak;
        }
        private void OnBreak(Collision collision)
        {
            var ibreak = collision.gameObject.GetComponent<IBreakable>();
            ibreak?.OnBreak();
        }
    }
}