   M S B A M L   `   `   `  ����B  >UI_MVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:UI_MVVM.ViewsUI_MVVM.Views  5      7 clr-namespace:UI_MVVM.ConvertersUI_MVVM.Converters  5           UI_MVVM.Views.StudentsList   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:UI_MVVM.Views     5      3local2 clr-namespace:UI_MVVM.Converters     5        � 	Resources  5
      %   UI_MVVM.Converters.MaskPassword   MaskPassword��%   &	          5   
   6   5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��students��5         AutoGenerateColumns$ False��6D      
IsReadOnly$
 True��6`   ��6#   �� *
Students+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      
IsReadOnly$
 True��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *_id+  5      $ Names��6%   $
 True��64    6F   �� *Name+  5      $ Password��6%   $
 True��67    6I   �� *
Password+ �� 	Converter# [    5      $ Roles��6%   $
 True��64    6F   �� *Role+  5      $ 
Expires In��6%   $
 True��69    6K   �� *	Expires+5      5      5      