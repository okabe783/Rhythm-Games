using System;

public class Context
{
    private int _selectedIndex = -1;
    public event Action<int> OnSelectionChanged;
    
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