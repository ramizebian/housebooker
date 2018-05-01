Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class ExceptionUtility
    ' All methods are static, so this can be private 
    Private Sub New()
        MyBase.New()
    End Sub

    ' Log an Exception 
    Public Shared Sub LogException(exc As Exception, source As String)
        ' Include enterprise logic for logging exceptions 
        ' Get the absolute path to the log file 
        'Dim logFile = "App_Data/ErrorLog.txt"
        Dim logFile = "ErrorLog.txt"
        logFile = HttpContext.Current.Server.MapPath(logFile)

        ' Open the log file for append and write the log 
        Dim sw = New StreamWriter(logFile, True)
        sw.WriteLine("**** " & DateTime.Now & " ****")
        If exc.InnerException IsNot Nothing Then
            sw.Write("Inner Exception Type: ")
            sw.WriteLine(exc.InnerException.GetType.ToString)
            sw.Write("Inner Exception: ")
            sw.WriteLine(exc.InnerException.Message)
            sw.Write("Inner Source: ")
            sw.WriteLine(exc.InnerException.Source)
            If exc.InnerException.StackTrace IsNot Nothing Then
                sw.WriteLine("Inner Stack Trace: ")
                sw.WriteLine(exc.InnerException.StackTrace)
            End If
        End If
        sw.Write("Exception Type: ")
        sw.WriteLine(exc.GetType.ToString)
        sw.WriteLine("Exception: " & exc.Message)
        sw.WriteLine("Source: " & source)
        If exc.StackTrace IsNot Nothing Then
            sw.WriteLine("Stack Trace: ")
            sw.WriteLine(exc.StackTrace)
        End If
        sw.WriteLine()
        sw.Close()
    End Sub

    ' Notify System Operators about an exception 
    Public Shared Sub NotifySystemOps(exc As Exception)
        ' Include code for notifying IT system operators 

        'create the mail message
        Dim mail As New MailMessage()

        'set the addresses
        mail.From = New MailAddress(ConfigurationManager.AppSettings("AdminSupportEmail").ToString)
        mail.To.Add(ConfigurationManager.AppSettings("AdminSupportEmail").ToString)
        mail.IsBodyHtml = True

        'set the content
        mail.Subject = "The Chill Hill | Error logged"
        mail.Body =
            "<font style='font-family: Arial' size='2' color='#000000'>Dear Support,<br/><br/>An error was logged on The Chill Hill website. For more details, please check your <a href='" &
            ConfigurationManager.AppSettings("LivePath") &
            "/ErrorLog.txt'>log</a> file.<br/><br/>System Administrator</font>"

        'send the message
        Dim smtp As New SmtpClient(ConfigurationManager.AppSettings("SMTP"))

        'to authenticate we set the username and password properites on the SmtpClient
        smtp.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("AdminSupportEmail"),
                                                 ConfigurationManager.AppSettings("AdminSupportPassword"))
        smtp.EnableSsl = True
        smtp.Port = ConfigurationManager.AppSettings("Port").ToString
        Try
            smtp.Send(mail)
        Catch e As Exception
            Throw (e)
        End Try
    End Sub
End Class
