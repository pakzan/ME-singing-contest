Imports System.IO
Imports AxWMPLib

Public Class Form1
    Private timeLeft As Integer
    Public mark As Integer
    Private tempMark As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        mark = CInt(Int((99 * Rnd()) + 1))
        Me.WindowState = FormWindowState.Maximized
        label.Left = My.Computer.Screen.Bounds.Width / 2 - label.Width / 2
        Label1.Left = My.Computer.Screen.Bounds.Width / 2 - Label1.Width / 2

        label.Top = My.Computer.Screen.Bounds.Height / 2 - label.Height / 2 - Label1.Height + 2
        Label1.Top = My.Computer.Screen.Bounds.Height / 2 - Label1.Height / 2
        AxWindowsMediaPlayer1.stretchToFit() = True
        AxWindowsMediaPlayer2.stretchToFit() = True

        Dim files() As String = IO.Directory.GetFiles("C:\Users\tpz\Documents\Visual Studio 2015\Projects\ME singing contest\ME singing contest\Song list\")

        For Each file As String In files
            Dim text As String = file.Replace("C:\Users\tpz\Documents\Visual Studio 2015\Projects\ME singing contest\ME singing contest\Song list\", "").Replace(".wmv", "") + "                                                    "
            ListView1.Items.Add(text)
        Next
        ListView1.Items.Add("Search Online")
        For Each lvi In ListView1.Items
            lvi.Font = New Font(New FontFamily("Arial"), 10, FontStyle.Regular)
        Next
    End Sub

    Private Sub WebBrowserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Start the timer.
        'timeLeft = 10
        'label.Text = Int(timeLeft / 60) & ":" & Int((timeLeft Mod 60) / 10) & timeLeft Mod 60 Mod 10
    End Sub

    Private Sub Timer1_Tick() Handles Timer1.Tick

        'If timeLeft > 0 Then
        '    ' Display the new time left
        '    ' by updating the Time Left label.
        '    timeLeft -= 1
        '    label.Text = Int(timeLeft / 60) & ":" & Int((timeLeft Mod 60) / 10) & timeLeft Mod 60 Mod 10
        '    If timeLeft = 6 Then
        '        My.Computer.Audio.Play(My.Resources.countdown, AudioPlayMode.Background)
        '    End If
        'Else
        '    ' If the user ran out of time, stop the timer, show
        '    ' a MessageBox, and fill in the answers.
        '    WebBrowser1.Dispose()
        '    AxWindowsMediaPlayer1.Dispose()
        '    Timer1.Stop()
        '    System.Threading.Thread.Sleep(100)
        '    Timer2.Start()
        'End If

    End Sub

    Private Sub Timer2_Tick() Handles Timer2.Tick

        If tempMark < mark Then
            Timer2.Interval = 10 + 90 * tempMark / mark
            tempMark += 1
            My.Computer.Audio.Play(My.Resources.score, AudioPlayMode.Background)
            label.Text = "  Your Score: " & If(tempMark < 10, " ", "") & tempMark & "  "
        Else
            If mark <= 40 Then
                Label1.Text = "Try again later"
                My.Computer.Audio.Play(My.Resources.tryagain, AudioPlayMode.Background)
                ListView1.Visible = True
            Else
                Label1.Text = "Congratulations!"
                My.Computer.Audio.Play(My.Resources.Clapping, AudioPlayMode.Background)
                AxWindowsMediaPlayer2.URL = "C:\Users\tpz\Documents\Visual Studio 2015\Projects\ME singing contest\ME singing contest\fireworks.wmv"
                AxWindowsMediaPlayer2.Ctlcontrols.play()
            End If
            Label1.Left = Me.Width / 2 - Label1.Width / 2
            Label1.Visible = True
            Timer2.Stop()
        End If

    End Sub

    Private Sub label_TextChanged(sender As Object, e As EventArgs) Handles label.TextChanged
        label.Left = Me.Width / 2 - label.Width / 2
    End Sub

    Private Sub AxWindowsMediaPlayer1_PlayStateChange(sender As Object, e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles AxWindowsMediaPlayer1.PlayStateChange

        Select Case e.newState
            Case 1 ' Stopped
                Debug.Print("Stopped")
                label.Visible = True
                WebBrowser1.Dispose()
                AxWindowsMediaPlayer1.Visible = False
                System.Threading.Thread.Sleep(100)
                Timer2.Start()

            Case 2    ' Paused
                Debug.Print("Paused")
                ListView1.Visible = True

            Case 3 ' Playing
                Debug.Print("Playing")
                label.Visible = False
                Label1.Visible = False
                ListView1.Visible = False
                tempMark = 0
                Randomize()
                mark = CInt(Int((99 * Rnd()) + 1))

            Case 4 ' ScanForward
                Debug.Print("ScanForward")

            Case 5 ' ScanReverse
                Debug.Print("ScanReverse")

            Case 6 ' Buffering
                Debug.Print("Buffering")

            Case 7 ' Waiting
                Debug.Print("Waiting")

            Case 8 ' MediaEnded
                Debug.Print("MediaEnded")

            Case 9 ' Transitioning
                Debug.Print("Transitioning")

            Case 10 ' Ready
                Debug.Print("Ready")

            Case 11 ' Reconnecting
                Debug.Print("Reconnecting")

            Case 12 ' Last
                Debug.Print("Last")

            Case Else
                Debug.Print("Undefined/Unknown: " & e.newState)

        End Select
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        AxWindowsMediaPlayer1.Visible = True
        AxWindowsMediaPlayer2.Ctlcontrols.stop()
        If ListView1.FocusedItem IsNot Nothing Then
            If File.Exists("C:\Users\tpz\Documents\Visual Studio 2015\Projects\ME singing contest\ME singing contest\Song list\" + ListView1.FocusedItem.SubItems(0).Text.Trim("    ") + ".wmv") Then
                AxWindowsMediaPlayer1.URL = "C:\Users\tpz\Documents\Visual Studio 2015\Projects\ME singing contest\ME singing contest\Song list\" + ListView1.FocusedItem.SubItems(0).Text.Trim("    ") + ".wmv"
                AxWindowsMediaPlayer1.Ctlcontrols.play()
            ElseIf ListView1.FocusedItem.SubItems(0).Text = "Search Online" Then
                AxWindowsMediaPlayer1.Visible = False
                Me.WebBrowser1.Navigate("https://www.youtube.com/user/singkingkaraoke/playlists")
            End If
        End If
    End Sub

    Private Sub AxWindowsMediaPlayer2_PlayStateChange(sender As Object, e As _WMPOCXEvents_PlayStateChangeEvent) Handles AxWindowsMediaPlayer2.PlayStateChange

        Select Case e.newState
            Case 1 ' Stopped
                Debug.Print("Stopped")
                ListView1.Visible = True

            Case 2    ' Paused
                Debug.Print("Paused")
                ListView1.Visible = True

            Case 3 ' Playing
                Debug.Print("Playing")
                ListView1.Visible = False
        End Select
    End Sub
End Class
