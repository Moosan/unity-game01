using UnityEngine;
namespace Bike
{
    //放射線を止める拡張機能
    public class RadiationDefenceExtension : MonoBehaviour,IBikeExtension
    {
        public void AttachExtension(BikeController bike)
        {
            bike.OnTriggerEvent += OnDefence;
        }
        public void DetachExtension(BikeController bike)
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