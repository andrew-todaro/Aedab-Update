Public Class Form1
    Sub getswf()
        Dim sourcecode As String = String.Empty
        Try
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://guardian.battleon.com/game/flash/game.asp?launchtype=large")
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            sourcecode = sr.ReadToEnd
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            Dim Fst As Integer = sourcecode.IndexOf("src=")
            Dim Snd As String = sourcecode.Substring(Fst, sourcecode.Length - Fst) : sourcecode = Snd
            Dim Thrd As Integer = sourcecode.IndexOf("FlashVars=")
            Dim Frth As String = sourcecode.Substring(0, Thrd)
            Dim fth As String = "src="
            Dim sth() As String = {fth}
            Dim svn = Frth.Split(sth, 2, StringSplitOptions.RemoveEmptyEntries)
            Dim ate As String = svn(1)
            Dim Final As String = ate.Replace("""", "")
            Dim f2() As String = {fth}
            Dim f3 = Final.Split(sth, 2, StringSplitOptions.RemoveEmptyEntries)
            TextBox1.Text = f3(1)
        Catch ex As Exception
            MsgBox("No web access / AE website has broke autofetch code, manually find and enter the new SWF by going to the games website and right clicking then view page source and search for .swf")
        End Try

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getswf()
        Flash.Base = "http://guardian.battleon.com/game/flash/game.asp?launchtype=large"
        Flash.Movie = "http://guardian.battleon.com/game/flash/Lore451.swf?ver=2" & TextBox1.Text
        Flash.Play()
        Try
            Dim dir As New IO.DirectoryInfo(Application.StartupPath & "/data/")
            Dim files As IO.FileInfo() = dir.GetFiles("*.swf")
            Dim fileName As IO.FileInfo

            For Each fileName In files
                ListBox1.Items.Add(fileName.Name)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick, Timer.Tick
        Try
            Dim value As Integer = Flash.GetVariable("_root.player.intBaseSP")
            Dim value2 As Integer = Flash.GetVariable("_root.player.intBaseMP")
            If value + value2 > 0.0 Then
                Flash.SetVariable("_root.player.intSP", value2)
                Flash.SetVariable("_root.player.intMP", value)
            Else
                Flash.SetVariable("_root.player.intSP", 10000)
                Flash.SetVariable("_root.player.intMP", 10000)
            End If
            Dim num As Integer = Flash.GetVariable("_root.player.intBaseHP")
            If num > 0 Then
                Flash.SetVariable("_root.player.intHP", num)
            Else
                Flash.SetVariable("_root.player.intHP", num)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Timer.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Flash.SetVariable("_root.monster.intHP", 0)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Flash.Movie = "http://guardian.battleon.com/Build30/" & TextBox1.Text
        Flash.GotoFrame(1)
        Flash.Play()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Flash.SetVariable(TextBox2.Text, textbox3.text)
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Flash.SetVariable("_root.player.intAccessLevel", 22) 'Not working
        Flash.SetVariable("_root.elite", 13)
        Flash.LoadMovie(Layer1.Value, Application.StartupPath & "/Data/" & ListBox1.SelectedItem)
        Flash.SetVariable("_root.elite", 13)
        Layer1.Minimum = Layer1.Value
        Layer1.Value += 1
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then RichTextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText)
    End Sub
    Dim arr As Array
    Dim lines As Array
    Sub dum(ByRef lines As RichTextBox)
        If lines.Text.Length > 0 Then
            arr = {}
            For Each c As String In lines.Lines
                Try
                arr = c.Split(" ")
                    Flash.SetVariable(arr(0), arr(1))
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If CheckBox1.Checked Then
            If CustomTimer.Enabled = True Then
                CustomTimer.Enabled = False
            Else : End If
            CustomTimer.Interval = NumericUpDown1.Value
            CustomTimer.Enabled = True
        Else

            dum(RichTextBox1)

        End If
    End Sub

    Private Sub CustomTimer_Tick(sender As Object, e As EventArgs) Handles CustomTimer.Tick
        dum(RichTextBox1)
    End Sub
    Dim X As Integer = 1
    Sub fuck()
        Do Until X > Layer1.Value
            Try
                Flash.LoadMovie(X, "null.swf") : X += 1
            Catch ex As Exception

            End Try
        Loop
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Timer.Enabled = False
        CustomTimer.Enabled = False
        Layer1.Minimum = 1
        fuck()
        X = 1
    End Sub

End Class
