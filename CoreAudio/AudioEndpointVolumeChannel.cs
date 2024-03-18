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
    public class AudioEndpointVolumeChannel {
        readonly uint channel;
        readonly IAudioEndpointVolume audioEndpointVolume;
        internal Guid eventContext;

        internal AudioEndpointVolumeChannel(IAudioEndpointVolume parent, int channel, ref Guid eventContext) {
            audioEndpointVolume = parent;
            this.channel = (uint)channel;
            this.eventContext = eventContext;
        }

        public float VolumeLevel {
            get {
                Marshal.ThrowExceptionForHR(audioEndpointVolume.GetChannelVolumeLevel(channel, out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(audioEndpointVolume.SetChannelVolumeLevel(channel, value, ref eventContext));
        }

        public float VolumeLevelScalar {
            get {
                Marshal.ThrowExceptionForHR(audioEndpointVolume.GetChannelVolumeLevelScalar(channel, out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(audioEndpointVolume.SetChannelVolumeLevelScalar(channel, value, ref eventContext));
        }

    }
}
