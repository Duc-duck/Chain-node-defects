   M S B A M L   `   `   `  ���� E  AApp_DongBo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:App_DongBo.MyUserControlApp_DongBo.MyUserControl  +    %App_DongBo.MyUserControl.UserControl1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5UC&clr-namespace:App_DongBo.MyUserControl     -   ���#faf0e6.���   � ColumnDefinitions  �� $	��86* ��� $
��145* ��� $	��95* � � RowDefinitions �� $	��24* ��� $
��233* ��� $
��498* �.+��� -    ��Name$ Section1��$��1��$��0�� �� CornerRadius$ 21��$��3q�
���   ����#5f9ea0$��
18,0,18,21q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 3Q�   ShadowDepth$ 15Q�   Color$ Black��._��   �� $��* ��� $��* ���  �� $
��1.5* ��� �� .+��� -    ��Name$ Oneshot_Button��$��0��$��0��   cameraButton#��[  .����   menuButtonIcon#��[ $(��"/App_DongBo;component/Images/1.ico���� -   $ Continuous_Button��$��1��$��0��#��[  .���� #��[ $,��&/App_DongBo;component/Images/Video.ico���� -   $ Stop_Button��$��2��$��0��#��[  .���� #��[ $+��%/App_DongBo;component/Images/Stop.ico���� -   	 ��Name$	 Camera_status��$��1��$��3��$��Center=�
�������$��42(�
 �� TextAlignment$
 Center=�$
��Wrap=����� *
Chain_ID+�� -   $��2��$��3��$��Chọn��$��27(�$
��Bold"�
���   �$��3q�$��52��$	��180��$��Center=�$	��Top=� � 	Resources  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 21���� -   $ Section2��$��2��$��0��$ 21��$��3q�
���   ����#5f9ea0$��
18,5,18,18q�   $ 3Q�$ 15Q�$ Black��._��   �� �� ��  �� $	��97* ��� $	��32* ��� $	��45* ��� $	��35* ��� $	��51* ��� $	��35* ��� $	��44* ��� $	��70* ��� $	��60* �.+��� -	   $ Draw_Button��$��0��$��0��  
menuButton#��[ .���� #��[ $*��$/App_DongBo;component/Images/Add.ico���� -
   $ Save_Button��$��1��$��0��#��[ .���� #��[ $+��%/App_DongBo;component/Images/Save.ico���� -   $��2��$��0��#��[ .���� $,��&/App_DongBo;component/Images/Clear.ico��$��39��$��36���� $��3��$��Chọn loại xích��$
��Bold"�$��24(�$��1��$��Center=�$��32��$
��15,0q��� -    ��Name$ Choose_model��$��2��$��3��$��	15,5,15,5q�.@��� $��
Xích dày���� $��Xích mỏng���� $��3��$��3��$ 9��$��3q�
���   �$��	18,0,18,0q����#191E95._��� $��24(�$
 Center=�$��Center=�$��Làm mỏng��
��������� $��4��$��3��$��Center=�$��45��._��   �� �� .+��� -    ��Name$ Row_ero��  TextRow_Hint#��[  �� TextAlignment$ Center=�$��30(�$��3q�
���   �$��	15,0,15,0q�.�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 16��.���� -   $ Col_ero��  TextCol_Hint#��[ $ Center=�$��30(�$��3q�
���   �$��1��$��	15,0,15,0q�.�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 16��.���� $��5��$��3��$ 9��$��3q�
���   ����#191E95$��	18,0,18,0q�._��� $��24(�$
 Center=�$��Center=�$��	Làm dày��
��������� $��6��$��3��._��   �� �� .+��� -   $ Row_cls��#��[ $ Center=�$��30(�$��3q�
���   �$��	15,0,15,0q�.�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 16��.���� -   $ Col_cls��#��[ $ Center=�$��30(�$��3q�
���   �$��1��$��	15,0,15,0q�.�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 16��.���� $��7��$��3��$��Center=�$��50��._��   �� �� .+��� -     
ModeButton#��[ $�� Diện tích ��$��27(�$��50��$��2��$
��30,0q��� -   $ Border1��$ 15��
�������
���   �$��3q�$��8��$��3��$��
18,0,18,12q�._��� -   $	 
Ideal_Area��$��30(�$
 Center=��� $��1��$��2��$��
24,0,24,24q�$��1��$ 12��
���   �}�. ._��� -    ��Name$ canvasControl1������ -   pictureBox1�� j� Stretch$
 Fill=��� $��2��$��18,12,18,24q�$��2��$ 12��
���   �}�. ._��� -   $ canvasControl2������ -   pictureBox2��$
 Fill=��� -   $ Section4��$��2��$��1��$ 21��$��3q�
���   ����#5f9ea0$��
18,0,18,18q�   $ 3Q�$ 15Q�$ Black��._��   �� ��  �� $	��66* ��� $	��36* ��� $	��43* ��� $	��20* �.+�d� -    d�Name$ 
btnToggle1��  ToggleButton#��[ $��52��$	��131��$��Center=�$��Center=��� -   $��1��$
��Lưu��$��27(�$
��Bold"�
���   �$��3q�$��52��$	��180��$��Center=�$��Center=� ��%:   '��       �� " ��.���  �@��CornerRadius$ 21���� $��1��$��2��$ 9��$��3q�
���   �$��	12,9,12,3q����#191E95._��� $��24(�$
 Center=�$��Center=�$!��Chênh lệch tối thiểu��
��������� -   $ Max_diff��$ Center=�$��30(�$��3q�
���   �$��2��$��2��$��	12,4,12,4q�.�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 16��.��