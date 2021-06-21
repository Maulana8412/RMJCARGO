Imports DevExpress.XtraSplashScreen
Imports MySql.Data.MySqlClient

Public Class FrmKotaPengirimanList
    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged

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
                                        "ibukota from kotapengiriman " &
                                        "order by kota", MySQLconn)
            MySQLcmd.CommandType = CommandType.Text
            MySQLdr.Close()
            MySQLda = New MySqlDataAdapter(MySQLcmd)
            Dim sqltable As New DataTable
            MySQLconn.Close()
            MySQLda.Fill(sqltable)
            GC.DataSource = sqltable
            GV.Columns(0).Width = 150
            GV.Columns(0).Caption = "kota Pengiriman"
            GV.Columns(1).Width = 100
            GV.Columns(1).Caption = "Wilayah"
            GV.Columns(2).Visible = False

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

    Private Sub FrmKotaPengirimanList_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        If TextEdit1.Text = "<Kota Pengiriman>" Then
            TextEdit1.Text = ""
        End If
    End Sub
End Class