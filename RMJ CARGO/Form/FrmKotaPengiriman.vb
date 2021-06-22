Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient
Imports DevExpress.XtraEditors
Public Class FrmKotaPengiriman
    Private Sub FrmKotaPengiriman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        CreateListWilayah()
    End Sub
    Sub CreateListWilayah()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select Id,wilayah from wilayah order by wilayah ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("Id") = 0
            rsMaster("wilayah") = "<Baru>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "wilayah asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit1
                .Properties.DataSource = dtsource1
                .Properties.ShowHeader = True
                .Properties.ShowFooter = True
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.DisplayMember = "wilayah"
                .Properties.ValueMember = "Id"

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub LookUpEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles LookUpEdit1.EditValueChanged
        If LookUpEdit1.Text = "<Baru>" Then
            FrmWilayah.ShowDialog()
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            OpenConnectionMySQL()
            Dim ibukota As String
            If CheckEdit1.Checked = True Then
                ibukota = "1"
            Else
                ibukota = "0"
            End If
            MySQLcmd = New MySqlCommand("INSERT INTO kotapengiriman(kota," &
                                        "wilayah, " &
                                        "ibukota, " &
                                        "username)values(@kota, " &
                                        "@wilayah, " &
                                        "@ibukota, " &
                                        "@username)", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@kota", TextEdit1.Text)
                .Parameters.AddWithValue("@wilayah", LookUpEdit1.Text)
                .Parameters.AddWithValue("@ibukota", ibukota)
                .Parameters.AddWithValue("@username", MenuUtama.BarStaticItem3.Caption)
            End With
            MySQLcmd.ExecuteNonQuery()

            XtraMessageBox.Show("Data berhasil disimpan...")

            TextEdit1.Text = ""
            LookUpEdit1.Text = ""
            CheckEdit1.Checked = False

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class