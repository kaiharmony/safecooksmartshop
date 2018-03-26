Imports System.Data.SqlClient

Public Class UpdateRecipe
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim strRecipeid As String = String.Empty
            strRecipeid = Request.QueryString("recipeid")
            If Not strRecipeid = String.Empty Then
                SetCreateIntialRow()
                SetCreateDirections()
                SetInformation(strRecipeid)
                SetDirections(strRecipeid)
                SetRecipeName(strRecipeid)
            Else
                Response.Redirect("default.aspx")

            End If
          
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
                recipeName.Value = Convert.ToString(pubset.Tables(0).Rows(intRow)("recipename"))
                serving.Value = Convert.ToString(pubset.Tables(0).Rows(intRow)("serving"))
                ethnicity.SelectedValue = Convert.ToString(pubset.Tables(0).Rows(intRow)("ethnicity"))
                intRow += 1
            Next
        End If

    End Sub
    Private Function GetInformation(ByVal recipeid As Integer) As DataSet
        Dim pubset As New DataSet
        Dim strsql As String = "select measurement, cost, ingredientname from ingredients where recipeid = " & recipeid
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
    Private Function GetDirections(ByVal recipeid As Integer) As DataSet
        Dim pubset As New DataSet
        Dim strsql As String = "select directions from recipedirections where recipeid = " & recipeid
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
    Private Sub SetInformation(ByVal recipeid As Integer)
        Dim pubset As New DataSet
        pubset = GetInformation(recipeid)
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
                        box1.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("ingredientname"))

                        Dim box2 As TextBox = DirectCast(GridViewCreateDisplay.Rows(rowIndex).Cells(1).FindControl("txtbxCreateMeasurement"), TextBox)
                        box2.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("measurement"))

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
    Private Sub SetDirections(ByVal recipeid As Integer)
        Dim pubset As New DataSet
        pubset = GetDirections(recipeid)
        Dim intRow As Integer = 0
        Dim rowIndex As Integer = 0
        If pubset.Tables(0).Rows.Count > 0 Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentDirectionsTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable IsNot Nothing Then
                If dtCurrentTable.Rows.Count > 0 Then
                    For Each myRow In pubset.Tables(0).Rows
                        Dim intRowCount As Integer = dtCurrentTable.Rows.Count
                        'extract the TextBox values
                        Dim box1 As TextBox = DirectCast(GridViewCreateDirection.Rows(rowIndex).Cells(0).FindControl("Directions"), TextBox)
                        box1.Text = Convert.ToString(pubset.Tables(0).Rows(intRow)("directions"))

                  

                        drCurrentRow = dtCurrentTable.NewRow()
                        dtCurrentTable.Rows(intRowCount - 1)(0) = box1.Text


                        dtCurrentTable.Rows.Add(drCurrentRow)
                        ViewState("CurrentDirectionsTable") = dtCurrentTable
                        GridViewCreateDirection.DataSource = dtCurrentTable
                        GridViewCreateDirection.DataBind()
                        intRow += 1

                    Next
                    'Set Previous Data on Postbacks
                    SetCreatePreviousDirections()
                End If
            End If
        End If
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

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim connStr As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Dim intrecipeid As Integer = Request.QueryString("recipeid")
        Using conn As New SqlConnection(connStr)

            Try
                conn.Open()
                updateItems(GridViewCreateDisplay, intrecipeid, conn)
                insertIngredients(GridViewCreateDisplay, intrecipeid, conn)
                InsertDirections(GridViewCreateDirection, intrecipeid, conn)
            Catch ex As Exception
                Response.Redirect("ErrorMessage.aspx?Error=" + HttpUtility.UrlEncode(ex.Message))
            Finally

                Response.Redirect("ViewRecipe.aspx?recipeid=" & intrecipeid)

            End Try

        End Using

    End Sub
    Private Sub updateItems(ByVal grdView As GridView, ByVal intListId As Integer, ByVal conn As SqlConnection)

        Dim cmd As SqlCommand = New SqlCommand("updateRecipeInformation", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Clear()

        cmd.Parameters.Add("@recipeid", SqlDbType.Int).Value = intListId

        cmd.ExecuteNonQuery()

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