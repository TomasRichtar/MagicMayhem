using System.Collections.Generic;
using TastyCore.Components.ScrollOptimizer;
using UnityEngine;

public class ExampleListOptimizer : ListOptimizer<ExampleItem,ExampleItemContext>
{
    [SerializeField] private int _exampleItems = 50;
    
    private Dictionary<int, ExampleItemContext> _cache;

    private void Awake()
    {
        LoadData(_exampleItems);
    }

    private void Start()
    {
        Initialize(_exampleItems);
    }
    
    private void LoadData(int items)
    {
        _cache = new Dictionary<int, ExampleItemContext>();
        for (var i = 0; i < items; i++)
        {
            _cache.Add(i, new ExampleItemContext
            {
                Guid = System.Guid.NewGuid().ToString(),
                TextValue = $"This is item on index {i}"
            });
        }
    }
    
    protected override ExampleItemContext GetContext(int index)
    {
        return _cache.ContainsKey(index)
            ? _cache[index]
            : new ExampleItemContext { Guid = "Item not found", TextValue = ""};
    }
}
