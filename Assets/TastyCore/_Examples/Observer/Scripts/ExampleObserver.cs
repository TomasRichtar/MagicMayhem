using TastyCore.Patterns.Observer;
using TastyCore.Patterns.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleObserver : MonoObserver<ExampleObserverArgs>
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _debugText;

    private void Start()
    {
        ChangeState(false);
    }

    public void Subscribe()
    {
        ServiceLocator.Get<ExampleSubject>()?.Subscribe(this);
        ChangeState(true);
    }

    public void Unsubscribe()
    {
        ServiceLocator.Get<ExampleSubject>()?.Unsubscribe(this);    
        ChangeState(false);
    }
    
    public override void OnNotify(ExampleObserverArgs args)
    {
        _debugText.text = $"Received: {args.Message}";
    }

    private void ChangeState(bool registered)
    {
        var color = registered
            ? Color.green
            : Color.red;

        color.a = 0.5f;
        _image.color = color;
        
        _debugText.text = registered
            ? "Waiting for message"
            : "Disconnected";
    }
    
}
