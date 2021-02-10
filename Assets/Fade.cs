using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    enum State
    {
        FadeIn,
        Wait,
        FadeOut,
        Done,
    }

    public float FadeInTime = 0;
    public float FadeWaitTime = 0;
    public float FadeOutTime = 1;
    public bool DestroyOnFadeOut = false;

    private State FadeState { get; set; }
    private float FadeTimer { get; set; }

    private Renderer[] _renderers;
    private CanvasGroup[] _canvasGroups;

    public float Alpha
    {
        get
        {
            switch (FadeState)
            {
                case State.FadeIn:
                    return 1.0f - FadeTimer / FadeInTime;
                case State.FadeOut:
                    return FadeTimer / FadeOutTime;
            }

            return 1.0f;
        }
    }

    void Start()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
        _canvasGroups = gameObject.GetComponentsInChildren<CanvasGroup>();

        StartFade();
    }

    void Update()
    {
        if (FadeState == State.Done)
            return;

        FadeTimer -= Time.deltaTime;
        if (FadeTimer <= 0)
            OnFadeDone();

        UpdateAlpha();
    }

    private void UpdateAlpha()
    {
        var alpha = Alpha;

        foreach (var renderer in _renderers)
        {
            var color = renderer.material.color;
            renderer.material.color = color.WithAlpha(alpha);
        }

        foreach (var canvasGroup in _canvasGroups)
        {
            canvasGroup.alpha = alpha;
        }
    }

    private void OnFadeDone()
    {
        switch (FadeState)
        {
            case State.FadeIn:
                {
                    FadeTimer = FadeWaitTime;
                    FadeState = State.Wait;
                }
                break;
            case State.Wait:
                {
                    FadeTimer = FadeOutTime;
                    FadeState = State.FadeOut;
                }
                break;
            case State.FadeOut:
                {
                    if (DestroyOnFadeOut)
                        Destroy(gameObject);

                    StopFade();
                }
                break;
        }
    }
    public void StartFade()
    {
        enabled = true;

        if (FadeInTime > 0)
        {
            FadeTimer = FadeInTime;
            FadeState = State.FadeIn;
        }
        else
        {
            FadeTimer = FadeOutTime;
            FadeState = State.FadeOut;
        }
    }

    public void StopFade()
    {
        enabled = false;
        FadeTimer = 0.0f;
        FadeState = State.Done;
    }
}