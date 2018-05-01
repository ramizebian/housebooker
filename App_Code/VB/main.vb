' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json

Public Class main
    'Declare the Connection and Command
    Dim myconnection As SqlConnection
    Dim mycommand As SqlCommand


    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function verifyRecaptcha() As Boolean
        'Code for g-recaptcha
        '-----------------------------------------------------------------------------------------------------------------
        'Getting Response String Append to Post Method
        Dim strResponse As String = HttpContext.Current.Request("g-recaptcha-response")
        Dim valid = False

        'Send WebRequest
        'Dim myRequest As WebRequest = WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe&response=" & strResponse) 'TEST KEY
        Dim myRequest As WebRequest =
                WebRequest.Create(
                    "https://www.google.com/recaptcha/api/siteverify?secret=6LeWElMUAAAAAEqXpnQgAsajwT2tT-GpbNKkiwh0&response=" &
                    strResponse)
        myRequest.Credentials = CredentialCache.DefaultCredentials

        'Get Response
        Dim myResponse As WebResponse = myRequest.GetResponse()
        Dim dataStream As Stream = myResponse.GetResponseStream()
        Dim myReader As New StreamReader(dataStream)
        Dim responseFromServer As String = myReader.ReadToEnd

        'Deserialize JSON
        Dim x = JsonConvert.DeserializeObject (Of Object)(responseFromServer)
        Dim verify As Boolean = x("success").ToString

        'For Testing
        'verify = True

        myReader.Close()
        myResponse.Close()

        Return verify
        '-----------------------------------------------------------------------------------------------------------------
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function FixOrder(strTableName As String, intID As Integer, intTypeID As Integer) As Boolean
        Dim _intOrder As Integer
        Dim _intOtherID As Integer
        Dim _intOtherOrder As Integer
        myconnection = New SqlConnection
        myconnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            myconnection.Open()
            mycommand = New SqlCommand("SELECT displayorder FROM " & strTableName & " WHERE id = @id", myconnection)
            mycommand.Parameters.Add("@id", SqlDbType.Int).Value = intID
            _intOrder = mycommand.ExecuteScalar
            If intTypeID = 1 Then 'Move UP
                mycommand =
                    New SqlCommand(
                        "SELECT TOP 1 id FROM " & strTableName & " WHERE displayorder < " & _intOrder &
                        " ORDER BY displayorder DESC", myconnection)
                _intOtherID = mycommand.ExecuteScalar
                If _intOtherID > 0 Then

                    mycommand = New SqlCommand("SELECT displayorder FROM " & strTableName & " WHERE id = @id",
                                               myconnection)
                    mycommand.Parameters.Add("@id", SqlDbType.Int).Value = _intOtherID
                    _intOtherOrder = mycommand.ExecuteScalar

                    mycommand =
                        New SqlCommand("UPDATE " & strTableName & " SET displayorder = @newDisplayOrder WHERE id = @id",
                                       myconnection)
                    mycommand.Parameters.Add("@newDisplayOrder", SqlDbType.Int).Value = _intOtherOrder
                    mycommand.Parameters.Add("@id", SqlDbType.Int).Value = intID
                    mycommand.ExecuteNonQuery()

                    mycommand =
                        New SqlCommand("UPDATE " & strTableName & " SET displayorder = @oldDisplayOrder WHERE id = @id",
                                       myconnection)
                    mycommand.Parameters.Add("@id", SqlDbType.Int).Value = _intOtherID
                    mycommand.Parameters.Add("@oldDisplayOrder", SqlDbType.Int).Value = _intOrder
                    mycommand.ExecuteNonQuery()
                End If
            ElseIf intTypeID = 2 Then 'Move Down
                mycommand =
                    New SqlCommand(
                        "SELECT TOP 1 id FROM " & strTableName & " WHERE displayorder > " & _intOrder &
                        " ORDER BY displayorder ASC", myconnection)
                _intOtherID = mycommand.ExecuteScalar
                If _intOtherID > 0 Then

                    mycommand = New SqlCommand("SELECT displayorder FROM " & strTableName & " WHERE id = @id",
                                               myconnection)
                    mycommand.Parameters.Add("@id", SqlDbType.Int).Value = _intOtherID
                    _intOtherOrder = mycommand.ExecuteScalar

                    mycommand =
                        New SqlCommand("UPDATE " & strTableName & " SET displayorder = @newDisplayOrder WHERE id = @id",
                                       myconnection)
                    mycommand.Parameters.Add("@id", SqlDbType.Int).Value = intID
                    mycommand.Parameters.Add("@newDisplayOrder", SqlDbType.Int).Value = _intOtherOrder
                    mycommand.ExecuteNonQuery()

                    mycommand =
                        New SqlCommand("UPDATE " & strTableName & " SET displayorder = @oldDisplayOrder  WHERE id = @id",
                                       myconnection)
                    mycommand.Parameters.Add("@id", SqlDbType.Int).Value = _intOtherID
                    mycommand.Parameters.Add("@oldDisplayOrder", SqlDbType.Int).Value = _intOrder
                    mycommand.ExecuteNonQuery()
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            myconnection.Close()
        End Try
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function getRealIpFromCf() As String
        Try
            Dim context As HttpContext = HttpContext.Current
            Dim ipAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If (Not String.IsNullOrEmpty((ipAddress))) Then
                Dim addresses() As String = ipAddress.Split(",")
                If (addresses.Length <> 0) Then
                    Return addresses(0)
                Else
                    Return "0.0.0.0"
                End If
            Else
                Return context.Request.ServerVariables("HTTP_CF_CONNECTING_IP")
            End If
        Catch ex As Exception
            Return "0.0.0.0"
        End Try
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function CreateDateTreeDir(strFolderName As String) As String
        Dim _dir = ConfigurationManager.AppSettings("BasePath") & "\files\" & strFolderName & "\" & Year(Now()) & "\" &
                   Month(Now()) & "\" & Day(Now())
        If (Not Directory.Exists(_dir)) Then
            Directory.CreateDirectory(_dir)
        End If
        Return _dir
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function SaveImageToDisk(strImageSource As String, strFolderName As String) As String
        Dim users As New users
        Dim _strRandom As String = users.GeneratePassword(6)
        Try
            Dim base64 = strImageSource.Split(",")(1)
            Dim strImageName As String, strImagePath As String
            strImageName = Year(Now()) & Month(Now()) & Day(Now()) & Hour(TimeOfDay) & Minute(TimeOfDay) &
                           Second(TimeOfDay) & "-" & _strRandom & ".png"
            strImagePath = CreateDateTreeDir(strFolderName) & "\"
            File.WriteAllBytes(strImagePath & strImageName, Convert.FromBase64String(base64))
            Return strImageName
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    ' Modified: Sinan Noureddine <sinan.noureddine@lelabodigital.com>
    Public Function UpdateImageToDisk(strImageSource As String, strFolderName As String) As String
        Dim users As New users
        Dim _strRandom As String = users.GeneratePassword(6)
        Try
            Dim base64 = strImageSource.Split(",")(1)
            Dim strImageName As String, strImagePath As String
            strImageName = Year(Now()) & Month(Now()) & Day(Now()) & Hour(TimeOfDay) & Minute(TimeOfDay) &
                           Second(TimeOfDay) & "-" & _strRandom & ".png"
            strImagePath = ConfigurationManager.AppSettings("BasePath") & "\files\" & strFolderName & "\"
            File.WriteAllBytes(strImagePath & strImageName, Convert.FromBase64String(base64))
            Return strImageName
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function fetchCreationDate(intID As Integer, strTableName As String) As String

        myconnection = New SqlConnection
        myconnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Dim _creationDate = ""
        Try
            myconnection.Open()
            mycommand = New SqlCommand("SELECT creation_date FROM " & strTableName & " WHERE id = @intID", myconnection)
            mycommand.Parameters.Add("@intID", SqlDbType.Int, 50).Value = intID
            _creationDate = mycommand.ExecuteScalar()
            Return _creationDate
        Catch ex As Exception
            Return ""
        Finally
            myconnection.Close()
        End Try
    End Function

    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
    Public Function GetImagePath(intID As Integer, strTableName As String) As String
        myconnection = New SqlConnection
        myconnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Dim _strPath = ""
        Try
            myconnection.Open()
            mycommand =
                New SqlCommand(
                    "SELECT (cast(year(creation_date) as nvarchar)  + '/' + cast(month(creation_date) as nvarchar) + '/' + cast(day(creation_date) as nvarchar) + '/') AS path FROM " &
                    strTableName & " WHERE id = @id", myconnection)
            mycommand.Parameters.Add("@id", SqlDbType.Int).Value = intID
            _strPath = mycommand.ExecuteScalar()
            Return _strPath
        Catch ex As Exception
            Return ""
        Finally
            myconnection.Close()
        End Try
    End Function
End Class

