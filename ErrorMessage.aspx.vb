Public Class ErrorMessage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblErrorMessage.Text = HttpUtility.UrlDecode(Request.QueryString("Error"))
    End Sub

End Class