Imports DevExpress.Utils
Imports DevExpress.XtraSplashScreen
Imports MySql.Data.MySqlClient


Public Class FrmPelangganList
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FrmPelanggan.Show()
    End Sub

    Private Sub FrmPelangganList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        tampilGrid()
    End Sub
    Sub tampilGrid()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("SELECT *FROM pelanggan", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 100
            GV.Columns(0).Caption = "No. Pelanggan"
            GV.Columns(1).Width = 150
            GV.Columns(1).Caption = "Nama Pelanggan"
            GV.Columns(2).Width = 150
            GV.Columns(2).Caption = "Alamat"
            GV.Columns(4).Width = 100
            GV.Columns(4).Caption = "Kota"
            GV.Columns(5).Width = 100
            GV.Columns(5).Caption = "Telp"
            GV.Columns(6).Width = 150
            GV.Columns(6).Caption = "Hp"
            GV.Columns(8).Width = 100
            GV.Columns(8).Caption = "Saldo"
            GV.Columns(9).Width = 100
            GV.Columns(9).Caption = "Pembayaran"

            With GV

                .Columns(8).DisplayFormat.FormatType = FormatType.Numeric
                .Columns(8).DisplayFormat.FormatString = CustomFormatDec0


                .Columns(3).Visible = False
                .Columns(7).Visible = False
                .Columns(10).Visible = False
                .Columns(11).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(14).Visible = False
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

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        If TextEdit1.Text = "" Or TextEdit1.Text = "<No. Pelanggan>" Then
        Else
            GV.ActiveFilterString = "[NOPELANGGAN] Like '%" + TextEdit1.Text + "%'"
        End If
    End Sub

    Private Sub TextEdit1_GotFocus(sender As Object, e As EventArgs) Handles TextEdit1.GotFocus
        TextEdit1.Text = ""
        TextEdit2.Text = "<Nama Pelanggan>"
    End Sub

    Private Sub TextEdit2_GotFocus(sender As Object, e As EventArgs) Handles TextEdit2.GotFocus
        TextEdit2.Text = ""
        TextEdit1.Text = "<No. Pelanggan>"
    End Sub

    Private Sub TextEdit1_LostFocus(sender As Object, e As EventArgs) Handles TextEdit1.LostFocus
        TextEdit1.Text = "<No. Pelanggan>"
    End Sub

    Private Sub TextEdit2_LostFocus(sender As Object, e As EventArgs) Handles TextEdit2.LostFocus
        TextEdit2.Text = "<Nama Pelanggan>"
    End Sub

    Private Sub TextEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit2.EditValueChanged
        If TextEdit2.Text = "" Or TextEdit2.Text = "<Nama Pelanggan>" Then
        Else
            GV.ActiveFilterString = "[NAMAPELANGGAN] Like '%" + TextEdit2.Text + "%'"
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        tampilGrid()
        TextEdit1.Text = "<No. Pelanggan>"
        TextEdit2.Text = "<Nama Pelanggan>"
        GV.ActiveFilterString = ""
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Try
            With FrmPelangganEdit
                .Show()
                .Text = "Pelanggan <" & GV.GetRowCellValue(GV.FocusedRowHandle, "NAMAPELANGGAN").ToString & ">"
                .TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NOPELANGGAN").ToString
                .TextEdit1.Enabled = False
                .TextEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NAMAPELANGGAN").ToString
                .TextEdit3.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "ALAMAT1").ToString
                .TextEdit4.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "ALAMAT2").ToString
                .TextEdit5.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "KOTA").ToString
                .TextEdit6.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "TELEPON").ToString
                .TextEdit7.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "HP").ToString
                .TextEdit8.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "SALDO")
                .LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "PEMBAYARAN").ToString
                .LookUpEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "PAJAK").ToString
                .TextEdit9.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NPWP").ToString
                .ComboBox1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "TINGKAT").ToString
                If GV.GetRowCellValue(GV.FocusedRowHandle, "STATUS").ToString = "1" Then
                    .CheckEdit1.Checked = False
                Else
                    .CheckEdit1.Checked = True
                End If
            End With
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub GV_DoubleClick(sender As Object, e As EventArgs) Handles GV.DoubleClick
        Try
            With FrmPelangganEdit
                .Show()
                .Text = "Pelanggan <" & GV.GetRowCellValue(GV.FocusedRowHandle, "NAMAPELANGGAN").ToString & ">"
                .TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NOPELANGGAN").ToString
                .TextEdit1.Enabled = False
                .TextEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NAMAPELANGGAN").ToString
                .TextEdit3.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "ALAMAT1").ToString
                .TextEdit4.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "ALAMAT2").ToString
                .TextEdit5.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "KOTA").ToString
                .TextEdit6.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "TELEPON").ToString
                .TextEdit7.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "HP").ToString
                .TextEdit8.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "SALDO")
                .LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "PEMBAYARAN").ToString
                .LookUpEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "PAJAK").ToString
                .TextEdit9.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NPWP").ToString
                .ComboBox1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "TINGKAT").ToString
                If GV.GetRowCellValue(GV.FocusedRowHandle, "STATUS").ToString = "1" Then
                    .CheckEdit1.Checked = False
                Else
                    .CheckEdit1.Checked = True
                End If
            End With
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
End Class