Imports System.Globalization
Imports DevExpress.XtraEditors
Imports System.Media
Module ModuleFunction
    Public ciID As CultureInfo = New CultureInfo("id-ID")
    Public ciUS As CultureInfo = New CultureInfo("en-US")
    Public CustomFormatCurrency As String = "#,###,###,##0.####"
    Public CustomFormatDec0 As String = "#,###,###,##0.####;(#,###,###,##0.####)"
    Public CustomFormatDec1 As String = "#,###,###,##0.####;(#,###,###,##0.####)"
    Public CustomFormatDec2 As String = "#,###,###,##0.####;(#,###,###,##0.####)"
    Public CustomFormatDec3 As String = "#,###,###,##0.####;(#,###,###,##0.####)"

    'Public CustomFormatCurrency As String = "#.###.###.###,##"
    'Public CustomFormatDec0 As String = "#.###.###.###"
    'Public CustomFormatDec1 As String = "#.###.###.###,#"
    'Public CustomFormatDec2 As String = "#.###.###.###,##"
    'Public CustomFormatDec3 As String = "#.###.###.###,###"

    'DATATABLE PRIMARY KEY
    Public Sub SetPrimaryKey(ByVal value As DataTable, ByVal columnName As String)
        value.PrimaryKey = New DataColumn() {value.Columns(columnName)}
    End Sub
    Public Function FindByPrimaryKey(ByVal value As DataTable, ByVal key As Object) As DataRow
        Return value.Rows.Find(key)
    End Function

    Public Function checkNull(ByVal variable As Object) As String
        If IsDBNull(variable) Then
            Return ("")
        Else
            Return (CStr(variable))
        End If

    End Function
    Public Function checkNullDecimal(ByVal variable As Object) As Decimal
        If IsDBNull(variable) Then
            Return (0)
        Else
            Return (CDec(variable))
        End If
    End Function
#Region "GetDayOfMonth"
    'Get the first day of the month
    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Return New DateTime(sourceDate.Year, sourceDate.Month, 1)
    End Function

    'Get the last day of the month
    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function
#End Region

#Region "FunctionString"
    Function BetweenText(value As String, a As String,
                     b As String) As String
        ' Get positions for both string arguments.
        Dim posA As Integer = value.IndexOf(a)
        Dim posB As Integer = value.LastIndexOf(b)
        If posA = -1 Then
            Return ""
        End If
        If posB = -1 Then
            Return ""
        End If

        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= posB Then
            Return ""
        End If

        ' Get the substring between the two positions.
        Return value.Substring(adjustedPosA, posB - adjustedPosA)
    End Function

    Function BeforeText(value As String, a As String) As String
        ' Get index of argument and return substring up to that point.
        Dim posA As Integer = value.IndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Return value.Substring(0, posA)
    End Function

    Function AfterText(value As String, a As String) As String
        ' Get index of argument and return substring after its position.
        Dim posA As Integer = value.LastIndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= value.Length Then
            Return ""
        End If
        Return value.Substring(adjustedPosA)
    End Function

    'Sub Main()
    '    Dim test As String = "DEFINE:A=TWO"
    '    ' Test the Between Function.
    '    Console.WriteLine(Between(test, "DEFINE:", "="))
    '    Console.WriteLine(Between(test, ":", "="))

    '    ' Test the Before Function.
    '    Console.WriteLine(Before(test, ":"))
    '    Console.WriteLine(Before(test, "="))

    '    ' Test the After Function.
    '    Console.WriteLine(After(test, ":"))
    '    Console.WriteLine(After(test, "DEFINE:"))
    '    Console.WriteLine(After(test, "="))
    'End Sub
#End Region

#Region "Function WorkingDayElapsed"
    Public Function WorkingDaysElapsed(ByVal pFromDate As Date, ByVal pToDate As Date) As Integer

        Dim _elapsedDays As Integer = 0
        Dim _weekendDays As DayOfWeek() = {DayOfWeek.Sunday}
        Dim i
        For i = 0 To (pToDate - pFromDate).Days
            If Not _weekendDays.Contains(pFromDate.AddDays(i).DayOfWeek) Then _elapsedDays += 1
        Next

        Return _elapsedDays
    End Function
#End Region

#Region "GetDayWorkExcludeSunday"

    Public Function GetDaysBetweenDatesExcludingWeekendsExt(ByVal dFrom As Date, ByVal dTo As Date,
                                Optional ByVal DaysOff As List(Of Date) = Nothing) As Integer

        Try

            Dim ts As TimeSpan = dTo - dFrom
            Dim weeks As Integer = ts.Days \ 7
            Dim weekdays As Integer = weeks * 5
            Dim dy As DayOfWeek
            Dim d

            For i As Integer = (weeks * 7) + 1 To ts.Days
                dy = dFrom.AddDays(i).DayOfWeek

                'If dy = DayOfWeek.Saturday AndAlso dy = DayOfWeek.Sunday Then
                '    weekdays += 1
                'End If

                If dy = DayOfWeek.Sunday Then
                    weekdays += 1
                End If

            Next

            If Not DaysOff Is Nothing Then
                For Each d In DaysOff
                    dy = d.DayOfWeek
                    'If d > dFrom AndAlso d < dTo AndAlso dy <> DayOfWeek.Saturday _
                    '    AndAlso dy <> DayOfWeek.Sunday Then
                    '    weekdays -= 1
                    'End If
                    If d > dFrom AndAlso dy <> DayOfWeek.Sunday Then
                        weekdays -= 1
                    End If
                Next
            End If
            Return weekdays

        Catch ex As Exception
            XtraMessageBox.Show(ErrorToString, "Error On Count Day Work", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function
#End Region
End Module