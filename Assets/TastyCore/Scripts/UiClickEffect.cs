using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Vector3 _pressedScale = new Vector3(0.9f, 0.9f, 0.9f);
    private Coroutine _coroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(LerpScale(_pressedScale));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(LerpScale(Vector3.one));
    }

    private IEnumerator LerpScale(Vector3 finishScale)
    {
        var elapsedTime = 0f;
        var waitTime = 0.1f;
        var startScale = transform.localScale;

        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(startScale, finishScale, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = finishScale;
    }
}