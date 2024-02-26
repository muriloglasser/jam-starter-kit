using EntityCreator;
using System;

public struct TransitionStruct
{
    public FadeType fadeType;
    public Action onTransitionEnd;
}
/// <summary>
/// Fade enum type 
/// </summary>
public enum FadeType
{
    In,
    Out
}