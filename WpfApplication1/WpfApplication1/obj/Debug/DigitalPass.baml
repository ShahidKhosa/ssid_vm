   M S B A M L   `   `   `  ����  SchoolSafeID    SchoolSafeID.DigitalPass   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      V RGu.Wpf.Adorners, Version=1.5.2.0, Culture=neutral, PublicKeyToken=6dfb64f9972bd31d$adornershttp://gu.se/Adorners  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      X TXceed.Wpf.Toolkit, Version=3.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45wpfx1)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          N� Title$  Get Digital Pass��5	         SSIDPage#��[  65   .��5      ]� ��. .+�5   
   �� $	��Top=�6   6,   �� 5   
   $��Bottom=�6   $	��100��6-   
���   �.+�5      �� -   5        $��	footerBar��5   2    �� Orientation$ 
Horizontal=�6   ��5      �� 5       �� ImageSource$( "Resources/poweredby_schoolsafe.png��6!    �� Opacity$ 1Q�6R    j� Stretch$
 None=�6^    j� 
AlignmentX$ Right=�6m   .+�5      �� -   5          MyButton#��[ 5      $��	10 10 0 0q�6;   $��GO BACK��6N   
�������6r   5      5      �� 5   
   $
��Left=�6   6-   �� 5   
   $��Right=�6   6.   � 5   
   $��0,35,0,0q�6   $
��1200��6!   $	��Top=�6.    � ColumnDefinitions 5       �� 5!      $	��430 �6#   �� 5"      $��2* �6#    � RowDefinitions 5$      �� 5%      $��* �6    .+�5(      �� $��0��6   $��Right=�6)   $��0 93 0 0q�6E   .+�5)      �� $	��372��6   $&�� Resources/digit_pass_preview.png��6$    � ToolTip$ Digital Pass��6N   5*      � 5,      $��1��6   $��Center=�6(   .+�5-      �� ���#088F40$��70(�61   	 �� 
LineHeight$	 50��6?   $��SemiBold"�6O    # MaterialDesignDisplay3TextBlock#��[ 6e   $
��Wrap=�6�   $
��Left=�6�   $��0q�6�   $��	Save Time��6�   6�   �� 5/      .+�50      %  Xceed.Wpf.Toolkit.MaskedTextBox  -   5        $��	txt_Phone��50   O     MsTextField#��[ 6*   
 � 
InputScope$
 TelephoneNumber��6`     WaterMarkStyle  Gu.Wpf.Adorners.Watermark   	TextStyle# [ 6}      Text$ Enter Phone Number��6�   6  �� 51      -   5        $��	txt_Email��51   q    �� 	MaxLength$	 100��6   $
 EmailSmtpAddress��6.     MyTextField#��[ 6L   # [ 6�   $ Enter Email Address��6�   6  52      �� 54      $ 
Horizontal=�6   .+�55      �� -   5        $��btnCreateDigtalPass��55      #��[ 68   $��SEND MY PASS AND CONTINUE��6Z   $	��420��6~   $��0 10 0 0q�6�   6�   �� 56      -   5        $��btnNoThanks��56      #��[ 60   $��
NO, THANKS��6R   $	��210��6g   $��	20 10 0 0q�6s   6�   57      �� 59      $��0,15,0,0q�6   .+�5:      �� ���#088F40$��36(�65   $	 30��6C   $��Medium"�6S    # MaterialDesignDisplay1TextBlock#��[ 6g   $
��Left=�6�   $��0,15,0,0q�6�   $��Receive the Digital Pass��6�   6�   �� 5;      $
��25,3q�6&    �� Bullet 5<      �� 5=      $��18��6$   $��18��6/   $��Resources/select.png��6;   6[   ._�5?      �� $��Faster Check-In��6$   $��24(�6;   $��8,0,0,0q�6I   6\   5@      �� 5A      $
��25,3q�6&    5B      �� 5C      $��18��6$   $��18��6/   $��Resources/select.png��6;   6[   ._�5E      �� $��No License Needed��6$   $��24(�6=   $��8,0,0,0q�6K   6^   5F      �� 5G      $
��25,3q�6&    5H      �� 5I      $��18��6$   $��18��6/   $��Resources/select.png��6;   6[   ._�5K      �� $"��Receive School Notifications��6$   $��24(�6H   $��8,0,0,0q�6V   6i   5L      5M      5O      5P      5R      5T      