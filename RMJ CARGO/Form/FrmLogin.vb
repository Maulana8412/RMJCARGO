Imports System.ComponentModel
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports DevExpress.XtraEditors

Imports Microsoft.Win32

Public Class FrmLogin

    Dim bl As String
    Dim thn As String
    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RmjCargo", True) Is Nothing Then
            Registry.CurrentUser.OpenSubKey("SOFTWARE", True).CreateSubKey("RmjCargo", RegistryKeyPermissionCheck.ReadWriteSubTree)
        End If

        If Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RmjCargo", True).OpenSubKey("Database", True) Is Nothing Then
            Registry.CurrentUser.OpenSubKey("SOFTWARE", True).OpenSubKey("RmjCargo", True).CreateSubKey("Database", RegistryKeyPermissionCheck.ReadWriteSubTree)
        End If

        'Try
        'Dim rKeyMySQL As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\RmjCargo\Database\MySQL", True)
        'Dim Server As String = Decrypt(rKeyMySQL.GetValue("Server").ToString)
        'Dim Port As String = Decrypt(rKeyMySQL.GetValue("Port").ToString)
        'Dim Database As String = rKeyMySQL.GetValue("Database").ToString
        'Dim User As String = rKeyMySQL.GetValue("User").ToString
        'Dim Pwd As String = Decrypt(rKeyMySQL.GetValue("Password").ToString)

        'Dim sql As String = String.Format("Server={0};Port={1};Uid={3};Pwd={4};Database={2};" _
        '& "SslMode=none", Server, Port, Database, User, Pwd)



        'MySQLconn = New MySqlConnection(sql)
        'If MySQLconn.State = ConnectionState.Closed Then
        '    MySQLconn.Open()
        'End If
        OpenConnectionMySQL()

            'Catch ex As Exception
            '    MsgBox(Err.Description)
            '    Me.Hide()
            '    SettingDBB.ShowDialog()

            'End Try

            'SQLconn.Close()

            'Dim rKeyMySQL1 As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\IndieCashier\Database", True)
            'Dim rKeyMySQL2 As RegistryKey = rKeyMySQL1.CreateSubKey("SQL", RegistryKeyPermissionCheck.ReadWriteSubTree)
            'Dim rKeyMySQL3 As RegistryKey = rKeyMySQL1.CreateSubKey("MySQL", RegistryKeyPermissionCheck.ReadWriteSubTree)



            txtUserName.Text = ""
        txtPass.Text = ""
        'SettingDBSQL.Hide()
        'DBAlias.Hide()



    End Sub


    Private Sub btnLogin_Click1(sender As Object, e As EventArgs) Handles btnLogin.Click

        If txtUserName.Text = "" Or txtPass.Text = "" Then
            XtraMessageBox.AllowCustomLookAndFeel = True
            XtraMessageBox.Show("Data yang Anda masukkan belum lengkap !!!")
        Else
            OpenConnectionMySQL()

            Dim str As String
            str = "select *From userprogram where username='" & txtUserName.Text & "' and password='" & txtPass.Text & "'"
            MySQLcmd = New MySqlCommand(str, MySQLconn)
            MySQLdr = MySQLcmd.ExecuteReader
            If MySQLdr.HasRows Then
                MySQLdr.Close()


                'MenuUtama.BarStaticItem1.Caption = "Periode : " + Format(Now, "MM/yyyy")
                'MenuUtama.BarStaticItem2.Caption = "Pengguna :"
                'MenuUtama.BarStaticItem3.Caption = txtUserName.Text

                MenuUtama.Show()

                Me.Hide()
                'SettingDBSQL.Close()
                'DBAlias.Close()
                MySQLdr.Close()
                MySQLconn.Close()
            Else
                XtraMessageBox.AllowCustomLookAndFeel = True
                XtraMessageBox.Show("Username atau password tidak terdaftar !!!")
                txtPass.Text = ""
                txtPass.Focus()
            End If
        End If
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()

    End Sub

    Private Sub txtPass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtUserName.Text = "" Or txtPass.Text = "" Then
                XtraMessageBox.AllowCustomLookAndFeel = True
                XtraMessageBox.Show("Data yang Anda masukkan belum lengkap !!!")
            Else
                OpenConnectionMySQL()

                Dim str As String
                str = "Select *From userprogram where username='" & txtUserName.Text & "' and password='" & txtPass.Text & "'"
                MySQLcmd = New MySqlCommand(str, MySQLconn)
                MySQLdr = MySQLcmd.ExecuteReader
                If MySQLdr.HasRows Then
                    MySQLdr.Close()

                    'MenuUtama.BarStaticItem1.Caption = "Periode : " + Format(Now, "MM/yyyy")
                    'MenuUtama.BarStaticItem2.Caption = "Pengguna :"
                    'MenuUtama.BarStaticItem3.Caption = txtUserName.Text
                    MenuUtama.Show()


                    Me.Hide()
                    'SettingDBSQL.Close()
                    'DBAlias.Close()
                    MySQLdr.Close()
                    MySQLconn.Close()
                Else
                    XtraMessageBox.AllowCustomLookAndFeel = True
                    XtraMessageBox.Show("Username atau password tidak terdaftar !!!")
                    txtPass.Text = ""
                    txtPass.Focus()
                End If
            End If
        End If


    End Sub

    Private Sub txtPass_Resize(sender As Object, e As EventArgs) Handles txtPass.Resize

    End Sub


    Private Sub txtPass_TextChanged(sender As Object, e As EventArgs) Handles txtPass.TextChanged

    End Sub

    Private Sub txtUserName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUserName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPass.Focus()
        End If
    End Sub

    Private Sub txtUserName_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub FrmLogin_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FrmLogin_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub
End Class
