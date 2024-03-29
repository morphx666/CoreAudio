﻿using CoreAudio.Interfaces;
using System;
using System.Runtime.InteropServices;

/* 
    Created by Xavier Flix (2022/10/22)

    Portions of the code inspired by or blatanly copied from filoe/cscore
*/

namespace CoreAudio {
    [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
    public class MMNotificationClient : IMMNotificationClient {
        private readonly MMDeviceEnumerator deviceEnumerator;

        public event EventHandler<DeviceStateChangedEventArgs>? DeviceStateChanged;
        public event EventHandler<DeviceNotificationEventArgs>? DeviceAdded;
        public event EventHandler<DeviceNotificationEventArgs>? DeviceRemoved;
        public event EventHandler<DefaultDeviceChangedEventArgs>? DefaultDeviceChanged;
        public event EventHandler<DevicePropertyChangedEventArgs>? DevicePropertyChanged;

        public MMNotificationClient(MMDeviceEnumerator enumerator) {
            deviceEnumerator = enumerator;
            Marshal.ThrowExceptionForHR(deviceEnumerator.RegisterEndpointNotificationCallback(this));
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string deviceId) {
            DefaultDeviceChanged?.Invoke(this, new DefaultDeviceChangedEventArgs(deviceId, flow, role));
        }

        public void OnDeviceAdded(string deviceId) {
            DeviceAdded?.Invoke(this, new DeviceNotificationEventArgs(deviceId));
        }

        public void OnDeviceRemoved(string deviceId) {
            DeviceRemoved?.Invoke(this, new DeviceNotificationEventArgs(deviceId));
        }

        public void OnDeviceStateChanged(string deviceId, DeviceState newState) {
            DeviceStateChanged?.Invoke(this, new DeviceStateChangedEventArgs(deviceId, newState));
        }

        public void OnPropertyValueChanged(string deviceId, PropertyKey key) {
            DevicePropertyChanged?.Invoke(this, new DevicePropertyChangedEventArgs(deviceId, key));
        }
    }
}
