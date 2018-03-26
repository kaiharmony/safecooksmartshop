Imports System.Data.SqlClient

Public Class UpdateList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strListId As String = String.Empty
            strListId = Request.QueryString("listid")
            If Not strListId = String.Empty Then
                SetCreateIntialRow()
                SetInformation(strListId)
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
            ListName.Value = cmd.ExecuteScalar
        End Using

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
    Private Sub SetInformation(ByVal listid As Integer)
        Dim pubset As New DataSet
        pubset = GetInformation(listid)
        Dim intRow As Integer = 0
        Dim rowIndex As Integer = 0
        If pubset.Tables(0).Rows.Count > 0 Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentCreateTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable IsNot Nothing Then
                If dtCurrentTable.Rows.Count > 0 Then
                    For Each myRow In pubset.Tables(0).Rows
                        Dim intRowCount As Integer = dtCurrentTable.Rows.Count
                        'extract the TextBox values
                        Dim box1 As TextBox = DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(0).FindControl("Item"), TextBox)
                        box1.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("item"))

                        Dim box2 As TextBox = DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxQuantity"), TextBox)
                        box2.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("quantity"))

                        Dim box3 As TextBox = DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(2).FindControl("txtbxCost"), TextBox)
                        box3.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("cost"))

                        drCurrentRow = dtCurrentTable.NewRow()
                        dtCurrentTable.Rows(intRowCount - 1)(0) = box1.Text
                        dtCurrentTable.Rows(intRowCount - 1)(1) = box2.Text
                        dtCurrentTable.Rows(intRowCount - 1)(2) = box3.Text


                        dtCurrentTable.Rows.Add(drCurrentRow)
                        ViewState("CurrentCreateTable") = dtCurrentTable
                        GridViewCreateDisplay.DataSource = dtCurrentTable
                        GridViewCreateDisplay.DataBind()
                        intRow += 1

                    Next
                    'Set Previous Data on Postbacks
                    SetCreatePreviousItems()
                End If
            End If
        End If
    End Sub

    Private Function GetInformation(ByVal listID As Integer) As DataSet
        Dim pubset As New DataSet
        Dim strsql As String = "select quantity, cost, item from groceryitems where listid = " & listID
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Using conn As New SqlConnection(connStr)
            Dim cmd As SqlCommand = New SqlCommand(strsql, conn)
            cmd.CommandType = CommandType.Text

            conn.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(pubset)
        End Using

        Return pubset
    End Function

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

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Dim intListID As Integer = Request.QueryString("listid")
        Using conn As New SqlConnection(connStr)

            Try
                conn.Open()
                updateItems(GridViewCreateDisplay, intListID, conn)
                insertItems(GridViewCreateDisplay, intListID, conn)
            Catch ex As Exception
                Response.Redirect("ErrorMessage.aspx?Error=" + HttpUtility.UrlEncode(ex.Message))
            Finally

                Response.Redirect("ViewList.aspx?listid=" + intListID)

            End Try

        End Using

    End Sub
    Private Sub updateItems(ByVal grdView As GridView, ByVal intListId As Integer, ByVal conn As SqlConnection)

            Dim cmd As SqlCommand = New SqlCommand("updateGroceryItems", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()

        cmd.Parameters.Add("@listid", SqlDbType.Int).Value = intListId

            cmd.ExecuteNonQuery()

    End Sub
    Private Sub insertItems(ByVal grdView As GridView, ByVal intListId As Integer, ByVal conn As SqlConnection)
        Dim strItem As String = String.Empty
        Dim strQuantity As String = String.Empty
        Dim strCost As String = String.Empty
        For i As Integer = 0 To grdView.Rows.Count - 1
            strItem = DirectCast(grdView.Rows(i).Cells(0).FindControl("Item"), TextBox).Text
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
        Next
    End Sub
End Class