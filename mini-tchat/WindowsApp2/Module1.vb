Imports System
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text
Imports System.Exception

Module Module1
    Private urlJson As String = Form1.TextBox4.Text + "/chat.json"
    Private urlPhpScript As String = Form1.TextBox4.Text + "/index.php"
    Public Property JsonConvert As Object
    Public Function getListFromWebJson()
        Dim sURL As String
        sURL = urlJson

        Dim wrGETURL As WebRequest
        wrGETURL = WebRequest.Create(sURL)

        Dim myProxy As New WebProxy("myproxy", 80)
        myProxy.BypassProxyOnLocal = True

        'wrGETURL.Proxy = myProxy
        wrGETURL.Proxy = WebProxy.GetDefaultProxy()

        Dim objStream As Stream
        Dim _data
        Try
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim webContent As String = ""
            Dim i As Integer = 0

            Do While Not sLine Is Nothing
                i += 1
                sLine = objReader.ReadLine
                If Not sLine Is Nothing Then
                    'Console.WriteLine("{0}:{1}", i, sLine)
                    webContent = webContent + sLine
                End If
            Loop

            Dim json As String = webContent

            Dim ser As JObject = JObject.Parse(json)
            _data = ser.Children().ToList
        Catch e As Exception
            _data = "connection_problem"
        End Try
        Return _data
    End Function

    Public Function sendData(_user, _message, _date)
        Dim s As HttpWebRequest
        Dim enc As UTF8Encoding
        Dim postdata As String
        Dim postdatabytes As Byte()
        Try
            s = HttpWebRequest.Create(urlPhpScript)
            enc = New System.Text.UTF8Encoding()
            postdata = "user=" + _user + "&message=" + _message + "&date=" + _date
            postdatabytes = enc.GetBytes(postdata)
            s.Method = "POST"
            s.ContentType = "application/x-www-form-urlencoded"
            s.ContentLength = postdatabytes.Length

            Using stream = s.GetRequestStream()
                stream.Write(postdatabytes, 0, postdatabytes.Length)
            End Using
            Dim result = s.GetResponse()
        Catch e As Exception
        End Try
    End Function

    Public Function seturl(_url)
        urlJson = _url + "/chat.json"
        urlPhpScript = _url + "/index.php"
    End Function
End Module
