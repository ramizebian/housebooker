' Package: Kalamon Website
' Version: 1.0
' Date Created: 25-01-2012
' Date Created: 11-02-2012
' Authors: Rami Zebian <rami.zebian@progous.com>
' Copyright 2012 by Progous sarl, All Rights Reserved

Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.String
Imports System.Configuration.ConfigurationManager
Imports System.Collections
Imports System.Security.Cryptography
Imports System.Security.Policy.Hash
Imports System.IO

Partial Class transfer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim user As New users
        Dim strRandom1 As String = user.GeneratePassword(4)
        Dim strRandom2 As String = user.GeneratePassword(4)

        merchTxnRef.Value = strRandom1
        orderInfo.Value = strRandom2

        amount.Value = 555
    End Sub
End Class
