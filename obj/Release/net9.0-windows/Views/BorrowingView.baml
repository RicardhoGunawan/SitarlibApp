   M S B A M L   `   `   `  ���� C  ?Sitarlib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SitarLib.ViewsSitarLib.Views  "    SitarLib.Views.BorrowingView   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:SitarLib.Views       � 	Resources     SidebarButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius � ColumnDefinitions �� Triggers	 P� Property
 P� Value �� 
TargetName3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Opacity   Color  CustomDataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow  SearchButtonStyle  ActionButtonStyle  CustomComboBoxStyle  CustomTextBoxStyle  CustomDatePickerStyle(  "System.Windows.Controls.DatePicker  FieldLabelStyle  ActiveMenuItemStyle �� BasedOn��%�  &	        &	 4    '  {    '  +    &	 o    &	 �    &	 $    &	 �    &	 	    &	 `	    &	 �	    7   �� " ��.���  � ��$
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
 Hand��.���  �� $	��240 ��� $��* �.+��� $��0������  �� 
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
��👥��$��16(��� $��1��$��Data Anggota���� #��[ $
��15,4q�.���  �� $��30 ��� $��* �.+��� $��0��$
��📋��$��16(��� $��1��$��Data Peminjaman���� #��[  $
��15,4q����� *NavigateToReportCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📈��$��16(��� $��1��$��Laporan��� $��1������ $ 0.5,1&�$ 0.5,0&�.��� $ #f8f9fa��$ 0Q�� $ #e9ecef��$ 1Q� �� $
��Auto ��� $��* �.+��� $��0��$ 25,18q�
�������$��0,0,0,1q����#EEEEEE._��  �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�$��Center=�.+��� $��
Piminjaman��$��24(�$��SemiBold"����#333333�� $'��!Kelola semua transaksi peminjaman��$��13(����#777777$��0,5,0,0q�� $��1��$��25q� �� $��* ��� $	��350 �.+��� $��0��
����������#DDDDDD$��1q�$ 8��$��0,0,15,0q�   $ 10Q�$ 1Q�$	 0.2Q�._�� $��20q� �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�.+��� $��Daftar Peminjaman��$
��Bold"�$��18(����#333333�� $'��!Daftar semua transaksi peminjaman�����#777777$��12(�$��0,5,0,0q��� $��1��$��↻ Refresh��#��[ ���� *RefreshDataCommand+� $��1��$��	0,15,0,10q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$	��150��$��0,0,10,0q�$	��8,5q����#DDDDDD���� *StatusFilters+ �� SelectedItem �� *FilterStatus+�� $��1��#��[ ���� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� $��2��$��Search��#��[   $��2��#��[ ���� *
Borrowings+ �� *SelectedBorrowing+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *Id+ 	  ElementStyle �� " ��.���  � ��$
 10,0q���  � ��$ Center=�  $ 
Book Title��$ *  �� *
Book.Title+ �� " ��.���  � ��$
 10,0q���  � ��$ Center=���  � ��$ CharacterEllipsis=�  $ Member��$ *  �� *Member.FullName+ �� " ��.���  � ��$
 10,0q���  � ��$ Center=�  $ Borrow Date��$
 Auto  �� *
BorrowDate+  �� StringFormat$  d�� �� " ��.���  � ��$
 10,0q���  � ��$ Center=�  $ Due Date��$
 Auto  �� *	DueDate+$  d�� �� " ��.���  � ��$
 10,0q���  � ��$ Center=�  $ Return Date��$
 Auto  �� *
ReturnDate+$  d�� �� " ��.���  � ��$
 10,0q���  � ��$ Center=�4
  .System.Windows.Controls.DataGridTemplateColumn
  $ Status��$	 100 ! 
  CellTemplate! �� .R��� $ 3��$	 8,3q�$	��5,0q�$��Center=����� " ��.���  � �� �#e1f5fe" �� Triggers" �� # �� Binding# �� *Status+$ �� Value$ Overdue.a���  � �� �#ffebee�� # �� *Status+$ Returned.a���  � �� �#e8f5e9._��� $��Center=�$��Center=����� *Status+���� " ��.���  � �� �#0288d1" �� # �� *Status+$ Overdue.a���  � �� �#d32f2f�� # �� *Status+$ Returned.a���  � �� �#388e3c�� $��1��
����������#DDDDDD$��1q�$ 8��$��15,0,0,0q�   $ 10Q�$ 1Q�$	 0.2Q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=����� $	 0,0&�$	 1,0&�.��� $ #3498db��$ 0Q�� $ #2980b9��$ 1Q��� $��1��$��20q�$
��Auto=�.���� .+��� $��Pinjaman Baru��$
��Bold"�$��18(����#333333$��0,0,0,5q��� $-��'Isi rincian untuk membuat pinjaman baru�����#777777$��12(�$��0,0,0,20q��� $2��,Pilih Buku (Type to search by Title or ISBN)��#��[ �� % �� DisplayMemberPath$% Title��& �� 
IsEditable$
& True��' �� IsTextSearchEnabled$
' True��( �� StaysOpenOnEdit$
( True��#��[ ���� *FilteredBooks+ �� *SelectedBook+) �� Text) �� *BookSearchText+$ PropertyChanged=��� $4��.Pilih Anggota (Type to search by Name or Code)��#��[ �� $% FullName��$
& True��$
' True��$
( True��#��[ ���� *FilteredMembers+ �� *SelectedMember+) �� *MemberSearchText+$ PropertyChanged=��� $��Tanggal Peminjaman��#��[   ${�False��#��[ *   SelectedDate* �� *CurrentBorrowing.BorrowDate+�� $��Tenggat waktu��#��[   #��[ * �� *CurrentBorrowing.DueDate+�� $��Pinjam Buku�����#2ecc71#��[ ���� *BorrowCommand+�� $��1�����#DDDDDD$��	0,20,0,20q��� $��Peminjaman Terpilih��$
��Bold"�$��16(����#333333$��0,0,0,15q��� $��Kembalikan Buku Pilihan�����#3498db#��[ ���� *ReturnCommand+�� $��Membatalkan�����#95a5a6#��[ ���� *CancelCommand+