using UnityEngine;
namespace Bike{
    //壊せるブロック
    [RequireComponent(typeof(Collider))]
    public class BreakableBlock : MonoBehaviour,IBreakable
    {
        public void OnBreak()
        {
            Debug.Log("Break!");
        }
    }
}