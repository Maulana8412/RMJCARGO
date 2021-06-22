Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient

Public Class FrmProdukEdit
    Private Sub FrmProdukEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        CreateListKota()
        CreateListKota2()
        CreateListSatuan()
        SetFormatTextNumeric()
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

    Sub CreateListKota2()
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

            With LookUpEdit2
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

    Sub CreateListSatuan()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,satuan from satuan order by satuan ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("Id") = 0
            rsMaster("satuan") = "<Baru>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "satuan asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit3
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "satuan"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub LookUpEdit3_EditValueChanged_1(sender As Object, e As EventArgs) Handles LookUpEdit3.EditValueChanged
        If LookUpEdit3.Text = "<Baru>" Then
            FrmSatuan.ShowDialog()
        End If
    End Sub

    Sub SetFormatTextNumeric()
        With TextEdit2
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

        With TextEdit4
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
        With TextEdit5
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
        With TextEdit6
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
        With TextEdit7
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
        With TextEdit8
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
        With TextEdit10
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
        With TextEdit11
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
        With TextEdit12
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
        With TextEdit13
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
        Dim p As String
        OpenConnectionMySQL()

        If TextEdit6.Text = "0" Then
            TextEdit6.EditValue = 0
        End If
        If TextEdit7.Text = "0" Then
            TextEdit7.EditValue = 0
        End If
        If TextEdit8.Text = "0" Then
            TextEdit8.EditValue = 0
        End If
        If TextEdit9.Text = "0" Then
            TextEdit9.EditValue = 0
        End If
        If TextEdit10.Text = "0" Then
            TextEdit10.EditValue = 0
        End If
        If TextEdit11.Text = "0" Then
            TextEdit11.EditValue = 0
        End If
        If TextEdit12.Text = "0" Then
            TextEdit12.EditValue = 0
        End If
        If TextEdit13.Text = "0" Then
            TextEdit13.EditValue = 0
        End If

        If TextEdit1.Text = "" Then
            MsgBox("Produk belum di isi...")
        ElseIf LookUpEdit1.Text = "" Then
            MsgBox("Kota Asal belum di pilih...")
        ElseIf LookUpEdit2.Text = "" Then
            MsgBox("Kota Tujuan belum di pilih...")
        ElseIf LookUpEdit1.Text = LookUpEdit2.Text Then
            MsgBox("Kota Asal tidak boleh sama dengan kota tujuan...")
        ElseIf TextEdit2.Text = "0" Then
            MsgBox("Berat Minimal belum di isi...")
        ElseIf LookUpEdit3.Text = "" Then
            MsgBox("Satuan belum di pilih...")
        ElseIf TextEdit4.Text = "0" Then
            MsgBox("Harga Minimum 1 belum di isi...")
        ElseIf TextEdit5.Text = "0" Then
            MsgBox("Harga Per Kg/Ton 1 belum di isi...")
        Else
            Try
                MySQLcmd = New MySqlCommand("UPDATE produk set BERATMIN=@BERATMIN, " &
                                          "VIA=@VIA, " &
                                          "SATUAN=@SATUAN, " &
                                          "HARGAMIN=@HARGAMIN, " &
                                          "HARGAPERKG=@HARGAPERKG, " &
                                          "HARGAMIN2=@HARGAMIN2, " &
                                          "HARGAPERKG2=@HARGAPERKG2, " &
                                          "HARGAMIN3=@HARGAMIN3, " &
                                          "HARGAPERKG3=@HARGAPERKG3, " &
                                          "HARGAMIN4=@HARGAMIN4, " &
                                          "HARGAPERKG4=@HARGAPERKG4, " &
                                          "HARGAMIN5=@HARGAMIN5, " &
                                          "HARGAPERKG5=@HARGAPERKG5 " &
                                          "WHERE PRODUK=@PRODUK " &
                                          "AND DARI=@DARI " &
                                          "AND KEPADA=@KEPADA ", MySQLconn)
                With MySQLcmd
                    .Parameters.AddWithValue("@PRODUK", TextEdit1.Text)
                    .Parameters.AddWithValue("@DARI", LookUpEdit1.Text)
                    .Parameters.AddWithValue("@KEPADA", LookUpEdit2.Text)
                    .Parameters.AddWithValue("@BERATMIN", TextEdit2.EditValue)
                    .Parameters.AddWithValue("@VIA", TextEdit3.Text)
                    .Parameters.AddWithValue("@SATUAN", LookUpEdit3.Text)
                    .Parameters.AddWithValue("@HARGAMIN", TextEdit4.EditValue)
                    .Parameters.AddWithValue("@HARGAPERKG", TextEdit5.EditValue)
                    .Parameters.AddWithValue("@HARGAMIN2", TextEdit6.EditValue)
                    .Parameters.AddWithValue("@HARGAPERKG2", TextEdit7.EditValue)
                    .Parameters.AddWithValue("@HARGAMIN3", TextEdit8.EditValue)
                    .Parameters.AddWithValue("@HARGAPERKG3", TextEdit9.EditValue)
                    .Parameters.AddWithValue("@HARGAMIN4", TextEdit10.EditValue)
                    .Parameters.AddWithValue("@HARGAPERKG4", TextEdit11.EditValue)
                    .Parameters.AddWithValue("@HARGAMIN5", TextEdit12.EditValue)
                    .Parameters.AddWithValue("@HARGAPERKG5", TextEdit13.EditValue)
                    .Parameters.AddWithValue("@USERNAME", MenuUtama.BarStaticItem3.Caption)
                End With

                MySQLcmd.ExecuteNonQuery()
                MsgBox("Data sudah diperbarui...")

                MySQLconn.Close()
            Catch ex As Exception
                MsgBox(Err.Description)
            End Try
        End If
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class