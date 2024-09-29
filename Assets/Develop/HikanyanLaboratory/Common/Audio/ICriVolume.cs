using UniRx;

namespace HikanyanLaboratory.Common
{
    public interface ICriVolume
    {
        IReactiveProperty<float> Volume { get; }
        void SetVolume(float volume);
    }
}