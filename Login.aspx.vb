Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click
        Dim connectionString As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Dim strsql As String = "select loginid from logincredentials where loginname = '" + txtbxLoginName.Value + "' and unhashpassword = '" + txtbxPassword.Value + "'"
        Dim strValue As String = String.Empty
        Using conn As New SqlConnection(connectionString)
            Dim cmd As SqlCommand = New SqlCommand(strsql, conn)
            cmd.CommandType = CommandType.Text
            Try
                conn.Open()
                strValue = cmd.ExecuteScalar
            Catch ex As SqlException
                Response.Redirect("ErrorMessage.aspx?Error=" + HttpUtility.UrlEncode(ex.Message))
            Finally

            End Try

        End Using
        If Not strValue = String.Empty Then
            Response.Redirect("default.aspx")
        Else
            lblError.Text = "Invalid username/password."
        End If
    End Sub
End Class