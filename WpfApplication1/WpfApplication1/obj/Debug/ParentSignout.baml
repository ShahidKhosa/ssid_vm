   M S B A M L   `   `   `  ����G  CSchoolSafeID, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SchoolSafeIDSchoolSafeID  5           SchoolSafeID.ParentSignout   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RGu.Wpf.Adorners, Version=1.5.2.0, Culture=neutral, PublicKeyToken=6dfb64f9972bd31d$adornershttp://gu.se/Adorners  5      &localclr-namespace:SchoolSafeID  5      X TXceed.Wpf.Toolkit, Version=3.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45wpfx1)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          N� Title$  Parent Sign-out��5      |�.    SSIDPage#��[  6E    +� FocusedElement 5
      ��  �� ElementName$ txtBarcodeData��.��5      ]� ��. .+�5   
   �� $	��Top=�6   6,   �� 5   
   $��Bottom=�6   $	��100��6-   
���   �.+�5      �� -   5        $��	footerBar��5   2    �� Orientation$ 
Horizontal=�6   ��5      �� 5       �� ImageSource$( "Resources/poweredby_schoolsafe.png��6!    �� Opacity$ 1Q�6R    j� Stretch$
 None=�6^    j� 
AlignmentX$ Right=�6m   .+�5      �� -   5        $��btnBack��5   �     MyButton#��[ 6   $
��Left=�6;   $��	10 10 0 0q�6V   $��GO BACK��6i   
�������6�   5      5      �� 5   
   $
��Left=�6   6-   �� 5   
   $��Right=�6   6.   �� 5   
   $��Center=�6   $��Center=�62   $��0q�6M   .+�5       ��  # MaterialDesignDisplay1TextBlock#��[ 6   $
��Wrap=�6Q   $
��Left=�6e   $��36(�6�   $��0,0,0,10q�6�   $��STUDENT SIGN-OUT��6�   6�   �� 5!      .+�5"      �� -   5        $��show_suggestions��5"      .+�5#      ��   	ssidLabel#��[ 6   .��6?   Enter Student Name:6T   �� 5$      -   5        $��
cmbStudent��5,   W   $��0q�5$      $	��700��6*     ssid_ComboBox#��[ 5%      $��Center=�5&      $��Center=�5'       �� IsTextSearchEnabled .  	 �� 
IsEditable	 . ��5-      �� 5.      
 �� 
TargetType"
 ��6$   .�5/   "   ��  �� Property � �� �� Value$ 80��6;   �� 50   "    � ��$ 15q�6<   �� 51   "    � ��$ Center=�6F   52      54      55      �� 57      -   5        $��manual_entry_label��57      $ 
Horizontal=�67   .+�58      �� $	��400��6   #��[ 6(   .��6K   Enter Student Name:6`   �� 59      $��	15 0 15 0q�6   $	��120��6/   #��[ 6;   .��6^   Grade:6f   5:      �� 5<      -   5        $��manual_entry_fields��5<      $ 
Horizontal=�68   .+�5=      �� -   5        $��txtStudentName��5=   Z   $��0q�6   $	��400��6)     MyTextField#��[ 65   6Z   �� 5>      -	   5        $��txtStudentGrade��5>   b   $��	15 0 15 0q�6   $	��120��61   #��[ 6=    �� 	MaxLength$ 10��6y   �� 5?      -
   5        $��	btnAddNew��5?      $	��150��6.   #��[ 6:   $��ADD NEW��6\   $��0q�6n   $��72��6y   6�   5@      �� 5B      -   5        $��lblSelectedStudents��5B      $y�	Collapsed=�63   #��[ 6J   .��6m   Selected Student(s):6�   �� 5C      -   5        $��selecteStudents��5C   ~   $��1q�6   $y�	Collapsed=�6.   ���#ccc$ �� IsSynchronizedWithCurrentItem$
 TrueY�6X   $��Stretch=�6�   $	��Top=�6�   $	��110��6�    �� AlternationCount$ 2��6�     AlternatingListBoxItem#��[ 6�   ��5D      �� 5E      .R�5F      �� $ 
Horizontal=�6)   .+�5G   "   �� $	��600��6,   $��20(�68   $
��Left=�6F   $��Center=�6a   ��6|   �� *Name+�� 5H   "   -   5        ���#088F40
�������$��Delete��5H   d   
 � Tag 6Q   �� *ID+6�   5I      5J      5L      �� 5N      #��[ 6   .��6;   Reason for sign-out:6Q   �� 5O      -   5        $��cmbSignoutReasons��5S      $��0q�5O      $	��700��6&    �� Text$ Select Reason...��62   #��[ 5P      $��Center=�5Q      $��Center=�5R      ��5T      �� 5U      "
 ��6    .�5X      ��  � ��$ Center=�6B   5Y      5[      �� 5]      #��[ 6   .��6;   ,*Authorized Individual Signing Student Out:6g   �� 5^      -   5        $��txtIndividual��5^   V   $	��700��6   $��0q�6&   #��[ 61   {�.  6k   �� 5`      -   5        $��
btnConfirm��5`      #��[ 6+   $��SUBMIT��6M   $��0 20 0 0q�6^   6p   5b      5d      5f      5h      