
Partial Class controls_authentication
    Inherits System.Web.UI.UserControl

    Private Sub controls_authentication_Load(sender As Object, e As EventArgs) Handles Me.Load
        if session("login")<>true
            Response.Redirect("index")
        End If
    End Sub
End Class
