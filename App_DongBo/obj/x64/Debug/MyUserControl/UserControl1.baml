   M S B A M L   `   `   `  ����E  AApp_DongBo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:App_DongBo.MyUserControlApp_DongBo.MyUserControl  5      +    %App_DongBo.MyUserControl.UserControl1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5UC&clr-namespace:App_DongBo.MyUserControl     5      -   5        ���#faf0e6.��5      �   � ColumnDefinitions  5   
   �� 5      $	��86* �6   �� 5      $
��145* �6   �� 5      $	��95* �6    � RowDefinitions 5   
   �� 5      $	��24* �6   �� 5      $
��233* �6   �� 5      $
��498* �6   .+�5   
   �� -   5         ��Name$ Section1��5      $��1��6#   $��0��60    �� CornerRadius$ 21��5       $��3q�6#   
���   ����#5f9ea0$��
18,0,18,21q�5"       J� Effect 5#      3  -System.Windows.Media.Effects.DropShadowEffect  5$         
BlurRadius$ 3Q�6#      ShadowDepth$ 15Q�62      Color$ Black��6C   ._�5&      �   5'      �� 5(      $��* �6'   �� 5)      $��* �6'   �� 5*       5,      �� 5-      $
��1.5* �6$   �� 5.      �� 5/      .+�52      �� -   5         ��Name$ Oneshot_Button��52      $��0��61   $��0��6A      cameraButton#��[  6N   .��54      ��   menuButtonIcon#��[ 6   $(��"/App_DongBo;component/Images/1.ico��6D   55      �� 56      -   5        $ Continuous_Button��56      $��1��64   $��0��6D   #��[  6Q   .��57      �� #��[ 6   $,��&/App_DongBo;component/Images/Video.ico��6D   58      �� 59      -   5        $ Stop_Button��59      $��2��6.   $��0��6>   #��[  6K   .��5;      �� #��[ 6   $+��%/App_DongBo;component/Images/Stop.ico��6D   5<      �� 5>      -   5        	 ��Name$	 Camera_status��5>      $��1��63   $��3��6@   $��Center=�5?      
�������$��42(�5@      
 �� TextAlignment$
 Center=�6)   $
��Wrap=�6@   ��5?   6   �� *
Chain_ID+5@   @   �� 5A      -   5        $��2��5A      $��3��6&   $��Chọn��6:   $��27(�6I   $
��Bold"�6W   
���   �$��3q�5B   -   $��52��6A   $	��180��6M   $��Center=�6Y   $	��Top=�5C       � 	Resources 5D       �� 
TargetType �� Property �� Value��%_   '��       �� 5E      " ��6    .�5F      ��  �@��CornerRadius$ 21��6=   5G      5H      5I      5J      5K      �� 5O   
   -   5        $ Section2��5O      $��2��6#   $��0��60   $ 21��5P      $��3q�6#   
���   ����#5f9ea0$��
18,5,18,18q�5R       5S        5T      $ 3Q�6#   $ 15Q�62   $ Black��6C   ._�5V      �   5W      �� 5X      �� 5Y      �� 5Z       5\      �� 5]      $	��97* �6$   �� 5^      $	��32* �6$   �� 5_      $	��45* �6$   �� 5`      $	��35* �6$   �� 5a      $	��51* �6$   �� 5b      $	��35* �6$   �� 5c      $	��44* �6$   �� 5d      $	��70* �6$   �� 5e      $	��60* �6$   .+�5g      �� -	   5        $ Draw_Button��5g      $��0��6.   $��0��6>     
menuButton#��[ 6K   .��5h      �� #��[ 6   $*��$/App_DongBo;component/Images/Add.ico��6D   5i      �� 5j      -
   5        $ Save_Button��5j      $��1��6.   $��0��6>   #��[ 6K   .��5k      �� #��[ 6   $+��%/App_DongBo;component/Images/Save.ico��6D   5l      �� 5m      -   5        $��2��5m      $��0��6)   #��[ 66   .��5n      �� $,��&/App_DongBo;component/Images/Clear.ico��6   $��39��6L   $��36��6X   5o      �� 5p      $��3��6   $��Chọn loại xích��60   $
