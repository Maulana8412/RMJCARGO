Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient
Public Class FrmPerusahaan
    Private Sub FrmPerusahaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        CreateListKota()
        Tampil()

    End Sub
    Sub CreateListKota()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,kota from kotapengiriman order by kota ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            'rsMaster("Id") = 0
            'rsMaster("kota") = "<Semua>"
            'dtsource.Rows.Add(rsMaster)

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
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            OpenConnectionMySQL()

            If TextEdit1.Text = "" Then
                MsgBox("Nama Perusahaan belum di isi...")
            ElseIf TextEdit2.Text = "" Then
                MsgBox("Alamat belum di isi...")
            ElseIf LookUpEdit1.Text = "" Then
                MsgBox("Kota belum di pilih...")
            Else
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
                    .Parameters.AddWithValue("@KOTA", LookUpEdit1.Text)
                    .Parameters.AddWithValue("@TELP", TextEdit8.Text)
                End With
                MySQLcmd.ExecuteNonQuery()
                XtraMessageBox.Show("Data Berhasil Di Simpan...")

            End If

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
                LookUpEdit1.Text = MySQLdr("KOTA")
                TextEdit8.Text = MySQLdr("TELP")
            End If
        Catch ex As Exception
            XtraMessageBox.AllowCustomLookAndFeel = True
            XtraMessageBox.Show(Err.Description)
        End Try
    End Sub
End Class