   M S B A M L   `   `   `  ����G  CSchoolSafeID, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SchoolSafeIDSchoolSafeID  5          SchoolSafeID.ScanLicense   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RGu.Wpf.Adorners, Version=1.5.2.0, Culture=neutral, PublicKeyToken=6dfb64f9972bd31d$adornershttp://gu.se/Adorners  5      &localclr-namespace:SchoolSafeID  5      X TXceed.Wpf.Toolkit, Version=3.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45wpfx1)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          N� Title$  Scan License��5
      |�.    SSIDPage#��[  6B    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Open��5      6U    J� InputBindings 5      �� 5   
    �� Command$ ApplicationCommands.Open��6    �� Gesture$ Ctrl+O��68   .��5      ]� ��. .+�5   
   �� $	��Top=�6   6,   �� 5   
   $��Bottom=�6   $	��100��6-   
���   �.+�5      �� -   5        $��	footerBar��5   2    �� Orientation$ 
Horizontal=�6   ��5      �� 5       �� ImageSource$( "Resources/poweredby_schoolsafe.png��6!    �� Opacity$ 1Q�6R   	 j� Stretch$
	 None=�6^   
 j� 
AlignmentX$
 Right=�6m   .+�5      �� -   5          MyButton#��[ 5      $
��Left=�6;   $��	10 10 0 0q�6V   $��GO BACK��6i   
�������6�   �� 5      -   5        #��[ 5      $
��Left=�6;   $��	10 10 0 0q�6V   $��OFFICE USE ONLY��6i   6�   5      5       �� 5"   
   $
��Left=�6   6-   �� 5$   
   $��Right=�6   6.   � 5&   
   $
��1200��6   $��Center=�6   $��Center=�69   $��0,0,0,0q�6T    � ColumnDefinitions 5'      �� 5(      $	��430 �6#   �� 5)      $��2* �6#    � RowDefinitions 5+      �� 5,      $��* �6    .+�5/      �� $��0��6   $��Right=�6)   $	��Top=�6E   $��0,42,0,0q�6]   .+�50      �� $	��369��6   $	��261��6$   $��0 16 0 0q�61   $#��Resources/barcode_scanner.png��6C   �� 51      $
��Left=�6    # MaterialDesignDisplay1TextBlock#��[ 67   $��0 10 0 0q�6p   $
��Wrap=�6�   $��20(�6�   ���#249c24$D��>Press scan button on device if it does not automatically scan.��6�   6  �� 53      $��0 50 0 0q�6   $	��369��6*   $	��111��66   $��Right=�6C   $��Resources/pdf417.png��6_   �� 54      $��Center=�6   $��0 10 0 0q�69   #��[ 6K   $
��Wrap=�6�   $��20(�6�   $(��"Sample barcode on back of license.��6�   6�   55      � 57      $��1��6   $��Center=�6(   $	��Top=�6E   .+�58      �� #��[ 6   $
��Wrap=�6U   $
��Left=�6i   $��35(�6�   $��0,0,0,0q�6�   $1��+Scan Drivers License or Frequent Visitor ID��6�   6�   �� 59      $��0 8 0 0q�6   .+�5:      (  "Xceed.Wpf.Toolkit.WatermarkTextBox  -   5        $��txt_FirstName��5:   p    � 
InputScope$ PersonalFullName��6-     WmTextField#��[ 6K      	Watermark$ Legal First Name��6�   {�.  6�     5;      -   5        $��txt_LastName��5;   p   $ PersonalFullName��6-   #��[ 6K   $ Legal Last Name��6�   {�.  6�   %  Xceed.Wpf.Toolkit.MaskedTextBox  5<      -   5        $��txt_DateOfBirth��5<   c   $ Number��6*     MsTextField#��[ 6>   {�.    WaterMarkStyle  Gu.Wpf.Adorners.Watermark   	TextStyle# [ 6�      Text$  Date of Birth (MM/DD/YYYY)��6�   6$  �� 5=      -	   5        $��
btnConfirm��5=      {�.  #��[ 6A   $��CONFIRM AND SUBMIT��6c   $	��650��6�   $��	0 10 0 15q�6�   6�   5>      �� 5@      $��0 11 0 0q�6   $	��650��6.    �� 
LineHeight$ 36��6:   $��24(�6T   #��[ 6b   $
��Wrap=�6�   $����Hold your drivers license up to the scanning device as shown. The back of your license should face the scanning device so that it can read the 2D bar code. Once you hear the beep, please confirm your first name, last name and date of birth. Hit submit to continue.��6�   6�  5B      5C      5E      5G      