��Bold"�6F   $��24(�6X   $��1��6f   $��Center=�6s   $��32��6�   $
��15,0q�6�   �� 5q      -   5         ��Name$ Choose_model��5q      $��2��61   $��3��6?   $��	15,5,15,5q�6S   .@�5r      �� $��
Xích dày��6#   �� 5s      $��Xích mỏng��6#   5t      �� 5u      $��3��6   $��3��6&   $ 9��6:   $��3q�6K   
���   �$��	18,0,18,0q�5v   -   ���#191E95._�5w      �� $��24(�6    $
 Center=�6.   $��Center=�6E   $��Làm mỏng��6`   
�������6p   5x      �� 5z      $��4��6   $��3��6&   $��Center=�6:   $��45��6U   ._�5{      �   5|      �� 5}      �� 5~      .+�5�      �� -   5         ��Name$ Row_ero��5�   "     TextRow_Hint#��[ 63    �� TextAlignment$ Center=�6Y   $��30(�6p   $��3q�6~   
���   �$��	15,0,15,0q�5�   6   .��6I    5�      ��%_   '��       �� 5�   "   " ��6(   .�5�   &   ��  �@��CornerRadius$ 16��6E   5�   #   5�      .��61   5�      �� 5�      -   5        $ Col_ero��5�   "     TextCol_Hint#��[ 63   $ Center=�6Y   $��30(�6p   $��3q�6~   
���   �$��1��5�   6   $��	15,0,15,0q�6F   .��6Y    5�      ��%_   '��       �� 5�   "   " ��6(   .�5�   &   ��  �@��CornerRadius$ 16��6E   5�   #   5�      .��61   5�      5�      5�      �� 5�      $��5��6   $��3��6&   $ 9��6:   $��3q�6K   
���   ����#191E95$��	18,0,18,0q�5�   B   ._�5�      �� $��24(�6    $
 Center=�6.   $��Center=�6E   $��	Làm dày��6`   
�������6o   5�      �� 5�      $��6��6   $��3��6&   ._�5�      �   5�      �� 5�      �� 5�      .+�5�      �� -   5        $ Row_cls��5�   "   #��[ 63   $ Center=�6Y   $��30(�6p   $��3q�6~   
���   �$��	15,0,15,0q�6�   .��6�    5�      ��%_   '��       �� 5�   "   " ��6(   .�5�   &   ��  �@��CornerRadius$ 16��6E   5�   #   5�      .��61   5�      �� 5�      -   5        $ Col_cls��5�   "   #��[ 63   $ Center=�6Y   $��30(�6p   $��3q�6~   
���   �$��1��6�   $��	15,0,15,0q�6�   .��6�    5�      ��%_   '��       �� 5�   "   " ��6(   .�5�   &   ��  �@��CornerRadius$ 16��6E   5�   #   5�      .��61   5�      5�      5�      �� 5�      $��7��6   $��3��6&   $��Center=�6:   $��50��6U   ._�5�      �   5�      �� 5�      �� 5�      .+�5�      �� -   5          
ModeButton#��[ 5�   !   $�� Diện tích ��6E   $��27(�6[   $��50��6�   $��2��6�   $
��30,0q�6�   5�      5�      �� 5�      -   5        $ Border1��5�      $ 15��6*   
�������
���   �$��3q�6c   $��8��6w   $��3��6�   $��
18,0,18,12q�6�   ._�5�      �� -   5        $	 
Ideal_Area��5�       $��30(�64   $
 Center=�6B   5�      5�      5�      �� 5�   
   $��1��6   $��2��6!   $��
24,0,24,24q�62   $��1��6F   $ 12��6S   
���   �}�. ._�5�      �� -   5         ��Name$ canvasControl1��5�      ��5�      �� 5�      -   5        pictureBox1�� j� Stretch$
 Fill=�5�   6   5�      5�      �� 5�   
   $��2��6   $��18,12,18,24q�6!   $��2��66   $ 12��6C   
���   �}�. ._�5�      �� -   5        $ canvasControl2��5�      ��5�      �� 5�      -   5        pictureBox2��$
 Fill=�5�   6   5�      5�      �� 5�   
   -   5        $ Section4��5�      $��2��6$   $��1��64   $ 21��6A   $��3q�6S   
���   ����#5f9ea0$��
18,0,18,18q�5�   :    5�        5�      $ 3Q�6#   $ 15Q�62   $ Black��6C   ._�5�      �   5�      �� 5�      �� 5�       5�      �� 5�      $	��66* �6$   �� 5�      $	��36* �6$   �� 5�      $	��43* �6$   �� 5�      $	��20* �6$   .+�5�      d� -   5         d�Name$ 
btnToggle1��5�        ToggleButton#��[ 63   $��52��6Y   $	��131��6e   $��Center=�6q   $��Center=�6�   6�   �� 5�      -   5        $��1��5�      $
��Lưu��6)   $��27(�67   $
��Bold"�6E   
���   �$��3q�5�      $��52��6-   $	��180��69   $��Center=�6E   $��Center=�6b    5�      ��%_   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 21��6=   5�      5�      5�      �� 5�      $��1��6   $��2��6&   $ 9��6:   $��3q�6K   
���   �$��	12,9,12,3q�5�   -   ���#191E95._�5�      �� $��24(�6    $
 Center=�6.   $��Center=�6E   $!��Chênh lệch tối thiểu��6`   
�������6|   5�      �� 5�      -   5        $ Max_diff��5�      $ Center=�6,   $��30(�6C   $��3q�6Q   
���   �$��2��5�   .   $��2��6B   $��	12,4,12,4q�6O   .��6b    5�      ��%_   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 16��6=   5�      5�      .��6)   5�      5     5     5     5     