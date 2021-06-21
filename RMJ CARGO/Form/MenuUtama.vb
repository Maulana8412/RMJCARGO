Imports System.ComponentModel

Public Class MenuUtama


    Private Sub MenuUtama_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        SettingDBB.Close()
        FrmLogin.Close()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MPerusahaan.ItemClick
        FrmPerusahaan.Show()
    End Sub

    Private Sub MKotaPengiriman_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MKotaPengiriman.ItemClick
        FrmKotaPengirimanList.Show()
    End Sub
End Class