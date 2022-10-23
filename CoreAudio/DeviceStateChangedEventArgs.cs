namespace CoreAudio {
    public class DeviceStateChangedEventArgs : DeviceNotificationEventArgs {
        public DeviceState DeviceState { get; private set; }

        public DeviceStateChangedEventArgs(string deviceId, DeviceState deviceState) : base(deviceId) {
            DeviceState = deviceState;
        }
    }
}
