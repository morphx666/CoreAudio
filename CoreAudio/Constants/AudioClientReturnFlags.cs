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

namespace CoreAudio {
    [Flags]
    public enum AudioClientReturnFlags : uint {
        Ok = 0,
        NotInitialized = 0x001,
        AlreadyInitialized = 0x002,
        WrongEndpointType = 0x003,
        DeviceInvalidated = 0x004,
        NotStopped = 0x005,
        BufferTooLarge = 0x006,
        OutOfOrder = 0x007,
        UnsupportedFormat = 0x008,
        InvalidSize = 0x009,
        DeviceInUse = 0x00a,
        BufferOperationPending = 0x00b,
        ThreadNotRegistered = 0x00c,
        ExclusiveModeNotAllowed = 0x00e,
        EndpointCreateFailed = 0x00f,
        ServiceNotRunning = 0x010,
        EventhandleNotExpected = 0x011,
        ExclusiveModeOnly = 0x012,
        BufdurationPeriodNotEqual = 0x013,
        EventhandleNotSet = 0x014,
        IncorrectBufferSize = 0x015,
        BufferSizeError = 0x016,
        CpuusageExceeded = 0x017,
        BufferError = 0x018,
        BufferSizeNotAligned = 0x019,
        InvalidDevicePeriod = 0x020,
        BufferEmpty = 0x001,
        ThreadAlreadyRegistered = 0x002,
        PositionStalled = 0x003
    }
}
