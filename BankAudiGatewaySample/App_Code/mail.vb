' Package: ZB CMS
' Version: 1.0
' Date Created: 19-10-2011
' last Modified: 15-04-2011
' Authors: Rami Zebian <rami.zebian@progous.com>
' Copyright 2012 by Progous sarl, All Rights Reserved

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports System.Net.Mail
Imports System.Net

Public Class mail

    ' user::SendLogin()
    ' Sends the user new login information in an email
    ' Access: Public
    ' Input: strEmail, strPassword
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>

    Public Function SendLogin(ByVal strEmail As String, ByVal strPassword As String) As Boolean
        'create the mail message
        Dim mail As New MailMessage()

        'set the addresses
        mail.From = New MailAddress(AppSettings("FromEmail").ToString)
        mail.To.Add(strEmail)
        mail.IsBodyHtml = True

        'set the content
        mail.Subject = "Your login information"
        mail.Body = "<font style='font-family: Arial' size='2' color='#000000'>Your username is: " & strEmail & ", and your new password is: " & strPassword & "<br/><br/>You can change your password later in the account page<br><br>System Administrator</font>"

        'send the message
        Dim smtp As New SmtpClient(AppSettings("SMTP"))

        'to authenticate we set the username and password properites on the SmtpClient
        smtp.Credentials = New NetworkCredential(AppSettings("AdminFromEmail"), AppSettings("AdminFromPassword"))
        Try
            smtp.Send(mail)
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function
End Class
