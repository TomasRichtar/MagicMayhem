using TastyCore.Components.ScrollOptimizer;
using TMPro;
using UnityEngine;

public class ExampleItem : ListItem<ExampleItemContext>
{
    [SerializeField]
    private TextMeshProUGUI _guidField;
    
    [SerializeField]
    private TextMeshProUGUI _textField;
    
    public override void UpdateContent(int index, ExampleItemContext context)
    {
        _guidField.text = context.Guid;
        _textField.text = context.TextValue;
    }
}
