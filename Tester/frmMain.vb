Imports CoreAudio
Imports System.Threading

Public Class frmMain
    Private Delegate Sub DoSafeEnum()
    Private timer As Timer

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        timer = New Timer(Sub() SafeEnum(), Nothing, 10, Timeout.Infinite)
    End Sub

    Private Sub SafeEnum()
        Me.Invoke(New DoSafeEnum(Sub() EnumDevices(EDataFlow.eAll)))
    End Sub

    Private Sub EnumDevices(ByVal flow As EDataFlow)
        Dim mmde = New MMDeviceEnumerator()
        Dim devCol = mmde.EnumerateAudioEndPoints(flow, DEVICE_STATE.DEVICE_STATE_ACTIVE)

        tbOutput.Text = ""

        For d As Integer = 0 To devCol.Count - 1
            Dim dev As MMDevice = devCol(d)
            Dim epvl = dev.AudioEndpointVolume

            'Debug.WriteLine(dev.Properties(PKEY.PKEY_DeviceInterface_FriendlyName))

            'For p = 0 To dev.Properties.Count - 1
            '    Dim prop As PropertyStoreProperty = dev.Properties(p)
            '    Debug.WriteLine(prop.Key.fmtid.ToString() & ", " & prop.Key.pid & ": " & prop.Value.ToString())
            'Next

            Dim deviceTopology = dev.DeviceTopology
            Dim connectorEndpoint = deviceTopology.GetConnector(0)
            Dim connectorDevice = connectorEndpoint.GetConnectedTo
            Dim part = connectorDevice.GetPart

            tabLevel = 0
            WalkTreeBackwardsFromPart(part)
            'GetLines(part)
            'Stop
        Next

        tbOutput.SelectionStart = 0
        tbOutput.ScrollToCaret()
    End Sub

    Private tabLevel As Integer

    Private lines As New List(Of Part)
    Private ctrls As New List(Of Part)
    Private Sub GetLines(ByVal part As Part)
        If part.GetPartType = PartType.Connector Then lines.Add(part)

        Dim plIn = part.EnumPartsIncoming
        If plIn IsNot Nothing Then
            For i As Integer = 0 To plIn.GetCount - 1
                Dim iPart = plIn.GetPart(i)
                GetLines(iPart)
            Next
        End If
    End Sub

    Private Sub GetControls(ByVal part As Part)
        If part.GetPartType = PartType.Subunit Then lines.Add(part)
    End Sub

    Private Sub WalkTreeBackwardsFromPart(ByVal part As Part)
        Dim tabs = StrDup(tabLevel, vbTab)
        tbOutput.Text += (tabs + part.GetSubTypeName + ": " + IIf(part.GetName = "", "(Unnamed)", part.GetName) + " (" + part.GetPartType.ToString() + ")") + vbCrLf
        'If part.GetSubTypeName = "UNDEFINED" Then Stop

        If part.GetPartType = PartType.Connector Then tabLevel += 1
        Select Case part.GetSubType
            Case KSNODETYPE.VOLUME
                Dim avl = part.AudioVolumeLevel
                If avl IsNot Nothing Then
                    For c As Integer = 0 To avl.GetChannelCount - 1
                        Dim lr As AudioVolumeLevel.LevelRange = avl.GetLevelRange(c)
                        tbOutput.Text += (vbTab + String.Format("{0}Volume for channel {1} is {2:F2}dB (range is {3:F2}dB to {4:F2}dB in increments of {5:F2}dB)", tabs, c, avl.GetLevel(c), lr.minLevel, lr.maxLevel, lr.stepping)) + vbCrLf
                    Next
                End If
            Case KSNODETYPE.SPEAKER
                tbOutput.Text += tabs + "Incoming Parts:" + vbCrLf
            Case KSNODETYPE.MUTE
                Dim am = part.AudioMute
                If am IsNot Nothing Then tbOutput.Text += tabs + vbTab + If(Not am.Mute, "Not ", "") + "Muted" + vbCrLf
            Case KSNODETYPE.PEAKMETER
                Dim apm = part.AudioPeakMeter
                If apm IsNot Nothing Then
                    For c As Integer = 0 To apm.GetChannelCount - 1
                        tbOutput.Text += (vbTab + String.Format("{0}Level for channel {1} is {2:F2}dB", tabs, c, apm.GetLevel(c))) + vbCrLf
                    Next
                End If
            Case KSNODETYPE.LOUDNESS
                Dim al = part.AudioLoudness
                If al IsNot Nothing Then tbOutput.Text += tabs + vbTab + If(Not al.Enabled, "Not ", "") + "Enabled" + vbCrLf
            Case Else
                If part.GetPartType = PartType.Connector Then
                    tbOutput.Text += vbTab + tabs + "I/O Jack" + vbCrLf
                Else
                    tbOutput.Text += vbTab + tabs + "Undefined Part: " + part.GetSubTypeName + vbCrLf
                End If
        End Select

        Dim plIn = part.EnumPartsIncoming
        If plIn Is Nothing Then
            tbOutput.Text += vbCrLf
        Else
            If plIn.GetCount = 0 Then
                tbOutput.Text += vbCrLf
            Else
                For i As Integer = 0 To plIn.GetCount - 1
                    Dim iPart = plIn.GetPart(i)
                    WalkTreeBackwardsFromPart(iPart)
                Next
            End If
        End If

        If part.GetPartType = PartType.Connector Then tabLevel -= 1
        part.Dispose()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        timer.Change(Timeout.Infinite, Timeout.Infinite)
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        timer.Change(10, Timeout.Infinite)
    End Sub

    Private Sub chkAutoRefresh_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAutoRefresh.CheckedChanged
        If chkAutoRefresh.Checked Then
            btnRefresh.Enabled = False
            timer.Change(10, 2000)
        Else
            btnRefresh.Enabled = True
            timer.Change(Timeout.Infinite, Timeout.Infinite)
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Using dlg As New SaveFileDialog()
            With dlg
                .FileName = "CoreAudioNET_Report.txt"
                .Filter = "Text Files|*.txt"
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                .Title = "Save CoreAudioNET Report"
                If .ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    IO.File.WriteAllText(.FileName, tbOutput.Text)
                End If
            End With
        End Using
    End Sub
End Class
