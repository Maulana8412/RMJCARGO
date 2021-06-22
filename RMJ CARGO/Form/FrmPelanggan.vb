Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient
Public Class FrmPelanggan
    Private Sub FrmPelanggan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        CreateListpembayaran()
        CreateListPajak()
        SetFormatTextNumeric()
        ComboBox1.Items.Add("1")
        ComboBox1.Items.Add("2")
        ComboBox1.Items.Add("3")
        ComboBox1.Items.Add("4")
        ComboBox1.Items.Add("5")
    End Sub

    Sub CreateListpembayaran()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,NAMA from pembayaran order by NAMA ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("Id") = 0
            rsMaster("NAMA") = "<Baru>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "NAMA asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit1
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "NAMA"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub CreateListPajak()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,NAMA from pajak order by NAMA ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("Id") = 0
            rsMaster("NAMA") = "<Baru>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "NAMA asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit2
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "NAMA"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub LookUpEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles LookUpEdit1.EditValueChanged
        If LookUpEdit1.Text = "<Baru>" Then
            FrmPembayaran.Show()
        End If
    End Sub

    Private Sub LookUpEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles LookUpEdit2.EditValueChanged
        If LookUpEdit2.Text = "<Baru>" Then
            FrmPajak.Show()
        End If
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub TextEdit2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit2.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub
    Sub SetFormatTextNumeric()
        With TextEdit9
            .Properties.DisplayFormat.FormatType = FormatType.Numeric
            .Properties.DisplayFormat.FormatString = CustomFormatDec3
            .Properties.EditFormat.FormatType = FormatType.Numeric
            .Properties.EditFormat.FormatString = "n0"
            .Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            .Properties.Mask.EditMask = CustomFormatDec3
            .Properties.Mask.UseMaskAsDisplayFormat = False
            .Properties.Mask.BeepOnError = True
            .Properties.NullText = "0"

        End With
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        OpenConnectionMySQL()
        Try
            Dim Status, p As String
            If TextEdit9.Text = "0" Then
                TextEdit9.EditValue = 0
            End If

            If CheckEdit1.Checked = True Then
                Status = "0"
            ElseIf CheckEdit1.Checked = False Then
                Status = "1"
            End If

            If TextEdit1.Text = "" Then
                MsgBox("No. Pelanggan Belum di Isi...")
            ElseIf TextEdit2.Text = "" Then
                MsgBox("Nama Pelanggan Belum Di Isi...")
            ElseIf LookUpEdit1.Text = "" Then
                MsgBox("Pembayaran Belum Dipilih...")
            ElseIf ComboBox1.Text = "" Then
                MsgBox("Tingkat harga belum di pilih...")
            Else
                MySQLcmd = New MySqlCommand("SELECT * FROM pelanggan WHERE NOPELANGGAN=@NOPELANGGAN ", MySQLconn)
                With MySQLcmd
                    .Parameters.AddWithValue("@NOPELANGGAN", TextEdit1.Text)
                End With
                MySQLdr = MySQLcmd.ExecuteReader
                MySQLdr.Read()
                If MySQLdr.HasRows Then
                    p = CType(MsgBox("No Pelanggan sudah terdaftar, " &
                                     "Apakah Ingin Melanjutkan???", vbYesNo), String)
                    If p = vbYes Then
                        MySQLdr.Close()

                        MySQLcmd = New MySqlCommand("DELETE FROM pelanggan WHERE NOPELANGGAN='" &
                                                  TextEdit1.Text & "'", MySQLconn)
                        MySQLcmd.ExecuteNonQuery()

                        MySQLcmd = New MySqlCommand("INSERT INTO pelanggan(NOPELANGGAN, " &
                                                  "NAMAPELANGGAN, " &
                                                  "ALAMAT1, " &
                                                  "ALAMAT2, " &
                                                  "KOTA, " &
                                                  "TELEPON, " &
                                                  "HP, " &
                                                  "EMAIL," &
                                                  "SALDO, " &
                                                  "PEMBAYARAN, " &
                                                  "PAJAK, " &
                                                  "NPWP, " &
                                                  "TINGKAT, " &
                                                  "STATUS, " &
                                                  "USERNAME)VALUES(@NOPELANGGAN, " &
                                                  "@NAMAPELANGGAN, " &
                                                  "@ALAMAT1, " &
                                                  "@ALAMAT2, " &
                                                  "@KOTA, " &
                                                  "@TELEPON, " &
                                                  "@HP, " &
                                                  "@EMAIL," &
                                                  "@SALDO, " &
                                                  "@PEMBAYARAN, " &
                                                  "@PAJAK, " &
                                                  "@NPWP, " &
                                                  "@TINGKAT, " &
                                                  "@STATUS, " &
                                                  "@USERNAME)", MySQLconn)
                        With MySQLcmd
                            .Parameters.AddWithValue("@NOPELANGGAN", TextEdit1.Text)
                            .Parameters.AddWithValue("@NAMAPELANGGAN", TextEdit2.Text)
                            .Parameters.AddWithValue("@ALAMAT1", TextEdit3.Text)
                            .Parameters.AddWithValue("@ALAMAT2", TextEdit4.Text)
                            .Parameters.AddWithValue("@KOTA", TextEdit5.Text)
                            .Parameters.AddWithValue("@TELEPON", TextEdit6.Text)
                            .Parameters.AddWithValue("@HP", TextEdit7.Text)
                            .Parameters.AddWithValue("@EMAIL", TextEdit8.Text)
                            .Parameters.AddWithValue("@SALDO", TextEdit9.EditValue)
                            .Parameters.AddWithValue("@PEMBAYARAN", LookUpEdit1.Text)
                            .Parameters.AddWithValue("@PAJAK", LookUpEdit2.Text)
                            .Parameters.AddWithValue("@NPWP", TextEdit10.Text)
                            .Parameters.AddWithValue("@TINGKAT", ComboBox1.Text)
                            .Parameters.AddWithValue("@STATUS", Status)
                            .Parameters.AddWithValue("@USERNAME", MenuUtama.BarStaticItem3.Caption)
                        End With
                        MySQLcmd.ExecuteNonQuery()

                        MsgBox("Data sudah disimpan...")
                        TextEdit1.Text = ""
                        TextEdit2.Text = ""
                        TextEdit3.Text = ""
                        TextEdit4.Text = ""
                        TextEdit5.Text = ""
                        TextEdit6.Text = ""
                        TextEdit7.Text = ""
                        TextEdit8.Text = ""
                        TextEdit9.Text = "0"
                        LookUpEdit1.Text = ""
                        LookUpEdit2.Text = ""
                        TextEdit10.Text = ""
                        ComboBox1.Text = ""

                    End If
                Else
                    MySQLdr.Close()
                    MySQLcmd = New MySqlCommand("INSERT INTO pelanggan(NOPELANGGAN, " &
                                                  "NAMAPELANGGAN, " &
                                                  "ALAMAT1, " &
                                                  "ALAMAT2, " &
                                                  "KOTA, " &
                                                  "TELEPON, " &
                                                  "HP, " &
                                                  "EMAIL," &
                                                  "SALDO, " &
                                                  "PEMBAYARAN, " &
                                                  "PAJAK, " &
                                                  "NPWP, " &
                                                  "TINGKAT, " &
                                                  "STATUS, " &
                                                  "USERNAME)VALUES(@NOPELANGGAN, " &
                                                  "@NAMAPELANGGAN, " &
                                                  "@ALAMAT1, " &
                                                  "@ALAMAT2, " &
                                                  "@KOTA, " &
                                                  "@TELEPON, " &
                                                  "@HP, " &
                                                  "@EMAIL," &
                                                  "@SALDO, " &
                                                  "@PEMBAYARAN, " &
                                                  "@PAJAK, " &
                                                  "@NPWP, " &
                                                  "@TINGKAT, " &
                                                  "@STATUS, " &
                                                  "@USERNAME)", MySQLconn)
                    With MySQLcmd
                        .Parameters.AddWithValue("@NOPELANGGAN", TextEdit1.Text)
                        .Parameters.AddWithValue("@NAMAPELANGGAN", TextEdit2.Text)
                        .Parameters.AddWithValue("@ALAMAT1", TextEdit3.Text)
                        .Parameters.AddWithValue("@ALAMAT2", TextEdit4.Text)
                        .Parameters.AddWithValue("@KOTA", TextEdit5.Text)
                        .Parameters.AddWithValue("@TELEPON", TextEdit6.Text)
                        .Parameters.AddWithValue("@HP", TextEdit7.Text)
                        .Parameters.AddWithValue("@EMAIL", TextEdit8.Text)
                        .Parameters.AddWithValue("@SALDO", TextEdit9.EditValue)
                        .Parameters.AddWithValue("@PEMBAYARAN", LookUpEdit1.Text)
                        .Parameters.AddWithValue("@PAJAK", LookUpEdit2.Text)
                        .Parameters.AddWithValue("@NPWP", TextEdit10.Text)
                        .Parameters.AddWithValue("@TINGKAT", ComboBox1.Text)
                        .Parameters.AddWithValue("@STATUS", Status)
                        .Parameters.AddWithValue("@USERNAME", MenuUtama.BarStaticItem3.Caption)
                    End With
                    MySQLcmd.ExecuteNonQuery()

                    MsgBox("Data sudah disimpan...")
                    TextEdit1.Text = ""
                    TextEdit2.Text = ""
                    TextEdit3.Text = ""
                    TextEdit4.Text = ""
                    TextEdit5.Text = ""
                    TextEdit6.Text = ""
                    TextEdit7.Text = ""
                    TextEdit8.Text = ""
                    TextEdit9.Text = "0"
                    LookUpEdit1.Text = ""
                    LookUpEdit2.Text = ""
                    TextEdit10.Text = ""
                    ComboBox1.Text = ""

                End If
            End If
            MySQLdr.Close()
            MySQLconn.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class