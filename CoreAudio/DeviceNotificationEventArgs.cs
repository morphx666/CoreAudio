﻿using System;

namespace CoreAudio {
    public class DeviceNotificationEventArgs : EventArgs {
        public string DeviceId { get; private set; }

        public DeviceNotificationEventArgs(string deviceId) {
            DeviceId = deviceId;
        }

        public bool TryGetDevice(out MMDevice? device) {
            try {
                var deviceEnumerator = new MMDeviceEnumerator(Guid.Empty);
                device = deviceEnumerator.GetDevice(DeviceId);
                return true;
            } catch(Exception) {
                device = null;
            }

            return false;
        }
    }
}
