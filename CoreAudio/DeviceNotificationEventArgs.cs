using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAudio {
    public class DeviceNotificationEventArgs : EventArgs {
        public string DeviceId { get; private set; }

        public DeviceNotificationEventArgs(string deviceId) {
            DeviceId = deviceId;
        }

        public bool TryGetDevice(out MMDevice? device) {
            try {
                var deviceEnumerator = new MMDeviceEnumerator();
                device = deviceEnumerator.GetDevice(DeviceId);
                return true;
            } catch(Exception) {
                device = null;
            }

            return false;
        }
    }
}
