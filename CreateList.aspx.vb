Imports System.Data.SqlClient

Public Class CreateList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetCreateIntialRow()

        End If
    End Sub

    Private Sub SetCreateIntialRow()
        Dim dt As DataTable = New DataTable()
        Dim dr As DataRow = Nothing
        Try
            'add new columns
            dt.Columns.Add(New DataColumn("Item", GetType(String)))
            dt.Columns.Add(New DataColumn("Quantity", GetType(String)))
            dt.Columns.Add(New DataColumn("Cost", GetType(String)))
            'add a row
            dr = dt.NewRow
            dr("Item") = String.Empty
            dr("Quantity") = String.Empty
            dr("Cost") = String.Empty
            'add row to the table
            dt.Rows.Add(dr)
            'save table to a session
            ViewState("CurrentCreateTable") = dt
            'use datatable as gridview datasource

            GridViewCreateDisplay.DataSource = dt
            GridViewCreateDisplay.DataBind()
        Catch ex As Exception
            Response.Redirect("ErrorMessage.aspx?Error=" + ex.Message)
        End Try
    End Sub

    Private Sub btnAddItem_ServerClick(sender As Object, e As EventArgs) Handles btnAddItem.ServerClick
        Dim rowIndex As Integer = 0

        Try
            If ViewState("CurrentCreateTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentCreateTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count
                        'extract the control values
                        Dim box1 As TextBox =
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(0).FindControl("item"), TextBox)
                        Dim box2 As TextBox =
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxQuantity"), TextBox)
                        Dim box3 As TextBox =
                       DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(2).FindControl("txtbxCost"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()
                        dtCurrentTable.Rows(i - 1)(0) = box1.Text
                        dtCurrentTable.Rows(i - 1)(1) = box2.Text
                        dtCurrentTable.Rows(i - 1)(2) = box3.Text
                        rowIndex += 1
                    Next
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentCreateTable") = dtCurrentTable

                    GridViewCreateDisplay.DataSource = dtCurrentTable
                    GridViewCreateDisplay.DataBind()
                End If
            Else
                Response.Redirect("ErrorMessage.aspx?Error=" + "ViewState is null")
            End If


        Catch ex As SqlException
            Response.Redirect("ErrorMessage.aspx?Error=" + ex.Message)
        End Try
        'Set Previous Data on Postbacks
        SetCreatePreviousItems()
    End Sub
    Private Sub SetCreatePreviousItems()
        'if a new row is added to the gridview then it resets the gridview with previous information
        Dim rowIndex As Integer = 0
        Try

            If ViewState("CurrentCreateTable") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(ViewState("CurrentCreateTable"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim box1 As TextBox =
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxQuantity"), TextBox)
                        Dim box2 As TextBox =
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(0).FindControl("item"), TextBox)
                        Dim box3 As TextBox =
                           DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(2).FindControl("txtbxCost"), TextBox)
                        box1.Text = dt.Rows(i)(1).ToString()
                        box2.Text = dt.Rows(i)(0).ToString()
                        box3.Text = dt.Rows(i)(2).ToString()
                        rowIndex += 1
                    Next
                End If
            End If
        Catch ex As Exception
            Response.Redirect("ErrorMessage.aspx?Error=" + ex.Message)
        End Try
    End Sub
    Private Sub GridViewCreateDisplay_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewCreateDisplay.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim rowIndex As Integer = 0

        Try
            'updates the viewstate table before deleting a row
            If ViewState("CurrentCreateTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentCreateTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 1 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count
                        'extract the control values
                        Dim box1 As TextBox =
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(0).FindControl("item"), TextBox)
                        Dim box2 As TextBox =
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxQuantity"), TextBox)
                        Dim box3 As TextBox =
                              DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(2).FindControl("txtbxCost"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()
                        dtCurrentTable.Rows(i - 1)(0) = box1.Text
                        dtCurrentTable.Rows(i - 1)(1) = box2.Text
                        dtCurrentTable.Rows(i - 1)(2) = box3.Text
                        rowIndex += 1
                    Next
                    'deletes row from datatable then set datatable as the gridview datasource
                    dtCurrentTable.Rows(index).Delete()
                    ViewState("CurrentCreateTable") = dtCurrentTable

                    GridViewCreateDisplay.DataSource = dtCurrentTable
                    GridViewCreateDisplay.DataBind()
                    'Set Previous Data on Postbacks
                    SetCreatePreviousItems()
                End If
            Else
                Response.Redirect("ErrorMessage.aspx?Error=" + "ViewState is null")
            End If

        Catch ex As Exception
            Response.Redirect("ErrorMessage.aspx?Error=" + ex.Message)
        End Try
    End Sub

    Private Sub btnSaveList_ServerClick(sender As Object, e As EventArgs) Handles btnSaveList.ServerClick
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Dim intListID As Integer = 0
        Using conn As New SqlConnection(connStr)
            Dim cmd As SqlCommand = New SqlCommand("insertList", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@listname", SqlDbType.VarChar).Value = ListName.Value
            cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = "user2"
            cmd.Parameters.Add("@listid", SqlDbType.Int).Direction = ParameterDirection.Output

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                intListID = cmd.Parameters("@listid").Value
            Catch ex As Exception
                Response.Redirect("ErrorMessage.aspx?Error=" + HttpUtility.UrlEncode(ex.Message))
            Finally

                insertItems(GridViewCreateDisplay, intListID, conn)

            End Try

        End Using
        Response.Redirect("ViewList.aspx?listid=" & intListID)
    End Sub

    Private Sub insertItems(ByVal grdView As GridView, ByVal intListId As Integer, ByVal conn As SqlConnection)
        Dim strItem As String = String.Empty
        Dim strQuantity As String = String.Empty
        Dim strCost As String = String.Empty
        For i As Integer = 0 To grdView.Rows.Count - 1
            strItem = DirectCast(grdView.Rows(i).Cells(0).FindControl("Item"), TextBox).Text
           
            If Not strItem = String.Empty Then
                strQuantity = DirectCast(grdView.Rows(i).Cells(1).FindControl("txtbxQuantity"), TextBox).Text
                strCost = DirectCast(grdView.Rows(i).Cells(2).FindControl("txtbxCost"), TextBox).Text

                Dim cmd As SqlCommand = New SqlCommand("insertGroceryItems", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@item", SqlDbType.VarChar).Value = strItem
                cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = strQuantity
                cmd.Parameters.Add("@cost", SqlDbType.VarChar).Value = strCost
                cmd.Parameters.Add("@listid", SqlDbType.Int).Value = intListId
                cmd.Parameters.Add("@stepnumber", SqlDbType.Int).Value = i + 1

                cmd.ExecuteNonQuery()
            End If
         

        Next
    End Sub
End Class