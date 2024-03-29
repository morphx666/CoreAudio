﻿namespace CoreAudio {
    public class DevicePropertyChangedEventArgs : DeviceNotificationEventArgs {
        public PropertyKey PropertyKey { get; private set; }

        public DevicePropertyChangedEventArgs(string deviceId, PropertyKey propertyKey) : base(deviceId) {
            PropertyKey = propertyKey;
        }
    }
}
