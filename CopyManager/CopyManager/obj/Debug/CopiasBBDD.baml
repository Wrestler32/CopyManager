   M S B A M L   `   `   `  ����F  BCopyManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CopyManagerCopyManager  5   	       CopyManager.CopiasBBDD   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CopyManager     5   	     � Title$  Copy Manager��5   	   $	��450��6   $	��600��6+    � 
ResizeMode$ NoResize=�67   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6   � 5      $ 	#FFB6AE0A��6    � Offset$ 1Q�6-   5      .��5      ��  �� CornerRadius$ 20��6   $��10q�6   ��5   
   �� 5       �� ImageSource$o ihttps://p4.wallpaperbetter.com/wallpaper/659/311/729/gears-mechanism-nuts-steampunk-wallpaper-preview.jpg��6    j� Stretch$ UniformToFill=�6�   	 �� Opacity$
	 0.85Q�6�   ._�5   
   � 
 � ColumnDefinitions
 5      �� 5      $
��Auto �6#    � RowDefinitions 5      �� 5      $��70 �6    �� 5      $	��240 �6    �� 5      $��40 �6    .+�5      �� -   5        $��tittle��5      $��COPIAS DE SEGURIDAD��6"   $��22(�6@   
�������$��173,17,161,10q�6b   $��Impact)�6y   $��Center=�6�   �� 5      -   5        $��volver��5      $��Volver��6#   $	��124��64   $��0��6@   $��2��6P   $
��Left=�6]   $��230,26,0,-8q�6x   ���	#FF959519���	#FF848F1A$��Franklin Gothic Demi)�6�   $��14(�6�   �� 5      -   5        $��verBase��5      $��Ver Información��6$   $	��120��6>   $��23��6J   $��1��6V   $��Right=�6c   $��0,206,348,12q�6   ���	#FF959519���	#FF848F1A$��Franklin Gothic Demi)�6�   $��14(�6�   �� 5       -   5        $��	seeBackup��5       $��
Ver copias��6%   $
��Left=�6:   $��
104,10,0,0q�6U   $��1��6i   $	��Top=�6v   
�������$��20(�6�   $��Franklin Gothic Demi)�6�   $��Center=�6�   �� 5!      -   5        $��addNew��5!      $��Crear nueva��6"   $
��Left=�68   $��
377,10,0,0q�6S   $��1��6g   $	��Top=�6t   
�������$��20(�6�   $��Franklin Gothic Demi)�6�   $��Center=�6�   �� 5"      -   5        $��
origenText��5"      $��28��6(   $��
308,62,1,0q�64   $��1��6H    �� TextWrapping$
 Wrap=�6U   $��Origen��6i   $	��Top=�6w   �� 5#      -   5        $��
crearCopia��5#      $��Crear copia��6(   $
��Left=�6>   $��372,206,0,0q�6Y   $��1��6n   $	��Top=�6{   $	��120��6�   ���	#FF959519���	#FF848F1A$��Franklin Gothic Demi)�6�   $��23��6	  $��14(�6  �� 5$      -   5        $��destinoText��5$      $��28��6)   $��
308,95,1,0q�65   $��1��6I   $
 Wrap=�6V   $��Destino��6j   $	��Top=�6y   �� 5%      -	   5        $��
nombreText��5%      $��28��6(   $��308,128,1,0q�64   $��1��6I   $
 Wrap=�6V   $��Nombre��6j   $	��Top=�6x   �� 5&      -
   5         ��Name$ grupoComboBox��5&      $��26��6.   $��1��6:   $��Center=�6G   $��308,161,1,54q�6b   �� 5'      -   5        $ nombreComboBox��5'      $
��Left=�6/   $��26��6J   $��10,62,0,153q�6V   $��1��6k   $��Center=�6x   $	��267��6�   �� 5(      -   5        $��destinoButton��5(      $��v��6*   $
��Left=�66   $��
537,95,0,0q�6Q   $��1��6e   $	��Top=�6r   $��15��6�   $��28��6�   ���	#FF959519���	#FF848F1A$��Franklin Gothic Demi)�6�   $��14(�6  �� 5)      -   5        $��origenButton��5)      $��v��6)   $
��Left=�65   $��
537,62,0,0q�6P   $��1��6d   $	��Top=�6q   $��15��6�   $��28��6�   ���	#FF959519���	#FF848F1A$��Franklin Gothic Demi)�6�   $��14(�6  5*      5+      5,      