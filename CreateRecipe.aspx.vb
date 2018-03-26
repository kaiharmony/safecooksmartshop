Imports System.Data.SqlClient

Public Class CreateRecipe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            SetCreateIntialRow()
            SetCreateDirections()

        End If
    End Sub

    Private Sub SetCreateIntialRow()
        Dim dt As DataTable = New DataTable()
        Dim dr As DataRow = Nothing
        Try
            'add new columns
            dt.Columns.Add(New DataColumn("Item", GetType(String)))
            dt.Columns.Add(New DataColumn("Measurement", GetType(String)))
            dt.Columns.Add(New DataColumn("Cost", GetType(String)))
            'add a row
            dr = dt.NewRow
            dr("Item") = String.Empty
            dr("Measurement") = String.Empty
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
    Private Sub SetCreateDirections()
        Dim dt As DataTable = New DataTable()
        Dim dr As DataRow = Nothing
        Try
            'add new columns
            dt.Columns.Add(New DataColumn("Directions", GetType(String)))
            'add a row
            dr = dt.NewRow
            dr("Directions") = String.Empty
            'add row to the table
            dt.Rows.Add(dr)
            'save table to a session
            ViewState("CurrentDirectionsTable") = dt
            'use datatable as gridview datasource

            GridViewCreateDirection.DataSource = dt
            GridViewCreateDirection.DataBind()
        Catch ex As Exception
            Response.Redirect("ErrorMessage.aspx?Error=" + ex.Message)
        End Try
    End Sub
    Private Sub addDirection_ServerClick(sender As Object, e As EventArgs) Handles addDirection.ServerClick
        Dim rowIndex As Integer = 0

        Try
            If ViewState("CurrentDirectionsTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentDirectionsTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count
                        'extract the control values
                        Dim box1 As TextBox =
                                DirectCast(GridViewCreateDirection.Rows(rowIndex).Cells(0).FindControl("Directions"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()
                        dtCurrentTable.Rows(i - 1)(0) = box1.Text
                        rowIndex += 1
                    Next
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentDirectionsTable") = dtCurrentTable

                    GridViewCreateDirection.DataSource = dtCurrentTable
                    GridViewCreateDirection.DataBind()
                End If
            Else
                Response.Redirect("ErrorMessage.aspx?Error=" + "ViewState is null")
            End If


        Catch ex As SqlException
            Response.Redirect("ErrorMessage.aspx?Error=" + ex.Message)
        End Try
        'Set Previous Data on Postbacks
        SetCreatePreviousDirections()
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
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxCreateMeasurement"), TextBox)
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
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxCreateMeasurement"), TextBox)
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
    Private Sub SetCreatePreviousDirections()
        'if a new row is added to the gridview then it resets the gridview with previous information
        Dim rowIndex As Integer = 0
        Try

            If ViewState("CurrentDirectionsTable") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(ViewState("CurrentDirectionsTable"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim box1 As TextBox =
                                DirectCast(GridViewCreateDirection.Rows(rowIndex).Cells(0).FindControl("Directions"), TextBox)

                        box1.Text = dt.Rows(i)(0).ToString()
                     
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
                                DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxCreateMeasurement"), TextBox)
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

    Private Sub GridViewCreateDirection_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewCreateDirection.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim rowIndex As Integer = 0

        Try
            'updates the viewstate table before deleting a row
            If ViewState("CurrentDirectionsTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentDirectionsTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 1 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count
                        'extract the control values
                        Dim box1 As TextBox =
                                DirectCast(GridViewCreateDirection.Rows(rowIndex).Cells(0).FindControl("Directions"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()
                        dtCurrentTable.Rows(i - 1)(0) = box1.Text

                        rowIndex += 1
                    Next
                    'deletes row from datatable then set datatable as the gridview datasource
                    dtCurrentTable.Rows(index).Delete()
                    ViewState("CurrentDirectionsTable") = dtCurrentTable

                    GridViewCreateDirection.DataSource = dtCurrentTable
                    GridViewCreateDirection.DataBind()
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

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Dim intRecipeId As Integer = 0
        Using conn As New SqlConnection(connStr)
            Dim cmd As SqlCommand = New SqlCommand("insertrecipe", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@recipename", SqlDbType.VarChar).Value = recipeName.Value
            cmd.Parameters.Add("@serving", SqlDbType.VarChar).Value = serving.Value
            cmd.Parameters.Add("@ethnicity", SqlDbType.VarChar).Value = ethnicity.SelectedValue
            cmd.Parameters.Add("@loginname", SqlDbType.VarChar).Value = "user2"
            cmd.Parameters.Add("@recipeid", SqlDbType.Int).Direction = ParameterDirection.Output

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                intRecipeId = cmd.Parameters("@recipeid").Value
            Catch ex As Exception
                Response.Redirect("ErrorMessage.aspx?Error=" + HttpUtility.UrlEncode(ex.Message))
            Finally

                insertIngredients(GridViewCreateDisplay, intRecipeId, conn)
                InsertDirections(GridViewCreateDirection, intRecipeId, conn)
            End Try

        End Using

        Response.Redirect("ViewRecipe.aspx?recipeid=" & intRecipeId)
    End Sub

    Private Sub insertIngredients(ByVal grdView As GridView, ByVal intRecipeId As Integer, ByVal conn As SqlConnection)
        Dim strItem As String = String.Empty
        Dim strMeasurement As String = String.Empty
        Dim strCost As String = String.Empty
        For i As Integer = 0 To grdView.Rows.Count - 1
            strItem = DirectCast(grdView.Rows(i).Cells(0).FindControl("item"), TextBox).Text
            If Not strItem = String.Empty Then
                strMeasurement = DirectCast(grdView.Rows(i).Cells(1).FindControl("txtbxCreateMeasurement"), TextBox).Text
                strCost = DirectCast(grdView.Rows(i).Cells(2).FindControl("txtbxCost"), TextBox).Text

                Dim cmd As SqlCommand = New SqlCommand("insertIngredients", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Clear()

                cmd.Parameters.Add("@ingredientname", SqlDbType.VarChar).Value = strItem
                cmd.Parameters.Add("@measurement", SqlDbType.VarChar).Value = strMeasurement
                cmd.Parameters.Add("@cost", SqlDbType.VarChar).Value = strCost
                cmd.Parameters.Add("@recipeid", SqlDbType.Int).Value = intRecipeId
                cmd.Parameters.Add("@stepnumber", SqlDbType.Int).Value = i + 1

                cmd.ExecuteNonQuery()
            End If
        Next
    End Sub

    Private Sub InsertDirections(ByVal grdDirections As GridView, ByVal intRecipeId As Integer, ByVal conn As SqlConnection)
        Dim strDirections As String = String.Empty
        For i As Integer = 0 To grdDirections.Rows.Count - 1
            strDirections = DirectCast(grdDirections.Rows(i).Cells(0).FindControl("Directions"), TextBox).Text
            If Not strDirections = String.Empty Then
                Dim cmd As SqlCommand = New SqlCommand("insertRecipeDirections", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@stepnumber", SqlDbType.Int).Value = i + 1
                cmd.Parameters.Add("@directions", SqlDbType.VarChar).Value = strDirections
                cmd.Parameters.Add("@recipeid", SqlDbType.Int).Value = intRecipeId

                cmd.ExecuteNonQuery()
            End If
        Next
    End Sub

End Class