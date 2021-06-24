Imports DevExpress.XtraSplashScreen
Imports MySql.Data.MySqlClient

Public Class FrmKotaPengirimanList
    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        If TextEdit1.Text = "" Or TextEdit1.Text = "<Kota Pengiriman>" Then
        Else
            GV.ActiveFilterString = "[kota] Like '%" + TextEdit1.Text + "%'"
        End If
    End Sub

    Private Sub FrmKotaPengirimanList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        tampilGrid()
        TextEdit1.Text = "<Kota Pengiriman>"
    End Sub
    Sub tampilGrid()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("select kota, " &
                                        "wilayah, " &
                                        "ibukota,kodepos from kotapengiriman " &
                                        "order by kota", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 150
            GV.Columns(0).Caption = "Kota Pengiriman"
            GV.Columns(1).Width = 100
            GV.Columns(1).Caption = "Wilayah"
            GV.Columns(2).Visible = False
            GV.Columns(3).Visible = False

            SplashScreenManager.CloseForm()
        Catch ex As Exception
            If Application.OpenForms().OfType(Of WaitProcess).Any Then
                SplashScreenManager.CloseForm()
            End If
            'MsgBox(Err.Description)
        End Try
        MySQLconn.Close()
    End Sub

    Private Sub FrmKotaPengirimanList_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        If TextEdit1.Text = "" Then
            TextEdit1.Text = "<Kota Pengiriman>"
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FrmKotaPengiriman.Show()
    End Sub

    Private Sub TextEdit1_GotFocus(sender As Object, e As EventArgs) Handles TextEdit1.GotFocus
        If TextEdit1.Text = "<Kota Pengiriman>" Then
            TextEdit1.Text = ""
        End If
    End Sub

    Private Sub TextEdit1_LostFocus(sender As Object, e As EventArgs) Handles TextEdit1.LostFocus
        TextEdit1.Text = "<Kota Pengiriman>"
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        GV.ActiveFilterString = ""
        tampilGrid()
        TextEdit1.Text = "<Kota Pengiriman>"
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        FrmKotaPengirimanEdit.Show()
        FrmKotaPengirimanEdit.Text = "Kota <" & GV.GetRowCellValue(GV.FocusedRowHandle, "kota").ToString & ">"
        FrmKotaPengirimanEdit.TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "kota").ToString
        FrmKotaPengirimanEdit.LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "wilayah").ToString
        FrmKotaPengirimanEdit.TextEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "kodepos").ToString
        If GV.GetRowCellValue(GV.FocusedRowHandle, "ibukota").ToString = "1" Then
            FrmKotaPengirimanEdit.CheckEdit1.Checked = True
        Else
            FrmKotaPengirimanEdit.CheckEdit1.Checked = False
        End If
        FrmKotaPengirimanEdit.TextEdit1.Enabled = False
    End Sub

    Private Sub GV_DoubleClick(sender As Object, e As EventArgs) Handles GV.DoubleClick
        FrmKotaPengirimanEdit.Show()
        FrmKotaPengirimanEdit.Text = "Kota <" & GV.GetRowCellValue(GV.FocusedRowHandle, "kota").ToString & ">"
        FrmKotaPengirimanEdit.TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "kota").ToString
        FrmKotaPengirimanEdit.LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "wilayah").ToString
        FrmKotaPengirimanEdit.TextEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "kodepos").ToString
        If GV.GetRowCellValue(GV.FocusedRowHandle, "ibukota").ToString = "1" Then
            FrmKotaPengirimanEdit.CheckEdit1.Checked = True
        Else
            FrmKotaPengirimanEdit.CheckEdit1.Checked = False
        End If
        FrmKotaPengirimanEdit.TextEdit1.Enabled = False
    End Sub
End Class