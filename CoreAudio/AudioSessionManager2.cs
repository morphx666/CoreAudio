/*
  LICENSE
  -------
  Copyright (C) 2007-2010 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/

using System;
using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    public class AudioSessionManager2 : IDisposable {
        readonly IAudioSessionManager2 audioSessionManager2;
        SessionCollection? sessions;

        public delegate void SessionCreatedDelegate(object sender, IAudioSessionControl2 newSession);
        public event SessionCreatedDelegate? OnSessionCreated;
        internal Guid eventContext;

        AudioSessionNotification? _AudioSessionNotification;

        internal AudioSessionManager2(IAudioSessionManager2 realAudioSessionManager2, ref Guid eventContext) {
            audioSessionManager2 = realAudioSessionManager2;
            this.eventContext = eventContext;
            RefreshSessions();
        }

        internal void FireSessionCreated(IAudioSessionControl2 newSession) {
            OnSessionCreated?.Invoke(this, newSession);
        }

        public void RefreshSessions() {
            UnregisterNotifications();

            Marshal.ThrowExceptionForHR(audioSessionManager2.GetSessionEnumerator(out IAudioSessionEnumerator sessionEnum));
            sessions = new SessionCollection(sessionEnum, ref eventContext);

            _AudioSessionNotification = new AudioSessionNotification(this);
            Marshal.ThrowExceptionForHR(audioSessionManager2.RegisterSessionNotification(_AudioSessionNotification));
        }

        public SessionCollection? Sessions => sessions;

        void UnregisterNotifications() {
            sessions = null;

            if(_AudioSessionNotification != null) {
                try {
                    Marshal.ThrowExceptionForHR(audioSessionManager2.UnregisterSessionNotification(_AudioSessionNotification));
                } catch { }
            }
        }

        public void Dispose() {
            UnregisterNotifications();
        }

        ~AudioSessionManager2() {
            Dispose();
        }
    }
}
