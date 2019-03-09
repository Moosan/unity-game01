using UnityEngine;
namespace Bike
{
    //壊す拡張機能
    public class BreakExtension : MonoBehaviour, IBikeExtension
    {
        public void AttachExtension(BikeController bike)
        {
            bike.OnCollisionEnterEvent += OnBreak;
        }
        public void DetachExtension(BikeController bike)
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