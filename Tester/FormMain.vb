Imports CoreAudio
Imports System.Threading

Public Class FormMain
    Private timer As Timer

    Private tabLevel As Integer

    Private lines As New List(Of Part)
    Private ctrls As New List(Of Part)

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timer = New Timer(Sub() Me.Invoke(New MethodInvoker(Sub() EnumDevices(EDataFlow.eAll))), Nothing, 10, Timeout.Infinite)

        AddHandler Me.FormClosing, Sub() timer.Dispose()
    End Sub

    Private Sub EnumDevices(flow As EDataFlow)
        Dim mmde As MMDeviceEnumerator = New MMDeviceEnumerator()
        Dim devCol As MMDeviceCollection = mmde.EnumerateAudioEndPoints(flow, DEVICE_STATE.DEVICE_STATE_ACTIVE)
        Dim result As String = ""

        TextBoxOutput.Text = ""

        For d As Integer = 0 To devCol.Count - 1
            Dim dev As MMDevice = devCol(d)
            'Dim epvl As AudioEndpointVolume = dev.AudioEndpointVolume

#If DEBUG Then
            Static isFirstTime As Boolean = True
            If Not isFirstTime Then
                isFirstTime = True
                ' Debug.WriteLine(dev.Properties(PKEY.PKEY_DeviceInterface_FriendlyName))
                For p = 0 To dev.Properties.Count - 1
                    Dim prop As PropertyStoreProperty = dev.Properties(p)
                    Dim valType As String = prop.Value.GetType().ToString()
                    If valType.Contains(".") Then valType = valType.Split(".").Last()
                    Debug.Write($"{prop.Key.fmtid}, {prop.Key.pid} ({valType}): ")

                    Select Case prop.Value.GetType()
                        Case GetType(Byte())
                            For Each b As Byte In CType(prop.Value, Byte())
                                Debug.Write($"{b.ToString("X2")} ")
                            Next
                        Case Else
                            Debug.Write(prop.Value.ToString())
                    End Select
                    Debug.WriteLine("")
                Next
            End If
