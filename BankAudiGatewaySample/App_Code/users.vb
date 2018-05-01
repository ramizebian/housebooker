' Package: ZB CMS
' Version: 1.0
' Date Created: 19-10-2011
' Last Modified: 19-10-2011
' Authors Rami Zebian <rami.zebian@progous.com>
' Copyright 2011 by Progous sarl, All Rights Reserved

Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.String
Imports System.Configuration.ConfigurationManager
Imports System.Collections
Imports System.Security.Cryptography
Imports System.Security.Policy.Hash
Imports System.IO

Public Class users

    'Declare the connection and command
    Dim MyConnection As SqlConnection
    Dim MyCommand As SqlCommand

    ' user::CheckLogin()
    ' Checks and Validates login information
    ' Access: Public
    ' Input: strUserEmail, strUserPassword
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function CheckLogin(ByVal strUserEmail As String, ByVal strUserPassword As String) As Boolean
        Dim _strPassword As String
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT password FROM users WHERE email = '" & strUserEmail & "'", MyConnection)
            _strPassword = MyCommand.ExecuteScalar
            If _strPassword = strUserPassword Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::UpdateUserLastLogin()
    ' Update user last login date
    ' Access: Public
    ' Input: inUserID, strDate
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function UpdateUserLastLogin(ByVal intUserID As Integer, ByVal strDate As String) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET lastlogin = '" & strDate & "' WHERE id = " & intUserID, MyConnection)
            MyCommand.ExecuteScalar()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::ResetUserAccount()
    ' Resets the user password, and email the new login information
    ' Access: Public
    ' Input: strUserEmail
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function ResetUserAccount(ByVal strUserEmail As String) As Boolean
        Dim _intID As Integer
        Dim _strPassword As String
        Dim _strHashedPassword As String
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT id FROM users WHERE email = '" & strUserEmail & "'", MyConnection)
            _intID = MyCommand.ExecuteScalar
            Dim Mail As New mail
            If _intID <> Nothing Then
                'Generate a new password
                _strPassword = GeneratePassword(6)
                _strHashedPassword = EncryptPassword(_strPassword)
                'Update the password in the database
                UpdatePassword(_intID, _strHashedPassword)
                'Send the new login info to the user's email
                If (Mail.SendLogin(strUserEmail, _strPassword)) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::UpdatePassword()
    ' Stores the new password in the database
    ' Access: Public
    ' Input: intUserID, strPassword
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function UpdatePassword(ByVal intUserID As Integer, ByVal strPassword As String) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET password = '" & strPassword & "' WHERE id = " & intUserID, MyConnection)
            MyCommand.ExecuteScalar()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::EncryptPassword()
    ' Encrypts a password
    ' Access: Public
    ' Input: strPassword
    ' Output: strEncryptedPassword
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Function EncryptPassword(ByVal strPassword As String) As String
        Dim _strEncryptedPassword As String
        Dim bytHash As Byte()
        Dim uEncode As New UnicodeEncoding()
        Dim bytPassword() As Byte = uEncode.GetBytes(strPassword)
        Dim Cryptex As New SHA1CryptoServiceProvider
        bytHash = Cryptex.ComputeHash(bytPassword)
        _strEncryptedPassword = Convert.ToBase64String(bytHash)
        Return _strEncryptedPassword
    End Function

    ' user::GeneratePassword()
    ' Generates a random password
    ' Access: Public
    ' Input: intLength
    ' Output: New String(chPassword)
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Function GeneratePassword(ByVal intLength As Integer) As String
        'Make sure length is valid....
        If ((intLength < 1) OrElse (intLength > 128)) Then
            Throw New ArgumentException("Membership_password_length_incorrect")
        End If

        Do While True
            Dim i As Integer
            Dim nonANcount As Integer = 0
            Dim buffer1 As Byte() = New Byte(intLength - 1) {}

            'chPassword contains the password's characters as it's built up
            Dim chPassword As Char() = New Char(intLength - 1) {}

            'Get a cryptographically strong series of bytes
            Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider
            rng.GetBytes(buffer1)

            For i = 0 To intLength - 1
                'Convert each byte into its representative character
                Dim rndChr As Integer = (buffer1(i) Mod 87)
                If (rndChr < 10) Then
                    chPassword(i) = Convert.ToChar(Convert.ToUInt16(48 + rndChr))
                Else
                    If (rndChr < 36) Then
                        chPassword(i) = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10))
                    Else
                        If (rndChr < 62) Then
                            chPassword(i) = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36))
                        Else
                            chPassword(i) = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 62))
                        End If
                    End If
                End If
            Next
            Return New String(chPassword)
        Loop
    End Function

    ' user::FecthActiveUsers()
    ' Fetch active users
    ' Access: Public
    ' Input: Nothing
    ' Output: dtUsers
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function FecthActiveUsers() As DataSet
        Dim UsersDataAdapter As SqlDataAdapter
        Dim dsUsers As DataSet = New DataSet
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            UsersDataAdapter = New SqlDataAdapter("SELECT users.id, users.firstname, users.lastname, users.email FROM users WHERE users.active = 1", MyConnection)
            UsersDataAdapter.Fill(dsUsers, "users")
            Return dsUsers
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' user::FecthInActiveUsers()
    ' Fetch the inactive users
    ' Access: Public
    ' Input: Nothing
    ' Output: dtUsers
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function FecthInActiveUsers() As DataSet
        Dim UsersDataAdapter As SqlDataAdapter
        Dim dsUsers As DataSet = New DataSet
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            UsersDataAdapter = New SqlDataAdapter("SELECT users.id, users.firstname, users.lastname, users.email FROM users WHERE users.active = 0", MyConnection)
            UsersDataAdapter.Fill(dsUsers, "users")
            Return dsUsers
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ' user::DeActivateUser()
    ' Deactivate a user in the database
    ' Access: Public
    ' Input: intUserID 
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function DeActivateUser(ByVal intUserID As Integer) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET active = 0 WHERE id = " & intUserID, MyConnection)
            MyCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::ActivateUser()
    ' Deactivate a user in the database
    ' Access: Public
    ' Input: intUserID 
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function ActivateUser(ByVal intUserID As Integer) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET active = 1 WHERE id = " & intUserID, MyConnection)
            MyCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::FecthUserDetails()
    ' Fetch user details
    ' Access: Public
    ' Input: strUserEmail
    ' Output: drUserDetails
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function FecthUserDetails(ByVal strUserEmail As String) As SqlDataReader
        Dim drUserDetails As SqlDataReader
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT * FROM users WHERE email = '" & strUserEmail & "'", MyConnection)
            drUserDetails = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return drUserDetails
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' user::InsertUser()
    ' Fetch active users
    ' Access: Public
    ' Input: strFirstName, strLastname, strEmail, strPassword, intRole, strCreationDate, strlastLogin
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function InsertUser(ByVal strFirstName As String, ByVal strLastname As String, ByVal strEmail As String, ByVal strUsername As String, ByVal strPassword As String, ByVal intRole As Integer) As Boolean
        Dim _strHashedPassword As String
        'Hach the password
        _strHashedPassword = EncryptPassword(strPassword)
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("INSERT INTO users VALUES ('" & strFirstName & "', '" & strLastname & "', '" & strEmail & "', '" & strUsername & "', '" & _strHashedPassword & "', " & intRole & ", '" & Format(Now(), "dd/MM/yyyy-HH:mm:ss") & "', '" & Format(Now(), "dd/MM/yyyy-HH:mm:ss") & "', 1)", MyConnection)
            MyCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' user::UpdateUser()
    ' Update a user in the database
    ' Access: Public
    ' Input: intUserID, strFirstName, strLastName, strEmail 
    ' Output: Boolean
    ' Date Created: 23-11-2009
    ' Last Modified: 23-11-2009
    ' Authors: Osama Shamseddine <osama@progous.com>
    Public Function UpdateUser(ByVal intUserID As Integer, ByVal strFirstName As String, ByVal strLastName As String, ByVal strEmail As String) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET firstname = '" & strFirstName & "', lastname = '" & strLastName & "', email = '" & strEmail & "' WHERE id = " & intUserID, MyConnection)
            MyCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function
End Class
