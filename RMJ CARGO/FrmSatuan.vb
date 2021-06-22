Imports MySql.Data.MySqlClient
Public Class FrmSatuan
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("delete from satuan where satuan='" & TextEdit1.Text & "'", MySQLconn)
            MySQLcmd.ExecuteNonQuery()

            MySQLcmd = New MySqlCommand("INSERT INTO satuan (satuan)values('" & TextEdit1.Text & "')", MySQLconn)
            MySQLcmd.ExecuteNonQuery()
            MsgBox("Data sudah di simpan...")

            TextEdit1.Text = ""

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
End Class