   M S B A M L   `   `   `  ���� C  ?Sitarlib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SitarLib.ViewsSitarLib.Views  "    SitarLib.Views.DashboardView   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:SitarLib.Views       � 	Resources     SidebarButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius � ColumnDefinitions �� Triggers	 P� Property
 P� Value �� 
TargetName3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Opacity   Color  LogoutButtonStyle �� BasedOn  ActiveMenuItemStyle  StatCardStyle  StatTitleStyle  StatValueStyle  CustomDataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow  RefreshButtonStyle��%�
  &	        &	 4    7   &	 �    7   &	 �    &	 �    &	 D    &	 �    '  �    '  �    &	 �    �� " ��.���  � ��$
 10,4q���  � ��$ 15,12q���  � ��
 ���� ��  � �� �#FFFFFF��  � ��$ 0q���  � ��$ Medium"���  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#2196F3��  �@�Cursor$
 Hand���� $ ButtonBorder�� �@J�Effect   $ 5Q�$ 1Q�$	 0.3Q�$ #000000���� " ��8   .���  � �� �#FFFFFF��  � �� �#E53935��  � ��$ 15,10q��� " ��8   .���  � �� �#2196F3��  � ��
 �������  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#1E88E5��  �@�Cursor$
 Hand���� " ��.���  � ��
 �������  � �� �#DDDDDD��  � ��$ 1q���  �@��CornerRadius$ 8����  � ��$ 10q���  �@��Padding$ 20,15q���  �@J�Effect   $ 10Q�$ 1Q�$	 0.2Q��� " ��.���  � ��$
 Bold"���  � ��$ 14(���  � ��$ 0,0,0,10q���  � �� �#555555�� " ��.���  � ��$ 32(���  � ��$ SemiBold"���  � ��$ Center=��� "  .���  � ��$ 0,10,0,0q���  �@ AutoGenerateColumns$ False����  �@ 
IsReadOnly$
 True����  �@ CanUserAddRows$ False����  �@ CanUserDeleteRows$ False����  �@ CanUserReorderColumns$ False����  �@ CanUserResizeRows$ False����  �@ CanUserSortColumns$
 True����  �@ HeadersVisibility$ Column=���  � ��$ 1q���  �@ GridLinesVisibility$
 None=���   �@ AlternatingRowBackground �#F9F9F9��  � �� �#DDDDDD��  � ��
 �������  �@ 	RowHeight$ 35Q��� "  .���  � �� �#f0f6fb��  � �� �#333333��  � ��$ SemiBold"���  � ��$
 10,8q���  � �� �#DDDDDD��  � ��$ 0,0,0,1q��� "  .���  � �� �#EEEEEE��  � ��$ 0,0,0,1q��� " ��.���  � ��$
 12,5q���  � �� �#4CAF50��  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z�� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#3D8B40��  �@�Cursor$
 Hand��.���  �� $	��240 ��� $��* �.+��� $��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #1565C0�� � Offset$ 0Q�� $ #0D47A1��$ 1Q�._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Padding$ 15,20q�$��0,0,0,1q����#1C2B39._���  �� Orientation$ 
Horizontal=�$��Center=�.+��� $ ��/Assets/SMAN-TKN-PABAR.ico��$��70��$��70��$��0,0,10,0q��� $��SitarLib��$��24(�$
��Bold"�
�������$��Center=� J� Effect   $ 3Q�$ 1Q�$	 0.5Q��� $��1��$
��Auto=�$��0,15,0,0q�.���� .+��� #��[ $
��15,4q�.���  �� $��30 ��� $��* �.+��� $��0��$
��🏠��$��16(��� $��1��$��	Dashboard��$��SemiBold"��� #��[  $
��15,4q����� *NavigateToBookCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📚��$��16(��� $��1��$��	Data Buku���� #��[  $
��15,4q����� *NavigateToMemberCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��👥��$��16(��� $��1��$��Data Anggota���� #��[  $
��15,4q����� *NavigateToBorrowingCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📋��$��16(��� $��1��$��Data Peminjaman���� #��[  $
��15,4q����� *NavigateToReportCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📈��$��16(��� $��1��$��Laporan���� $��2��$��15,15q�$��0q�$ 8�����#E53935._��� #��[ 
������ $��0q����� *LogoutCommand+.���  �� $��30 ��� $��* �.+��� $��0��$	��⏻��$��16(��� $��1��$��Logout��$��SemiBold"�� $��1������ $ 0.5,1&�$ 0.5,0&�.��� $ #f8f9fa��$ 0Q�� $ #e9ecef��$ 1Q� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$ 25,18q�
�������$��0,0,0,1q����#EEEEEE._��  �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�$��Center=�.+��� $��	Dashboard��$��24(�$��SemiBold"����#333333�� $>��8Selamat datang di sistem manajemen perpustakaan SitarLib��$��13(����#777777$��0,5,0,0q�� $��1��$��25,25,25,20q� �� $��* ��� $��* ��� $��* ��� $��* �.+��� $��0��#��[ ._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=�$��-20,-15,-20,10q����� $	 0,0&�$	 1,0&�.��� $ #3498db��$ 0Q�� $ #2980b9��$ 1Q��� $��1��.+��� $��Jumlah Buku��#��[ �� #��[ ���#3498db���� *
TotalBooks+�� $��1��#��[ ._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=�$��-20,-15,-20,10q����� $	 0,0&�$	 1,0&�.��� $ #9b59b6��$ 0Q�� $ #8e44ad��$ 1Q��� $��1��.+��� $��Jumlah Anggota��#��[ �� #��[ ���#9b59b6���� *TotalMembers+�� $��2��#��[ ._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=�$��-20,-15,-20,10q����� $	 0,0&�$	 1,0&�.��� $ #2ecc71��$ 0Q�� $ #27ae60��$ 1Q��� $��1��.+��� $��Pinjaman Aktif��#��[ �� #��[ ���#2ecc71���� *ActiveBorrowings+�� $��3��#��[ ._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=�$��-20,-15,-20,10q����� $	 0,0&�$	 1,0&�.��� $ #e74c3c��$ 0Q�� $ #c0392b��$ 1Q��� $��1��.+��� $��Melewati Tenggat��#��[ �� #��[ ���#e74c3c���� *OverdueBorrowings+�� $��2��
����������#DDDDDD$��1q�$ 8��$��
25,0,25,25q�   $ 10Q�$ 1Q�$	 0.2Q�._�� $��20q� �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�.+��� $��Pinjaman Terbaru��$
��Bold"�$��18(����#333333�� $'��!Daftar transaksi pinjaman terkini�����#777777$��12(�$��0,5,0,0q��� $��1��$��↻ Refresh��#��[ ���� *RefreshDataCommand+� $��1��$��	0,15,0,15q� �� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��20 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Filter by Date:��$��Center=��� $��2��$
��From��$��Center=�(  "System.Windows.Controls.DatePicker  $��4��$	��120��   SelectedDate �� *StartDateFilter+ �� Mode$ TwoWay=��� $��6��$��To��$��Center=�  $��8��$	��120�� �� *EndDateFilter+$ TwoWay=��� $��10��$��Apply Filter��$
��10,5q�#��[ $��Right=����� *ApplyDateFilterCommand+  ��%9   '��       �� " ��.���  �@��CornerRadius$ 4��  $��2��#��[ ���� *RecentBorrowings+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Book Title��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *
Book.Title+ 	  ElementStyle �� " ��.���  � ��$
 10,0q���  � ��$ Center=���  � ��$ CharacterEllipsis=�  $ Member��$ *  �� *Member.FullName+ �� " ��.���  � ��$
 10,0q���  � ��$ Center=�  $ Borrow Date��$
 Auto  �� *
BorrowDate+  �� StringFormat$  d�� �� " ��.���  � ��$
 10,0q���  � ��$ Center=�  $ Due Date��$
 Auto  �� *	DueDate+$  d�� �� " ��.���  � ��$
 10,0q���  � ��$ Center=�4
  .System.Windows.Controls.DataGridTemplateColumn
  $ Status��$ 80 ! 
  CellTemplate! �� .R��� $ 3��$	 8,3q�$	��5,0q�$��Center=����� " ��.���  � �� �#e1f5fe" �� Triggers" �� # �� Binding# �� *Status+$ �� Value$ Overdue.a���  � �� �#ffebee�� # �� *Status+$ Returned.a���  � �� �#e8f5e9._��� $��Center=�$��Center=����� *Status+���� " ��.���  � �� �#0288d1" �� # �� *Status+$ Overdue.a���  � �� �#d32f2f�� # �� *Status+$ Returned.a���  � �� �#388e3c