using System;
using TastyCore._Examples.UiManager.Scripts;
using TastyCore.Scripts.Components.UiManager;
using UnityEngine;

public class ExampleSettingsPage : Page
{
    public void CloseSettings()
    {
        if(PageController.IsPageOnTopOfStack(this))
            PageController.PopPage();
    }

    protected override void InitData(IPageData data)
    {
        var optionData = data as ExampleOptionPageData;

        if (optionData == null)
        {
            throw new ArgumentException(
                $"Invalid data type for InitData method. Expected ExampleOptionPageData object but received {data.GetType().Name}"
            );
        }

        Debug.Log(optionData.ExampleDebugOption);
    }
}
