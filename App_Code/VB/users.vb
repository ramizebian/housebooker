' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class users
    'Declare the connection and command
    Dim MyConnection As SqlConnection
    Dim MyCommand As SqlCommand

    ' user::CheckLogin()
    ' Checks and Validates login information
    ' Access: Public
    ' Input: strUserEmail, strUserPassword
    ' Output: Boolean
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function CheckLogin(strUserEmail As String, strUserPassword As String) As Boolean
        Dim _strPassword As String
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT password FROM users WHERE email = @email", MyConnection)
            MyCommand.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = strUserEmail
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function UpdateUserLastLogin(intUserID As Integer, strDate As String) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET lastlogin = @date WHERE id= @id", MyConnection)
            MyCommand.Parameters.Add("@id", SqlDbType.Int, 50).Value = intUserID
            MyCommand.Parameters.Add("@date", SqlDbType.VarChar, 50).Value = strDate
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function ResetUserAccount(strUserEmail As String) As Boolean
        Dim _intID As Integer
        Dim _strPassword As String
        Dim _strHashedPassword As String
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT id FROM users WHERE email = @email", MyConnection)
            MyCommand.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = strUserEmail
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function UpdatePassword(intUserID As Integer, strPassword As String) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET password = @password WHERE id = @id", MyConnection)
            MyCommand.Parameters.Add("@id", SqlDbType.Int, 50).Value = intUserID
            MyCommand.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = strPassword
            MyCommand.ExecuteScalar()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' user::CreateRandomSalt()
    ' Creates a random salt
    ' Access: Public
    ' Input: None
    ' Output: salt
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    'Public Function CreateRandomSalt() As String
    '    'the following is the string that will hold the salt charachters
    '    Dim mix As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=][}{<>"
    '    Dim salt As String = ""
    '    Dim rnd As New Random
    '    Dim sb As New StringBuilder
    '    For i As Integer = 1 To 100 'Length of the salt
    '        Dim x As Integer = rnd.Next(0, mix.Length - 1)
    '        salt &= (mix.Substring(x, 1))
    '    Next
    '    Return salt
    'End Function

    ' user::EncryptPassword()
    ' Encrypts a password
    ' Access: Public
    ' Input: strPassword
    ' Output: strEncryptedPassword
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Function EncryptPassword(strPassword As String) As String
        Dim _strEncryptedPassword = ""
        Dim _strSalt = "asdsUhslSKJ!*asja!@haU93ssa#0n@9n@=A$2"
        Dim _strSaltedPassword = strPassword & _strSalt
        Dim convertedToBytes As Byte() = Encoding.UTF8.GetBytes(_strSaltedPassword)
        Dim hashType As HashAlgorithm = New SHA512Managed()
        Dim hashBytes As Byte() = hashType.ComputeHash(convertedToBytes)
        _strEncryptedPassword = Convert.ToBase64String(hashBytes)
        Return _strEncryptedPassword
    End Function

    ' user::GeneratePassword()
    ' Generates a random password
    ' Access: Public
    ' Input: intLength
    ' Output: New String(chPassword)
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Function GeneratePassword(intLength As Integer) As String
        'Make sure length is valid....
        If ((intLength < 1) OrElse (intLength > 128)) Then
            Throw New ArgumentException("Membership_password_length_incorrect")
        End If

        Do While True
            Dim i As Integer
            Dim nonANcount = 0
            Dim buffer1 = New Byte(intLength - 1) {}

            'chPassword contains the password's characters as it's built up
            Dim chPassword = New Char(intLength - 1) {}

            'Get a cryptographically strong series of bytes
            Dim rng As New RNGCryptoServiceProvider
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function FecthActiveUsers() As DataSet
        Dim UsersDataAdapter As SqlDataAdapter
        Dim dsUsers = New DataSet
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            UsersDataAdapter =
                New SqlDataAdapter(
                    "SELECT users.id, users.firstname, users.lastname, users.email FROM users WHERE users.active = 1",
                    MyConnection)
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function FecthInActiveUsers() As DataSet
        Dim UsersDataAdapter As SqlDataAdapter
        Dim dsUsers = New DataSet
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            UsersDataAdapter =
                New SqlDataAdapter(
                    "SELECT users.id, users.firstname, users.lastname, users.email FROM users WHERE users.active = 0",
                    MyConnection)
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function DeActivateUser(intUserID As Integer) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET active = 0 WHERE id = @id", MyConnection)
            MyCommand.Parameters.Add("@id", SqlDbType.Int, 50).Value = intUserID
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function ActivateUser(intUserID As Integer) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("UPDATE users SET active = 1 WHERE id = @id", MyConnection)
            MyCommand.Parameters.Add("@id", SqlDbType.Int, 50).Value = intUserID
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function FecthUserDetails(strUserEmail As String) As SqlDataReader
        Dim drUserDetails As SqlDataReader
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT * FROM users WHERE email = @email", MyConnection)
            MyCommand.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = strUserEmail
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function InsertUser(strFirstName As String, strLastname As String, strEmail As String, strUsername As String,
                               strPassword As String, intRole As Integer) As Boolean
        Dim _strHashedPassword As String
        'Hach the password
        _strHashedPassword = EncryptPassword(strPassword)
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand =
                New SqlCommand(
                    "INSERT INTO users VALUES (@firstname, @lastname, @email, @username, @password, @role, '" &
                    [String].Format(Now(), "dd/MM/yyyy-HH:mm:ss") & "', '" &
                    [String].Format(Now(), "dd/MM/yyyy-HH:mm:ss") & "', 1)", MyConnection)
            MyCommand.Parameters.Add("@firstname", SqlDbType.VarChar, 50).Value = strFirstName
            MyCommand.Parameters.Add("@lastname", SqlDbType.VarChar, 50).Value = strLastname
            MyCommand.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = strEmail
            MyCommand.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = strUsername
            MyCommand.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = _strHashedPassword
            MyCommand.Parameters.Add("@role", SqlDbType.Int, 50).Value = intRole
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
    ' Date Created: 06-12-2017
    ' Last Modified: 06-12-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function UpdateUser(intUserID As Integer, strFirstName As String, strLastName As String, strEmail As String) _
        As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand =
                New SqlCommand(
                    "UPDATE users SET firstname = @firstname, lastname = @lastname, email = @email WHERE id = @id",
                    MyConnection)
            MyCommand.Parameters.Add("@id", SqlDbType.Int, 50).Value = intUserID
            MyCommand.Parameters.Add("@firstname", SqlDbType.VarChar, 50).Value = strFirstName
            MyCommand.Parameters.Add("@lastname", SqlDbType.VarChar, 50).Value = strLastName
            MyCommand.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = strEmail
            MyCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            MyConnection.Close()
        End Try
    End Function

    ' Last Modified: 19-02-2018
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function InsertFalseLogin(strEmail As String, strIPAddress As String) As Boolean
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        if isnothing(strIpAddress)
            strIPAddress = "127.0.0.1"
        End If
        Try
            MyConnection.Open()
            MyCommand =
                New SqlCommand(
                    "INSERT INTO users_false_logins VALUES (@email, @ip, '" &
                    [String].Format(Now(), "dd/MM/yyyy-HH:mm:ss") & "')", MyConnection)
            MyCommand.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = strEmail
            MyCommand.Parameters.Add("@ip", SqlDbType.VarChar, 50).Value = strIPAddress
            MyCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
