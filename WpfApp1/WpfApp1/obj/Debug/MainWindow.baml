   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  Incomes and expenses��5   	   $	��350��6&   $	��525��63   .��5	      � $��0,0,0.4,8.8q�6   .+�5
   
   �� -   5         ��Name$ button��5
      $#��Добавить доходы��6!   $
��Left=�6;   $��
200,44,0,0q�6V   $	��Top=�6j   $	��110��6�    J� RenderTransformOrigin$ 1.187,0.583&�6�   $��23��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -360.591Q�6&   T� 5      5      5      �� 5   
   -   5        $ button2��5      $��Накопить��6"   $
��Left=�65   $��192,227,0,0q�6P   $	��Top=�6e   $	��140��6}   6�   �� 5   
   -   5         ��Name$ textBox��5      $
��Left=�6#   $��23��6>   $��	59,42,0,0q�6J    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��124��6�   $ 
0.27,0.511&�6�    �� 	IsEnabled . $��0��6�   �� 5   
   -   5        $ textBox1��5      $
��Left=�6$   $��23��6?   $��	59,79,0,0q�6K   $
 Wrap=�6^   $	��Top=�6r   $	��124��6�   $ 0.553,0.546&�6�   $��0��6�   �� 5   
   -   5         ��Name$ listBox1��5      $
��Left=�6$   $��29��6?   $��178,132,0,0q�6K   $	��Top=�6`   $	��164��6x   $ 0.555,0.531&�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
datapicker��5      $
��Left=�6)   $��409,278,0,0q�6D   $	��Top=�6Y   	   SelectedDateFormat$	 Short=�6q   �� 5   
   -   5        
 ��Name$
 cb��5      $��Зарплата��6   $
��Left=�62   $��	59,22,0,0q�6M   $	��Top=�6`   6x   �� 5   
   -   5        $ Update��5      $��Обновить��6!   $
��Left=�64   $��223,179,0,0q�6O   $	��Top=�6d   $��75��6|   $ -0.28,-0.49&�6�   6�   �� 5   
   -	   5        $%��Добавить расходы��5      $
��Left=�6,   $��
201,79,0,0q�6G   $	��Top=�6[   $	��110��6s   $��23��6   $ 0.211,0.252&�6�   6�   �� 5   
   -
   5        $��Cтатистика��5      $
��Left=�6&   $��192,263,0,0q�6A   $	��Top=�6V   $	��140��6n   6z   5      5      