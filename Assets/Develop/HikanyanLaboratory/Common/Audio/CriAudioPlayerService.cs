using System;
using System.Collections.Generic;
using System.Linq;
using CriWare;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.Common
{
    public class CriAudioPlayerService : ICriAudioPlayerService
    {
        private readonly CriAtomExPlayer _criAtomExPlayer; // 複数の音声を再生するためのプレイヤー
        private readonly CriAtomEx3dSource _criAtomEx3dSource; // 3D音源
        private readonly Dictionary<Guid, CriAtomExPlayback> _playbacks; // 再生中の音声を管理
        private readonly CriAtomListener _criAtomListener; // リスナー
        private readonly string _cueSheetName; // ACBファイルの名前
        private const float MasterVolume = 1f; // マスターボリューム

        public IReactiveProperty<float> Volume { get; private set; } = new ReactiveProperty<float>(1f); // ボリューム

        public CriAudioPlayerService(string cueSheetName, CriAtomListener criAtomListener)
        {
            _cueSheetName = cueSheetName;
            _criAtomListener = criAtomListener;
            _criAtomExPlayer = new CriAtomExPlayer();
            _criAtomEx3dSource = new CriAtomEx3dSource();
            _playbacks = new Dictionary<Guid, CriAtomExPlayback>();

            Volume.Subscribe(SetVolume);
        }

        ~CriAudioPlayerService()
        {
            Dispose();
        }

        public Guid Play<TEnum>(CriAudioType type, TEnum cueName, float volume = 1f, bool isLoop = false)
        {
            string cueNameString = cueName.ToString();
            if (!CheckCueSheet())
            {
                Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return Guid.Empty;
            }

            var tempAcb = CriAtom.GetCueSheet(_cueSheetName).acb;
            tempAcb.GetCueInfo(cueNameString, out var cueInfo);

            _criAtomExPlayer.SetCue(tempAcb, cueNameString);
            _criAtomExPlayer.SetVolume(volume * Volume.Value * MasterVolume);
            _criAtomExPlayer.Loop(isLoop);

            var playback = _criAtomExPlayer.Start();
            var id = Guid.NewGuid();
            _playbacks[id] = playback;
            return id;
        }

        public Guid Play3D<TEnum>(Transform transform, CriAudioType type, TEnum cueName, float volume = 1f,
            bool isLoop = false)
        {
            string cueNameString = cueName.ToString();
            if (!CheckCueSheet())
            {
                Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return Guid.Empty;
            }

            var tempAcb = CriAtom.GetCueSheet(_cueSheetName).acb;
            tempAcb.GetCueInfo(cueNameString, out var cueInfo);


            _criAtomEx3dSource.SetPosition(transform.position.x, transform.position.y, transform.position.z);
            _criAtomEx3dSource.Update();

            _criAtomExPlayer.Set3dSource(_criAtomEx3dSource);
            _criAtomExPlayer.Set3dListener(_criAtomListener.nativeListener);
            _criAtomExPlayer.SetCue(tempAcb, cueNameString);
            _criAtomExPlayer.SetVolume(volume * Volume.Value * MasterVolume);
            _criAtomExPlayer.Loop(isLoop);

            var playback = _criAtomExPlayer.Start();
            var id = Guid.NewGuid();
            _playbacks[id] = playback;
            return id;
        }

        public void Stop(Guid id)
        {
            if (!_playbacks.TryGetValue(id, out var playback)) return;
            playback.Stop();
            _playbacks.Remove(id);
        }

        public void Pause(Guid id)
        {
            if (_playbacks.TryGetValue(id, out var playback))
            {
                playback.Pause();
            }
        }

        public void Resume(Guid id)
        {
            if (_playbacks.TryGetValue(id, out var playback))
            {
                playback.Resume(CriAtomEx.ResumeMode.PausedPlayback);
            }
        }

        public void StopAll()
        {
            foreach (var playback in _playbacks.Values)
            {
                playback.Stop();
            }

            _playbacks.Clear();
        }

        public void PauseAll()
        {
            foreach (var playback in _playbacks.Values)
            {
                playback.Pause();
            }
        }

        public void ResumeAll()
        {
            foreach (var playback in _playbacks.Values)
            {
                playback.Resume(CriAtomEx.ResumeMode.PausedPlayback);
            }
        }

        public void SetVolume(float volume)
        {
            _criAtomExPlayer.SetVolume(volume * MasterVolume);
            _criAtomExPlayer.UpdateAll();
        }

        public void Dispose()
        {
            foreach (var playback in _playbacks.Values)
            {
                playback.Stop();
            }

            _criAtomExPlayer.Dispose();
            _criAtomEx3dSource.Dispose();
        }


        private bool CheckCueSheet()
        {
            var tempAcb = CriAtom.GetCueSheet(_cueSheetName)?.acb;
            if (tempAcb == null)
            {
                Debug.LogWarning($"ACBがNullです。CueSheet: {_cueSheetName}");
                return false;
            }

            return true;
        }

        public void CheckPlayerStatus()
        {
            var idsToRemove = (from kvp in _playbacks
                where kvp.Value.GetStatus() == CriAtomExPlayback.Status.Removed
                select kvp.Key).ToList();

            foreach (var id in idsToRemove)
            {
                _playbacks.Remove(id);
            }
        }
    }
}