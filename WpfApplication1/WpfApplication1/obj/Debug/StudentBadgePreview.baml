   M S B A M L   `   `   `  ����G  CSchoolSafeID, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SchoolSafeIDSchoolSafeID  5      &     SchoolSafeID.StudentBadgePreview   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:SchoolSafeID     5      -   5          N� Title$  Student Badge Preview��5         SSIDPage#��[  6S   .��5
      ]� ��. .+�5   
   �� $	��Top=�6   6,   �� 5   
   $��Bottom=�6   $	��100��6-   
���   �.+�5      �� -   5        $��	footerBar��5   2    �� Orientation$ 
Horizontal=�6   ��5      �� 5       �� ImageSource$( "Resources/poweredby_schoolsafe.png��6!    �� Opacity$ 1Q�6R    j� Stretch$
 None=�6^    j� 
AlignmentX$ Right=�6m   .+�5      �� -   5        $��
btn_GoBack��5        MyButton#��[ 6+   $��	10 10 0 0q�6M   $��GO BACK��6`   
�������6�   5      5      �� 5   
   $
��Left=�6   6-   �� 5   
   $��Right=�6   6.   � 5   
   $	��960��6   $��Center=�6    .+�5      �� -   5        $��PageHeading��5       �� TextAlignment$ Center=�6+   $	��960��6B    # MaterialDesignDisplay1TextBlock#��[ 6N   $	��130��6�   �� 6�   $�ID Scan Successful.��6�   �� 6�   �� 6�   -   5         �Name$ PrintBadgeText��5   �   $��28(�6�   $)�#Would you like to print your badge?��6�   ���#7776  6*  � 5      $	��960��6   .+�5       �� $��Center=�6   $	��960��6:   .+�5!      � $	��555��6   $	��310��6'    � ColumnDefinitions 5"      �� 5#      $	��20* �6/   �� 5$      $	��30* �6/   �� 5%      $	��60* �6/   �� 5&      $	��60* �6/   	 � RowDefinitions	 5(      �� 5)      $	��10* �6,   �� 5*      $��3* �6,   .+�5,      �� 
 �� CornerRadius$
 8��6!   $��2��62   $��4��6C   $��1q�6W   ���#ccc6�   �� 5.      $��2��6%   $	��Top=�65   $	��246��6M   $��2��6Z   $��Center=�6n   $	��392��6�   .+�5/      �� -   5        $��txtSchoolName��5/   (   $��0,10,0,0q�6=   $��35��6O   $ Center=�6[   
���   �$��24(�6�   $��School Name Here��6�   �� 50      $��10q�6(   $��Stretch=�64   
���   �$��5��6_   (� 52      -   5        $��vbStudentName��52   D    (� Stretch$
 None=�6&   $	��360��65   .��53   "   �� -   5        $��txtStudentName��53   ,   $��Medium"�6B   
���   �$ Center=�6i   $�� ��6�   $��36(�6�   54      �� 56      -	   5        $��txtGrade��56   (   
���   �$��24(�6K   $ Center=�6Y   $��Grade��6p   �� 57      $��10q�6(   $��Stretch=�64   
���   �$��5��6_   �� 58      -
   5        $��txtDate��58   (   
���   �$��24(�6J   $ Center=�6X   $��Check-in: 09/15/19��6o   �� 59      -   5        $��txtTime��59   (   
���   �$��24(�6J   $ Center=�6X   $��08:23 AM��6o   5:      �� 5;      $��1��6%   $��4��62   $ 
Horizontal=�6F   $
��Auto��6_   $��Bottom=�6l   .+�5<      �� $��	10,5,40,0q�6$   $!��Resources/schoolsafe-id.png��67   $	��150��6\   $
��Left=�6h   �� 5=      $��Resources/bar_code.png��6$   $	��320��6D   $��Right=�6P   5>      �� 5?      $��2��6%   $	��Top=�69   $��10q�6Q   .+�5@      �� -   5        $��imgStudentImage��5@   $   $	��Top=�6;   $
��Left=�6S   $	��160��6n   �� 5A      -   5        $��txtSigninReason��5A   (   
���   �$
��Wrap=�6R   $��18(�6f   $
 Left=�6t   $��Sign-in Reason��6�   5B      5C      5D      5E      �� 5G      $��0 50 0 0q�6   $��Center=�6+   $	��960��6H   .+�5H      �� $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   .+�5I      �� -   5        $��btnPrintStudentPass��5I   h   $	��310��6   #��[ 6)   $��Print Student Pass��6K   6�   �� 5J      -   5        $��btnPrintTemporaryBadge��5J   �   $	��310��6   #��[ 6)   $��Print Temporary Badge��6K   ���#AC13156�   �� 5K      -   5        $��btnNoBadgeNeeded��5K   �   $	��310��6   #��[ 6)   $��No Badge Needed��6K   ���#FC0���#1116�   5L      5M      5O      5S      5T      