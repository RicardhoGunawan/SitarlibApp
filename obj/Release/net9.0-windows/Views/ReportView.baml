   M S B A M L   `   `   `  ���� C  ?Sitarlib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SitarLib.ViewsSitarLib.Views  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf     SitarLib.Views.ReportView   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:SitarLib.Views     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf       � 	Resources     SidebarButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius � ColumnDefinitions �� Triggers	 P� Property
 P� Value �� 
TargetName3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Opacity   Color  CustomDataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow  SearchButtonStyle  ActionButtonStyle  CustomComboBoxStyle  CustomTextBoxStyle  CustomDatePickerStyle(  "System.Windows.Controls.DatePicker  FieldLabelStyle  ActiveMenuItemStyle �� BasedOn 	 ChartTitleStyle 
 ChartCardStyle  DataCardStyle  ListItemStyle��%3  &	        &	 4    '  {    '  +    &	 o    &	 �    &	 $    &	 �    &	 	    &	 `	    &	 �	    7   &		     &	
 �    &	 k    &	 P    �� " ��.���  � ��$
 10,4q���  � ��$ 15,12q���  � ��
 ���� ��  � �� �#FFFFFF��  � ��$ 0q���  � ��$ Medium"���  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#2196F3��  �@�Cursor$
 Hand���� $ ButtonBorder�� �@J�Effect   $ 5Q�$ 1Q�$	 0.3Q�$ #000000���� "  .���  � ��$ 0,10,0,0q���  �@ AutoGenerateColumns$ False����  �@ 
IsReadOnly$
 True����  �@ CanUserAddRows$ False����  �@ CanUserDeleteRows$ False����  �@ CanUserReorderColumns$ False����  �@ CanUserResizeRows$ False����  �@ CanUserSortColumns$
 True����  �@ HeadersVisibility$ Column=���  � ��$ 1q���  �@ GridLinesVisibility$
 None=���   �@ AlternatingRowBackground �#F9F9F9��  � �� �#DDDDDD��  � ��
 �������  �@ 	RowHeight$ 35Q��� "  .���  � �� �#f0f6fb��  � �� �#333333��  � ��$ SemiBold"���  � ��$
 10,8q���  � �� �#DDDDDD��  � ��$ 0,0,0,1q��� "  .���  � �� �#EEEEEE��  � ��$ 0,0,0,1q��� " ��.���  � ��$
 15,5q���  � �� �#4CAF50��  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#3D8B40��  �@�Cursor$
 Hand���� " ��.���  � ��$
 15,8q���  � ��$ 0,10,0,0q���  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  �@J�Opacity$	 0.9Q���  �@�Cursor$
 Hand���� " ��.���  � ��$	 8,5q���  � ��$ 0,0,0,15q���  � �� �#DDDDDD��  � ��$ 1q��� " ��.���  � ��$	 8,5q���  � ��$ 0,0,10,0q���  � �� �#DDDDDD��  � ��$ 1q��� "  .���  � ��$ 0,0,0,15q���  � �� �#DDDDDD��  � ��$ 1q��� " ��.���  � ��$ SemiBold"���  � �� �#555555��  � ��$ 0,0,0,5q��� " ��8   .���  � �� �#2196F3��  � ��
 �������  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#1E88E5��  �@�Cursor$
 Hand���� " ��.���  � ��$
 Bold"���  � ��$ 16(���  � ��$ 0,0,0,10q���  � �� �#333333�� " ��.���  �@��Padding$ 15q���  � ��$	 300����  � ��
 �������  � �� �#DDDDDD��  � ��$ 1q���  �@��CornerRadius$ 8����  �@J�Effect   $ 10Q�$ 1Q�$	 0.2Q��� " ��.���  �@��Padding$ 15q���  � ��$	 300����  � ��
 �������  � �� �#DDDDDD��  � ��$ 1q���  �@��CornerRadius$ 8����  �@J�Effect   $ 10Q�$ 1Q�$	 0.2Q��� " ��.���  � ��$	 0,5q���  � ��$ CharacterEllipsis=�.���  �� $	��240 ��� $��* �.+��� $��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #1565C0�� � Offset$ 0Q�� $ #0D47A1��$ 1Q�._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Padding$ 15,20q�$��0,0,0,1q����#1C2B39._���  �� Orientation$ 
