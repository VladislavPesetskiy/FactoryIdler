using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI.Components
{
    public class ProgressBar : MonoBehaviour
    {
        public enum FillType
        {
            Horizontal = 0,
            Vertical = 1
        }
        
        [SerializeField]
        private RectTransform rectFill;
        
        [SerializeField]
        private FillType fillType = FillType.Horizontal;

        [SerializeField]
        private float fillDuration = 0.25f;
        
        [SerializeField]
        private Ease fillEase = Ease.OutCirc;

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public virtual void SetNormalizedProgress(float progress, bool instant = false)
        {
            var currentAnchor = rectFill.anchorMax;

            switch (fillType)
            {
                case FillType.Horizontal:
                    currentAnchor.x = progress;
                    break;
                case FillType.Vertical:
                    currentAnchor.y = progress;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            FillProgressAnchor(currentAnchor, instant);
        }

        protected virtual void FillProgressAnchor(Vector2 targetAnchor, bool instant = false)
        {
            DOTween.Kill(this);

            if (instant)
            {
                rectFill.anchorMax = targetAnchor;
            }
            else
            {
                rectFill.DOAnchorMax(targetAnchor, fillDuration).SetEase(fillEase).SetId(this);
            }
        }
    }
}