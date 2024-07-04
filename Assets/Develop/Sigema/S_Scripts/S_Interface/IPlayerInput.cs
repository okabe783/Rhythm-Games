public interface IPlayerInput
{
    public void InputUpper();
    public void InputUpperAndLower(); // 同時押し
    public void InputLower();
    public void InputLongPressStart(); // 長押し開始
    public void InputLongPressEnd(); // 長押し終了
}
