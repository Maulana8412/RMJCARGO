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

    Private Sub BarButtonItem1_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FrmProdukList.Show()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        FrmPelangganList.Show()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        FrmKaryawanList.Show()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        FrmPengirimanList.Show()
    End Sub
End Class