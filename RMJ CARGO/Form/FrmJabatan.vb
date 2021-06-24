Imports MySql.Data.MySqlClient
Public Class FrmJabatan
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("DELETE FROM jabatan WHERE NAMA='" & TextEdit1.Text & "'", MySQLconn)
            MySQLcmd.ExecuteNonQuery()

            MySQLcmd = New MySqlCommand("INSERT INTO jabatan(NAMA)VALUES('" & TextEdit1.Text & "')", MySQLconn)
            MySQLcmd.ExecuteNonQuery()

            MsgBox("Data sudah disimpan...")
            TextEdit1.Text = ""
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub FrmJabatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub
End Class