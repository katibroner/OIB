   M S B A M L   `   `   `  ����!  MaterialManagerEventsTutorial7    1MaterialManagerEventsTutorial.Views.MainPanelView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5        $	��700��5      $
��1200��6     � Title$#  MaterialManagerEventsTutorial��5       � Icon$  ../Images/testing_view.ico��5      .��5      ]� ��. $��Stretch=�5      $��Stretch=�5      .+�5   
   �� .u�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      �� 5      $
��Auto �6$   .+�5      � $��0��6    5      �� 5      �� 5      �� 5       � ColumnDefinitions 5       �� 5!      $
��Auto �6+   �� 5"      .+�5$      �� $��OIB Host��6    $��0��60   $��0��6=   $��5q�6M   �� 5%      $��0��6   $��1��6+   $��5q�6T   ��6;   �� *	OibHost+6T   �� 5&      $��OIB Port��6    $��1��60   $��0��6=   $��5,0,5,5q�6M   $��18��6^   $��Bottom=�6j   �� 5'      $��1��6   $��1��6+   $��5q�6T   ��6;   �� *	OibPort+6T   5(      � 5)      $��1��6    5*      �� 5+      $
��Auto �6+   �� 5,      $
��Auto �6+   �� 5-      $��* �6+   .+�5/      �� $��0��6   $��0��6*   $��5q�6]   ��6:   �� *ConnectCommand+{�50      �� *ConnectButtonEnabled+.��6H   	Connect6Q   �� 51      $��0��6   $��1��6*   $��5q�6`   $
��Auto��52   L   ��51   :   �� *DisconnectCommand+{�52      �� *DisconnectButtonEnabled+.��6Y   
Disconnect6e   �� 53      $��0��6   $��2��6,   $��5q�6<   $��Center=�6G   $
��Auto��6c    d� 	IsChecked 6p   �� *UseClientAuthentication+.��6�   Use Client Authentication6�   54      � 55      $��2��6    56      �� 57      $
��Auto �6+   �� 58      �� 59      $
��Auto �6+   .+�5;      �� $��Filter: ��6    $��0��60   $��5q�6@   �� 5<      $��0��6   $��1��6+   $��5q�6x   ��6;   �� *Filter+ �� UpdateSourceTrigger$ PropertyChanged=�.��6�    J� InputBindings 5=      �� 5>      
 �� Key$ Return��6S    �� Command 6)   �� *FilterMessagesCommand+6S   .��5?   1   5@      �� 5A      $��2��6   $��5q�6W   ��6-   �� *FilterMessagesCommand+.��6b   Filter6j   5B      &   System.Windows.Controls.DataGrid  5D      $��3��6   	   AutoGenerateColumns	 .  !
   EnableColumnVirtualization
 .  � ForceCursor .    CanUserReorderColumns .    CanUserResizeColumns .    CanUserResizeRows .     CanUserSortColumns .    AlternatingRowBackground
 ����� �� AlternationCount$ 2��5H   >      FrozenColumnCount$ 2��5I   W   ��6   �� *EventDataCollectionView+ �� IsAsync .    Columns 5K      0  *System.Windows.Controls.DataGridTextColumn  5L      ,  &System.Windows.Controls.DataGridColumn   Header$
 Time��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6;      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *ReceiveDateTime+ �� Mode$ OneWay=�6y     5M      $ 	MessageId��6-   $
 auto 6@    .  6M   �� *	MessageId+$ OneWay=�6x     5N      $	 Key��6-   $
 auto 6:    .  6G   �� *Key+$ OneWay=�6l   4  .System.Windows.Controls.DataGridTemplateColumn  5O      $ Value��61   $ * 6@      CellTemplate 5P      �� 5Q   "   .R�5R   &   ��  �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�5S   2   " �� VerticalScrollBarVisibility$
 Auto=�6F   ��5R   .   �� *Value+$ OneWay=�.��5S   i   5T   '   5U   #   5W      5Y      � 5Z      $��4��6    5[      �� 5\      $
��Auto �6+   �� 5]      $
��Auto �6+   .+�5_      �� $��0��6   $	��5pxq�6-   ��6:   �� *SaveMessagesCommand+.��6b   	Save...6k   �� 5`      $��1��6   $	��5pxq�6-   ��6:   �� *ClearMessagesCommand+.��6c   Clear6j   5a      5b      5d      5e      5f      