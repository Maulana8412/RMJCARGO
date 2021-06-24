Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient

Public Class FrmPengiriman
    Private Sub FrmPengiriman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        tNoPengiriman.ReadOnly = True
        tKotaTujuan.ReadOnly = True
        tKodePos.ReadOnly = True
        tHargaKg.ReadOnly = True
        tBeratMin.ReadOnly = True
        tHargaMin.ReadOnly = True
        tAsuransi.ReadOnly = True
        tTotal.ReadOnly = True

        deTanggal.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        deTanggal.Properties.DisplayFormat.FormatType = FormatType.DateTime
        deTanggal.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        deTanggal.Properties.EditFormat.FormatType = FormatType.DateTime
        deTanggal.Properties.Mask.EditMask = "dd/MM/yyyy"
        deTanggal.EditValue = Date.Now
        kotaAsal()
        NoPengiriman()
        CreateListPelanggan()
        CreateListTujuan()
        CreateListMarketing()
        luPelanggan.Select()
        SetFormatTextNumeric()
    End Sub
    Sub kotaAsal()
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("SELECT KOTA FROM perusahaan", MySQLconn)
            MySQLdr = MySQLcmd.ExecuteReader
            MySQLdr.Read()
            If MySQLdr.HasRows Then
                tKotaAsal.Text = CType(MySQLdr("KOTA"), String)
            End If
            MySQLdr.Close()
            MySQLconn.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub NoPengiriman()
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("select right(NOPENGIRIMAN,4) as NOPENGIRIMAN from parameter " &
                                        "where substring(NOPENGIRIMAN,6,4)='" _
                                        & Format(deTanggal.EditValue, "yyMM") &
                                        "' order by right(NOPENGIRIMAN,4) desc", MySQLconn)
            MySQLdr = MySQLcmd.ExecuteReader
            If MySQLdr.HasRows Then
                MySQLdr.Read()
                tNoPengiriman.Text = "RMJ." + Format(deTanggal.EditValue, "yyMM") + (Val(Trim(MySQLdr.Item(0).ToString)) + 1).ToString
            Else
                tNoPengiriman.Text = "RMJ." + Format(deTanggal.EditValue, "yyMM") + "1001"
            End If
            MySQLdr.Close()
            MySQLconn.Close()
        Catch ex As Exception
            'MsgBox("Error Notrans")
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub CreateListPelanggan()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select NOPELANGGAN,NAMAPELANGGAN from pelanggan order by NAMAPELANGGAN ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            'rsMaster("NOPELANGGAN") = 0
            'rsMaster("NAMAPELANGGAN") = "<Baru>"
            'dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "NAMAPELANGGAN asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With luPelanggan
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "NAMAPELANGGAN"
                .Properties.ValueMember = "NOPELANGGAN"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Sub CreateListTujuan()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,kota from kotapengiriman order by kota ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            'rsMaster("NOPELANGGAN") = 0
            'rsMaster("NAMAPELANGGAN") = "<Baru>"
            'dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "kota asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With luKotaTujuan
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
    Sub CreateListProduk()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,PRODUK from produk " &
                                           "WHERE DARI='" & tKotaAsal.Text _
                                           & "' AND KEPADA='" & luKotaTujuan.Text _
                                           & "' order by PRODUK ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            'rsMaster("NOPELANGGAN") = 0
            'rsMaster("NAMAPELANGGAN") = "<Baru>"
            'dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "PRODUK asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With luJenisService
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "PRODUK"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub CreateListMarketing()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select NIK,NAMA from karyawan order by NIK ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            'rsMaster("NOPELANGGAN") = 0
            'rsMaster("NAMAPELANGGAN") = "<Baru>"
            'dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "NAMA asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With luMarketing
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "NAMA"
                .Properties.ValueMember = "NIK"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Private Sub luKotaTujuan_EditValueChanged(sender As Object, e As EventArgs) Handles luKotaTujuan.EditValueChanged
        luJenisService.Properties.DataSource = ""
        CreateListProduk()
        Try
            OpenConnectionMySQL()
            MySQLcmd = New MySqlCommand("SELECT kota,kodepos FROM kotapengiriman WHERE kota='" & luKotaTujuan.Text & "' ", MySQLconn)
            MySQLdr = MySQLcmd.ExecuteReader
            MySQLdr.Read()
            If MySQLdr.HasRows Then
                tKotaTujuan.Text = CType(MySQLdr("kota"), String)
                tKodePos.Text = CType(MySQLdr("kodepos"), String)
            End If
            MySQLdr.Close()
            MySQLconn.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub luPelanggan_EditValueChanged(sender As Object, e As EventArgs) Handles luPelanggan.EditValueChanged
        Try
            OpenConnectionMySQL()
            Dim pajak As String
            Dim pembayaran As String
            tPajak.Text = ""
            tNamaPajak.Text = ""
            MySQLcmd = New MySqlCommand("SELECT NAMAPELANGGAN, " &
                                        "ALAMAT1, " &
                                        "TELEPON, " &
                                        "PEMBAYARAN, " &
                                        "TINGKAT, " &
                                        "PAJAK FROM pelanggan " &
                                        "WHERE NOPELANGGAN=@NOPELANGGAN", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@NOPELANGGAN", luPelanggan.EditValue)
            End With
            MySQLdr = MySQLcmd.ExecuteReader
            MySQLdr.Read()
            If MySQLdr.HasRows Then
                tPengirim.Text = CType(MySQLdr("NAMAPELANGGAN"), String)
                tAlamatPengirim.Text = CType(MySQLdr("ALAMAT1"), String)
                tTelpPengirim.Text = CType(MySQLdr("TELEPON"), String)
                pembayaran = CType(MySQLdr("PEMBAYARAN"), String)
                tTingkat.Text = CType(MySQLdr("TINGKAT"), String)
                pajak = CType(MySQLdr("PAJAK"), String)

                MySQLdr.Close()
                MySQLcmd = New MySqlCommand("SELECT NAMA,PERSEN FROM pajak WHERE NAMA='" & pajak & "'", MySQLconn)
                MySQLdr = MySQLcmd.ExecuteReader
                MySQLdr.Read()
                If MySQLdr.HasRows Then
                    tNamaPajak.Text = CType(MySQLdr("NAMA"), String)
                    tPajak.Text = CType(MySQLdr("PERSEN"), String)
                End If

                MySQLdr.Close()
                MySQLcmd = New MySqlCommand("SELECT PELUNASAN FROM pembayaran WHERE NAMA='" & pembayaran & "'", MySQLconn)
                MySQLdr = MySQLcmd.ExecuteReader
                MySQLdr.Read()
                If MySQLdr.HasRows Then
                    tPembayaran.Text = CType(MySQLdr("PELUNASAN"), String)
                End If

            End If

            If tPajak.Text = "" Then
                tPajak.Text = "0"
            End If
            If tNamaPajak.Text = "" Then
                tNamaPajak.Text = "-"
            End If

            MySQLdr.Close()
            MySQLconn.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub SetFormatTextNumeric()
        With tBerat
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
        With tKoli
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
        With tHargaKg
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
        With tBeratMin
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
        With tHargaMin
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
        With tNilaiBarang
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
        With tAsuransi
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
        With tBPacking
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
        With tTotal
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
        With tTotalPajak
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

    Private Sub luJenisService_EditValueChanged(sender As Object, e As EventArgs) Handles luJenisService.EditValueChanged
        OpenConnectionMySQL()
        Try
            MySQLcmd = New MySqlCommand("SELECT *FROM produk WHERE PRODUK=@PRODUK AND " &
                                        "DARI=@DARI AND KEPADA=@KEPADA", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@PRODUK", luJenisService.Text)
                .Parameters.AddWithValue("@DARI", tKotaAsal.Text)
                .Parameters.AddWithValue("@KEPADA", luKotaTujuan.Text)
            End With
            MySQLdr = MySQLcmd.ExecuteReader
            MySQLdr.Read()
            If MySQLdr.HasRows Then
                tBeratMin.EditValue = MySQLdr("BERATMIN")
                If tTingkat.Text = "1" Then
                    tHargaKg.EditValue = MySQLdr("HARGAPERKG")
                    tHargaMin.EditValue = MySQLdr("HARGAMIN")
                ElseIf tTingkat.Text = "2" Then
                    tHargaKg.EditValue = MySQLdr("HARGAPERKG2")
                    tHargaMin.EditValue = MySQLdr("HARGAMIN2")
                ElseIf tTingkat.Text = "3" Then
                    tHargaKg.EditValue = MySQLdr("HARGAPERKG3")
                    tHargaMin.EditValue = MySQLdr("HARGAMIN3")
                ElseIf tTingkat.Text = "4" Then
                    tHargaKg.EditValue = MySQLdr("HARGAPERKG4")
                    tHargaMin.EditValue = MySQLdr("HARGAMIN4")
                ElseIf tTingkat.Text = "5" Then
                    tHargaKg.EditValue = MySQLdr("HARGAPERKG5")
                    tHargaMin.EditValue = MySQLdr("HARGAMIN5")
                End If
            End If
        Catch ex As Exception

        End Try
        MySQLdr.Close()
        MySQLconn.Close()
    End Sub
End Class