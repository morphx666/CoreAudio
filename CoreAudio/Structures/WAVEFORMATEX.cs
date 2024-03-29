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

using System.Runtime.InteropServices;

namespace CoreAudio.Interfaces {
    [StructLayout(LayoutKind.Sequential)]
    internal struct WaveFormatEx {
        ushort FormatTag;		// format type
        ushort Channels;		// number of channels (i.e. mono, stereo...)
        uint SamplesPerSec;     // sample rate
        uint AvgBytesPerSec;	// for buffer estimation
        ushort BlockAlign;	    // block size of data
        ushort BitsPerSample;	// number of bits per sample of mono data
        ushort Size;			// the count in bytes of
    }
}
