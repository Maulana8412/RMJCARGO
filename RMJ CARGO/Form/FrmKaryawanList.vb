Imports DevExpress.Utils
Imports DevExpress.XtraSplashScreen
Imports MySql.Data.MySqlClient
Public Class FrmKaryawanList
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FRMKARYAWAN.Show()
    End Sub

    Private Sub FrmKaryawanList_Load(sender As Object, e As EventArgs) Handles Me.Load
        MdiParent = MenuUtama
        tampilGrid()
    End Sub
    Sub tampilGrid()
        Try
            OpenConnectionMySQL()
            SplashScreenManager.ShowForm(GetType(WaitProcess))
            MySQLcmd = New MySqlCommand("SELECT *FROM karyawan", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 100
            GV.Columns(0).Caption = "NIK"
            GV.Columns(1).Width = 150
            GV.Columns(1).Caption = "Nama Karyawan"
            GV.Columns(2).Width = 150
            GV.Columns(2).Caption = "Tgl Masuk"
            GV.Columns(3).Width = 100
            GV.Columns(3).Caption = "Jabatan"


            With GV
                .Columns(2).DisplayFormat.FormatType = FormatType.DateTime
                .Columns(2).DisplayFormat.FormatString = "dd-MM-yyyy"
                .Columns(4).Visible = False

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
        If TextEdit1.Text = "" Or TextEdit1.Text = "<Nama Karyawan>" Then
        Else
            GV.ActiveFilterString = "[NAMA] Like '%" + TextEdit1.Text + "%'"
        End If
    End Sub

    Private Sub TextEdit1_GotFocus(sender As Object, e As EventArgs) Handles TextEdit1.GotFocus
        If TextEdit1.Text = "<Nama Karyawan>" Then
            TextEdit1.Text = ""
        End If
    End Sub

    Private Sub TextEdit1_LostFocus(sender As Object, e As EventArgs) Handles TextEdit1.LostFocus
        If TextEdit1.Text = "" Then
            TextEdit1.Text = "<Nama Karyawan>"
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        GV.ActiveFilterString = ""
        tampilGrid()
        TextEdit1.Text = "<Nama Karyawan>"
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        With FRMKARYAWANEdit
            .Show()
            .Text = "Karyawan <" & GV.GetRowCellValue(GV.FocusedRowHandle, "NAMA").ToString & ">"
            .TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NIK").ToString
            .TextEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NAMA").ToString
            .DateEdit1.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "TGLMASUK")
            .LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "JABATAN").ToString
            .TextEdit1.Enabled = False
        End With
    End Sub

    Private Sub GV_DoubleClick(sender As Object, e As EventArgs) Handles GV.DoubleClick
        With FRMKARYAWANEdit
            .Show()
            .Text = "Karyawan <" & GV.GetRowCellValue(GV.FocusedRowHandle, "NAMA").ToString & ">"
            .TextEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NIK").ToString
            .TextEdit2.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "NAMA").ToString
            .DateEdit1.EditValue = GV.GetRowCellValue(GV.FocusedRowHandle, "TGLMASUK")
            .LookUpEdit1.Text = GV.GetRowCellValue(GV.FocusedRowHandle, "JABATAN").ToString
            .TextEdit1.Enabled = False
        End With
    End Sub
End Class