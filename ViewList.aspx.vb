Imports System.Data.SqlClient

Public Class ViewList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strListId As String = String.Empty
            strListId = Request.QueryString("listid")
            If Not strListId = String.Empty Then
    
                SetListName(strListId)
            Else
                Response.Redirect("default.aspx")
            End If

        End If
    End Sub
    Private Sub SetListName(ByVal intListID As Integer)
        Dim strsql As String = "select listname from grocerylist where listid = " & intListID
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Using conn As New SqlConnection(connStr)
            Dim cmd As SqlCommand = New SqlCommand(strsql, conn)
            cmd.CommandType = CommandType.Text

            conn.Open()
            lblName.Text = cmd.ExecuteScalar
        End Using

    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Response.Redirect("UpdateList.aspx?listid=" & Request.QueryString("listid"))
    End Sub
  
End Class