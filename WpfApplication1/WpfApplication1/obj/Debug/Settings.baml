   M S B A M L   `   `   `  ����G  CSchoolSafeID, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SchoolSafeIDSchoolSafeID  5   	       SchoolSafeID.Settings   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RGu.Wpf.Adorners, Version=1.5.2.0, Culture=neutral, PublicKeyToken=6dfb64f9972bd31d$adornershttp://gu.se/Adorners  5   	   &localclr-namespace:SchoolSafeID  5   	   X TXceed.Wpf.Toolkit, Version=3.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45wpfx1)http://schemas.xceed.com/wpf/xaml/toolkit  5   	      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��14(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignFont#���  5   	   $��20q�5   	    � WindowStyle$ 
ToolWindow=�6    � 
ResizeMode$ NoResize=�6/    � Title$ Settings��5   	    � Icon$ favicon.ico��6   $��Center=�6-   $��Center=�6J   $��Center=�5   	   $	��700��6-   $	��365��69    � WindowStartupLocation$ CenterScreen=�6F   .��5      �� $��0,5,0,0q�6   .+�5   
   �� $��20,0,0,0q�6   .��6"   Select Printer:63   �� 5   
   -   5        $��printersList��5   M     ssid_ComboBox#��[ 6   $��	20,0,20,0q�6:   $
��Auto��6a   �� 5   
   $��20,5,0,0q�6   .��6"   
School No:6.   �� 5   
   -   5        $��
school_url��5   Z    � 
InputScope$ Number��6    �� 	MaxLength$ 15��6&     MyTextField#��[ 65     Gu.Wpf.Adorners.Watermark	   Text$	 	School No��6l   $��	20,0,20,0q�6�   $
��Auto��6�   6�   �� 5   
   -   5        $��btnSettings��5        MyButton#��[ 6$   $
��Save��6F   $��85��6U   $	��220��6a   $
��Left=�6m   $��20q�6�   6�   5      5      