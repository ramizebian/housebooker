' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
Imports System.Data
Imports System.Data.SqlClient

Public Class methods
    'Declare the connection and command
    Dim MyConnection As SqlConnection
    Dim MyCommand As SqlCommand

    ' fetchHome()
    ' fetches home data
    ' Access: Public
    ' Input: None
    ' Output: DataSet
    ' Date Created: 16-10-2017
    ' Last Modified: 16-10-2017
    ' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

    Public Function fetchHome() As DataSet
        'Data adapters
        '---------------------------------------
        Dim daHomeCover As SqlDataAdapter
        Dim daGallery1 As SqlDataAdapter
        Dim daGallery2 As SqlDataAdapter
        dim daEvent as SqlDataAdapter
        '---------------------------------------
        Dim dsHome = New DataSet()
        MyConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Try
            MyConnection.Open()
            MyCommand = New SqlCommand("SELECT * FROM [] WHERE id=1", MyConnection)
            daHomeCover = New SqlDataAdapter(MyCommand)

            MyCommand = New SqlCommand("SELECT * FROM [] ORDER BY id", MyConnection)
            daGallery1 = New SqlDataAdapter(MyCommand)

            MyCommand = New SqlCommand("SELECT * FROM [] ORDER BY id", MyConnection)
            daGallery2 = New SqlDataAdapter(MyCommand)

            MyCommand = New SqlCommand("SELECT * FROM [] WHERE id=1", MyConnection)
            daEvent = New SqlDataAdapter(MyCommand)

            daHomeCover.Fill(dsHome, "x")
            daGallery1.Fill(dsHome, "y")
            daGallery2.Fill(dsHome, "z")
            daEvent.Fill(dsHome, "zz")
            Return dsHome
        Catch ex As Exception
            Return Nothing
        Finally
            MyConnection.Close()
        End Try
    End Function
End Class
