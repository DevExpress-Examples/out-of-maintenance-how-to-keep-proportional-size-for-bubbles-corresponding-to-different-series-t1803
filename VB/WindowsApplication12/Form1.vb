Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports System.Linq

Namespace WindowsApplication12
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            chartControl1.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Bubble)
            chartControl1.SeriesTemplate.ArgumentDataMember = "Date"
            chartControl1.SeriesDataMember = "Name"
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() { "Value", "Weight" })
            chartControl1.DataSource = CreateTable()

        End Sub

        Private Function CreateTable() As DataTable
            Dim data As New DataTable()
            data.Columns.Add("Name", GetType(String))
            data.Columns.Add("Date", GetType(Date))
            data.Columns.Add("Value", GetType(Double))
            data.Columns.Add("Weight", GetType(Double))

            data.Rows.Add(New Object() { "Aaa", Date.Today.AddDays(0), 7, 4 })
            data.Rows.Add(New Object() { "Aaa", Date.Today.AddDays(1), 17, 1 })
            data.Rows.Add(New Object() { "Aaa", Date.Today.AddDays(2), 11, 5 })

            data.Rows.Add(New Object() { "Bbb", Date.Today.AddDays(0), 5, 6 })
            data.Rows.Add(New Object() { "Bbb", Date.Today.AddDays(1), 13, 9 })
            data.Rows.Add(New Object() { "Bbb", Date.Today.AddDays(2), 9, 5 })

            data.Rows.Add(New Object() { "Ccc", Date.Today.AddDays(0), 9, 4 })
            data.Rows.Add(New Object() { "Ccc", Date.Today.AddDays(1), 8, 6 })
            data.Rows.Add(New Object() { "Ccc", Date.Today.AddDays(2), 7, 5 })

            Return data
        End Function

        Private Sub chartControl1_BoundDataChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chartControl1.BoundDataChanged
            Dim chart As ChartControl = DirectCast(sender, ChartControl)
            Dim infos = chart.Series.Cast(Of Series)().Where(Function(s) TryCast(s.View, BubbleSeriesView) IsNot Nothing).Select(Function(s) New SeriesInfo() With {.View = TryCast(s.View, BubbleSeriesView), .Min = s.Points.Cast(Of SeriesPoint)().Min(Function(p) p.Values(1)), .Max = s.Points.Cast(Of SeriesPoint)().Max(Function(p) p.Values(1))})
            If infos.Count() > 1 Then
                Dim minValue As Double = infos.Min(Function(s) s.Min)
                Dim maxValue As Double = infos.Max(Function(s) s.Max)
                Dim valueDelta As Double = maxValue - minValue
                Dim minSize As Double = infos.Min(Function(s) s.View.MinSize)
                Dim maxSize As Double = infos.Max(Function(s) s.View.MaxSize)
                Dim sizeDelta As Double = maxSize - minSize
                For Each si In infos
                    si.View.MaxSize = maxSize
                    si.View.MinSize = minSize
                    si.View.MaxSize = maxSize - sizeDelta * (maxValue - si.Max) / valueDelta
                    si.View.MinSize = minSize + sizeDelta * (si.Min - minValue) / valueDelta
                Next si
            End If

        End Sub

    End Class
    Public Class SeriesInfo
        Public Property View() As BubbleSeriesView
        Public Property Min() As Double
        Public Property Max() As Double
    End Class
End Namespace