Horizontal=�$��Center=�.+��� $ ��/Assets/SMAN-TKN-PABAR.ico��$��70��$��70��$��0,0,10,0q��� $��SitarLib��$��24(�$
��Bold"�
�������$��Center=� J� Effect   $ 3Q�$ 1Q�$	 0.5Q��� $��1��$
��Auto=�$��0,15,0,0q�.���� .+��� #��[  $
��15,4q����� *NavigateToDashboardCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��🏠��$��16(��� $��1��$��	Dashboard��$��SemiBold"��� #��[  $
��15,4q����� *NavigateToBookCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📚��$��16(��� $��1��$��	Data Buku���� #��[  $
��15,4q����� *NavigateToMemberCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��👥��$��16(��� $��1��$��Data Anggota���� #��[  $
��15,4q����� *NavigateToBorrowingCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📋��$��16(��� $��1��$��Data Peminjaman���� #��[ $
��15,4q�.���  �� $��30 ��� $��* �.+��� $��0��$
��📈��$��16(��� $��1��$��Laporan��� $��1������ $ 0.5,1&�$ 0.5,0&�.��� $ #f8f9fa��$ 0Q�� $ #e9ecef��$ 1Q� �� $
��Auto ��� $��* �.+��� $��0��$ 25,18q�
�������$��0,0,0,1q����#EEEEEE._��  �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�$��Center=�.+��� $��laporan Perpustakaan��$��24(�$��SemiBold"����#333333�� $1��+Visual aktivitas dan statistik perpustakaan��$��13(����#777777$��0,5,0,0q�� $��1��$��25q� �� $
��Auto ��� $��* �.+��� $��0��
����������#DDDDDD$��1q�$ 8��$ 20q�$��0,0,0,20q�   $ 10Q�$ 1Q�$	 0.2Q�._��  �� $
��Auto ��� $��10 ��� $
��Auto ��� $��15 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Periode Laporan:��$��SemiBold"�$��Center=��� $��2��$
��From��$��Center=�  $��4��$	��130��$��5q�$��Center=����#DDDDDD   SelectedDate �� *	StartDate+�� $��6��$��To��$��Center=�  $��7��$	��130��$��5q�$��Center=����#DDDDDD$
��Left=� �� *	EndDate+�� $��8��$��Apply Filter��#��[ ���� *ApplyFilterCommand+�� $��1��$
��Auto=�.���  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��$��0,0,0,20q� �� $��* ��� $��* �.+��� $��0��#��[
 $��0,0,10,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��Pinjaman per Bulan��#��[	 #  LiveCharts.Wpf.CartesianChart  $��1��&   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$
 None=�   Series �� *BorrowingsByMonthSeries+   AxisX   LiveCharts.Wpf.Axis     LabelsRotation$ 45Q�   Labels �� *BorrowingsByMonthLabels+   	Separator 	  LiveCharts.Wpf.Separator	    	  Step$  1Q�!   AxisY!   "   LabelFormatter" �� *BorrowingsByMonthFormatter+ 	  �� $��1��#��[
 $��10,0,0,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��Distribusi Kategori Buku��#��[	 
  LiveCharts.Wpf.PieChart
  $��1��$ Right=�# 
  InnerRadius$# 50Q� �� *BookCategoriesSeries+$   ChartLegend$ "  LiveCharts.Wpf.DefaultLegend  %   
BulletSize$% 10Q�� $��1��$��0,0,0,20q� �� $��* ��� $��* �.+��� $��0��#��[
 $��0,0,10,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��Status Peminjaman��#��[	 
  $��1��$ Right=�$# 50Q� �� *BorrowingStatusSeries+$   $% 10Q��� $��1��#��[
 $��10,0,0,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��Aktivitas Harian��#��[	   $��1��$	 Top=� �� *DailyActivitySeries+    �� *DailyActivityLabels+ 	  $  1Q�!   " �� *DailyActivityFormatter+ 	  � $��2�� �� $��* ��� $��* ��� $��* �.+��� $��0��#��[ $��0,0,10,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$&�� Buku yang Paling Banyak Dipinjam��#��[	 �� $��1��$��0q�
������ ���� *MostBorrowedBooks+���� .R��� $ Vertical=�$	��0,5q�.+��� #��[ $��SemiBold"����� *Title+�� #��[ $��12(����#666���� *Author+�� #��[ $��12(����#666���� *BorrowCount+& �� StringFormat$& Borrowed {0} times���� $��1��#��[ $��5,0,5,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��Anggota Paling Aktif��#��[	 �� $��1��$��0q�
������ ���� *MostActiveMembers+���� .R��� $ Vertical=�$	��0,5q�.+��� #��[ $��SemiBold"����� *
FullName+�� #��[ $��12(����#666���� *BorrowCount+$& {0} borrowings���� $��2��#��[ $��10,0,0,0q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��Pinjaman Terbaru��#��[	 �� $��1��$��0q����� *RecentBorrowings+���� .R��� $ Vertical=�$	��0,5q�.+��� #��[ $��SemiBold"����� *
Book.Title+�� #��[ $��12(����#666���� *Member.FullName+�� #��[ $��12(����#666���� *
BorrowDate+$& Borrowed on {0:d}��