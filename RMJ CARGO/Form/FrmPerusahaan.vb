Imports DevExpress.XtraEditors
Imports MySql.Data.MySqlClient
Public Class FrmPerusahaan
    Private Sub FrmPerusahaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        Tampil()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("delete from perusahaan", MySQLconn)
            MySQLcmd.ExecuteNonQuery()

            MySQLcmd = New MySqlCommand("Insert Into perusahaan(NAMA," &
                                        "ALAMAT1, " &
                                        "ALAMAT2, " &
                                        "ALAMAT3, " &
                                        "ALAMAT4, " &
                                        "KOTA, " &
                                        "TELP)VALUES(@NAMA, " &
                                        "@ALAMAT1, " &
                                        "@ALAMAT2, " &
                                        "@ALAMAT3, " &
                                        "@ALAMAT4, " &
                                        "@KOTA, " &
                                        "@TELP)", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@NAMA", TextEdit1.Text)
                .Parameters.AddWithValue("@ALAMAT1", TextEdit2.Text)
                .Parameters.AddWithValue("@ALAMAT2", TextEdit3.Text)
                .Parameters.AddWithValue("@ALAMAT3", TextEdit4.Text)
                .Parameters.AddWithValue("@ALAMAT4", TextEdit5.Text)
                .Parameters.AddWithValue("@KOTA", TextEdit6.Text)
                .Parameters.AddWithValue("@TELP", TextEdit8.Text)
            End With
            MySQLcmd.ExecuteNonQuery()
            XtraMessageBox.Show("Data Berhasil Di Simpan...")

            MySQLconn.Close()
        Catch ex As Exception
            XtraMessageBox.AllowCustomLookAndFeel = True
            XtraMessageBox.Show(Err.Description)
        End Try
    End Sub
    Sub Tampil()
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("Select *From perusahaan", MySQLconn)
            MySQLdr = MySQLcmd.ExecuteReader
            MySQLdr.Read()

            If MySQLdr.HasRows Then
                TextEdit1.Text = MySQLdr("NAMA")
                TextEdit2.Text = MySQLdr("ALAMAT1")
                TextEdit3.Text = MySQLdr("ALAMAT2")
                TextEdit4.Text = MySQLdr("ALAMAT3")
                TextEdit5.Text = MySQLdr("ALAMAT4")
                TextEdit6.Text = MySQLdr("KOTA")
                TextEdit8.Text = MySQLdr("TELP")
            End If
        Catch ex As Exception
            XtraMessageBox.AllowCustomLookAndFeel = True
            XtraMessageBox.Show(Err.Description)
        End Try
    End Sub
End Class