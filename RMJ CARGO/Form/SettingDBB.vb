Imports System.ComponentModel
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient


Public Class SettingDBB

    Sub HapusDatabaseMySQL()
        Dim rKeyMySQL1 As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\RmjCargo\Database", True)
        rKeyMySQL1.DeleteSubKey("MySQL", True)
    End Sub
    Sub BuatDatabaseMySQL()
        Dim rKeyMySQL1 As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\RmjCargo\Database", True)
        Dim rKeyMySQL3 As RegistryKey = rKeyMySQL1.CreateSubKey("MySQL", RegistryKeyPermissionCheck.ReadWriteSubTree)
        rKeyMySQL3.SetValue("Server", Encrypt(TextEdit3.Text), RegistryValueKind.String)
        rKeyMySQL3.SetValue("Port", Encrypt(TextEdit4.Text), RegistryValueKind.String)
        rKeyMySQL3.SetValue("User", TextEdit5.Text, RegistryValueKind.String)
        rKeyMySQL3.SetValue("Password", Encrypt(TextEdit6.Text), RegistryValueKind.String)
        rKeyMySQL3.SetValue("Database", LookUpEdit1.Text, RegistryValueKind.String)
        rKeyMySQL3.Close()
        rKeyMySQL1.Close()
    End Sub


    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try

            If TextEdit3.Text = "" Then
                MsgBox("Nama Server untuk MySQL belum di isi...")
                Exit Sub
            End If
            If TextEdit5.Text = "" Then
                MsgBox("User untuk MySQL belum di isi...")
                Exit Sub
            End If
            If TextEdit6.Text = "" Then
                MsgBox("Password untuk MySQL belum di isi...")
                Exit Sub
            End If
            If LookUpEdit1.Text = "" Then
                MsgBox("Database untuk MySQL belum di isi...")
                Exit Sub
            End If
            If TextEdit4.Text = "" Then
                MsgBox("Port untuk MySQL belum di isi...")
                Exit Sub
            End If

            Dim rKeyMySQL As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\PayRollSici\Database\MySQL", True)

            If rKeyMySQL Is Nothing Then
                BuatDatabaseMySQL()
            Else
                HapusDatabaseMySQL()
                BuatDatabaseMySQL()
            End If

            MsgBox("Program akan di tutup, untuk memperbarui setting database...", vbOKOnly, "Setting Database")
            Me.Close()
            MenuUtama.Close()
            FrmLogin.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub


    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        If Application.OpenForms().OfType(Of MenuUtama).Any Then
            Me.Dispose()
        Else
            Me.Dispose()
            FrmLogin.Close
        End If
    End Sub





    Private Sub LookUpEdit1_GotFocus(sender As Object, e As EventArgs) Handles LookUpEdit1.GotFocus
        Try
            Dim list As List(Of String) = New List(Of String)
            Dim conString As String = "Server=" & TextEdit3.Text & ";Port=" & TextEdit4.Text & ";Uid=" & TextEdit5.Text & ";Pwd=" & TextEdit6.Text & ";SslMode = none"

            Using con As MySqlConnection = New MySqlConnection(conString)
                con.Open()

                Using cmd As MySqlCommand = New MySqlCommand("SHOW DATABASES", con)

                    Using dr As IDataReader = cmd.ExecuteReader()

                        While dr.Read()
                            list.Add(dr(0).ToString())
                            LookUpEdit1.Properties.DataSource = list
                        End While
                    End Using
                End Using

            End Using
        Catch ex As Exception
            'XtraMessageBox.AllowCustomLookAndFeel = True
            'XtraMessageBox.Show(Err.Description)

        End Try
    End Sub

End Class

