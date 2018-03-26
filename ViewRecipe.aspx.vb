Imports System.Data.SqlClient

Public Class ViewRecipe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strRecipeid As String = String.Empty
        strRecipeid = Request.QueryString("recipeid")
        If Not strRecipeid = String.Empty Then
            SetRecipeName(strRecipeid)
        Else
            Response.Redirect("default.aspx")
        End If
    End Sub
    Private Sub SetRecipeName(ByVal recipeid As Integer)
        Dim strsql As String = "select * from recipe where recipeid = " & recipeid
        Dim pubset As New DataSet
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Using conn As New SqlConnection(connStr)
            Dim cmd As SqlCommand = New SqlCommand(strsql, conn)
            cmd.CommandType = CommandType.Text

            conn.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(pubset)
        End Using
        Dim intRow As Integer = 0
        If pubset.Tables(0).Rows.Count > 0 Then
            For Each myRow In pubset.Tables(0).Rows
                lblName.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("recipename"))
                servings.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("serving"))
                ethnicity.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("ethnicity"))
                intRow += 1
            Next
        End If

    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Response.Redirect("UpdateRecipe.aspx?recipeid=" & Request.QueryString("recipeid"))
    End Sub
End Class