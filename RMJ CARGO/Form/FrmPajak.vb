Imports DevExpress.Utils
Imports MySql.Data.MySqlClient

Public Class FrmPajak
    Private Sub FrmPajak_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = MenuUtama
        SetFormatTextNumeric()
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
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        OpenConnectionMySQL()
        If TextEdit2.Text = "0" Then
            TextEdit2.EditValue = 0
        End If
        Try
            MySQLcmd = New MySqlCommand("DELETE FROM pajak WHERE NAMA='" & TextEdit1.Text & "'", MySQLconn)
            MySQLcmd.ExecuteNonQuery()

            MySQLcmd = New MySqlCommand("INSERT INTO pajak (NAMA, " &
                                      "PERSEN)VALUES(@NAMA, " &
                                      "@PERSEN)", MySQLconn)
            With MySQLcmd
                .Parameters.AddWithValue("@NAMA", TextEdit1.Text)
                .Parameters.AddWithValue("@PERSEN", TextEdit2.EditValue)
            End With
            MySQLcmd.ExecuteNonQuery()
            MsgBox("Data sudah disimpan...")
            TextEdit1.Text = ""
            TextEdit2.Text = "0"

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub
End Class