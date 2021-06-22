Imports MySql.Data.MySqlClient
Public Class FrmPembayaran
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            Dim LUNAS As String
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("DELETE FROM pembayaran WHERE NAMA='" & TextEdit1.Text & "'", MySQLconn)
            MySQLcmd.ExecuteNonQuery()

            If CheckEdit1.Checked = True Then
                LUNAS = "1"
            Else
                LUNAS = "0"
            End If

            MySQLcmd = New MySqlCommand("INSERT INTO pembayaran(NAMA, " &
                                        "PELUNASAN, " &
                                        "USERNAME)VALUES('" & TextEdit1.Text _
                                        & "','" & LUNAS _
                                        & "','" & MenuUtama.BarStaticItem3.Caption & "')", MySQLconn)
            MySQLcmd.ExecuteNonQuery()
            MsgBox("Data sudah disimpan...")
            TextEdit1.Text = ""
            CheckEdit1.Checked = False
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub FrmPembayaran_Load(sender As Object, e As EventArgs) Handles Me.Load
        MdiParent = MenuUtama
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub
End Class