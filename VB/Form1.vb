Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Imports DevExpress.XtraLayout
Imports DevExpress.XtraEditors


Namespace WindowsApplication56
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'nwindDataSet.Categories' table. You can move, or remove it, as needed.
			Me.categoriesTableAdapter.Fill(Me.nwindDataSet.Categories)

			dataLayoutControl1.RetrieveFields()

			For Each baseItem As BaseLayoutItem In dataLayoutControl1.Items
				Dim item As LayoutControlItem = TryCast(baseItem, LayoutControlItem)
				If item IsNot Nothing AndAlso item.Control.DataBindings.Count > 0 Then
					If item.Control.DataBindings(0).BindingMemberInfo.BindingField = "Description" Then
						dataLayoutControl1.BeginUpdate()
						Dim prevControl As Control = item.Control
						Dim binding As Binding = prevControl.DataBindings(0)
						prevControl.DataBindings.Clear()
						dataLayoutControl1.Controls.Remove(prevControl)
						Dim newControl As Control = New MemoEdit()
						newControl.Name = "myMemoEdit"
						' Bind the new control to the same field as the previous control.
						newControl.DataBindings.Add(New Binding(binding.PropertyName, binding.DataSource, binding.BindingMemberInfo.BindingField, binding.FormattingEnabled))
						dataLayoutControl1.Controls.Add(newControl)
						item.Control = newControl
						prevControl.Dispose()
						dataLayoutControl1.EndUpdate()
						' Change the item's size after the EndUpdate method.
						item.Size = New Size(100, 80)
						Exit For
					End If
				End If
			Next baseItem
		End Sub
	End Class
End Namespace