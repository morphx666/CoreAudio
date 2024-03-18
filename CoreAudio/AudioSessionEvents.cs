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
/* Updated by John de Jong (2020/04/02) */

using System;
using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    internal class AudioSessionEvents : IAudioSessionEvents {
        IIAudioSessionControl parent;

        internal AudioSessionEvents(IIAudioSessionControl parent) {
            this.parent = parent;
        }

        [PreserveSig]
        public int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string newDisplayName, ref Guid eventContext) {
            parent.FireDisplayNameChanged(newDisplayName, ref eventContext);
            return 0;
        }

        [PreserveSig]
        public int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string newIconPath, ref Guid eventContext) {
            parent.FireOnIconPathChanged(newIconPath, ref eventContext);
            return 0;
        }

        [PreserveSig]
        public int OnSimpleVolumeChanged(float newVolume, bool newMute, ref Guid eventContext) {
            parent.FireSimpleVolumeChanged(newVolume, newMute, ref eventContext);
            return 0;
        }

        [PreserveSig]
        public int OnChannelVolumeChanged(uint channelCount, IntPtr newChannelVolumeArray, uint changedChannel, ref Guid eventContext) {
            parent.FireChannelVolumeChanged(channelCount, newChannelVolumeArray, changedChannel, ref eventContext);
            return 0;
        }

        [PreserveSig]
        public int OnGroupingParamChanged(ref Guid newGroupingParam, ref Guid eventContext) {
            return 0;
        }

        [PreserveSig]
        public int OnStateChanged(AudioSessionState newState) {
            parent.FireStateChanged(newState);
            return 0;
        }

        [PreserveSig]
        public int OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason) {
            return 0;
        }
    }
}
