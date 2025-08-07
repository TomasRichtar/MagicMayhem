using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Scripts.Components.UiManager;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public class PageController : MonoBehaviour
{
    private struct PageStackValue
    {
        public Page Page;
        public IPageData PageData;
    }
    
    private static PageController Instance;
    
    [SerializeField]
    private Page InitialPage;
    
    [SerializeField]
    private GameObject FirstFocusItem;
    
    private Canvas RootCanvas;
    private Stack<PageStackValue> PageStack = new Stack<PageStackValue>();
    private List<Page> PageCache = new List<Page>();
    
    private void Awake()
    {
        Instance = this;
        RootCanvas = GetComponent<Canvas>();

        PageCache = FindObjectsOfType<Page>().ToList();
        PageCache.ForEach(page => page.Exit(false));
    }

    private void Start()
    {
        if (FirstFocusItem != null)
        {
            EventSystem.current.SetSelectedGameObject(FirstFocusItem);
        }

        // Delay so Pages can Init in Awake/Start 
        StartCoroutine(InitialPageDelay());
        
        IEnumerator InitialPageDelay()
        {
            yield return new WaitForEndOfFrame();
            
            if (InitialPage != null)
                PushPage(InitialPage);
        }
    }

    private void OnCancel() // Back Button 
    {
        if (!RootCanvas.enabled) return;
        if (!RootCanvas.gameObject.activeInHierarchy) return;
        if (PageStack.Count <= 0) return;
        
        PopPage();
    }

    #region Helpers

    public bool IsEmpty()
    {
        return PageStack.Count == 0;
    }
    
    public static T GetPage<T>() where T : Page
    {
        foreach (var page in Instance.PageCache)
        {
            if (page is T tPage)
                return tPage;
        }

        return null;
    }
    
    public static bool IsPageInStack(Page page)
    {
        return Instance.PageStack.Count(p => p.Page == page) > 0;
    }

    public static bool IsPageOnTopOfStack(Page page)
    {
        return IsPageInStack(page) && page == Instance.PageStack.Peek().Page;
    }

    #endregion
    
    // TODO
    // Add functionality - Some pages wont go onto stack ( like Init, Login, Etc.. )
    
    public static void PushPage<T>(IPageData data = default) where T : Page
    {
        var page = GetPage<T>();
        if (page == null) return;
        
        PushPage(page,data);
    }
    
    public static void PushPage(Page page, IPageData data = default)
    {
        page.Enter(true,data);

        if (Instance.PageStack.Count > 0)
        {
            var currentPage = Instance.PageStack.Peek();

            if (currentPage.Page.ExitOnNewPagePush)
            {
                currentPage.Page.Exit(false);
            }
        }

        Instance.PageStack.Push(new PageStackValue
        {
            Page = page,
            PageData = data
        });
    }

    public static void PopPage()
    {
        if (Instance.PageStack.Count > 1)
        {
            var page = Instance.PageStack.Pop();
            page.Page.Exit(true);

            var newCurrentPage = Instance.PageStack.Peek();
            if (newCurrentPage.Page.ExitOnNewPagePush)
            {
                // Todo remember data within stack
                newCurrentPage.Page.Enter(false);
            }
        }
        else
        {
            Debug.LogWarning("Trying to pop a page but only 1 page remains in the stack!");
        }
    }

    public static void PopAllPages()
    {
        for (int i = 1; i < Instance.PageStack.Count; i++)
        {
            PopPage();
        }
    }
}
