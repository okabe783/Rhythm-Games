using System;
using UnityEngine;

namespace HikanyanLaboratory.Common
{
    public interface ICriAudioPlayerService　: ICriVolume, IDisposable
    {
        Guid Play<TEnum>
            (CriAudioType type, TEnum cueName, float volume = 1f, bool isLoop = false);

        Guid Play3D<TEnum>
            (Transform transform, CriAudioType type, TEnum cueName, float volume = 1f, bool isLoop = false);

        void Stop(Guid id);
        void Pause(Guid id);
        void Resume(Guid id);
        void StopAll();
        void PauseAll();
        void ResumeAll();
    }
}