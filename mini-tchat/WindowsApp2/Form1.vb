Imports System
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class Form1
    Private date_message As Date
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'On charge la conversation
        'TextBox2.ScrollBars.Vertical.v
        If Module1.getListFromWebJson IsNot "connection_problem" Then
            Dim Data As List(Of JToken) = Module1.getListFromWebJson()
            Dim user As String = ""
            Dim message As String = ""
            Label1.BackColor = Color.Green
            Label1.Text = "Connected"
            For Each item As JProperty In Data
                'Console.WriteLine(webContent)
                item.CreateReader()
                Select Case item.Name
                    Case "data"
                        TextBox2.Text = ""
                        For Each msg As JObject In item.Values
                            user = msg("user")
                            message = msg("message")
                            date_message = msg("date")
                            TextBox2.AppendText(user + " : " + message + Environment.NewLine)
                        Next
                End Select
            Next
            'TextBox2.AppendText("")
        Else
            Label1.BackColor = Color.Red
            Label1.Text = "Failed to connect"
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Start()
        Timer1.Interval = 300
        'on update les nouveaux messages
        If Module1.getListFromWebJson IsNot "connection_problem" Then
            Dim Data As List(Of JToken) = Module1.getListFromWebJson()
            Dim user As String = ""
            Dim message As String = ""
            Label1.BackColor = Color.Green
            Label1.Text = "Connected"
            For Each item As JProperty In Data
                'Console.WriteLine(webContent)
                item.CreateReader()
                Select Case item.Name
                    Case "data"
                        Dim msg As JObject = item.Value.Last
                        If date_message < msg("date") Then
                            user = msg("user")
                            message = msg("message")
                            TextBox2.AppendText(user + " : " + message + Environment.NewLine)
                            date_message = msg("date")
                        End If
                End Select
            Next
        Else
            Label1.BackColor = Color.Red
            Label1.Text = "Failed to connect"
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Module1.sendData(TextBox1.Text, TextBox3.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
        TextBox3.Text = ""
        Module1.seturl(TextBox4.Text)
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
            e.SuppressKeyPress = True
            'TextBox3.Text = Trim(TextBox3.Text)
            'TextBox3.Select(0, 0)
            'TextBox3.ScrollToCaret()
        End If
    End Sub
End Class
