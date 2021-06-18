Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient


Module ModuleSQL

    Public MySQLconn As MySqlConnection
    Public MySQLcmd As MySqlCommand
    Public MySQLda As MySqlDataAdapter
    Public MySQLdr As MySqlDataReader
    Public MySQLds As DataSet
    Public MySQLtable As DataTable
    Public Sub OpenConnectionMySQL()
        Dim rKeyMySQL As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\RmjCargo\Database\MySQL", True)
        Dim Server As String = Decrypt(rKeyMySQL.GetValue("Server").ToString)
        Dim Port As String = Decrypt(rKeyMySQL.GetValue("Port").ToString)
        Dim Database As String = rKeyMySQL.GetValue("Database").ToString
        Dim User As String = rKeyMySQL.GetValue("User").ToString
        Dim Pwd As String = Decrypt(rKeyMySQL.GetValue("Password").ToString)

        Dim sql As String = String.Format("Server={0};Port={1};Uid={3};Pwd={4};Database={2};" _
        & "SslMode=none", Server, Port, Database, User, Pwd)

        'MySQLconn = New MySqlConnection(My.Settings.ConnectionStringMySQL)
        MySQLconn = New MySqlConnection(sql)
        Try
            If MySQLconn.State = ConnectionState.Closed Then
                MySQLconn.Open()

            End If
        Catch ex As Exception
            'XtraMessageBox.AllowCustomLookAndFeel = True
            'XtraMessageBox.Show(Err.Description)
        End Try
    End Sub

End Module
