   M S B A M L   `   `   `  ���� C  ?Sitarlib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SitarLib.ViewsSitarLib.Views      SitarLib.Views.BookView   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:SitarLib.Views       � 	Resources      BooleanToVisibilityConverter  ModernImportButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value  ModernMenuItemButtonStyle  ModernToolTipStyle	 �� Padding3  -System.Windows.Media.Effects.DropShadowEffect  EnhancedDropShadowEffect
   ShadowDepth   	Direction   Color   Opacity   
BlurRadius  CellText  CellTextTrim �� BasedOn  SidebarButtonStyle ��Name � ColumnDefinitions �� 
TargetName  CustomDataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow 	 SearchButtonStyle 
 ActionButtonStyle  CustomTextBoxStyle  FormInputStyle  FieldLabelStyle  ActiveMenuItemStyle��%�  &	        &	      &	 (    &	 �    &	 �    &	     &	 G    7  &	 ~    &	 �    '  �	    '  �
    &		 �
    &	
 F    &	 �    &	     7  &	 A    &	 �    7  �� �� " ��.���  � �� �#2196F3��  � ��
 �������  � ��$ 16,10q���  � ��$ 0q���  �@�Cursor$
 Hand����  � ��$ Segoe UI)���  � ��$ 13(���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� #��z��#��z��#��z��$ 6��._��� #��z��#��z��#��z�� P�  �@J�IsMouseOver$
 True��. ���  � �� �#1E88E5P�  � ��$
 True��. ���  � �� �#1565C0P�  � {�$ False��. ���  � �� �#BDBDBD��  � �� �#757575�� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$
 8,12q���  � ��$
 Left=���  � ��$	 220����  � ��$ Segoe UI)���  � ��$ 13(���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� #��z��$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ���  � �� �#F5F5F5P�  � ��$
 True��. ���  � �� �#EEEEEE�� " _�.���  � �� �#424242��  � ��
 �������  � ��
 ���� ��  � ��$	 8,6q���  � ��$ Segoe UI)���  � ��$ 12(���  �@_�HasDropShadow$
 True����  � �� �� " _�.R��� #��z��#��z��#��z��$ 4��#	 z��._���   $
 2Q�$	 270Q�$ Black��$	 0.2Q�$ 8Q��� " ��.���  � ��$
 10,0q���  � ��$ Center=��� " ��8   .���  � ��$ CharacterEllipsis=��� " ��.���  � ��$
 10,4q���  � ��$ 15,12q���  � ��
 ���� ��  � �� �#FFFFFF��  � ��$ 0q���  � ��$ Medium"���  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ���  � �� �#2196F3��  �@�Cursor$
 Hand���� $ ButtonBorder�� �@J�Effect   $ 5Q�$
 1Q�$	 0.3Q�$ #000000���� "  .���  � ��$ 0,10,0,0q���  �@ AutoGenerateColumns$ False����  �@ 
IsReadOnly$
 True����  �@ CanUserAddRows$ False����  �@ CanUserDeleteRows$ False����  �@ CanUserReorderColumns$ False����  �@ CanUserResizeRows$ False����  �@ CanUserSortColumns$
 True����  �@ HeadersVisibility$ Column=���  � ��$ 1q���  �@ GridLinesVisibility$
 None=���   �@ AlternatingRowBackground �#F9F9F9��  � �� �#DDDDDD��  � ��
 �������  �@ 	RowHeight$ 35Q��� "  .���  � �� �#f0f6fb��  � �� �#333333��  � ��$ SemiBold"���  � ��$
 10,8q���  � �� �#DDDDDD��  � ��$ 0,0,0,1q��� "  .���  � �� �#EEEEEE��  � ��$ 0,0,0,1q��� " ��.���  � ��$
 15,5q���  � �� �#4CAF50��  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ���  � �� �#3D8B40��  �@�Cursor$
 Hand���� " ��.���  � ��$
 15,8q���  � ��$ 0,10,0,0q���  � ��
 �������  � ��$ 0q���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��#��z��#��z��$ 4��._��� $��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ���  �@J�Opacity$	 0.9Q���  �@�Cursor$
 Hand���� " ��.���  � ��$	 8,5q���  � ��$ 0,0,10,0q���  � �� �#DDDDDD��  � ��$ 1q��� " ��8   .���  � ��$ 0,5,0,15q��� " ��.���  � ��$ SemiBold"���  � �� �#555555��  � ��$ 0,0,0,5q��� " ��8   .���  � �� �#2196F3��  � ��
 �������  � �� �� " ��.R��� $ ButtonBorder��#��z��#��z��#��z��$ 8��._��  �� $
��Auto ��� $��* �.+��� $��1��#��z��$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ���  � �� �#1E88E5��  �@�Cursor$
 Hand��.���  �� $	��240 ��� $��* �.+��� $��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #1565C0�� � Offset$ 0Q�� $ #0D47A1��$ 1Q�._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$	 15,20q�$��0,0,0,1q����#1C2B39._���  �� Orientation$ 
Horizontal=�$��Center=�.+��� $ ��/Assets/SMAN-TKN-PABAR.ico��$��70��$��70��$��0,0,10,0q��� $��SitarLib��$��24(�$
��Bold"�
�������$��Center=� J� Effect   $ 3Q�$
 1Q�$	 0.5Q��� $��1��$
��Auto=�$��0,15,0,0q�.���� .+��� #��[ $
��15,4q����� *NavigateToDashboardCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��🏠��$��16(��� $��1��$��	Dashboard��$��SemiBold"��� #��[ $
��15,4q�.���  �� $��30 ��� $��* �.+��� $��0��$
��📚��$��16(��� $��1��$��	Data Buku���� #��[ $
��15,4q����� *NavigateToMemberCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��👥��$��16(��� $��1��$��Data Anggota���� #��[ $
��15,4q����� *NavigateToBorrowingCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📋��$��16(��� $��1��$��Data Peminjaman���� #��[ $
��15,4q����� *NavigateToReportCommand+.���  �� $��30 ��� $��* �.+��� $��0��$
��📈��$��16(��� $��1��$��Laporan��� $��1������ $ 0.5,1&�$ 0.5,0&�.��� $ #f8f9fa��$ 0Q�� $ #e9ecef��$ 1Q� �� $
��Auto ��� $��* �.+��� $��0��$	 25,18q�
�������$��0,0,0,1q����#EEEEEE._��  �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�$��Center=�.+��� $
��Buku��$��24(�$��SemiBold"����#333333�� $��Kelola buku perpustakaan��$��13(����#777777$��0,5,0,0q�� $��1��$��25q� �� $��* ��� $	��350 �.+��� $��0��
����������#DDDDDD$��1q�$ 8��$��0,0,15,0q�   $ 10Q�$
 1Q�$	 0.2Q�._�� $��20q� �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $
��Auto �.+��� $��0��$ Vertical=�.+��� $��Daftar Buku��$
��Bold"�$��18(����#333333�� $$��Daftar semua buku perpustakaan�����#777777$��12(�$��0,5,0,0q��� $��1��$ 
Horizontal=�.+��� -    ��Name$ ImportButton��#��[ ���� *ShowImportOptionsCommand+.���� $ 
Horizontal=�.+�F� |������!@]� �@]� @]� �@]� ��!��ا ��!��ا @]� �@KL @]� �@KL �ا ��ا �ا ��ا @KL �@]� @KL �@]� �ا ���!�ا ���!@]� 	$��16��$��16�� �� Stretch$ Uniform=�$��0,0,8,0q�����  �� RelativeSource ��  �� AncestorType
 Button �� Path$ 
Foreground��� $��Import��$��Center=�$��SemiBold"�$��Segoe UI)�F� F����Pq �� � � P�� Ѱ$� �� р���� � �  $� р�[ ��� �Pq �� 	$��12��$��12��$ Uniform=�$��8,0,0,0q�����  �� 
 Button$ 
Foreground� � ToolTip _� $��Import books from file��#��[ � -   $��ImportOptionsPopup��$��Bottom=�  � AllowsTransparency$
  True��! � 	StaysOpen$! False������ *IsImportOptionsOpen+" �� Mode$" TwoWay=�# � PlacementTarget# �� $ �� ElementName$$ ImportButton��.���� 
����������#E0E0E0$��1q�$ 6��# [ ._��� $��4q�.+��� #��[ ���� *ImportCsvCommand+.���� $ 
Horizontal=�.+�F� ����Ѐ�� �� р�[ �� � 	=  	=  �� �� � 	=  -1׀�[ ��O �� �� р���O� -1 -1 �� �� � -1 z р�� �� Ѐ� -1р�[  -1р�[  	= �@]�  	= �@]� @T� р�@T� р� -1���� ��!� � ��� �@T� ��� �@T� ��� ���� ��� ���� ��� �@]� ��!р�� ��!	���#2196F3$��18��$��18��$ Uniform=�$��8,0,12,0q��� $��
Import CSV��$��Center=�$��Segoe UI)��� $	��0,4q����#F0F0F0$��1���� #��[ ���� *ImportXlsxCommand+.���� $ 
Horizontal=�.+�F� ����Ѐ�� �� р�[ �� � 	=  	=  �� �� � 	=  -1׀�[ ��O �� �� р���O� -1 -1 �� �� � -1 z р�� �� Ѐ� -1р�[  -1р�[  	= �@]�  	= �@]� @T� р�@T� р� -1�0r� Pf� �P�� ༏ ��Q���0@�g� Q0r� �Q��+0@�p�� PH� �P�� p�� �0r� Pf� 	���#4CAF50$��18��$��18��$ Uniform=�$��8,0,12,0q��� $��Import Excel (XLSX)��$��Center=�$��Segoe UI)�� ���	#90000000y��� *IsBusy+% �� 	Converter#% [  .+��� ���#FFFFFF$ 10��$	��300��$	��160��$��Center=�$��Center=�   $
 3Q�$	 270Q�$ Black��$	 0.2Q�$ 10Q�._��� $��Center=�$��Center=�.+�� & � IsIndeterminate$
& True��$��40��$��40�����#2196F3$��0,0,0,15q����� " �.���  � �� �� " �.R��� 
������ $��0q�
������ $ 20��._�� .+�A� ' A�Name$' 
PART_Track��$��4�����#E0E0E0
������ A� $' PART_Indicator��$��4�����#2196F3
������ ( �� StrokeDashArray$( 0,100R�) J� RenderTransformOrigin$) 0.5,0.5&�z��� RotateTransform��* �� Angle$* 0Q� P�  �@�IsIndeterminate$
 true��. ��� $ PART_Indicator�� �@��StrokeDashArray$ 20,80R�+ N� EnterActions+ �� .���� .��W� , �� 
TargetName$, RotateTransform��- �� TargetProperty$- Angle�. W� From. 0	/ W� To/ 3600 g� Duration$0 0:0:1.5D�1 g� RepeatBehavior$1 Forever���� $��Importing Books�����#333333$��Center=�$��16(�$��SemiBold"��� ���#777777$��Center=�$
��Wrap=�$	��260��2 �� TextAlignment$2 Center=�$��12(�$��0,8,0,0q����� *BusyMessage+�� $$��Please don't close this window�����#999999$��Center=�$��11(�$��Italic$�$��0,12,0,0q�� $��1��$��	0,15,0,10q� �� $��* ��� $
��Auto �.+��� $��0��#��[ ���� *
SearchText+3 �� UpdateSourceTrigger$3 PropertyChanged=��� $��1��$��Search��#��[	   $��2��#��[ 4   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$4 * 5   HeadersVisibility$5 Column=�$6   HorizontalScrollBarVisibility$6 Disabled=����� *Books+7 �� SelectedItem7 �� *SelectedBook+8   Columns8 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn9   Header$
9 ISBN��:   Width$	: 130 1  +System.Windows.Controls.DataGridBoundColumn;   ElementStyle#; [ <   Binding< �� *ISBN+  $9 Judul��$: 3* #; [ < �� *Title+  $9 	Pengarang��$: 2* #; [ < �� *Author+  $9 Kategori��$
: 1.2* #; [ < �� *
Category+  $9 Jumlah��$: 80 < �� *Stock+; �� " ��# [ .���  � ��$ Center=���  � ��$ SemiBold"��� $��1��
����������#DDDDDD$��1q�$ 8��$��15,0,0,0q�   $ 10Q�$
 1Q�$	 0.2Q�._��  �� $
��Auto ��� $��* �.+��� $��0��$��5��$	��Top=����� $	 0,0&�$	 1,0&�.��� $ #3498db��$ 0Q�� $ #2980b9��$ 1Q��� $��1��$��20q�$
��Auto=�.���� .+��� $
��Bold"�$��18(����#333333$��0,0,0,5q����� *	IsEditing+  BooleanToStringConverter#% [ = �� ConverterParameter$ = Edit Buku;Tambah Buku Baru���� $1��+Isi detail untuk membuat atau mengedit buku�����#777777$��12(�$��0,0,0,20q��� $
��ISBN��#��[ �� #��[ ���� *CurrentBook.ISBN+$3 PropertyChanged=��� $��Judul��#��[ �� #��[ ���� *CurrentBook.Title+$3 PropertyChanged=��� $��	Pengarang��#��[ �� #��[ ���� *CurrentBook.Author+$3 PropertyChanged=��� $��Penerbit��#��[ �� #��[ ���� *CurrentBook.Publisher+$3 PropertyChanged=��� $��Tahun Terbit��#��[ �� #��[ ���� *CurrentBook.PublicationYear+$3 PropertyChanged=��� $��Kategori��#��[ �� #��[ ���� *CurrentBook.Category+$3 PropertyChanged=��� $��Stock��#��[ �� #��[ ���� *CurrentBook.Stock+$3 PropertyChanged=��� $��
Keterangan��#��[ �� #��[ > �� TextWrapping$
> Wrap=�? �� AcceptsReturn$
? True��$��80������ *CurrentBook.Description+$3 PropertyChanged=��� $��Simpan Buku�����#2ecc71#��[
 ���� *SaveCommand+�� $��1�����#DDDDDD$��	0,20,0,20q��� $��Tindakan��$
��Bold"�$��16(����#333333$��0,0,0,15q��� $��Tambahkan Buku Baru�����#3498db#��[
 ���� *AddNewCommand+�� $��Hapus Buku yang Dipilih�����#e74c3c#��[
 ���� *DeleteCommand+�� $��Membatalkan�����#95a5a6#��[
 ���� *CancelCommand+