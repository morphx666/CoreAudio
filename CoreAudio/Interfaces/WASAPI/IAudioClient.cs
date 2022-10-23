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

/* Created by Xavier Flix (2010/11/18) */

using System;
using System.Runtime.InteropServices;

namespace CoreAudio.Interfaces {
    [Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioClient {
        [PreserveSig]
        AudioClientReturnFlags Initialize(AudioClientShareMode ShareMode, AudioClientStreamFlags StreamFlags, long hnsBufferDuration, long hnsPeriodicity, WaveFormatEx pFormat, Guid AudioSessionGuid);
        [PreserveSig]
        AudioClientReturnFlags GetBufferSize(out uint pNumBufferFrames);
        [PreserveSig]
        AudioClientReturnFlags GetStreamLatency(out long phnsLatency);
        [PreserveSig]
        AudioClientReturnFlags GetCurrentPadding(out long pNumPaddingFrames);
        [PreserveSig]
        AudioClientReturnFlags IsFormatSupported(AudioClientShareMode ShareMode, WaveFormatEx pFormat, out WaveFormatEx ppClosestMatch);
        [PreserveSig]
        AudioClientReturnFlags GetMixFormat(out WaveFormatEx ppDeviceFormat);
        [PreserveSig]
        AudioClientReturnFlags GetDevicePeriod(out long phnsDefaultDevicePeriod, out long phnsMinimumDevicePeriod);
        [PreserveSig]
        AudioClientReturnFlags Start();
        [PreserveSig]
        AudioClientReturnFlags Stop();
        [PreserveSig]
        AudioClientReturnFlags Reset();
        [PreserveSig]
        AudioClientReturnFlags SetEventHandle(IntPtr eventHandle);
        [PreserveSig]
        AudioClientReturnFlags GetService(ref Guid riid, [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppv);
    }
}
