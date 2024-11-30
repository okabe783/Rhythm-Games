using System;

public class Context
{
    public event Action<int> OnSelectionChanged;
    private int _selectedIndex = -1;
    
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            _selectedIndex = value;
            OnSelectionChanged?.Invoke(_selectedIndex);
        }
    }
}