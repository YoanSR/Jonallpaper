using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class JonaUIAnimator : MonoBehaviour
{
    public bool isInAnimation;
    [SerializeField] private AnimationGroup[] _animationGroups;
    [SerializeField] private int _basePanelIndex = -1;
    [SerializeField] private bool _fadeBasePanelOnStart;
    public EventSystem eventSystem;

    private Stack<int> _backStackIndex = new Stack<int>();
    private List<int> _activeIndexList = new List<int>();
    private int _currentPanelIndex = -1;

    public void Start()
    {
        Singleton();
        if (_fadeBasePanelOnStart)
        {
            Fade(_basePanelIndex);
        }
    }

    public void Fade(int baseIndex)
    {
        int index = Mathf.Abs(baseIndex);
        if(_activeIndexList.Contains(index)) return;
        if (isInAnimation && !_animationGroups[index].forceAnimation) return;
        //if (_animationGroups[index].forceAnimation) DOTween.KillAll();

        if(_animationGroups[index].soloOnScreen)
        {
            foreach (int fadeOutIndex in _activeIndexList)
            {
                _animationGroups[fadeOutIndex].Animate(true);
            }
            _activeIndexList.Clear();
        }
        _activeIndexList.Add(index);
        if(_currentPanelIndex != -1 && baseIndex > 0)
        {
            _backStackIndex.Push(_currentPanelIndex);
        }
        _currentPanelIndex = index;
        isInAnimation = true;
        StartCoroutine(ToggleIsInAnimation(_animationGroups[index].Animate()));
    }

    public void FadeOut(int index)
    {
        print("Fade Out " + index);
        _animationGroups[index].Animate(true);
        if(_activeIndexList.Contains(index)) _activeIndexList.Remove(index);
    }

    private IEnumerator ToggleIsInAnimation(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        isInAnimation = !isInAnimation;
    }
    [Button]
    public void Back()
    {
        print("trying to go back");
        if(isInAnimation)
        {
            print("can't is in animation");
            return;
        }
        if(_backStackIndex.Count == 0 && _animationGroups[_currentPanelIndex].specialBackIndex == -1)
        {
            print("can't no back stack and no special back index");
            return;
        }
        if (!_animationGroups[_currentPanelIndex].canGoBack)
        {
            print("can't go back");
            return;
        }
        int targetIndex = _animationGroups[_currentPanelIndex].specialBackIndex == -1 ? _backStackIndex.Pop() : _animationGroups[_currentPanelIndex].specialBackIndex;
        if (_animationGroups[_currentPanelIndex].soloOnScreen) Fade(targetIndex * -1);
        _currentPanelIndex = targetIndex;
    }
    
    [Serializable]
    public class AnimationGroup
    {
        public bool soloOnScreen;
        public bool multipleObjects;
        public bool canGoBack = true;
        public int specialBackIndex = -1;
        public bool forceAnimation = false;
        public float timeScale = -1;
        public Selectable selectable;
        public List<Image> buttonToDisable = new List<Image>();
        public List<Slider> slidersToDisable = new List<Slider>();
        public AnimationObject singleAnimationObject;
        public AnimationObject[] animationObjects;
        public UnityEvent onAnimationStart;
        public float Animate(bool isFadeOut = false)
        {
            if(selectable) Instance.eventSystem.SetSelectedGameObject(selectable.gameObject);
            if(timeScale != -1 && !isFadeOut) Time.timeScale = timeScale;
            foreach (Image button in buttonToDisable)
            {
                button.raycastTarget = !isFadeOut;
            }

            foreach (Slider slider in slidersToDisable)
            {
                slider.interactable = !isFadeOut;
            }
            float sequenceTime = 0;
            if (multipleObjects)
            {
                foreach (var animationObject in animationObjects)
                {
                    float time = animationObject.delay + animationObject.duration;
                    if(time > sequenceTime) sequenceTime = time;
                }
                foreach (var animationObject in animationObjects) animationObject.Animate(isFadeOut,sequenceTime);
            }
            else
            {
                //DOTween.Kill(singleAnimationObject);
                sequenceTime = singleAnimationObject.delay + singleAnimationObject.duration;
                singleAnimationObject.Animate(isFadeOut);
            }
            onAnimationStart?.Invoke();
            return sequenceTime;
        }
    }
    
    [Serializable]
    public class AnimationObject
    {
        public RectTransform target;
        public bool disableTarget = true;
        public bool useBaseTargetPosition = true;
        public Vector2 targetPosition;
        public Vector2 direction;
        public float distance;
        public Vector2 baseScale;
        public bool differentEase;
        [HideIf("differentEase")] public Ease easeType;
        [ShowIf("differentEase")] public Ease inEaseType;
        [ShowIf("differentEase")] public Ease outEaseType;
        public float duration;
        public float delay;
        public UnityEvent onAnimationStart;
        public UnityEvent onAnimationEnd;

        private Sequence seq;
        
        private Vector2 _basePosition;
        public void Animate(bool isFadeOut = false, float biggestDelay = 0)
        {
            DOTween.Kill(target);
            print("animating " + isFadeOut);
            _basePosition = target.localPosition;
            
            if (isFadeOut)
            {
                if(!target.gameObject.activeSelf) return;
                target.localPosition = useBaseTargetPosition ? target.localPosition : targetPosition;
            }
            else
            {
                target.gameObject.SetActive(true);
                target.localScale = baseScale;
                target.localPosition = distance * direction + (useBaseTargetPosition ? target.localPosition : targetPosition);
            }
            Ease usedEase = differentEase ? (isFadeOut ? outEaseType : inEaseType) : easeType;
            seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.AppendInterval( isFadeOut ? Mathf.Max(biggestDelay - (delay + duration),0) : delay);
            seq.Append(target.DOLocalMove(isFadeOut ? distance * direction + (useBaseTargetPosition ? _basePosition : targetPosition):
                useBaseTargetPosition ? _basePosition  : targetPosition, duration).SetEase(usedEase)); 
            seq.Join(target.DOScale(isFadeOut ? baseScale : Vector2.one, duration).SetEase(usedEase));
            seq.JoinCallback(() => onAnimationStart?.Invoke());
            seq.OnComplete(() =>
            {
                
                onAnimationEnd?.Invoke();
                if (isFadeOut)
                {
                    if (disableTarget) target.gameObject.SetActive(false);
                    target.localPosition = _basePosition;
                    
                }
                else
                {
                    target.gameObject.SetActive(true);
                    target.localScale = Vector3.one;
                }
            });
        }
    }
    
    public static JonaUIAnimator Instance{ get; private set; }
    void Singleton()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
