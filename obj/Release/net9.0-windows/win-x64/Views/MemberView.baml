   M S B A M L   `   `   `  ���� C  ?Sitarlib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SitarLib.ViewsSitarLib.Views      SitarLib.Views.MemberView   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:SitarLib.Views       � 	Resources     CellText �� 
TargetType �� Property �� Value  CellTextTrim �� BasedOn  SidebarButtonStyle �� 
TargetType ��Name �� CornerRadius � ColumnDefinitions	 �� Triggers
 P� Property P� Value �� 
TargetName3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Opacity   Color  CustomDataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow  SearchButtonStyle  ActionButtonStyle  CustomTextBoxStyle  FormInputStyle  FieldLabelStyle 	 ActiveMenuItemStyle��%p  &	        &	 ?     7   &	 v     &	 �    '  �    '  �    &	 �    &	 >    &	 �    &	 	    7  &	 9	    &		 �	    7  �� " ��.���  � ��$
 10,0q���  � ��$ Center=��� " ��8   .���  � ��$ CharacterEllipsis=��� " ��.���  � ��$
 10,4q���  � ��$ 15,12q���  � ��
 ���� ��  � �� �#FFFFFF��  � ��$ 0q���  � ��$ Medium"���  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z��	 P� 
 �@J�IsMouseOver$
 True��. ���  � �� �#2196F3��  �@�Cursor$
 Hand���� $ ButtonBorder�� �@J�Effect   $ 5Q�$ 1Q�$	 0.3Q�$ #000000���� "  .���  � ��$ 0,10,0,0q���  �@ AutoGenerateColumns$ False����  �@ 
IsReadOnly$
 True����  �@ CanUserAddRows$ False����  �@ CanUserDeleteRows$ False����  �@ CanUserReorderColumns$ False����  �@ CanUserResizeRows$ False����  �@ CanUserSortColumns$
 True����  �@ HeadersVisibility$ Column=���  � ��$ 1q���  �@ GridLinesVisibility$
 None=���   �@ AlternatingRowBackground �#F9F9F9��  � �� �#DDDDDD��  � ��
 �������  �@ 	RowHeight$ 35Q��� "  .���  � �� �#f0f6fb��  � �� �#333333��  � ��$ SemiBold"���  � ��$
 10,8q���  � �� �#DDDDDD��  � ��$ 0,0,0,1q��� "  .���  � �� �#EEEEEE��  � ��$ 0,0,0,1q��� " ��.���  � ��$
 15,5q���  � �� �#4CAF50��  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z��	 P� 
 �@J�IsMouseOver$
 True��. ���  � �� �#3D8B40��  �@�Cursor$
 Hand���� " ��.���  � ��$
 15,8q���  � ��$ 0,10,0,0q���  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z��	 P� 
 �@J�IsMouseOver$
 True��. ���  �@J�Opacity$	 0.9Q���  �@�Cursor$
 Hand���� " ��.���  � ��$	 8,5q���  � ��$ 0,0,10,0q���  � �� �#DDDDDD��  � ��$ 1q��� " ��8   .���  � ��$ 0,5,0,15q��� " ��.���  � ��$ SemiBold"���  � �� �#555555��  � ��$ 0,0,0,5q��� " ��8   .���  � �� �#2196F3��  � ��
 �������  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z��	 P� 
 �@J�IsMouseOver$
 True��. ���  � �� �#1E88E5��  �@�Cursor$
 Hand��.���  �� $	��240 ��� $��* �.+��� $��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #1565C0�� � Offset$ 0Q�� $ #0D47A1��$ 1Q�._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Padding$ 15,20q�$��0,0,0,1q����#1C2B39._���  �� Orientation$ 
Horizontal=�$��Center=�.+��� $ ��/Assets/SMAN-TKN-PABAR.ico��$��70��$��70��$��0,0,10,0q��� $��SitarLib��$��24(�$
��Bold"�
�������$��Center=� J� Effect   $ 3Q�$ 1Q�$	 0.5Q��� $��1��$
��Auto=�$��0,15,0,0q�.���� .+��� #��[ $
��15,4q����� *NavigateToDashboardCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��🏠��$��16(��� $��1��$��	Dashboard��$��SemiBold"��� #��[ $
��15,4q����� *NavigateToBookCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📚��$��16(��� $��1��$��	Data Buku���� #��[	 $
��15,4q�.���  �� $��30 ��� $��* �.+��� $��0��$
��👥��$��16(��� $��1��$��Data Anggota���� #��[ $
��15,4q����� *NavigateToBorrowingCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📋��$��16(��� $��1��$��Data Peminjaman���� #��[ $
��15,4q����� *NavigateToReportCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📈��$��16(��� $��1��$��Laporan��� $��1������ $ 0.5,1&�$ 0.5,0&�.��� $ #f8f9fa��$ 0Q�� $ #e9ecef��$ 1Q� �� $
��Auto ��� $��* �.+��� $��0��$ 25,18q�
�������$��0,0,0,1q����#EEEEEE._��  �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�$��Center=�.+��� $��Anggota��$��24(�$��SemiBold"����#333333�� $!��Kelola anggota perpustakaan��$��13(����#777777$��0,5,0,0q�� $��1��$��25q� �� $��* ��� $	��350 �.+��� $��0��
����������#DDDDDD$��1q�$ 8��$��0,0,15,0q�   $ 10Q�$ 1Q�$	 0.2Q�._�� $��20q� �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�.+��� $��Daftar Anggota��$
��Bold"�$��18(����#333333�� $'��!Daftar semua anggota perpustakaan�����#777777$��12(�$��0,5,0,0q��� $��1��$��↻ Refresh��#��[ ���� *RefreshDataCommand+� $��1��$��	0,15,0,10q� �� $��* ��� $
��Auto �.+��� $��0��#��[ ���� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� $��1��$��Search��#��[   $��2��#��[    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    HeadersVisibility$ Column=�$   HorizontalScrollBarVisibility$ Disabled=����� *	Members+ �� SelectedItem �� *SelectedMember+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	No Member��    Width$	  110 1  +System.Windows.Controls.DataGridBoundColumn!   ElementStyle#! [  "   Binding" �� *
MemberCode+  $ Nama Lengkap��$  3* #! [ " �� *
FullName+  $ Kelas��$
  1.8* #! [ " �� *Class+  $ Phone��$	  120 #! [  " �� *PhoneNumber+  $ Expiry Date��$	  120 #! [  " �� *MembershipExpiry+# �� StringFormat$# d��4	  .System.Windows.Controls.DataGridTemplateColumn	  $ Status��$  65 $ 	  CellTemplate$ �� .R��� $ 3��$	 8,3q�$	��5,0q�$��Center=����� " ��.���  � �� �#e8f5e9% �� Triggers% �� & �� Binding& �� *
IsActive+' �� Value	' False.a���  � �� �#ffebee._��� $��Center=�$��Center=����� " ��.���  � ��$ Active����  � �� �#388e3c% �� & �� *
IsActive+	' False.a���  � ��$ Inactive����  � �� �#d32f2f�� $��1��
����������#DDDDDD$��1q�$ 8��$��15,0,0,0q�   $ 10Q�$ 1Q�$	 0.2Q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=����� $	 0,0&�$	 1,0&�.��� $ #3498db��$ 0Q�� $ #2980b9��$ 1Q��� $��1��$��20q�$
��Auto=�.���� .+��� $
��Bold"�$��18(����#333333$��0,0,0,5q����� *	IsEditing+ 
 BooleanToStringConverter( �� 	Converter#( [
 ) �� ConverterParameter$&)  Edit Anggota;Tambah Anggota Baru���� $4��.Isi detail untuk membuat atau mengedit anggota�����#777777$��12(�$��0,0,0,20q��� $��
No Anggota��#��[ �� #��[ ���� *CurrentMember.MemberCode+$ PropertyChanged=�* �� 
IsReadOnly* �� *	IsEditing+�� $��Nama Lengkap��#��[ �� #��[ ���� *CurrentMember.FullName+$ PropertyChanged=��� $��Kelas��#��[ �� #��[ ���� *CurrentMember.Class+$ PropertyChanged=��� $��Alamat��#��[ �� #��[ + �� TextWrapping$
+ Wrap=�, �� AcceptsReturn$
, True��$��60������ *CurrentMember.Address+$ PropertyChanged=��� $��Nomor telepon��#��[ �� #��[ ���� *CurrentMember.PhoneNumber+$ PropertyChanged=��� $��Email��#��[ �� #��[ ���� *CurrentMember.Email+$ PropertyChanged=��� $��Kedaluwarsa Keanggotaan��#��[ (
  "System.Windows.Controls.DatePicker
  $��0,5,0,15q�- 
  SelectedDate- �� * CurrentMember.MembershipExpiry+�� $��Sedang Aktif��$��0,0,0,20q�. d� 	IsChecked. �� *CurrentMember.IsActive+�� $��Simpan Anggota�����#2ecc71#��[ ���� *SaveCommand+�� $��1�����#DDDDDD$��	0,20,0,20q��� $��Tindakan��$
��Bold"�$��16(����#333333$��0,0,0,15q��� $��Tambahkan Anggota Baru�����#3498db#��[ ���� *AddNewCommand+�� $ ��Hapus Anggota yang Dipilih�����#e74c3c#��[ ���� *DeleteCommand+�� $��Membatalkan�����#95a5a6#��[ ���� *CancelCommand+