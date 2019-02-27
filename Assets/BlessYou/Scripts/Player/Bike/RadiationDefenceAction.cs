using UnityEngine;
namespace Bike
{
    //放射線を止める拡張機能
    public class RadiationDefenceAction : MonoBehaviour,IBikeActionable
    {
        public void AttachAction(BikeController bike)
        {
            bike.OnTriggerEvent += OnDefence;
        }
        public void DetachAction(BikeController bike)
        {
            bike.OnTriggerEvent -= OnDefence;
        }
        private void OnDefence(Collider other)
        {
            var radiation = other.gameObject.GetComponent<Radiation>();
            radiation?.Stop();
        }
    }
}