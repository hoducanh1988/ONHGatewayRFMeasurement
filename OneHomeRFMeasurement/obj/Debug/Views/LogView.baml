   M S B A M L   `   `   `  ����O  KOneHomeRFMeasurement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:OneHomeRFMeasurement.ViewsOneHomeRFMeasurement.Views  5      (    "OneHomeRFMeasurement.Views.LogView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:OneHomeRFMeasurement.Views     5      
�������  � 	Resources  5
       �� 
TargetType �� Property   fontSizeNormal �� Value �� Triggers P� Property P� Value��%�  '	�       7   �� 5   
   " 	�6   .�5      ��  � ��8   6)   �� 5       � ��$ Normal"�6+   �� 5       � ��
 �   �6+   �� 5       � ��$ 30��6'   �� 5       � ��$ 30,0,0,0q�6'   �� 5       � ��$ Center=�69    5      P� 5       �@d�	IsChecked$
 TrueY�60   . �5      ��  � ��
 �  ��63   �� 5       � ��$ SemiBold"�63   5      5      5      .��5      �� $��5,5,5,5q�6   .+�5   
   ��   fontSizeHeader#��[ 6   $;��5Vui lòng chọn loại dữ liệu cần truy vấn:��6;   	� 5   
   $��Log file��6_    d� 	IsChecked 6   ��  �� Path$ LM.isLog�	 �� UpdateSourceTrigger$	 PropertyChanged=�6_   	� 5   
   $��Setting file��6c    6   �� $ LM.isSetting�$	 PropertyChanged=�6c   	� 5   
   $,��&Tài liệu hướng dẫn sử dụng��6e    6   �� $ LM.isUserGuide�$	 PropertyChanged=�6e   �� 5!   
   $��	10,10,0,0q�6/   $��90��6B   $��35��6M     fontSizeButton#��[ 6Y   $
��Left=�6�   $��Go��6�   ��6   �� *	GoCommand+6�   5#      5%      