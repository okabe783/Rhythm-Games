public interface IPlayerInput
{
    /// <summary> Fキー入力時 </summary>
    public void InputUpper();
    /// <summary> FJキー同時押し入力時 </summary>
    public void InputUpperAndLower();
    /// <summary> Jキー入力時 </summary>
    public void InputLower();
    /// <summary> 長押し開始 </summary>
    public void InputLongPressStart();
    /// <summary> 長押し終了 </summary>
    public void InputLongPressEnd();
}