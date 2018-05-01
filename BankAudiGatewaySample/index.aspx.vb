' Package: Audi Gateway Website
' Version: 1.0
' Date Created: 29-05-2013
' Date Created: 29-05-2013
' Authors: Rami Zebian <rami.zebian@progous.com>
' Copyright 2013 by Progous sarl, All Rights Reserved

Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.String
Imports System.Configuration.ConfigurationManager
Imports System.Collections
Imports System.Security.Cryptography
Imports System.Security.Policy.Hash
Imports System.IO

Partial Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("href") = "results" Then
            divPay.Visible = False
            divResult.Visible = True
            If Request.QueryString("vpc_TxnResponseCode") = "0" Then
                divResult.InnerHtml = "Thanks for purchasing from Bank Audi..."
            Else
                divResult.InnerHtml = "Purchase was not successful. Please try again.<br/><br/>"
                divResult.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                divResult.InnerHtml += "<a href='index.aspx'>Return Home</a>"
            End If
        End If
    End Sub

    Protected Sub btnPay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPay.Click
        Response.Redirect("transfer.aspx")
    End Sub
End Class
