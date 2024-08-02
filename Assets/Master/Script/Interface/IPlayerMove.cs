public interface IPlayerMove 
{
    /// <summary> 速度 </summary>
    public float Speed { get; }
    /// <summary> 上下移動 </summary>
    public void VerticalMovement();
    /// <summary> 下降 </summary>
    public void DownwardMovement();
}