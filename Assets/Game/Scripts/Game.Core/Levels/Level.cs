using Game.Core.Configurations;
using UniRx;
using UnityEngine;

namespace Game.Core.Levels
{
    public abstract class Level : MonoBehaviour
    {
        public bool IsShow { get; private set; }
        public ISubject<bool> EventLevelResult { get; private set; }
        public LevelSettings LevelSettings { get; private set; }

        public virtual void Initialize(LevelSettings levelSettings)
        {
            EventLevelResult = new Subject<bool>();
            LevelSettings = levelSettings;
        }

        public virtual void DeInitialize()
        {
            EventLevelResult = null;
        }

        public virtual void Show()
        {
            if(IsShow) return;
            
            IsShow = true;
        }

        public virtual void Hide()
        {
            if(IsShow == false) return;

            IsShow = false;
        }

        public virtual void Finish(bool isWin)
        {
            EventLevelResult?.OnNext(isWin);
        }
    }
}