#End If

            Dim deviceTopology As DeviceTopology = dev.DeviceTopology
            Dim connectorEndpoint As Connector = deviceTopology.GetConnector(0)
            Dim connectorDevice As Connector = connectorEndpoint.GetConnectedTo
            Dim part As Part = connectorDevice.GetPart

            tabLevel = 0
            result += WalkTreeBackwardsFromPart(part)

            result += EnumSessions(dev)

            result += $"{StrDup(32, "-")}{Environment.NewLine}{Environment.NewLine}"
        Next

        TextBoxOutput.Text = result
        TextBoxOutput.SelectionStart = 0
        TextBoxOutput.ScrollToCaret()
    End Sub

    Private Function EnumSessions(dev As MMDevice)
        tabLevel += 1
        Dim tabs As String = StrDup(tabLevel, vbTab)
        Dim result As String = ""

        result += $"{tabs}SESSIONS{Environment.NewLine}"
        Dim sc As SessionCollection = dev.AudioSessionManager2.Sessions
        For i As Integer = 0 To sc.Count - 1
            Dim asc As AudioSessionControl2 = sc.Item(i)
            Dim sName As String = asc.DisplayName
            If sName.Contains(",") Then sName = sName.Split(",")(0)
            If sName = "" Then sName = "<No Name>"
            result += $"{tabs}{sName} [{asc.GetProcessID} | {asc.State}]:{Environment.NewLine}"
            result += $"{tabs}{vbTab}Volume: {asc.SimpleAudioVolume.MasterVolume:F2}dB{Environment.NewLine}"
            result += $"{tabs}{vbTab}Mute: {If(Not asc.SimpleAudioVolume.Mute, "Not ", "")}Muted{Environment.NewLine}{Environment.NewLine}"
            asc.Dispose()
        Next

        Return result
    End Function

    Private Sub GetLines(part As Part)
        If part.GetPartType = PartType.Connector Then lines.Add(part)

        Dim plIn = part.EnumPartsIncoming
        If plIn IsNot Nothing Then
            For i As Integer = 0 To plIn.GetCount - 1
                Dim iPart = plIn.GetPart(i)
                GetLines(iPart)
            Next
        End If
    End Sub

    Private Function WalkTreeBackwardsFromPart(part As Part) As String
        Dim tabs As String = StrDup(tabLevel, vbTab)
        Dim result As String = ""
        result += $"{tabs}{part.GetSubTypeName}: {If(part.GetName = "", "(Unnamed)", part.GetName)} ({part.GetPartType}){Environment.NewLine}"
        'If part.GetSubTypeName = "UNDEFINED" Then Stop

        If part.GetPartType = PartType.Connector Then tabLevel += 1
        Select Case part.GetSubType
            Case KSNODETYPE.VOLUME
                Dim avl As AudioVolumeLevel = part.AudioVolumeLevel
                If avl IsNot Nothing Then
                    For c As Integer = 0 To avl.GetChannelCount - 1
                        Dim lr As AudioVolumeLevel.LevelRange = avl.GetLevelRange(c)
                        result += $"{vbTab} {tabs}Volume for channel {c} is {avl.GetLevel(c):F2}dB (range is {lr.minLevel:F2}dB to {lr.maxLevel:F2}dB in increments of {lr.stepping:F2}dB){Environment.NewLine}"
                    Next
                End If
            Case KSNODETYPE.SPEAKER
                result += $"{tabs}Incoming Parts:{Environment.NewLine}"
            Case KSNODETYPE.MUTE
                Dim am As AudioMute = part.AudioMute
                If am IsNot Nothing Then result += $"{tabs}{vbTab}{If(Not am.Mute, "Not ", "")}Muted{Environment.NewLine}"
            Case KSNODETYPE.PEAKMETER
                Dim apm As AudioPeakMeter = part.AudioPeakMeter
                If apm IsNot Nothing Then
                    For c As Integer = 0 To apm.GetChannelCount - 1
                        result += $"{vbTab}{tabs}Level for channel {c} is {apm.GetLevel(c):F2}dB{Environment.NewLine}"
                    Next
                End If
            Case KSNODETYPE.LOUDNESS
                Dim al As AudioLoudness = part.AudioLoudness
                If al IsNot Nothing Then result += $"{tabs}{vbTab}{If(Not al.Enabled, "Not ", "")}Enabled{Environment.NewLine}"
            Case Else
                If part.GetPartType = PartType.Connector Then
                    result += $"{vbTab}{tabs}I/O Jack{Environment.NewLine}"
                Else
                    result += $"{vbTab}{tabs}Undefined Part: {part.GetSubTypeName}{Environment.NewLine}"
                End If
        End Select

        Dim plIn As PartsList = part.EnumPartsIncoming
        If plIn Is Nothing Then
            result += vbCrLf
        Else
            If plIn.GetCount = 0 Then
                result += vbCrLf
            Else
                For i As Integer = 0 To plIn.GetCount - 1
                    Dim iPart = plIn.GetPart(i)
                    result += WalkTreeBackwardsFromPart(iPart)
                Next
            End If
        End If

        If part.GetPartType = PartType.Connector Then tabLevel -= 1
        part.Dispose()

        Return result
    End Function

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        timer.Change(Timeout.Infinite, Timeout.Infinite)
        Me.Close()
    End Sub

    Private Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        timer.Change(10, Timeout.Infinite)
    End Sub

    Private Sub CheckBoxAutoRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAutoRefresh.CheckedChanged
        If CheckBoxAutoRefresh.Checked Then
            ButtonRefresh.Enabled = False
            timer.Change(10, 2000)
        Else
            ButtonRefresh.Enabled = True
            timer.Change(Timeout.Infinite, Timeout.Infinite)
        End If
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Using dlg As New SaveFileDialog()
            With dlg
                .FileName = "CoreAudioNET_Report.txt"
                .Filter = "Text Files|*.txt"
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                .Title = "Save CoreAudioNET Report"
                If .ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    IO.File.WriteAllText(.FileName, TextBoxOutput.Text)
                End If
            End With
        End Using
    End Sub
End Class
