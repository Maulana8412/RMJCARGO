Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraSplashScreen
Imports MySql.Data.MySqlClient
Public Class FrmProdukList
    Sub CreateListKota()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,kota from kotapengiriman order by kota ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("Id") = 0
            rsMaster("kota") = "<Semua>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "kota asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit1
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "kota"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Sub CreateListKota2()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,kota from kotapengiriman order by kota ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("Id") = 0
            rsMaster("kota") = "<Semua>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "kota asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit2
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "kota"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub tampilGrid()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("select PRODUK, " &
                                        "DARI, " &
                                        "KEPADA, " &
                                        "BERATMIN, " &
                                        "HARGAMIN, " &
                                        "HARGAPERKG, " &
                                        "SATUAN, " &
                                        "VIA, " &
                                        "HARGAMIN2, " &
                                        "HARGAPERKG2, " &
                                        "HARGAMIN3, " &
                                        "HARGAPERKG3, " &
                                        "HARGAMIN4, " &
                                        "HARGAPERKG4, " &
                                        "HARGAMIN5, " &
                                        "HARGAPERKG5 from produk " &
                                        "order by produk", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 100
            GV.Columns(0).Caption = "PRODUK"
            GV.Columns(1).Width = 150
            GV.Columns(1).Caption = "DARI"
            GV.Columns(2).Width = 150
            GV.Columns(2).Caption = "KEPADA"
            GV.Columns(3).Width = 100
            GV.Columns(3).Caption = "BERAT MIN"
            GV.Columns(4).Width = 100
            GV.Columns(4).Caption = "HARGA MIN"
            GV.Columns(5).Width = 150
            GV.Columns(5).Caption = "HARGA PERKG/TON"
            GV.Columns(6).Width = 100
            GV.Columns(6).Caption = "SATUAN"
            GV.Columns(7).Width = 100
            GV.Columns(7).Caption = "VIA"

            With GV

                .Columns(3).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(3).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(4).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(4).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(5).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(5).DisplayFormat.FormatString = CustomFormatDec0

                .Columns(8).Visible = False
                .Columns(9).Visible = False
                .Columns(10).Visible = False
                .Columns(11).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With


            SplashScreenManager.CloseForm()
        Catch ex As Exception
            If Application.OpenForms().OfType(Of WaitProcess).Any Then
                SplashScreenManager.CloseForm()
            End If
            MsgBox(Err.Description)
        End Try
        MySQLconn.Close()
    End Sub
    Sub tampilGridDari()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("select PRODUK, " &
                                        "DARI, " &
                                        "KEPADA, " &
                                        "BERATMIN, " &
                                        "HARGAMIN, " &
                                        "HARGAPERKG, " &
                                        "SATUAN, " &
                                        "VIA, " &
                                        "HARGAMIN2, " &
                                        "HARGAPERKG2, " &
                                        "HARGAMIN3, " &
                                        "HARGAPERKG3, " &
                                        "HARGAMIN4, " &
                                        "HARGAPERKG4, " &
                                        "HARGAMIN5, " &
                                        "HARGAPERKG5 from produk Where DARI='" & LookUpEdit1.Text & "' " &
                                        "order by produk", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 100
            GV.Columns(0).Caption = "PRODUK"
            GV.Columns(1).Width = 150
            GV.Columns(1).Caption = "DARI"
            GV.Columns(2).Width = 150
            GV.Columns(2).Caption = "KEPADA"
            GV.Columns(3).Width = 100
            GV.Columns(3).Caption = "BERAT MIN"
            GV.Columns(4).Width = 100
            GV.Columns(4).Caption = "HARGA MIN"
            GV.Columns(5).Width = 150
            GV.Columns(5).Caption = "HARGA PERKG/TON"
            GV.Columns(6).Width = 100
            GV.Columns(6).Caption = "SATUAN"
            GV.Columns(7).Width = 100
            GV.Columns(7).Caption = "VIA"

            With GV

                .Columns(3).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(3).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(4).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(4).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(5).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(5).DisplayFormat.FormatString = CustomFormatDec0

                .Columns(8).Visible = False
                .Columns(9).Visible = False
                .Columns(10).Visible = False
                .Columns(11).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With


            SplashScreenManager.CloseForm()
        Catch ex As Exception
            If Application.OpenForms().OfType(Of WaitProcess).Any Then
                SplashScreenManager.CloseForm()
            End If
            MsgBox(Err.Description)
        End Try
        MySQLconn.Close()
    End Sub
    Sub tampilGridKe()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("select PRODUK, " &
                                        "DARI, " &
                                        "KEPADA, " &
                                        "BERATMIN, " &
                                        "HARGAMIN, " &
                                        "HARGAPERKG, " &
                                        "SATUAN, " &
                                        "VIA, " &
                                        "HARGAMIN2, " &
                                        "HARGAPERKG2, " &
                                        "HARGAMIN3, " &
                                        "HARGAPERKG3, " &
                                        "HARGAMIN4, " &
                                        "HARGAPERKG4, " &
                                        "HARGAMIN5, " &
                                        "HARGAPERKG5 from produk Where KEPADA='" & LookUpEdit2.Text & "' " &
                                        "order by produk", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 100
            GV.Columns(0).Caption = "PRODUK"
            GV.Columns(1).Width = 150
            GV.Columns(1).Caption = "DARI"
            GV.Columns(2).Width = 150
            GV.Columns(2).Caption = "KEPADA"
            GV.Columns(3).Width = 100
            GV.Columns(3).Caption = "BERAT MIN"
            GV.Columns(4).Width = 100
            GV.Columns(4).Caption = "HARGA MIN"
            GV.Columns(5).Width = 150
            GV.Columns(5).Caption = "HARGA PERKG/TON"
            GV.Columns(6).Width = 100
            GV.Columns(6).Caption = "SATUAN"
            GV.Columns(7).Width = 100
            GV.Columns(7).Caption = "VIA"

            With GV

                .Columns(3).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(3).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(4).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(4).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(5).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(5).DisplayFormat.FormatString = CustomFormatDec0

                .Columns(8).Visible = False
                .Columns(9).Visible = False
                .Columns(10).Visible = False
                .Columns(11).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With


            SplashScreenManager.CloseForm()
        Catch ex As Exception
            If Application.OpenForms().OfType(Of WaitProcess).Any Then
                SplashScreenManager.CloseForm()
            End If
            MsgBox(Err.Description)
        End Try
        MySQLconn.Close()
    End Sub
    Sub tampilGridDariKe()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("select PRODUK, " &
                                        "DARI, " &
                                        "KEPADA, " &
                                        "BERATMIN, " &
                                        "HARGAMIN, " &
                                        "HARGAPERKG, " &
                                        "SATUAN, " &
                                        "VIA, " &
                                        "HARGAMIN2, " &
                                        "HARGAPERKG2, " &
                                        "HARGAMIN3, " &
                                        "HARGAPERKG3, " &
                                        "HARGAMIN4, " &
                                        "HARGAPERKG4, " &
                                        "HARGAMIN5, " &
                                        "HARGAPERKG5 from produk Where DARI='" & LookUpEdit1.Text _
                                         & "' AND KEPADA='" & LookUpEdit2.Text & "'" &
                                        "order by produk", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 100
            GV.Columns(0).Caption = "PRODUK"
            GV.Columns(1).Width = 150
            GV.Columns(1).Caption = "DARI"
            GV.Columns(2).Width = 150
            GV.Columns(2).Caption = "KEPADA"
            GV.Columns(3).Width = 100
            GV.Columns(3).Caption = "BERAT MIN"
            GV.Columns(4).Width = 100
            GV.Columns(4).Caption = "HARGA MIN"
            GV.Columns(5).Width = 150
            GV.Columns(5).Caption = "HARGA PERKG/TON"
            GV.Columns(6).Width = 100
            GV.Columns(6).Caption = "SATUAN"
            GV.Columns(7).Width = 100
            GV.Columns(7).Caption = "VIA"

            With GV

                .Columns(3).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(3).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(4).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(4).DisplayFormat.FormatString = CustomFormatDec0
                .Columns(5).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(5).DisplayFormat.FormatString = CustomFormatDec0

                .Columns(8).Visible = False
                .Columns(9).Visible = False
                .Columns(10).Visible = False
                .Columns(11).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = False
                .Columns(15).Visible = False
            End With

            SplashScreenManager.CloseForm()
        Catch ex As Exception
            If Application.OpenForms().OfType(Of WaitProcess).Any Then
                SplashScreenManager.CloseForm()
            End If
            MsgBox(Err.Description)
        End Try
        MySQLconn.Close()
    End Sub
    Private Sub FrmProdukList_Load(sender As Object, e As EventArgs) Handles Me.Load
        MdiParent = MenuUtama
        CreateListKota()
        CreateListKota2()
        tampilGrid()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FrmProduk.Show()
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        If TextEdit1.Text = "" Or TextEdit1.Text = "<Produk>" Then
        Else
            GV.ActiveFilterString = "[produk] Like '%" + TextEdit1.Text + "%'"
        End If
    End Sub

    Private Sub TextEdit1_GotFocus(sender As Object, e As EventArgs) Handles TextEdit1.GotFocus
        If TextEdit1.Text = "<Produk>" Then
            TextEdit1.Text = ""
        End If
    End Sub

    Private Sub TextEdit1_LostFocus(sender As Object, e As EventArgs) Handles TextEdit1.LostFocus
        If TextEdit1.Text = "" Or TextEdit1.Text <> "<Produk>" Then
            TextEdit1.Text = "<Produk>"
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        GV.ActiveFilterString = ""
        tampilGrid()

        CreateListKota()
        CreateListKota2()
        LookUpEdit1.Text = "<Semua>"
        LookUpEdit2.Text = "<Semua>"
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        TextEdit1.Text = "<Produk>"
        GV.ActiveFilterString = ""
        If LookUpEdit1.Text = "<Semua>" And LookUpEdit2.Text = "<Semua>" Then
            tampilGrid()
        ElseIf LookUpEdit1.Text <> "<Semua>" And LookUpEdit2.Text = "<Semua>" Then
            tampilGridDari()
        ElseIf LookUpEdit1.Text = "<Semua>" And LookUpEdit2.Text <> "<Semua>" Then
            tampilGridKe()
        ElseIf LookUpEdit1.Text <> "<Semua>" And LookUpEdit2.Text <> "<Semua>" Then
            tampilGridDariKe()
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        With FrmProdukEdit
            .Show()
            .Text = "Produk <" & GV.GetRowCellValue(GV.FocusedRowHandle, "PRODUK").ToString & ">"
            .TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "PRODUK").ToString
            .LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "DARI").ToString
            .LookUpEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "KEPADA").ToString
            .TextEdit2.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "BERATMIN")
            .TextEdit3.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "VIA").ToString
            .LookUpEdit3.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "SATUAN").ToString
            .TextEdit4.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN")
            .TextEdit5.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG")
            .TextEdit6.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN2")
            .TextEdit7.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG2")
            .TextEdit8.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN3")
            .TextEdit9.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG3")
            .TextEdit10.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN4")
            .TextEdit11.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG4")
            .TextEdit12.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN5")
            .TextEdit13.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG5")
            .TextEdit1.Enabled = False
            .LookUpEdit1.Enabled = False
            .LookUpEdit2.Enabled = False
        End With


    End Sub

    Private Sub GV_DoubleClick(sender As Object, e As EventArgs) Handles GV.DoubleClick
        With FrmProdukEdit
            .Show()
            .Text = "Produk <" & GV.GetRowCellValue(GV.FocusedRowHandle, "PRODUK").ToString & ">"
            .TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "PRODUK").ToString
            .LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "DARI").ToString
            .LookUpEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "KEPADA").ToString
            .TextEdit2.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "BERATMIN")
            .TextEdit3.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "VIA").ToString
            .LookUpEdit3.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "SATUAN").ToString
            .TextEdit4.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN")
            .TextEdit5.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG")
            .TextEdit6.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN2")
            .TextEdit7.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG2")
            .TextEdit8.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN3")
            .TextEdit9.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG3")
            .TextEdit10.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN4")
            .TextEdit11.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG4")
            .TextEdit12.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAMIN5")
            .TextEdit13.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "HARGAPERKG5")
            .TextEdit1.Enabled = False
            .LookUpEdit1.Enabled = False
            .LookUpEdit2.Enabled = False
        End With
    End Sub
End Class
