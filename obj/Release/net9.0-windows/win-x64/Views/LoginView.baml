   M S B A M L   `   `   `  ���� C  ?Sitarlib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SitarLib.ViewsSitarLib.Views       SitarLib.Views.LoginWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:SitarLib.Views       � Title$  Login - SitarLib��$	��800��$
��1200�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � 	Resources     BooleanToVisibilityConverter��%   &	        �� .���  � ColumnDefinitions �� $��* ��� $��* �.+�� $��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #1565C0�� � Offset$	 0.0Q�� $ #1E88E5��$	 1.0Q�.+��� $��/Assets/pattern.png��	 J� Opacity$
	 0.08Q�$��UniformToFill=��� $��Center=�$��40q�$��Center=�.+��� 
 �� CornerRadius$
 50��
�������$	��110��$	��110��$��0,0,0,20q�$
	 0.95Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 2Q�   
BlurRadius$ 5Q�   Color$ #222222��   Opacity$
 0.25Q�._��� $��/Assets/logo.png��$��90��$��90��$��Center=�$��Center=��� $��Selamat Datang��$��28(�$��Light"�
�������$��Center=��� $��SitarLib��$��50(�$
��Bold"�
�������$��Center=�$��	0,10,0,10q�   $ 1Q�$ 3Q�$ #222222��$	 0.3Q��� $#��Sistem Manajemen Perpustakaan��$��22(�$��Medium"�
�������$��Center=�$��0,0,0,30q��� $	��130��$��4��
�������$
 2��$
	 0.85Q�$��0,0,0,30q��� $O��ISolusi lengkap Anda untuk manajemen sumber daya perpustakaan yang efisien��$��16(�$
��Wrap=� �� TextAlignment$ Center=�$��Light"�
�������$
	 0.95Q�$	��380���� $��0,40,0,0q�.+�� $��0,0,0,15q� �� $
��Auto ��� $��* �.+��� $	��✓��
�������$��18(�$��0,0,15,0q�$��Center=��� $��1��$0��*Manajemen buku dan katalogisasi yang mudah��
�������$��14(�$��Center=�� $��0,0,0,15q� �� $
��Auto ��� $��* �.+��� $	��✓��
�������$��18(�$��0,0,15,0q�$��Center=��� $��1��$-��'Peminjaman dan pengembalian yang lancar��
�������$��14(�$��Center=�� $��0,0,0,0q� �� $
��Auto ��� $��* �.+��� $	��✓��
�������$��18(�$��0,0,15,0q�$��Center=��� $��1��$ ��Pelacakan secara real-time��
�������$��14(�$��Center=�� $��1�����#F5F5F5.+��� $��/Assets/light-pattern.png��$
	 0.07Q�$��UniformToFill=��� $	��380��$��Center=�.+��� $��Masuk ke Akun Anda��$��32(�$��SemiBold"�$��Center=����#333333$��0,0,0,40q��� 
�������$
 12�� �� Padding$ 35q����#E0E0E0$��1q�   $ 0Q�   	Direction$ 0Q�$ #1E88E5��$	 0.1Q�$ 20Q�._��� .+��� $��0,0,0,20q�.+��� $��Selamat Datang kembali��$
��Bold"����#333333$��22(�$��0,0,0,4q��� $&�� Silakan masukkan kredensial Anda�����#666666$��14(��� $��0,0,0,20q�.+��� $��Username��$��SemiBold"����#444444$��14(�$��0,0,0,8q��  �� $
��Auto ��� $��* �.+��� ���#F5F5F5$��40��$��40��$
 6,0,0,6��$��Stretch=�._��� $
��👤��$��16(�$��Center=�$��Center=��� -    ��Name$ UsernameTextBox��$��1��$��14(�$��1q����#E0E0E0���#FAFAFA$��Center=�$��5,0,0,0q����� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�.��.������  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R���  ��Name$ border��#��z��#��z��#��z�� J� SnapsToDevicePixels$
 True��._���  ��Name$ PART_ContentHost��$|�False��$��Hidden=�$��Hidden=�#��z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 true��. ���  �� 
TargetName$ border�� � �� �#1E88E5P�  �@J�	IsFocused$
 true��. ��� $ border�� � �� �#1E88E5�� $ border�� � �� �#FFFFFF��  � ��$ 0,0,0,0q�.���� $��1��  J� IsHitTestVisible$  False��$��Center=�$
��Left=�$��12,0,0,0q����#AAAAAA$��14(�$��Enter your username������ " ��.���  � y�$ 	Collapsed=�! �� Triggers! �� " �� Binding" �� *Text+# �� ElementName$# UsernameTextBox��$ �� Value$  .a���  � y�$ Visible=��� $��0,0,0,25q�.+��� $��Password��$��SemiBold"����#444444$��14(�$��0,0,0,8q��  �� $
��Auto ��� $��* �.+��� ���#F5F5F5$��40��$��40��$
 6,0,0,6��$��Stretch=�._��� $
��🔒��$��16(�$��Center=�$��Center=�G� -   % G�Name$% PasswordBox��$��1��$��14(�$��1q����#E0E0E0���#FAFAFA$��Center=�$��5,0,0,0q����� " G�.���  � �� �� " G�.R��� $ border��#��z��#��z��#��z��$
 True��._��� $ PART_ContentHost��$|�False��$��Hidden=�$��Hidden=�#��z�� P�  �@J�IsMouseOver$
 true��. ��� $ border�� � �� �#1E88E5P�  �@J�	IsFocused$
 true��. ��� $ border�� � �� �#1E88E5�� $ border�� � �� �#FFFFFF��  � ��$ 0,0,0,0q��� -   & ��Name$& PasswordPlaceholder��$��1��$  False��$��Center=�$
��Left=�$��12,0,0,0q����#AAAAAA$��14(�$��Enter your password������ " ��.���  � y�$ 	Collapsed=�! �� " �� *
Password+$# PasswordBox��$  .a���  � y�$ Visible=��� $��Masuk��$
��15,0q�$��48��' � Cursor$
' Hand��$��0,0,0,15q����� *LoginCommand+���� $# PasswordBox������ " ��.���  � �� �#1E88E5��  � ��
 �������  � ��$ 15(���  � ��$ SemiBold"���  � ��$ 0q���  � �� �� " ��.R��� $ border��#��z��#��z��#��z��$
 8��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � �� �#1565C0�� $ border�� �@J�Effect   $ 2Q�$	 270Q�$ #1565C0��$	 0.4Q�$ 8Q�P�  � ��$
 True��. ��� $ border�� � �� �#0D47A1�� $ border�� �@J�Effect   $ 1Q�$	 270Q�$ #1565C0��$	 0.2Q�$ 4Q��� ���#F44336$��Medium"�$��0,0,0,15q�$
��Wrap=�$ Center=����� *ErrorMessage+� $��0,30,0,0q� �� $��* ��� $
��Auto �.+��� $,��&© 2025 SitarLib - All Rights Reserved��$��12(�$
��Left=����#777777$��Center=�� ���	#90000000y��� *IsBusy+( �� 	Converter#( [  .+��� ���#FFFFFF$
 10��$	��250��$	��140��$��Center=�$��Center=�   $ 3Q�$	 270Q�$ Black��$	 0.2Q�$ 10Q�._��� $��Center=�$��Center=�.+�� ) � IsIndeterminate$
) True��$��40��$��40�����#1E88E5$��0,0,0,15q����� " �.���  � �� �� " �.R��� 
������ $��0q�
������ $
 20��._�� .+�A� * A�Name$* 
PART_Track��$��4�����#E0E0E0
������ A� $* PART_Indicator��$��4�����#1E88E5
������ + �� StrokeDashArray$+ 0,100R�, J� RenderTransformOrigin$, 0.5,0.5&�z��� RotateTransform��- �� Angle$- 0Q� P�  �@�IsIndeterminate$
 true��. ��� $ PART_Indicator�� �@��StrokeDashArray$ 20,80R�. N� EnterActions. �� .���� .��W� / �� 
TargetName$/ RotateTransform��0 �� TargetProperty$0 Angle�1 W� From1 0	2 W� To2 3603 g� Duration$3 0:0:2D�4 g� RepeatBehavior$4 Forever���� $ ��Logging in, please wait...�����#333333$��Center=�$��14(�$��SemiBold"��� $ ��Verifying your credentials�����#777777$��Center=�$��12(�$��0,5,0,0q