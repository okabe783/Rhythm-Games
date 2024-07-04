public interface IPlayerMove 
{
    public float Speed { get; }
    public void VerticalMovement(); // 上下移動
    public void DownwardMovement(); // 下降
}