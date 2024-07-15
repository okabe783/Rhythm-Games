using System;

public class Context
{
    //現在選択されているセルのindexを保存する
    //ユーザーがセルを決定したときにセルのindexに更新される
    public int _selectIndex = 1;

    public Action<int> OnClickSellIndex;
}
