Imports DevExpress.XtraEditors
Imports MySql.Data.MySqlClient
Public Class FrmWilayah
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("delete from wilayah where wilayah=@wilayah", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@wilayah", TextEdit1.Text)
            End With
            MySQLcmd.ExecuteNonQuery()

            MySQLcmd = New MySqlCommand("INSERT INTO wilayah(wilayah)values(@wilayah)", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@wilayah", TextEdit1.Text)
            End With
            MySQLcmd.ExecuteNonQuery()
            XtraMessageBox.Show("Data wilayah sudah disimpan...")
            TextEdit1.Text = ""

        Catch ex As Exception
            XtraMessageBox.AllowCustomLookAndFeel = True
            XtraMessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub
End Class