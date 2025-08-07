using System;
using System.Collections;
using TastyCore.Scripts.Components.UiManager;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
[DisallowMultipleComponent]
public abstract class Page : MonoBehaviour
{
    public event Action PrePushAction;
    public event Action PostPushAction;
    public event Action PrePopAction;
    public event Action PostPopAction;

    private AudioSource AudioSource;
    private RectTransform RectTransform;
    private CanvasGroup CanvasGroup;

    [Header("Configuration")] 
    [SerializeField]private bool _exitOnNewPagePush = true;

    [Header("Animations")] 
    [SerializeField] private float _animationSpeed = 1f;

    [SerializeField] private EntryMode _entryMode = EntryMode.SLIDE;
    [SerializeField] private Direction _entryDirection = Direction.LEFT;
    [SerializeField] private EntryMode _exitMode = EntryMode.SLIDE;
    [SerializeField] private Direction _exitDirection = Direction.LEFT;

    [Header("Audio")] 
    [SerializeField] private AudioClip _entryClip;
    [SerializeField] private AudioClip _exitClip;

    private Coroutine _animationCoroutine;
    private Coroutine _audioCoroutine;

    public bool ExitOnNewPagePush => _exitOnNewPagePush;

    protected abstract void InitData(IPageData data);
    
    #region Virtual Functions

    protected virtual void PrePush()
    {
        gameObject.SetActive(true);
        PrePushAction?.Invoke();
    }

    protected virtual void PostPush()
    {
        PostPushAction?.Invoke();
    }

    protected virtual void PrePop()
    {
        PrePopAction?.Invoke();
    }

    protected virtual void PostPop()
    {
        gameObject.SetActive(false);
        PostPopAction?.Invoke();
    }

    #endregion

    protected virtual void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        CanvasGroup = GetComponent<CanvasGroup>();
        AudioSource = GetComponent<AudioSource>();

        AudioSource.playOnAwake = false;
        AudioSource.loop = false;
        AudioSource.spatialBlend = 0;
        AudioSource.enabled = false;
    }

    public void Enter(bool playAudio, IPageData data = default)
    {
        PrePush();
        InitData(data);
        switch (_entryMode)
        {
            case EntryMode.NONE:
                PostPush();
                break;
            case EntryMode.SLIDE:
                SlideIn(playAudio);
                break;
            case EntryMode.ZOOM:
                ZoomIn(playAudio);
                break;
            case EntryMode.FADE:
                FadeIn(playAudio);
                break;
        }
    }

    public void Exit(bool playAudio)
    {
        PrePop();
        switch (_exitMode)
        {
            case EntryMode.NONE:
                PostPop();
                break;
            case EntryMode.SLIDE:
                SlideOut(playAudio);
                break;
            case EntryMode.ZOOM:
                ZoomOut(playAudio);
                break;
            case EntryMode.FADE:
                FadeOut(playAudio);
                break;
        }
    }

    #region Animations

    private void SlideIn(bool playAudio)
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine =
            StartCoroutine(PageAnimationHelper.SlideIn(RectTransform, _entryDirection, _animationSpeed, PostPush));

        PlayEntryClip(playAudio);
    }

    private void SlideOut(bool playAudio)
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine =
            StartCoroutine(PageAnimationHelper.SlideOut(RectTransform, _exitDirection, _animationSpeed, PostPop));

        PlayExitClip(playAudio);
    }

    private void ZoomIn(bool playAudio)
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.ZoomIn(RectTransform, _animationSpeed, PostPush));

        PlayEntryClip(playAudio);
    }

    private void ZoomOut(bool playAudio)
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.ZoomOut(RectTransform, _animationSpeed, PostPop));

        PlayExitClip(playAudio);
    }

    private void FadeIn(bool playAudio)
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.FadeIn(CanvasGroup, _animationSpeed, PostPush));

        PlayEntryClip(playAudio);
    }

    private void FadeOut(bool playAudio)
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.FadeOut(CanvasGroup, _animationSpeed, PostPop));

        PlayExitClip(playAudio);
    }

    #endregion

    #region Audio

    // TODO
    // Make this Page audio Dependent on Core Audio System ? 
    
    private void PlayEntryClip(bool playAudio)
    {
        if (playAudio && _entryClip != null && AudioSource != null)
        {
            StopRoutineIfActive(_audioCoroutine);
            _audioCoroutine = StartCoroutine(PlayClip(_entryClip));
        }
    }

    private void PlayExitClip(bool playAudio)
    {
        if (playAudio && _exitClip != null && AudioSource != null)
        {
            StopRoutineIfActive(_audioCoroutine);
            _audioCoroutine = StartCoroutine(PlayClip(_exitClip));
        }
    }

    private IEnumerator PlayClip(AudioClip Clip)
    {
        AudioSource.enabled = true;

        WaitForSeconds Wait = new WaitForSeconds(Clip.length);

        AudioSource.PlayOneShot(Clip);

        yield return Wait;

        AudioSource.enabled = false;
    }

    #endregion

    private void StopRoutineIfActive(Coroutine routine)
    {
        if(routine != null)
            StopCoroutine(routine);
    }
}
