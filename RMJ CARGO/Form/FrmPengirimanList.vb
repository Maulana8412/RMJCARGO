Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports MySql.Data.MySqlClient
Public Class FrmPengirimanList
    Private Sub FrmPengirimanList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        SetStartDateEndDate()

    End Sub
    Sub SetStartDateEndDate()
        'Try
        Dim original As DateTime = DateTime.Now
        DateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        DateEdit1.Properties.DisplayFormat.FormatType = FormatType.DateTime
        DateEdit1.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        DateEdit1.Properties.EditFormat.FormatType = FormatType.DateTime
        DateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy"
        DateEdit1.EditValue = CDate(Format(original.Date.AddMonths(0), "MM/01/yyyy"))

        DateEdit2.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        DateEdit2.Properties.DisplayFormat.FormatType = FormatType.DateTime
        DateEdit2.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        DateEdit2.Properties.EditFormat.FormatType = FormatType.DateTime
        DateEdit2.Properties.Mask.EditMask = "dd/MM/yyyy"
        DateEdit2.EditValue = DateTime.Now

        CreateListPelanggan()
        CheckEdit1.Checked = True

    End Sub
    Sub CreateListPelanggan()
        Try
            OpenConnectionMySQL()
            MySQLda = New MySqlDataAdapter("select NOPELANGGAN,NAMAPELANGGAN from pelanggan order by NAMAPELANGGAN ", MySQLconn)
            Dim dtsource As New DataTable
            MySQLda.Fill(dtsource)
            MySQLconn.Close()

            Dim rsMaster As DataRow = dtsource.NewRow

            rsMaster("NOPELANGGAN") = 0
            rsMaster("NAMAPELANGGAN") = "<Semua Pelanggan>"
            dtsource.Rows.Add(rsMaster)

            Dim dv As New DataView(dtsource) With {.Sort = "NAMAPELANGGAN asc"}
            Dim dtsource1 As DataTable = dv.ToTable

            With LookUpEdit1
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

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FrmPengiriman.Show()
    End Sub
End Class