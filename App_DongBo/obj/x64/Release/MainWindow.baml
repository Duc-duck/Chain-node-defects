   M S B A M L   `   `   `  ���� E  AApp_DongBo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:App_DongBo.MyUserControlApp_DongBo.MyUserControl  'clr-namespace:App_DongBo
App_DongBo      App_DongBo.MainWindow   -UC&clr-namespace:App_DongBo.MyUserControl  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:App_DongBo     -     � WindowState$  	Maximized=� � WindowStyle$ ThreeDBorderWindow=� � Title$ 
MainWindow��$��1629.95��$	��800�����#0a7e8c.���  � RowDefinitions �� $��3* ��� $	��46* � � ColumnDefinitions �� $
��642* ��� $
��173* �.+��� -    ��Name$ 
TabControl��$��0��$��2��$��0��$��2�����#faf0e6.@��� $
��Home��$
��Bold"�$��18(�.���  �� $��8* ��� $��2* � �� $	��29* ��� $	��58* ��� $	��41* ��� $	��17* ��� $	��58* �.+��� $��2�����#0a7e8c$��-16,12,12,12q� �� CornerRadius$ 18��._��  �� $��* ��� $
��0.4* ��� �� $
��0.4* ��� �� $��2* �.+��� -   $��
Model_code��$��42(� �� TextAlignment$ Center=�$��Center=�$��Center=�
�����������  �� ElementName$ User1��	 �� Path$	 Chain_ID��� $ 18��$��3q�$��Bottom=�$
��Left=�$��1��
���   �$��40��$	��150�����#191E95$��15,0,0,0q�._��� $��24(�$ Center=�$��Center=�$��Camera 1��
�������d� -   
 d�Name$
 
btnToggle1��   ToggleButton#��[  $��2��$��52��$	��131��$��Center=�$��Center=��� $ 18��$��3q�$��Bottom=�$
��Left=�
���   �$��40��$	��150�����#191E95$��3��$��15,0,0,0q�._��� $��24(�$ Center=�$��Center=�$��Camera 2��
�������d� -   $
 
btnToggle2��#��[  $��4��$��52��$	��131��$��Center=�$��Center=��� $��3q�
���  ��$��0��$��1��$ 9��$��6,12,6,0q�
���   �$��3��._��� -    ��Name$ canvasControl1������ -   pictureBox1�� j� Stretch$
 Fill=��� $��0��$��4��._��  �� �� .+��� $��3q�
���  ��$ 9��$��6,12,6,0q�
���   �._��� -   $ canvasControl5������ -	   pictureBox5��$
 Fill=��� $��3q�
���  ��
���   �$ 9��$��6,12,6,0q�$��1��._��� -
   $ canvasControl6������ -   pictureBox6��$
 Fill=��� $��3q�
���  ��$��0��$��2��$y�	Collapsed=�
���   �$ 9��$��6,12,6,0q�$��2��._��� -   $ canvasControl3���� $��3q�
���  ��$��0��$��4��
���   �$ 9��$��6,12,6,0q�$y�	Collapsed=�._��� -   $ canvasControl4��$
��Auto��$
��Auto���� $��1��$��2��$��0,598,328,0q�$��2��.+��� $��
Chain node��$��Center=�$��24(�$
��Bold"��� -    ��Name$ Chain_status_color��
���   �
������ $ 12��$��	60,0,60,0q�$��90��._��� -    ��Name$ Chain_status_text��$	��...��$ Center=�$��Center=�$��Center=�$��72(�$
��Bold"��� $��2��$��3��$��0,598,464,0q�$��2��$y�	Collapsed=�.+��� $��Washer��$��Center=�$��24(�$
��Bold"��� -   $ Washer_status_color��
���   �
������ $ 12��$��	60,0,60,0q�$��90��._��� -   $ Washer_status_text��$	��...��$ Center=�$��Center=�$��Center=�$��72(�$
��Bold"��� $��4��$��	0,598,0,0q�$��2��.+��� $�� ��$��Center=�$��24(�$
��Bold"��� -   $ Roller_status_color��
���   �
������ $ 12��$��90��._��� -   $ Roller_status_text��
����  �$ Center=�$��Center=�$��Center=�$��72(�$
��Bold"��� $��
Chain node��$��SemiBold"�$��18(�.��+   %App_DongBo.MyUserControl.UserControl1  -     Name$ User1���� $��0��$��1�� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ 
Run_Button��$	��Run��  
ModeButton#��[ $	��135���� -   $ Edit_Button��$
��Edit��#��[ $	��135��