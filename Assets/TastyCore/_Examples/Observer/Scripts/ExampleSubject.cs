using TastyCore.Patterns.Observer;
using TastyCore.Patterns.ServiceLocator;
using TMPro;
using UnityEngine;

public class ExampleSubject : MonoSubject<ExampleObserverArgs>, IRegistrable
{
   [SerializeField] private TMP_InputField _debugInput;

   private void Awake()
   {
      ServiceLocator.Register(this);
   }

   public void Broadcast()
   {
      Notify(new ExampleObserverArgs
      {
         Message = string.IsNullOrWhiteSpace(_debugInput.text)
            ? "Empty Message"
            : _debugInput.text
      });

      _debugInput.text = "";
   }
}
