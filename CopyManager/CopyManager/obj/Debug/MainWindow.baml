   M S B A M L   `   `   `  ����F  BCopyManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CopyManagerCopyManager  5   	       CopyManager.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CopyManager     5   	     � Title$  Copy Manager��5   	   $	��450��6   $	��800��6+    � 
ResizeMode$ NoResize=�67   ��5
      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0.217Q�6)   � 5      $ 	#FFA66E2A��6   $ 0.897Q�6-   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��589* �6   �� 5      $
��205* �6    � RowDefinitions 5   
   �� 5      $
��415* �6   �� 5      $��4* �6   .+�5   
   ��  �� CornerRadius$ 20��6   $��
10,10,10,6q�6#   $��2��67   ��5      �� 5      	 �� ImageSource$R	 Lhttps://i.pinimg.com/originals/95/f8/6a/95f86a6fed4a0de278d94cfaff7a4f05.jpg��6   
 j� Stretch$
 UniformToFill=�6x   ._�5      � $��
20,0,20,20q�6    5      �� 5      $	��365 �6'   �� 5       $	��365 �6'    5"      �� 5#      $��40 �6$   �� 5$      $��40 �6$   �� 5%      $	��200 �6$   �� 5&      $��40 �6$   .+�5(      � �� 5)      -   5        $��comboBoxBackups��5)      $��2��62   $��0��6?   $��25��6O   $	��300��6[   $��	33,38,0,0q�6g   $
��Left=�6z   $	��Top=�6�   �� 5*      -   5        $��addUser��5*      $��Añadir usuario��6(   $��1��6A   $��1��6Q   $
��Left=�6^   $	��130��6�   $��30,0,0,0q�6�   $��23��6�   ���	#FFA26000���	#FFAE6F00$��Franklin Gothic Demi)�6�   $��14(�6  �� 5+      -   5        $��delUser��5+      $��Eliminar usuario��6(   $��1��6C   $��1��6S   $��Right=�6`   $	��130��6�   $��0,0,30,0q�6�   $��23��6�   ���	#FFA26000���	#FFAE6F00$��Franklin Gothic Demi)�6�   $��14(�6  &   System.Windows.Controls.DataGrid  5,      -   5          Name$ cuentasDataGrid��5,      $   HorizontalScrollBarVisibility$ Disabled=�64   $��1��6]   $��10,92,10,10q�6m   $��3��6�   �� 5-      -   5        $��copiasButton��5-      $��Copias de seguridad��6-   $��1��6K   $��3��6[   $��23��6h   $	��140��6t   ���	#FFA26000���	#FFAE6F00$��Franklin Gothic Demi)�6�   $��14(�6�   $��117,10,109,8q�6�   5.      5/      �� 50   
   -   5        $��logout��50      $��Cerrar sesión��6   $��0��67   $��23��6Y   $��328,345,0,0q�6e   $	��120��6{   $	��Top=�6�   $
��Left=�6�   ���	#FFA26000���	#FFAE6F00$��Franklin Gothic Demi)�6�   $��14(�6  �� 51   
   -   5        $��verCopia��51      $��	Ver copia��6!   $
��Left=�65   $��156,303,0,0q�6P   $	��Top=�6e   $��23��6}   $	��120��6�   ���	#FFA26000���	#FFAE6F00$��Franklin Gothic Demi)�6�   $��14(�6�   �� 52   
   -   5        $��logout2��52      $��Cerrar sesión��6    $��0��68   $��23��6Z   $��156,339,0,0q�6f   $	��120��6|   $	��Top=�6�   $
��Left=�6�   ���	#FFA26000���	#FFAE6F00$��Franklin Gothic Demi)�6�   $��14(�6  53      54      