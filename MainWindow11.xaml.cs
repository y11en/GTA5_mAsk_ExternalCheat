using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using MahApps.Metro.Controls;
using Model.Libraries.KeyBoardHooking;
using SirMestreBlackCat.Model;

namespace WpfApplication
{
	// Token: 0x02000005 RID: 5
	public class MainWindow : MetroWindow, IComponentConnector
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002614 File Offset: 0x00000814
		public MainWindow()
		{
			this.InitializeComponent();
			new KeyBoardHooking();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AB4 File Offset: 0x00000CB4
		private void Armor_Slider_DragCompleted(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Thread thread = new Thread(delegate
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.MemoryFunctions.GAME_Armor((float)this.Armor_Slider.Value);
					});
				});
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004008 File Offset: 0x00002208
		private void Armor_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Armor((float)this.Armor_Slider.Value);
				});
				return;
			}
			this.Armor_Slider.Value = 0.0;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003E48 File Offset: 0x00002048
		private void Bullet_DMG_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Bullet_DMG((float)this.Bullet_DMG_Slider.Value);
				});
				return;
			}
			this.Bullet_DMG_Slider.Value = 0.0;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000406C File Offset: 0x0000226C
		private void Bullet_proof_Tires_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_Bullet_proof_Tires(this.Bullet_proof_Tires_ToggleSwitch.IsChecked);
				});
				return;
			}
			this.Bullet_proof_Tires_ToggleSwitch.IsChecked = new bool?(false);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003D48 File Offset: 0x00001F48
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.Vehile_ACCELERATION.Value = new double?((double)this.MemoryFunctions.GAME_Get_Vehlie_ACCELERATION());
					this.Vehicle_BRAKEFORCE.Value = new double?((double)this.MemoryFunctions.GAME_GET_Vehicle_BRAKEFORCE());
					this.Vehicle_TRACTION_CURVE_MIN.Value = new double?((double)this.MemoryFunctions.GAME_GET_Vehicle_TRACTION_CURVE_MIN());
					this.Vehicle_DEFORM_MULTIPLIER.Value = new double?((double)this.MemoryFunctions.GAME_GET_Vehicle_DEFORM_MULTIPLIER());
					this.Vehicle_UPSHIFT.Value = new double?((double)this.MemoryFunctions.GAME_GET_Vehicle_UPSHIFT());
					this.Vehicle_SUSPENSION_FORCE.Value = new double?((double)this.MemoryFunctions.GAME_GET_Vehicle_SUSPENSION_FORCE());
					this.Vehicle_Gravity.Value = new double?((double)this.MemoryFunctions.GAME_GET_Vehicle_Gravity());
				});
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003D98 File Offset: 0x00001F98
		private void Damage_Multiplier_NumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Damage_Multiplier((float)this.Vehicle_Damage_Multiplier.Value);
				});
				return;
			}
			this.Damage_Multiplier_NumericUpDown.Value = new double?(0.0);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003540 File Offset: 0x00001740
		private void EXP_Level_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_set_EXP_Level_60(this.EXP_Level_ToggleSwitch.IsChecked);
				});
				return;
			}
			this.EXP_Level_ToggleSwitch.IsChecked = new bool?(false);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003EA4 File Offset: 0x000020A4
		private void Fast_Shoot_Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Fast_Shoot();
				});
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000039E0 File Offset: 0x00001BE0
		private void FIX_Vehilc_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_FIX_Vehlie();
				});
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000037F8 File Offset: 0x000019F8
		private void Game_Thread()
		{
			while (this.Thead_IsClosed)
			{
				this.GameIsRunning = this.MemoryFunctions.IsGameRunning();
				Thread.Sleep(500);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003EE8 File Offset: 0x000020E8
		private void Get_Full_Ammo_Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Get_Full_Ammo();
				});
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C24 File Offset: 0x00000E24
		private void Get_HP_Click(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				this.MemoryFunctions.GAME_Get_HP();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002808 File Offset: 0x00000A08
		private void God_Mode_Thread()
		{
			while (this.God_Mode_isChecked && this.Thead_IsClosed)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_set_God_Mode(this.God_Mode_ToggleSwitch.IsChecked);
				});
				Thread.Sleep(10000);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002854 File Offset: 0x00000A54
		private void God_Mode_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (!this.MemoryFunctions.IsGameRunning())
			{
				this.God_Mode_ToggleSwitch.IsChecked = new bool?(false);
				return;
			}
			if (this.God_Mode_ToggleSwitch.IsChecked == false)
			{
				this.God_Mode_isChecked = false;
				this.MemoryFunctions.GAME_set_God_Mode(this.God_Mode_ToggleSwitch.IsChecked);
				return;
			}
			this.God_Mode_isChecked = true;
			ThreadStart start = new ThreadStart(this.God_Mode_Thread);
			Thread thread = new Thread(start);
			thread.Start();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002D50 File Offset: 0x00000F50
		private void God_Mode_Vehicle_Thread()
		{
			while (this.God_Mode_Vehicle_isChecked)
			{
				try
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.MemoryFunctions.GAME_set_God_Mode_Vehicle(this.God_Mode_Vehicle_ToggleSwitch.IsChecked);
					});
					Thread.Sleep(4000);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002DA8 File Offset: 0x00000FA8
		private void God_Mode_Vehicle_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (!this.MemoryFunctions.IsGameRunning())
			{
				this.God_Mode_Vehicle_ToggleSwitch.IsChecked = new bool?(false);
				return;
			}
			if (this.God_Mode_Vehicle_ToggleSwitch.IsChecked == false)
			{
				this.God_Mode_Vehicle_isChecked = false;
				this.MemoryFunctions.GAME_set_God_Mode_Vehicle(this.God_Mode_Vehicle_ToggleSwitch.IsChecked);
				return;
			}
			this.God_Mode_Vehicle_isChecked = true;
			ThreadStart start = new ThreadStart(this.God_Mode_Vehicle_Thread);
			Thread thread = new Thread(start);
			thread.Start();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000041F8 File Offset: 0x000023F8
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/收费版mAsk2.0;component/mainwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000038F7 File Offset: 0x00001AF7
		private void MetroWindow_Closed(object sender, EventArgs e)
		{
			this.Thead_IsClosed = false;
			this.MemoryFunctions.GAME_Close_2k_Drop();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003820 File Offset: 0x00001A20
		private void MetroWindow_Initialized(object sender, EventArgs e)
		{
			ThreadStart start = new ThreadStart(this.Game_Thread);
			Thread thread = new Thread(start);
			thread.Start();
			Thread.Sleep(1000);
			ThreadStart start2 = new ThreadStart(this.XYZ_Thread);
			Thread thread2 = new Thread(start2);
			thread2.Start();
			ThreadStart start3 = new ThreadStart(this.Player_IsInVehile_Thead);
			Thread thread3 = new Thread(start3);
			thread3.Start();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004128 File Offset: 0x00002328
		private void NO_Doors_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_Doors();
				});
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000028F8 File Offset: 0x00000AF8
		private void No_Ragdoll_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_No_Ragdoll(this.No_Ragdoll_ToggleSwitch.IsChecked);
				});
				return;
			}
			this.No_Ragdoll_ToggleSwitch.IsChecked = new bool?(false);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000038B8 File Offset: 0x00001AB8
		private void Open_XYZ_Thead_Button_Click(object sender, RoutedEventArgs e)
		{
			this.GetFocus = true;
			Thread.Sleep(200);
			this.GetFocus = false;
			ThreadStart start = new ThreadStart(this.XYZ_Thread);
			Thread thread = new Thread(start);
			thread.Start();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003889 File Offset: 0x00001A89
		private void Player_IsInVehile_Thead()
		{
			while (this.GameIsRunning && this.Thead_IsClosed)
			{
				this.Player_IsInVehile = this.MemoryFunctions.GAME_In_Vehile();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003F2C File Offset: 0x0000212C
		private void Reload_Multiplier_Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Reload_Multiplier();
				});
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003F70 File Offset: 0x00002170
		private void Reload_Vehcile_Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Reload_Vehicle();
				});
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000041D1 File Offset: 0x000023D1
		private void Rockets_Theard()
		{
			while (this.Rockest_Switch && this.Thead_IsClosed)
			{
				this.MemoryFunctions.GAME_SET_Rockest();
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004160 File Offset: 0x00002360
		private void Rockets_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (!this.GameIsRunning)
			{
				this.Rockets_ToggleSwitch.IsChecked = new bool?(false);
				return;
			}
			if (this.Rockets_ToggleSwitch.IsChecked == true)
			{
				this.Rockest_Switch = true;
				ThreadStart start = new ThreadStart(this.Rockets_Theard);
				Thread thread = new Thread(start);
				thread.Start();
				return;
			}
			this.Rockest_Switch = false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003988 File Offset: 0x00001B88
		private void Seatbelt_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Seatbelt(this.Seatbelt_ToggleSwitch.IsChecked);
				});
				return;
			}
			this.Seatbelt_ToggleSwitch.IsChecked = new bool?(false);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003DF8 File Offset: 0x00001FF8
		private void Spread_Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Spread();
				});
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B34 File Offset: 0x00000D34
		private void Sprint_Speed_Slider_DragCompleted(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Thread thread = new Thread(delegate
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.MemoryFunctions.GAME_set_Sprint_Speed((float)this.Sprint_Speed_Slider.Value);
					});
				});
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				return;
			}
			this.Sprint_Speed_Slider.Value = 1.0;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private void Swim_Speed_Slider_DragCompleted(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Thread thread = new Thread(delegate
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.MemoryFunctions.GAME_set_Swim_Speed((float)this.Swim_Speed_Slider.Value);
					});
				});
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				return;
			}
			this.Swim_Speed_Slider.Value = 1.0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004228 File Offset: 0x00002428
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MainWindow)target).Initialized += new EventHandler(this.MetroWindow_Initialized);
				((MainWindow)target).Closed += new EventHandler(this.MetroWindow_Closed);
				return;
			case 2:
				this.tabControl = (TabControl)target;
				return;
			case 3:
				this.God_Mode_ToggleSwitch = (ToggleSwitch)target;
				this.God_Mode_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.God_Mode_ToggleSwitch_Click);
				return;
			case 4:
				this.No_Ragdoll_ToggleSwitch = (ToggleSwitch)target;
				this.No_Ragdoll_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.No_Ragdoll_ToggleSwitch_Click);
				return;
			case 5:
				this.Seatbelt_ToggleSwitch = (ToggleSwitch)target;
				this.Seatbelt_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.Seatbelt_ToggleSwitch_Click);
				return;
			case 6:
				this.Get_HP_Button = (Button)target;
				this.Get_HP_Button.Click += new RoutedEventHandler(this.Get_HP_Click);
				return;
			case 7:
				this.Wanted_Level_NumericUpDown = (NumericUpDown)target;
				this.Wanted_Level_NumericUpDown.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Wanted_Level_NumericUpDown_ValueChanged);
				return;
			case 8:
				this.Armor_Slider = (Slider)target;
				this.Armor_Slider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(this.Armor_Slider_DragCompleted));
				return;
			case 9:
				this.Damage_Multiplier_NumericUpDown = (NumericUpDown)target;
				this.Damage_Multiplier_NumericUpDown.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Damage_Multiplier_NumericUpDown_ValueChanged);
				return;
			case 10:
				this.Sprint_Speed_Slider = (Slider)target;
				this.Sprint_Speed_Slider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(this.Sprint_Speed_Slider_DragCompleted));
				return;
			case 11:
				this.Swim_Speed_Slider = (Slider)target;
				this.Swim_Speed_Slider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(this.Swim_Speed_Slider_DragCompleted));
				return;
			case 12:
				this.Vehicle_Damage_Multiplier = (Slider)target;
				this.Vehicle_Damage_Multiplier.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(this.Vehicle_Damage_Multiplier_DragCompleted));
				return;
			case 13:
				this.Teleport_To_Waypoint_Button = (Button)target;
				this.Teleport_To_Waypoint_Button.Click += new RoutedEventHandler(this.Teleport_To_Waypoint_Button_Click);
				return;
			case 14:
				this.Open_XYZ_Thead_Button = (Button)target;
				this.Open_XYZ_Thead_Button.Click += new RoutedEventHandler(this.Open_XYZ_Thead_Button_Click);
				return;
			case 15:
				this.X_Textbox = (TextBox)target;
				this.X_Textbox.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(this.X_Textbox_Changed);
				this.X_Textbox.GotFocus += new RoutedEventHandler(this.X_Textbox_GotFocus);
				return;
			case 16:
				this.Y_Textbox = (TextBox)target;
				this.Y_Textbox.GotFocus += new RoutedEventHandler(this.X_Textbox_GotFocus);
				return;
			case 17:
				this.Z_Textbox = (TextBox)target;
				this.Z_Textbox.GotFocus += new RoutedEventHandler(this.X_Textbox_GotFocus);
				return;
			case 18:
				this.Teleport_To_FIB_Building_Top = (Button)target;
				this.Teleport_To_FIB_Building_Top.Click += new RoutedEventHandler(this.Teleport_To_FIB_Building_Top_Click);
				return;
			case 19:
				this.Teleport_To_Garment_Factory = (Button)target;
				this.Teleport_To_Garment_Factory.Click += new RoutedEventHandler(this.Teleport_To_Garment_Factory_Click);
				return;
			case 20:
				this.Teleport_To_Franklin_House = (Button)target;
				this.Teleport_To_Franklin_House.Click += new RoutedEventHandler(this.Teleport_To_Franklin_House_Click);
				return;
			case 21:
				this.Teleport_To_Michael_House = (Button)target;
				this.Teleport_To_Michael_House.Click += new RoutedEventHandler(this.Teleport_To_Michael_House_Click);
				return;
			case 22:
				this.Teleport_To_Trevor_House = (Button)target;
				this.Teleport_To_Trevor_House.Click += new RoutedEventHandler(this.Teleport_To_Trevor_House_Click);
				return;
			case 23:
				this.Teleport_To_Aunt_Denise_House = (Button)target;
				this.Teleport_To_Aunt_Denise_House.Click += new RoutedEventHandler(this.Teleport_To_Aunt_Denise_House_Click);
				return;
			case 24:
				this.Teleport_To_Floyd_House = (Button)target;
				this.Teleport_To_Floyd_House.Click += new RoutedEventHandler(this.Teleport_To_Floyd_House_Click);
				return;
			case 25:
				this.Teleport_To_Lester_House = (Button)target;
				this.Teleport_To_Lester_House.Click += new RoutedEventHandler(this.Teleport_To_Lester_House_Click);
				return;
			case 26:
				this.Teleport_To_Vanilla_Unicorn_Office = (Button)target;
				this.Teleport_To_Vanilla_Unicorn_Office.Click += new RoutedEventHandler(this.Teleport_To_Vanilla_Unicorn_Office_Click);
				return;
			case 27:
				this.Teleport_To_Bank_Vault_Pacific_Standard = (Button)target;
				this.Teleport_To_Bank_Vault_Pacific_Standard.Click += new RoutedEventHandler(this.Teleport_To_Bank_Vault_Pacific_Standard_Click);
				return;
			case 28:
				this.Teleport_To_Comedy_Club = (Button)target;
				this.Teleport_To_Comedy_Club.Click += new RoutedEventHandler(this.Teleport_To_Comedy_Club_Click);
				return;
			case 29:
				this.Teleport_To_Humane_Labs = (Button)target;
				this.Teleport_To_Humane_Labs.Click += new RoutedEventHandler(this.Teleport_To_Humane_Labs_Click);
				return;
			case 30:
				this.Teleport_To_Humane_Labs_Tunnel = (Button)target;
				this.Teleport_To_Humane_Labs_Tunnel.Click += new RoutedEventHandler(this.Teleport_To_Humane_Labs_Tunnel_Click);
				return;
			case 31:
				this.Teleport_To_IAA_Office = (Button)target;
				this.Teleport_To_IAA_Office.Click += new RoutedEventHandler(this.Teleport_To_IAA_Office_Click);
				return;
			case 32:
				this.Teleport_To_Torture_Room = (Button)target;
				this.Teleport_To_Torture_Room.Click += new RoutedEventHandler(this.Teleport_To_Torture_Room_Click);
				return;
			case 33:
				this.Teleport_To_Fort_Zancudo_Tower = (Button)target;
				this.Teleport_To_Fort_Zancudo_Tower.Click += new RoutedEventHandler(this.Teleport_To_Fort_Zancudo_Tower_Click);
				return;
			case 34:
				this.Teleport_To_Mine_Shaft = (Button)target;
				this.Teleport_To_Mine_Shaft.Click += new RoutedEventHandler(this.Teleport_To_Mine_Shaft_Click);
				return;
			case 35:
				this.God_Mode_Vehicle_ToggleSwitch = (ToggleSwitch)target;
				this.God_Mode_Vehicle_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.God_Mode_Vehicle_ToggleSwitch_Click);
				return;
			case 36:
				this.Bullet_proof_Tires_ToggleSwitch = (ToggleSwitch)target;
				this.Bullet_proof_Tires_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.Bullet_proof_Tires_ToggleSwitch_Click);
				return;
			case 37:
				this.Rockets_ToggleSwitch = (ToggleSwitch)target;
				this.Rockets_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.Rockets_ToggleSwitch_Click);
				return;
			case 38:
				this.FIX_Vehilc = (Button)target;
				this.FIX_Vehilc.Click += new RoutedEventHandler(this.FIX_Vehilc_Click);
				return;
			case 39:
				((Button)target).Click += new RoutedEventHandler(this.Button_Click);
				return;
			case 40:
				this.Vehicle_BRAKEFORCE = (NumericUpDown)target;
				this.Vehicle_BRAKEFORCE.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_BRAKEFORCE_ValueChanged);
				return;
			case 41:
				this.Vehile_ACCELERATION = (NumericUpDown)target;
				this.Vehile_ACCELERATION.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehile_ACCELERATION_ValueChanged);
				return;
			case 42:
				this.Vehicle_TRACTION_CURVE_MIN = (NumericUpDown)target;
				this.Vehicle_TRACTION_CURVE_MIN.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_TRACTION_CURVE_MIN_ValueChanged);
				return;
			case 43:
				this.Vehicle_DEFORM_MULTIPLIER = (NumericUpDown)target;
				this.Vehicle_DEFORM_MULTIPLIER.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_DEFORM_MULTIPLIER_ValueChanged);
				return;
			case 44:
				this.Vehicle_UPSHIFT = (NumericUpDown)target;
				this.Vehicle_UPSHIFT.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_UPSHIFT_ValueChanged);
				return;
			case 45:
				this.Vehicle_SUSPENSION_FORCE = (NumericUpDown)target;
				this.Vehicle_SUSPENSION_FORCE.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_SUSPENSION_FORCE_ValueChanged);
				return;
			case 46:
				this.Vehicle_Gravity = (NumericUpDown)target;
				this.Vehicle_Gravity.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_Gravity_ValueChanged);
				return;
			case 47:
				this.Vehicle_DirtLevel = (NumericUpDown)target;
				this.Vehicle_DirtLevel.ValueChanged += new RoutedPropertyChangedEventHandler<double?>(this.Vehicle_DirtLevel_ValueChanged);
				return;
			case 48:
				this.NO_Doors = (Button)target;
				this.NO_Doors.Click += new RoutedEventHandler(this.NO_Doors_Click);
				return;
			case 49:
				this.Unlimited_Ammo_ToggleSwitch = (ToggleSwitch)target;
				this.Unlimited_Ammo_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.Unlimited_Ammo_ToggleSwitch_Click);
				return;
			case 50:
				this.Bullet_DMG_Slider = (Slider)target;
				this.Bullet_DMG_Slider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(this.Bullet_DMG_Slider_DragCompleted));
				return;
			case 51:
				this.Spread_Button = (Button)target;
				this.Spread_Button.Click += new RoutedEventHandler(this.Spread_Button_Click);
				return;
			case 52:
				this.Fast_Shoot_Button = (Button)target;
				this.Fast_Shoot_Button.Click += new RoutedEventHandler(this.Fast_Shoot_Button_Click);
				return;
			case 53:
				this.Get_Full_Ammo_Button = (Button)target;
				this.Get_Full_Ammo_Button.Click += new RoutedEventHandler(this.Get_Full_Ammo_Button_Click);
				return;
			case 54:
				this.Reload_Multiplier_Button = (Button)target;
				this.Reload_Multiplier_Button.Click += new RoutedEventHandler(this.Reload_Multiplier_Button_Click);
				return;
			case 55:
				this.Reload_Vehcile_Button = (Button)target;
				this.Reload_Vehcile_Button.Click += new RoutedEventHandler(this.Reload_Vehcile_Button_Click);
				return;
			case 56:
				this.EXP_Level_ToggleSwitch = (ToggleSwitch)target;
				this.EXP_Level_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.EXP_Level_ToggleSwitch_Click);
				return;
			case 57:
				this.Wanted_Loop_ToggleSwitch = (ToggleSwitch)target;
				this.Wanted_Loop_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this.Wanted_Loop_ToggleSwitch_Click);
				return;
			case 58:
				this._2K_ToggleSwitch = (ToggleSwitch)target;
				this._2K_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this._2K_ToggleSwitch_Click);
				return;
			case 59:
				this._10W_ToggleSwitch = (ToggleSwitch)target;
				this._10W_ToggleSwitch.Click += new EventHandler<RoutedEventArgs>(this._10W_ToggleSwitch_Click);
				return;
			case 60:
				this.TP_10W_WAY = (Button)target;
				this.TP_10W_WAY.Click += new RoutedEventHandler(this.TP_10W_WAY_Click);
				return;
			case 61:
				this.Wanted_Lv_6 = (Button)target;
				this.Wanted_Lv_6.Click += new RoutedEventHandler(this.Wanted_Lv_6_Click);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F70 File Offset: 0x00001170
		private void TabItem_MouseEnter(object sender, MouseEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.X_Textbox.Text = this.MemoryFunctions.Game_Get_X().ToString();
					this.Y_Textbox.Text = this.MemoryFunctions.Game_Get_Y().ToString();
					this.Z_Textbox.Text = this.MemoryFunctions.Game_Get_Z().ToString();
				});
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003177 File Offset: 0x00001377
		private void Teleport_To_Aunt_Denise_House_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Aunt_Denise_House[0], this.Aunt_Denise_House[1], this.Aunt_Denise_House[2]);
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000327F File Offset: 0x0000147F
		private void Teleport_To_Bank_Vault_Pacific_Standard_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Bank_Vault_Pacific_Standard[0], this.Bank_Vault_Pacific_Standard[1], this.Bank_Vault_Pacific_Standard[2]);
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000032C1 File Offset: 0x000014C1
		private void Teleport_To_Comedy_Club_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Comedy_Club[0], this.Comedy_Club[1], this.Comedy_Club[2]);
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000302D File Offset: 0x0000122D
		private void Teleport_To_FIB_Building_Top_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.FIB_Building_Top[0], this.FIB_Building_Top[1], this.FIB_Building_Top[2]);
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000031B9 File Offset: 0x000013B9
		private void Teleport_To_Floyd_House_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Floyd_House[0], this.Floyd_House[1], this.Floyd_House[2]);
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000340B File Offset: 0x0000160B
		private void Teleport_To_Fort_Zancudo_Tower_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Fort_Zancudo_Tower[0], this.Fort_Zancudo_Tower[1], this.Fort_Zancudo_Tower[2]);
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000030B1 File Offset: 0x000012B1
		private void Teleport_To_Franklin_House_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Franklin_House[0], this.Franklin_House[1], this.Franklin_House[2]);
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000306F File Offset: 0x0000126F
		private void Teleport_To_Garment_Factory_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Garment_Factory[0], this.Garment_Factory[1], this.Garment_Factory[2]);
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003303 File Offset: 0x00001503
		private void Teleport_To_Humane_Labs_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Humane_Labs[0], this.Humane_Labs[1], this.Humane_Labs[2]);
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003345 File Offset: 0x00001545
		private void Teleport_To_Humane_Labs_Tunnel_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Humane_Labs_Tunnel[0], this.Humane_Labs_Tunnel[1], this.Humane_Labs_Tunnel[2]);
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003387 File Offset: 0x00001587
		private void Teleport_To_IAA_Office_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.IAA_Office[0], this.IAA_Office[1], this.IAA_Office[2]);
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000031FB File Offset: 0x000013FB
		private void Teleport_To_Lester_House_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Lester_House[0], this.Lester_House[1], this.Lester_House[2]);
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030F3 File Offset: 0x000012F3
		private void Teleport_To_Michael_House_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Michael_House[0], this.Michael_House[1], this.Michael_House[2]);
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000344D File Offset: 0x0000164D
		private void Teleport_To_Mine_Shaft_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Mine_Shaft[0], this.Mine_Shaft[1], this.Mine_Shaft[2]);
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000033C9 File Offset: 0x000015C9
		private void Teleport_To_Torture_Room_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Torture_Room[0], this.Torture_Room[1], this.Torture_Room[2]);
			});
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003135 File Offset: 0x00001335
		private void Teleport_To_Trevor_House_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Trevor_House[0], this.Trevor_House[1], this.Trevor_House[2]);
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000323D File Offset: 0x0000143D
		private void Teleport_To_Vanilla_Unicorn_Office_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				this.MemoryFunctions.TP_Way(this.Vanilla_Unicorn_Office[0], this.Vanilla_Unicorn_Office[1], this.Vanilla_Unicorn_Office[2]);
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002EC0 File Offset: 0x000010C0
		private void Teleport_To_Waypoint_Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Thread thread = new Thread(delegate
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.Teleport_To_Waypoint_Button.IsEnabled = false;
						this.Teleport_To_Waypoint_Button.Content = "传送中...";
					});
					this.MemoryFunctions.GAME_teleport_to_Waypoint();
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.Teleport_To_Waypoint_Button.IsEnabled = true;
						this.Teleport_To_Waypoint_Button.Content = "传送至导航点";
					});
				});
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.MTA);
				thread.Start();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036A0 File Offset: 0x000018A0
		private void TP_10W_WAY_Click(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.TP_Way(this._10W_TP_Way[0], this._10W_TP_Way[1], this._10W_TP_Way[2]);
				});
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CE8 File Offset: 0x00000EE8
		private void Unlimited_Ammo_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_set_Unlimited_Ammo(this.Unlimited_Ammo_ToggleSwitch.IsChecked);
				});
				return;
			}
			this.Unlimited_Ammo_ToggleSwitch.IsChecked = new bool?(false);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003A44 File Offset: 0x00001C44
		private void Vehicle_BRAKEFORCE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_BRAKEFORCE((float)this.Vehicle_BRAKEFORCE.Value.Value);
				});
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C74 File Offset: 0x00000E74
		private void Vehicle_Damage_Multiplier_DragCompleted(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Thread thread = new Thread(delegate
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						this.MemoryFunctions.GAME_set_Vehicle_Damage_Multiplier((float)this.Vehicle_Damage_Multiplier.Value);
					});
				});
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				return;
			}
			this.Vehicle_Damage_Multiplier.Value = 1.0;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003B0C File Offset: 0x00001D0C
		private void Vehicle_DEFORM_MULTIPLIER_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_DEFORM_MULTIPLIER((float)this.Vehicle_DEFORM_MULTIPLIER.Value.Value);
				});
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000040E4 File Offset: 0x000022E4
		private void Vehicle_DirtLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_DirtLevel((float)this.Vehicle_DirtLevel.Value.Value);
				});
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C38 File Offset: 0x00001E38
		private void Vehicle_Gravity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_Gravity((float)this.Vehicle_Gravity.Value.Value);
				});
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private void Vehicle_SUSPENSION_FORCE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_SUSPENSION_FORCE((float)this.Vehicle_SUSPENSION_FORCE.Value.Value);
				});
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003AA8 File Offset: 0x00001CA8
		private void Vehicle_TRACTION_CURVE_MIN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_TRACTION_CURVE_MIN((float)this.Vehicle_TRACTION_CURVE_MIN.Value.Value);
				});
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003B70 File Offset: 0x00001D70
		private void Vehicle_UPSHIFT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_SET_Vehicle_UPSHIFT((float)this.Vehicle_UPSHIFT.Value.Value);
				});
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003938 File Offset: 0x00001B38
		private void Vehile_ACCELERATION_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Set_Vehlie_ACCELERATION((float)this.Vehile_ACCELERATION.Value.Value);
				});
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000029B4 File Offset: 0x00000BB4
		private void Wanted_Level_NumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
		{
			if (this.Wanted_Level_NumericUpDown.Value.ToString() == "")
			{
				return;
			}
			int Wanted_Level_Selected = (int)this.Wanted_Level_NumericUpDown.Value.Value;
			if (this.Wanted_Level_NumericUpDown.IsInitialized)
			{
				this.Wanted_Level = Wanted_Level_Selected;
				if (this.MemoryFunctions.IsGameRunning())
				{
					Thread thread = new Thread(delegate
					{
						while (this.Wanted_Level == Wanted_Level_Selected)
						{
							if (this.MemoryFunctions.GAME_get_Wanted_Level() != this.Wanted_Level)
							{
								this.MemoryFunctions.GAME_set_Wanted_Level(this.Wanted_Level);
							}
							Thread.Sleep(100);
						}
					});
					thread.IsBackground = true;
					thread.SetApartmentState(ApartmentState.STA);
					thread.Start();
					return;
				}
				this.Wanted_Level_NumericUpDown.Value = new double?(0.0);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000346A File Offset: 0x0000166A
		private void Wanted_Loop_Thead()
		{
			while (this.Wanted_Loop_Switch)
			{
				this.MemoryFunctions.GAME_set_Wanted_Level(5);
				Thread.Sleep(10);
				this.MemoryFunctions.GAME_set_Wanted_Level(0);
				Thread.Sleep(10);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000349C File Offset: 0x0000169C
		private void Wanted_Loop_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (!this.MemoryFunctions.IsGameRunning())
			{
				this.Wanted_Loop_ToggleSwitch.IsChecked = new bool?(false);
				return;
			}
			if (this.Wanted_Loop_ToggleSwitch.IsChecked == true)
			{
				this.Wanted_Loop_Switch = true;
				ThreadStart start = new ThreadStart(this.Wanted_Loop_Thead);
				Thread thread = new Thread(start);
				thread.Start();
				return;
			}
			this.Wanted_Loop_Switch = false;
			Thread.Sleep(20);
			this.MemoryFunctions.GAME_set_Wanted_Level(0);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003FB8 File Offset: 0x000021B8
		private void Wanted_Lv_6_Click(object sender, RoutedEventArgs e)
		{
			if (this.GameIsRunning)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_set_Wanted_Level(6);
				});
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000037A8 File Offset: 0x000019A8
		private void XYZ_Thread()
		{
			while (this.GameIsRunning && !this.GetFocus && this.Thead_IsClosed)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.X_Textbox.Text = this.MemoryFunctions.Game_Get_X().ToString();
					this.Y_Textbox.Text = this.MemoryFunctions.Game_Get_Y().ToString();
					this.Z_Textbox.Text = this.MemoryFunctions.Game_Get_Z().ToString();
				});
				Thread.Sleep(100);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002FCC File Offset: 0x000011CC
		private void X_Textbox_Changed(object sender, RoutedEventArgs e)
		{
			if (this.MemoryFunctions.IsGameRunning())
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.Game_Set_X(float.Parse(this.X_Textbox.Text));
				});
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000038AE File Offset: 0x00001AAE
		private void X_Textbox_GotFocus(object sender, RoutedEventArgs e)
		{
			this.GetFocus = true;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000035A8 File Offset: 0x000017A8
		private void _10W_Thead()
		{
			while (this._10W_Switch)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.MemoryFunctions.GAME_Set_10W(this._10W_ToggleSwitch.IsChecked);
				});
				Thread.Sleep(5000);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000035EC File Offset: 0x000017EC
		private void _10W_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (!this.MemoryFunctions.IsGameRunning())
			{
				this._10W_ToggleSwitch.IsChecked = new bool?(false);
				return;
			}
			if (this._10W_ToggleSwitch.IsChecked == true)
			{
				this._10W_Switch = true;
				ThreadStart start = new ThreadStart(this._10W_Thead);
				Thread thread = new Thread(start);
				thread.Start();
				return;
			}
			this._10W_Switch = false;
			this.MemoryFunctions.GAME_Set_10W(this._10W_ToggleSwitch.IsChecked);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000036DC File Offset: 0x000018DC
		private void _2K_ToggleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (!this.MemoryFunctions.IsGameRunning())
			{
				this._2K_ToggleSwitch.IsChecked = new bool?(false);
				return;
			}
			if (this._2K_ToggleSwitch.IsChecked == true)
			{
				this.MemoryFunctions.GAME_Open_2K_Drop();
				return;
			}
			this.MemoryFunctions.GAME_Close_2k_Drop();
		}

		// Token: 0x0400002C RID: 44
		internal Slider Armor_Slider;

		// Token: 0x0400000F RID: 15
		private float[] Aunt_Denise_House = new float[]
		{
			-14.38f,
			-1438.51f,
			31.3f
		};

		// Token: 0x04000013 RID: 19
		private float[] Bank_Vault_Pacific_Standard = new float[]
		{
			255.85f,
			217f,
			101.9f
		};

		// Token: 0x04000055 RID: 85
		internal Slider Bullet_DMG_Slider;

		// Token: 0x04000048 RID: 72
		internal ToggleSwitch Bullet_proof_Tires_ToggleSwitch;

		// Token: 0x04000014 RID: 20
		private float[] Comedy_Club = new float[]
		{
			378.1f,
			-999.964f,
			-98.6f
		};

		// Token: 0x0400002D RID: 45
		internal NumericUpDown Damage_Multiplier_NumericUpDown;

		// Token: 0x0400005B RID: 91
		internal ToggleSwitch EXP_Level_ToggleSwitch;

		// Token: 0x04000057 RID: 87
		internal Button Fast_Shoot_Button;

		// Token: 0x0400000A RID: 10
		private float[] FIB_Building_Top = new float[]
		{
			136f,
			-750f,
			262f
		};

		// Token: 0x0400004A RID: 74
		internal Button FIX_Vehilc;

		// Token: 0x04000010 RID: 16
		private float[] Floyd_House = new float[]
		{
			-1151.77f,
			-1518.138f,
			10.85f
		};

		// Token: 0x04000019 RID: 25
		private float[] Fort_Zancudo_Tower = new float[]
		{
			-2358.132f,
			3249.754f,
			101.65f
		};

		// Token: 0x0400000C RID: 12
		private float[] Franklin_House = new float[]
		{
			7.119f,
			536.615f,
			176.2f
		};

		// Token: 0x04000021 RID: 33
		private bool GameIsRunning;

		// Token: 0x0400000B RID: 11
		private float[] Garment_Factory = new float[]
		{
			712.716f,
			-962.906f,
			30.6f
		};

		// Token: 0x04000022 RID: 34
		private bool GetFocus;

		// Token: 0x04000058 RID: 88
		internal Button Get_Full_Ammo_Button;

		// Token: 0x0400002A RID: 42
		internal Button Get_HP_Button;

		// Token: 0x0400001C RID: 28
		private bool God_Mode_isChecked;

		// Token: 0x04000027 RID: 39
		internal ToggleSwitch God_Mode_ToggleSwitch;

		// Token: 0x0400001E RID: 30
		private bool God_Mode_Vehicle_isChecked;

		// Token: 0x04000047 RID: 71
		internal ToggleSwitch God_Mode_Vehicle_ToggleSwitch;

		// Token: 0x04000015 RID: 21
		private float[] Humane_Labs = new float[]
		{
			3614.394f,
			3744.803f,
			28.9f
		};

		// Token: 0x04000016 RID: 22
		private float[] Humane_Labs_Tunnel = new float[]
		{
			3525.201f,
			3709.625f,
			21.2f
		};

		// Token: 0x04000017 RID: 23
		private float[] IAA_Office = new float[]
		{
			113.568f,
			-619.001f,
			206.25f
		};

		// Token: 0x04000011 RID: 17
		private float[] Lester_House = new float[]
		{
			1273.898f,
			-1719.304f,
			54.8f
		};

		// Token: 0x04000009 RID: 9
		private MemoryFunctions MemoryFunctions = new MemoryFunctions("GTA5", "GTA5.exe");

		// Token: 0x0400000D RID: 13
		private float[] Michael_House = new float[]
		{
			-813.603f,
			179.474f,
			72.5f
		};

		// Token: 0x0400001A RID: 26
		private float[] Mine_Shaft = new float[]
		{
			-595.342f,
			2086.008f,
			131.6f
		};

		// Token: 0x04000053 RID: 83
		internal Button NO_Doors;

		// Token: 0x04000028 RID: 40
		internal ToggleSwitch No_Ragdoll_ToggleSwitch;

		// Token: 0x04000032 RID: 50
		internal Button Open_XYZ_Thead_Button;

		// Token: 0x04000023 RID: 35
		private bool Player_IsInVehile;

		// Token: 0x04000059 RID: 89
		internal Button Reload_Multiplier_Button;

		// Token: 0x0400005A RID: 90
		internal Button Reload_Vehcile_Button;

		// Token: 0x04000025 RID: 37
		private bool Rockest_Switch;

		// Token: 0x04000049 RID: 73
		internal ToggleSwitch Rockets_ToggleSwitch;

		// Token: 0x04000029 RID: 41
		internal ToggleSwitch Seatbelt_ToggleSwitch;

		// Token: 0x04000056 RID: 86
		internal Button Spread_Button;

		// Token: 0x0400002E RID: 46
		internal Slider Sprint_Speed_Slider;

		// Token: 0x0400002F RID: 47
		internal Slider Swim_Speed_Slider;

		// Token: 0x04000026 RID: 38
		internal TabControl tabControl;

		// Token: 0x0400003B RID: 59
		internal Button Teleport_To_Aunt_Denise_House;

		// Token: 0x0400003F RID: 63
		internal Button Teleport_To_Bank_Vault_Pacific_Standard;

		// Token: 0x04000040 RID: 64
		internal Button Teleport_To_Comedy_Club;

		// Token: 0x04000036 RID: 54
		internal Button Teleport_To_FIB_Building_Top;

		// Token: 0x0400003C RID: 60
		internal Button Teleport_To_Floyd_House;

		// Token: 0x04000045 RID: 69
		internal Button Teleport_To_Fort_Zancudo_Tower;

		// Token: 0x04000038 RID: 56
		internal Button Teleport_To_Franklin_House;

		// Token: 0x04000037 RID: 55
		internal Button Teleport_To_Garment_Factory;

		// Token: 0x04000041 RID: 65
		internal Button Teleport_To_Humane_Labs;

		// Token: 0x04000042 RID: 66
		internal Button Teleport_To_Humane_Labs_Tunnel;

		// Token: 0x04000043 RID: 67
		internal Button Teleport_To_IAA_Office;

		// Token: 0x0400003D RID: 61
		internal Button Teleport_To_Lester_House;

		// Token: 0x04000039 RID: 57
		internal Button Teleport_To_Michael_House;

		// Token: 0x04000046 RID: 70
		internal Button Teleport_To_Mine_Shaft;

		// Token: 0x04000044 RID: 68
		internal Button Teleport_To_Torture_Room;

		// Token: 0x0400003A RID: 58
		internal Button Teleport_To_Trevor_House;

		// Token: 0x0400003E RID: 62
		internal Button Teleport_To_Vanilla_Unicorn_Office;

		// Token: 0x04000031 RID: 49
		internal Button Teleport_To_Waypoint_Button;

		// Token: 0x04000024 RID: 36
		private bool Thead_IsClosed = true;

		// Token: 0x04000018 RID: 24
		private float[] Torture_Room = new float[]
		{
			142.746f,
			-2201.189f,
			4.9f
		};

		// Token: 0x0400005F RID: 95
		internal Button TP_10W_WAY;

		// Token: 0x0400000E RID: 14
		private float[] Trevor_House = new float[]
		{
			1972.61f,
			3817.04f,
			33.65f
		};

		// Token: 0x04000054 RID: 84
		internal ToggleSwitch Unlimited_Ammo_ToggleSwitch;

		// Token: 0x04000012 RID: 18
		private float[] Vanilla_Unicorn_Office = new float[]
		{
			97.271f,
			-1290.994f,
			29.45f
		};

		// Token: 0x0400004B RID: 75
		internal NumericUpDown Vehicle_BRAKEFORCE;

		// Token: 0x04000030 RID: 48
		internal Slider Vehicle_Damage_Multiplier;

		// Token: 0x0400004E RID: 78
		internal NumericUpDown Vehicle_DEFORM_MULTIPLIER;

		// Token: 0x04000052 RID: 82
		internal NumericUpDown Vehicle_DirtLevel;

		// Token: 0x04000051 RID: 81
		internal NumericUpDown Vehicle_Gravity;

		// Token: 0x04000050 RID: 80
		internal NumericUpDown Vehicle_SUSPENSION_FORCE;

		// Token: 0x0400004D RID: 77
		internal NumericUpDown Vehicle_TRACTION_CURVE_MIN;

		// Token: 0x0400004F RID: 79
		internal NumericUpDown Vehicle_UPSHIFT;

		// Token: 0x0400004C RID: 76
		internal NumericUpDown Vehile_ACCELERATION;

		// Token: 0x0400001D RID: 29
		private int Wanted_Level;

		// Token: 0x0400002B RID: 43
		internal NumericUpDown Wanted_Level_NumericUpDown;

		// Token: 0x0400001F RID: 31
		private bool Wanted_Loop_Switch;

		// Token: 0x0400005C RID: 92
		internal ToggleSwitch Wanted_Loop_ToggleSwitch;

		// Token: 0x04000060 RID: 96
		internal Button Wanted_Lv_6;

		// Token: 0x04000033 RID: 51
		internal TextBox X_Textbox;

		// Token: 0x04000034 RID: 52
		internal TextBox Y_Textbox;

		// Token: 0x04000035 RID: 53
		internal TextBox Z_Textbox;

		// Token: 0x04000020 RID: 32
		private bool _10W_Switch;

		// Token: 0x0400005E RID: 94
		internal ToggleSwitch _10W_ToggleSwitch;

		// Token: 0x0400001B RID: 27
		private float[] _10W_TP_Way = new float[]
		{
			-698.101f,
			5810.264f,
			57f
		};

		// Token: 0x0400005D RID: 93
		internal ToggleSwitch _2K_ToggleSwitch;

		// Token: 0x04000061 RID: 97
		private bool _contentLoaded;
	}
}
