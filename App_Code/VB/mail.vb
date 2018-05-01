' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
Imports System.Net
Imports System.Net.Mail

Public Class mail
    ' CMS::SendLogin()
    ' Sends the user new login information in an email
    ' Access: Public
    ' Input: strEmail, strPassword
    ' Output: Boolean
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function SendLogin(strEmail As String, strPassword As String) As Boolean
        'create the mail message
        Dim mail As New MailMessage()

        'set the addresses
        mail.From = New MailAddress(ConfigurationManager.AppSettings("FromEmail").ToString)
        mail.To.Add(strEmail)
        mail.IsBodyHtml = True

        'set the content
        mail.Subject = "Your login information"
        mail.Body = "<font style='font-family: Arial' size='2' color='#000000'>Your username is: " & strEmail &
                    ", and your new password is: " & strPassword &
                    "<br/><br/>You can change your password later in the account page<br><br>System Administrator</font>"

        'send the message
        Dim smtp As New SmtpClient(ConfigurationManager.AppSettings("SMTP"))

        'to authenticate we set the username and password properites on the SmtpClient
        smtp.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("AdminFromEmail"),
                                                 ConfigurationManager.AppSettings("AdminFromPassword"))
        smtp.EnableSsl = True
        smtp.Port = ConfigurationManager.AppSettings("Port")
        Try
            smtp.Send(mail)
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function    
End Class
