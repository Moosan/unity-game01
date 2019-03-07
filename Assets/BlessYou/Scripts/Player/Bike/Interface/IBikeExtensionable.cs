namespace Bike
{
    //バイクの拡張機能は総じてこのインターフェースにある関数を持つよね
    public interface IBikeExtension
    {
        void AttachExtension(BikeController bike);
        void DetachExtension(BikeController bike);
    }
}