' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>

Imports System.Configuration.ConfigurationManager

Partial Class MasterPage
    Inherits UI.MasterPage

    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As EventArgs)

        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                    .HttpOnly = True,
                    .Value = _antiXsrfTokenValue
                    }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Validate the Anti-XSRF token
            If _
                DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse
                DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) _
                Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Private Sub MasterPage_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim PageName As String
        PageName = Request.AppRelativeCurrentExecutionFilePath
        PageName = PageName.Substring(2)
        PageName = PageName.Replace(".aspx", "")

        Dim strTitle As String = ""
        Dim strKeywords As String = ""
        Dim strDescription As String = ""

        strTitle = "Welcome to Housebooker"
        strKeywords = "#"
        strDescription = "#"
        'Facebook Open Graph
        Head1.Controls.Add(New LiteralControl("<meta property='og:site_name' content='" & strTitle & "' />"))
        Head1.Controls.Add(New LiteralControl("<meta property='og:title' content='" & strTitle & "' />"))
        Head1.Controls.Add(New LiteralControl("<meta property='og:description' content='" & strDescription & "' />"))
        Head1.Controls.Add(New LiteralControl("<meta property='og:image' content='" & AppSettings("LivePath") & "/images/logo-og.jpg' />"))
        'Twitter Cards Open Graph
        Head1.Controls.Add(New LiteralControl("<meta property='twitter:card' content='summary' />"))
        Head1.Controls.Add(New LiteralControl("<meta property='twitter:title' content='" & strTitle & "' />"))
        Head1.Controls.Add(New LiteralControl("<meta property='twitter:description' content='" & strDescription & "' />"))
        Head1.Controls.Add(New LiteralControl("<meta property='twitter:image' content='" & AppSettings("LivePath") & "/images/logo-og.jpg' />"))

        Dim metaKeywords As New Web.UI.HtmlControls.HtmlMeta
        Dim metaDescription As New HtmlMeta

        metaKeywords.Name = "keywords"
        metaDescription.Name = "description"
        metaKeywords.Content = strKeywords
        metaDescription.Content = strDescription
        
        'Adding the meta data
        Page.Title = strTitle
        Head1.Controls.Add(metaKeywords)
        Head1.Controls.Add(metaDescription)

        'Add Base Path and Canonical URL
        Dim strBasePath = "<base href='" & AppSettings("LivePath") & "' />"
        Head1.Controls.Add(New LiteralControl(strBasePath))
    End Sub

    Private Sub login_ServerClick(sender As Object, e As EventArgs) Handles login.ServerClick
        session("login")=true
        Response.Redirect("dashboard")
    End Sub

    Private Sub logout_ServerClick(sender As Object, e As EventArgs) Handles logout.ServerClick
        Session("login")=false
        Response.Redirect("index")
    End Sub
End Class

