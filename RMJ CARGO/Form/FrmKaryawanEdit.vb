Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient

Public Class FRMKARYAWANEdit
    Private Sub FRMKARYAWANEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama

        DateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        DateEdit1.Properties.DisplayFormat.FormatType = FormatType.DateTime
        DateEdit1.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        DateEdit1.Properties.EditFormat.FormatType = FormatType.DateTime
        DateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy"
        DateEdit1.EditValue = Date.Now
        CreateListJabatan()

    End Sub

    Sub CreateListJabatan()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,NAMA from jabatan order by NAMA ", MySQLconn)
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

    Private Sub LookUpEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles LookUpEdit1.EditValueChanged
        If LookUpEdit1.Text = "<Baru>" Then
            FrmJabatan.ShowDialog()
        End If
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub TextEdit2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit2.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        OpenConnectionMySQL()
        Dim p As String
        Try
            If TextEdit1.Text = "" Then
                MsgBox("NIK Karyawan belum di isi...")
            ElseIf TextEdit2.Text = "" Then
                MsgBox("Nama Karyawan belum di isi...")
            ElseIf LookUpEdit1.Text = "" Then
                MsgBox("Jabatan belum di pilih...")
            Else
                MySQLcmd = New MySqlCommand("UPDATE karyawan SET NAMA=@NAMA, " &
                                                             "TGLMASUK=@TGLMASUK, " &
                                                             "JABATAN=@JABATAN, " &
                                                             "USERNAME=@USERNAME " &
                                                             "WHERE NIK=@NIK", MySQLconn)
                With MySQLcmd
                    .Parameters.AddWithValue("@NIK", TextEdit1.Text)
                    .Parameters.AddWithValue("@NAMA", TextEdit2.Text)
                    .Parameters.AddWithValue("@TGLMASUK", Format(DateEdit1.EditValue, "yyyy-MM-dd"))
                    .Parameters.AddWithValue("@JABATAN", LookUpEdit1.Text)
                    .Parameters.AddWithValue("@USERNAME", MenuUtama.BarStaticItem3.Caption)
                End With
                MySQLcmd.ExecuteNonQuery()

                MsgBox("Data sudah diperbarui...")
            End If
            MySQLconn.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class