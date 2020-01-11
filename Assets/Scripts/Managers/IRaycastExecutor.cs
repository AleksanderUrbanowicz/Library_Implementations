using BaseLibrary.Managers;
using GeneralImplementations.Data;

namespace GeneralImplementations.Managers
{
    public interface IRaycastExecutor : IUpdateExecutor
    {
        void Init(RaycastData raycastData);

       // RaycastExecutorData GetExecutorData();
    }
}