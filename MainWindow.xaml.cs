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
    public partial class MainWindow : MetroWindow
    {
        // Token: 0x06000020 RID: 32 RVA: 0x00003190 File Offset: 0x00001390
        public MainWindow()
        {
            this.InitializeComponent();
            //new KeyBoardHooking();
        }

        // Token: 0x06000025 RID: 37 RVA: 0x0000355C File Offset: 0x0000175C
        private void Armor_Slider_DragCompleted(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                Thread thread = new Thread(()=>
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

        // Token: 0x06000061 RID: 97 RVA: 0x00004028 File Offset: 0x00002228
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

        // Token: 0x0600005B RID: 91 RVA: 0x00003EC4 File Offset: 0x000020C4
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

        // Token: 0x06000062 RID: 98 RVA: 0x00004074 File Offset: 0x00002274
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

        // Token: 0x06000058 RID: 88 RVA: 0x00003E00 File Offset: 0x00002000
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

        // Token: 0x06000059 RID: 89 RVA: 0x00003E38 File Offset: 0x00002038
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

        // Token: 0x06000043 RID: 67 RVA: 0x00003938 File Offset: 0x00001B38
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

        // Token: 0x0600005C RID: 92 RVA: 0x00003F10 File Offset: 0x00002110
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

        // Token: 0x06000051 RID: 81 RVA: 0x00003C78 File Offset: 0x00001E78
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

        // Token: 0x06000049 RID: 73 RVA: 0x000026CC File Offset: 0x000008CC
        private void Game_Thread()
        {
            while (this.Thead_IsClosed)
            {
                this.GameIsRunning = this.MemoryFunctions.IsGameRunning();
                Thread.Sleep(500);
            }
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00003F48 File Offset: 0x00002148
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

        // Token: 0x06000028 RID: 40 RVA: 0x00002493 File Offset: 0x00000693
        private void Get_HP_Click(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                this.MemoryFunctions.GAME_Get_HP();
            }
        }

        // Token: 0x06000021 RID: 33 RVA: 0x0000336C File Offset: 0x0000156C
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
        bool God_Mode_isChecked;
        // Token: 0x06000022 RID: 34 RVA: 0x000033B8 File Offset: 0x000015B8
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
        bool God_Mode_Vehicle_isChecked;
        // Token: 0x0600002B RID: 43 RVA: 0x00003708 File Offset: 0x00001908
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

        // Token: 0x0600002C RID: 44 RVA: 0x00003760 File Offset: 0x00001960
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

        // Token: 0x0600004E RID: 78 RVA: 0x00002721 File Offset: 0x00000921
        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.Thead_IsClosed = false;
            this.MemoryFunctions.GAME_Close_2k_Drop();
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00003B48 File Offset: 0x00001D48
        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            ThreadStart start = new ThreadStart(this.Game_Thread);
            Thread thread = new Thread(start);
            thread.Start();
            Thread.Sleep(1000);
            ThreadStart start3 = new ThreadStart(this.Player_IsInVehile_Thead);
            Thread thread3 = new Thread(start3);
            thread3.Start();
        }

        // Token: 0x06000064 RID: 100 RVA: 0x000040F8 File Offset: 0x000022F8
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

        // Token: 0x06000023 RID: 35 RVA: 0x00003444 File Offset: 0x00001644
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
        bool GetFocus;
        bool Player_IsInVehile;
        // Token: 0x0600004B RID: 75 RVA: 0x000026F3 File Offset: 0x000008F3
        private void Player_IsInVehile_Thead()
        {
            while (this.GameIsRunning && this.Thead_IsClosed)
            {
                this.Player_IsInVehile = this.MemoryFunctions.GAME_In_Vehile();
            }
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00003F80 File Offset: 0x00002180
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

        // Token: 0x0600005F RID: 95 RVA: 0x00003FB8 File Offset: 0x000021B8
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
        bool Rockest_Switch;
        // Token: 0x06000066 RID: 102 RVA: 0x00002735 File Offset: 0x00000935
        private void Rockets_Theard()
        {
            while (this.Rockest_Switch && this.Thead_IsClosed)
            {
                this.MemoryFunctions.GAME_SET_Rockest();
                Thread.Sleep(100);
            }
        }

        // Token: 0x06000065 RID: 101 RVA: 0x00004130 File Offset: 0x00002330
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

        // Token: 0x06000050 RID: 80 RVA: 0x00003C2C File Offset: 0x00001E2C
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

        // Token: 0x0600005A RID: 90 RVA: 0x00003E8C File Offset: 0x0000208C
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

        // Token: 0x06000026 RID: 38 RVA: 0x000035A4 File Offset: 0x000017A4
        private void Sprint_Speed_Slider_DragCompleted(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                Thread thread = new Thread(()=>
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

        // Token: 0x06000027 RID: 39 RVA: 0x00003600 File Offset: 0x00001800
        private void Swim_Speed_Slider_DragCompleted(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                Thread thread = new Thread(() =>
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

        // Token: 0x06000035 RID: 53 RVA: 0x0000253E File Offset: 0x0000073E
        private void Teleport_To_Aunt_Denise_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Aunt_Denise_House[0], this.Aunt_Denise_House[1], this.Aunt_Denise_House[2]);
            });
        }

        // Token: 0x06000039 RID: 57 RVA: 0x000025B2 File Offset: 0x000007B2
        private void Teleport_To_Bank_Vault_Pacific_Standard_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Bank_Vault_Pacific_Standard[0], this.Bank_Vault_Pacific_Standard[1], this.Bank_Vault_Pacific_Standard[2]);
            });
        }

        // Token: 0x0600003A RID: 58 RVA: 0x000025CF File Offset: 0x000007CF
        private void Teleport_To_Comedy_Club_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Comedy_Club[0], this.Comedy_Club[1], this.Comedy_Club[2]);
            });
        }

        // Token: 0x06000030 RID: 48 RVA: 0x000024AD File Offset: 0x000006AD
        private void Teleport_To_FIB_Building_Top_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.FIB_Building_Top[0], this.FIB_Building_Top[1], this.FIB_Building_Top[2]);
            });
        }

        // Token: 0x06000036 RID: 54 RVA: 0x0000255B File Offset: 0x0000075B
        private void Teleport_To_Floyd_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Floyd_House[0], this.Floyd_House[1], this.Floyd_House[2]);
            });
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00002660 File Offset: 0x00000860
        private void Teleport_To_Fort_Zancudo_Tower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Fort_Zancudo_Tower[0], this.Fort_Zancudo_Tower[1], this.Fort_Zancudo_Tower[2]);
            });
        }

        // Token: 0x06000032 RID: 50 RVA: 0x000024E7 File Offset: 0x000006E7
        private void Teleport_To_Franklin_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Franklin_House[0], this.Franklin_House[1], this.Franklin_House[2]);
            });
        }

        // Token: 0x06000031 RID: 49 RVA: 0x000024CA File Offset: 0x000006CA
        private void Teleport_To_Garment_Factory_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Garment_Factory[0], this.Garment_Factory[1], this.Garment_Factory[2]);
            });
        }

        // Token: 0x0600003B RID: 59 RVA: 0x000025EC File Offset: 0x000007EC
        private void Teleport_To_Humane_Labs_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Humane_Labs[0], this.Humane_Labs[1], this.Humane_Labs[2]);
            });
        }

        // Token: 0x0600003C RID: 60 RVA: 0x00002609 File Offset: 0x00000809
        private void Teleport_To_Humane_Labs_Tunnel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Humane_Labs_Tunnel[0], this.Humane_Labs_Tunnel[1], this.Humane_Labs_Tunnel[2]);
            });
        }

        // Token: 0x0600003D RID: 61 RVA: 0x00002626 File Offset: 0x00000826
        private void Teleport_To_IAA_Office_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.IAA_Office[0], this.IAA_Office[1], this.IAA_Office[2]);
            });
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00002578 File Offset: 0x00000778
        private void Teleport_To_Lester_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Lester_House[0], this.Lester_House[1], this.Lester_House[2]);
            });
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00002504 File Offset: 0x00000704
        private void Teleport_To_Michael_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Michael_House[0], this.Michael_House[1], this.Michael_House[2]);
            });
        }

        // Token: 0x06000040 RID: 64 RVA: 0x0000267D File Offset: 0x0000087D
        private void Teleport_To_Mine_Shaft_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Mine_Shaft[0], this.Mine_Shaft[1], this.Mine_Shaft[2]);
            });
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00002643 File Offset: 0x00000843
        private void Teleport_To_Torture_Room_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Torture_Room[0], this.Torture_Room[1], this.Torture_Room[2]);
            });
        }

        // Token: 0x06000034 RID: 52 RVA: 0x00002521 File Offset: 0x00000721
        private void Teleport_To_Trevor_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Trevor_House[0], this.Trevor_House[1], this.Trevor_House[2]);
            });
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00002595 File Offset: 0x00000795
        private void Teleport_To_Vanilla_Unicorn_Office_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Vanilla_Unicorn_Office[0], this.Vanilla_Unicorn_Office[1], this.Vanilla_Unicorn_Office[2]);
            });
        }

        // Token: 0x0600002D RID: 45 RVA: 0x000037EC File Offset: 0x000019EC
        private void Teleport_To_Waypoint_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                Thread thread = new Thread(()=>
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

        /*
                 private void Teleport_To_Waypoint_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning() == true)
            {
                Thread Thread = new Thread(() =>
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                    Teleport_To_Waypoint_Button.IsEnabled = false;
                        Teleport_To_Waypoint_Button.Content = "Teleporting...";
                    });
                    MemoryFunctions.GAME_teleport_to_Waypoint();
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        Teleport_To_Waypoint_Button.IsEnabled = true;
                        Teleport_To_Waypoint_Button.Content = "Teleport to Waypoint";
                    });
                });
                Thread.IsBackground = true;
                Thread.SetApartmentState(ApartmentState.STA);
                Thread.Start();
            }
        }

             */

        // Token: 0x06000046 RID: 70 RVA: 0x00003A58 File Offset: 0x00001C58
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
        private float[] _10W_TP_Way = new float[]
        {
            -698.101f,
            5810.264f,
            57f
        };

        // Token: 0x0600002A RID: 42 RVA: 0x000036B8 File Offset: 0x000018B8
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

        // Token: 0x06000052 RID: 82 RVA: 0x00003CB0 File Offset: 0x00001EB0
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

        // Token: 0x06000029 RID: 41 RVA: 0x0000365C File Offset: 0x0000185C
        private void Vehicle_Damage_Multiplier_DragCompleted(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                Thread thread = new Thread(()=>
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

        // Token: 0x06000054 RID: 84 RVA: 0x00003D20 File Offset: 0x00001F20
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

        // Token: 0x06000063 RID: 99 RVA: 0x000040C0 File Offset: 0x000022C0
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

        // Token: 0x06000057 RID: 87 RVA: 0x00003DC8 File Offset: 0x00001FC8
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

        // Token: 0x06000056 RID: 86 RVA: 0x00003D90 File Offset: 0x00001F90
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

        // Token: 0x06000053 RID: 83 RVA: 0x00003CE8 File Offset: 0x00001EE8
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

        // Token: 0x06000055 RID: 85 RVA: 0x00003D58 File Offset: 0x00001F58
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

        // Token: 0x0600004F RID: 79 RVA: 0x00003BF4 File Offset: 0x00001DF4
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
        int Wanted_Level;
        // Token: 0x06000024 RID: 36 RVA: 0x00003494 File Offset: 0x00001694
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
                    Thread thread = new Thread(()=>
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
        bool Wanted_Loop_Switch;
        // Token: 0x06000041 RID: 65 RVA: 0x0000269A File Offset: 0x0000089A
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

        // Token: 0x06000042 RID: 66 RVA: 0x000038AC File Offset: 0x00001AAC
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

        // Token: 0x06000060 RID: 96 RVA: 0x00003FF0 File Offset: 0x000021F0
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

        // Token: 0x0600004C RID: 76 RVA: 0x00002718 File Offset: 0x00000918
        private void X_Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GetFocus = true;
        }
        bool _10W_Switch;
        // Token: 0x06000044 RID: 68 RVA: 0x00003988 File Offset: 0x00001B88
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

        // Token: 0x06000045 RID: 69 RVA: 0x000039CC File Offset: 0x00001BCC
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

        // Token: 0x06000047 RID: 71 RVA: 0x00003A94 File Offset: 0x00001C94
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

        // Token: 0x04000014 RID: 20
        private float[] Comedy_Club = new float[]
        {
            378.1f,
            -999.964f,
            -98.6f
        };

        // Token: 0x0400000A RID: 10
        private float[] FIB_Building_Top = new float[]
        {
            136f,
            -750f,
            262f
        };


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

        private bool Thead_IsClosed = true;

        // Token: 0x04000018 RID: 24
        private float[] Torture_Room = new float[]
        {
            142.746f,
            -2201.189f,
            4.9f
        };
        // Token: 0x0400000E RID: 14
        private float[] Trevor_House = new float[]
        {
            1972.61f,
            3817.04f,
            33.65f
        };
        private float[] Vanilla_Unicorn_Office = new float[]
        {
            97.271f,
            -1290.994f,
            29.45f
        };

        private void Super_Jump_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
