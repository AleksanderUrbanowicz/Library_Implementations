using BaseLibrary.Managers;
using Data;

namespace GeneralImplementations.Managers
{
    public interface IRaycastExecutor : IUpdateExecutor
    {
        void Init(RaycastData raycastData);

        void GetHitInfo();
    }
}