namespace Bike
{
    //壊せるものが一種類じゃなくても対応したかったのでインターフェースで統一した
    public interface IBreakable
    {
        void OnBreak();
    }
}