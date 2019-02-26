namespace Bike
{
    //バイクの拡張機能は総じてこのインターフェースにある関数を持つよね
    public interface IBikeActionable
    {
        void AttachAction(BikeController bike);
        void DetachAction(BikeController bike);
    }
}