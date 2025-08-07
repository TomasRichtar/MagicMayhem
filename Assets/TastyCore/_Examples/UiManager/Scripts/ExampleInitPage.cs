using TastyCore.Scripts.Components.UiManager;

public class ExampleInitPage : Page
{
   public void Initialize()
   {
      PageController.PushPage<ExampleScanPage>();
      PageController.PushPage<ExampleScanPage>();
   }

   protected override void InitData(IPageData data) { }
}
