   M S B A M L   `   `   `  ����F  BCopyManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CopyManagerCopyManager  5   	       CopyManager.NuevoUsuario   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CopyManager     5   	     � Title$  Copy Manager��5   	   $	��400��6   $	��500��6+    � 
ResizeMode$ NoResize=�67   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6   � 5      $ 	#FF4E7C2F��6    � Offset$ 1Q�6-   5      .��5      � .+�5   
   ��  �� CornerRadius$ 20��6   $��10q�6#   ��5      �� 5       �� ImageSource$M Ghttps://i.pinimg.com/474x/fb/9b/ab/fb9bab6fa5d773dbd222fc3de1b5a2a8.jpg��6    j� Stretch$ UniformToFill=�6s   	 �� Opacity$
	 0.95Q�6�   ._�5      �� $��20q�6   .+�5      �� $��Crear Usuario��6   
�������$��32(�6C   $��Center=�6Q   $��Impact)�6n   �� 5      �� 5      -   5        $
��user��5      $��Usuario��6$   
�������$��18(�6I   $��Franklin Gothic Demi)�6W   �� 5      -   5        $��Usuario��5      ���#545d6a
�������$��18(�6Q   �� 5      -   5        $��password��5      $��Contraseña��6(   
�������$��18(�6P   $��Franklin Gothic Demi)�6^   �� 5      -   5        $��Contraseña��5      ���#545d6a
�������$��18(�6T   � 5      -   5        
 �Name$
 grid1��5      $
��Left=�6&   $	��Top=�6A    � ColumnDefinitions 5      �� 5      $
��Auto �6+    � RowDefinitions 5       �� 5!      $
��Auto �6(   �� 5"      $
��Auto �6(   .+�5$      �� -   5        $��group��5$      $��Grupo:��6)   $��0��6:   $
��Left=�6J   $��0��6e   $��Center=�6r   
�������$��18(�6�   $��Franklin Gothic Demi)�6�   �� 5%      -   5         ��Name$ grupoComboBox��5%      $��0��66   $
��Left=�6F   $��1��6a   $
��Auto��6n   $��Center=�6|   $	��434��6�   5&      �� 5'      -   5        $��enviar��5'      $��Agregar��6'   $��60 10q�69   ���#545d6a
�������$��18(�6p   $��28��6�    �� 	IsDefault . $��Franklin Gothic Demi)�6�   5(      5)      5*      5+      