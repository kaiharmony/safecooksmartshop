Public Class SearchRecipe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub GridViewItems_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewItems.RowCommand
        Dim recipeid As String = String.Empty
        If e.CommandName = "recipename" Then
            Try
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim gvRow As GridViewRow = GridViewItems.Rows(index)
                recipeid = HttpUtility.HtmlDecode(gvRow.Cells(2).Text.ToString)

            Catch ex As Exception

            End Try
        End If

        Response.Redirect("UpdateRecipe.aspx?recipeid=" & recipeid)
    End Sub
End Class