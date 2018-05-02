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
using mAsk.Model;
using System.Threading.Tasks;

namespace WpfApplication
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            new KeyBoardHooking();
            System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

            _timer.Interval = 10;
            _timer.Tick += TimerDealy;
            _timer.Start();
        }


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

        private void Armor_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Armor((float)this.Armor_Slider.Value);
                });
                return;
            }
            this.Armor_Slider.Value = 0.0;
        }

        private void Bullet_DMG_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Bullet_DMG((float)this.Bullet_DMG_Slider.Value);
                });
                return;
            }
            this.Bullet_DMG_Slider.Value = 0.0;
        }

        private void Bullet_proof_Tires_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_Bullet_proof_Tires(this.Bullet_proof_Tires_ToggleSwitch.IsChecked);
                });
                return;
            }
            this.Bullet_proof_Tires_ToggleSwitch.IsChecked = new bool?(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
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

        private void Fast_Shoot_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Fast_Shoot();
                });
            }
        }

        private void FIX_Vehilc_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_FIX_Vehlie();
                });
            }
        }

        private void Game_Thread()
        {
            while (this.Thead_IsClosed)
            {
                this.GameIsRunning = this.MemoryFunctions.IsGameRunning();
                Thread.Sleep(500);
            }
        }

        private void Get_Full_Ammo_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Get_Full_Ammo();
                });
            }
        }

        private void Get_HP_Click(object sender, RoutedEventArgs e)
        {
            if (this.MemoryFunctions.IsGameRunning())
            {
                this.MemoryFunctions.GAME_Get_HP();
            }
        }

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


        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.Thead_IsClosed = false;
            this.MemoryFunctions.GAME_Close_2k_Drop();
        }


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


        private void NO_Doors_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_Doors();
                });
            }
        }


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
        bool Player_IsInVehile;


        private void Player_IsInVehile_Thead()
        {
            while (this.GameIsRunning && this.Thead_IsClosed)
            {
                this.Player_IsInVehile = this.MemoryFunctions.GAME_In_Vehile();
            }
        }


        private void Reload_Multiplier_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Reload_Multiplier();
                });
            }
        }


        private void Reload_Vehcile_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Reload_Vehicle();
                });
            }
        }
        bool Rockest_Switch;


        private void Rockets_Theard()
        {
            while (this.Rockest_Switch && this.Thead_IsClosed)
            {
                this.MemoryFunctions.GAME_SET_Rockest();
                Thread.Sleep(100);
            }
        }


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


        private void Seatbelt_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Seatbelt(this.Seatbelt_ToggleSwitch.IsChecked);
                });
                return;
            }
            this.Seatbelt_ToggleSwitch.IsChecked = new bool?(false);
        }


        private void Spread_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Spread();
                });
            }
        }


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


        private void Teleport_To_Aunt_Denise_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Aunt_Denise_House[0], this.Aunt_Denise_House[1], this.Aunt_Denise_House[2]);
            });
        }


        private void Teleport_To_Bank_Vault_Pacific_Standard_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Bank_Vault_Pacific_Standard[0], this.Bank_Vault_Pacific_Standard[1], this.Bank_Vault_Pacific_Standard[2]);
            });
        }


        private void Teleport_To_Comedy_Club_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Comedy_Club[0], this.Comedy_Club[1], this.Comedy_Club[2]);
            });
        }


        private void Teleport_To_FIB_Building_Top_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.FIB_Building_Top[0], this.FIB_Building_Top[1], this.FIB_Building_Top[2]);
            });
        }


        private void Teleport_To_Floyd_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Floyd_House[0], this.Floyd_House[1], this.Floyd_House[2]);
            });
        }


        private void Teleport_To_Fort_Zancudo_Tower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Fort_Zancudo_Tower[0], this.Fort_Zancudo_Tower[1], this.Fort_Zancudo_Tower[2]);
            });
        }


        private void Teleport_To_Franklin_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Franklin_House[0], this.Franklin_House[1], this.Franklin_House[2]);
            });
        }


        private void Teleport_To_Garment_Factory_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Garment_Factory[0], this.Garment_Factory[1], this.Garment_Factory[2]);
            });
        }


        private void Teleport_To_Humane_Labs_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Humane_Labs[0], this.Humane_Labs[1], this.Humane_Labs[2]);
            });
        }


        private void Teleport_To_Humane_Labs_Tunnel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Humane_Labs_Tunnel[0], this.Humane_Labs_Tunnel[1], this.Humane_Labs_Tunnel[2]);
            });
        }


        private void Teleport_To_IAA_Office_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.IAA_Office[0], this.IAA_Office[1], this.IAA_Office[2]);
            });
        }


        private void Teleport_To_Lester_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Lester_House[0], this.Lester_House[1], this.Lester_House[2]);
            });
        }


        private void Teleport_To_Michael_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Michael_House[0], this.Michael_House[1], this.Michael_House[2]);
            });
        }


        private void Teleport_To_Mine_Shaft_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Mine_Shaft[0], this.Mine_Shaft[1], this.Mine_Shaft[2]);
            });
        }


        private void Teleport_To_Torture_Room_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Torture_Room[0], this.Torture_Room[1], this.Torture_Room[2]);
            });
        }


        private void Teleport_To_Trevor_House_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Trevor_House[0], this.Trevor_House[1], this.Trevor_House[2]);
            });
        }

        private void Teleport_To_Vanilla_Unicorn_Office_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.MemoryFunctions.TP_Way(this.Vanilla_Unicorn_Office[0], this.Vanilla_Unicorn_Office[1], this.Vanilla_Unicorn_Office[2]);
            });
        }

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

        private void Vehicle_BRAKEFORCE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_BRAKEFORCE((float)this.Vehicle_BRAKEFORCE.Value.Value);
                });
            }
        }

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

        private void Vehicle_DEFORM_MULTIPLIER_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_DEFORM_MULTIPLIER((float)this.Vehicle_DEFORM_MULTIPLIER.Value.Value);
                });
            }
        }

        private void Vehicle_DirtLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_DirtLevel((float)this.Vehicle_DirtLevel.Value.Value);
                });
            }
        }

        private void Vehicle_Gravity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_Gravity((float)this.Vehicle_Gravity.Value.Value);
                });
            }
        }

        private void Vehicle_SUSPENSION_FORCE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_SUSPENSION_FORCE((float)this.Vehicle_SUSPENSION_FORCE.Value.Value);
                });
            }
        }

        private void Vehicle_TRACTION_CURVE_MIN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_TRACTION_CURVE_MIN((float)this.Vehicle_TRACTION_CURVE_MIN.Value.Value);
                });
            }
        }

        private void Vehicle_UPSHIFT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_SET_Vehicle_UPSHIFT((float)this.Vehicle_UPSHIFT.Value.Value);
                });
            }
        }

        private void Vehile_ACCELERATION_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_Set_Vehlie_ACCELERATION((float)this.Vehile_ACCELERATION.Value.Value);
                });
            }
        }
        int Wanted_Level;

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

        private void Wanted_Lv_6_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MemoryFunctions.GAME_set_Wanted_Level(6);
                });
            }
        }

        bool _10W_Switch;

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


        private float[] Aunt_Denise_House = new float[]
        {
            -14.38f,
            -1438.51f,
            31.3f
        };

        private float[] Bank_Vault_Pacific_Standard = new float[]
        {
            255.85f,
            217f,
            101.9f
        };

        private float[] Comedy_Club = new float[]
        {
            378.1f,
            -999.964f,
            -98.6f
        };

        private float[] FIB_Building_Top = new float[]
        {
            136f,
            -750f,
            262f
        };


        private float[] Floyd_House = new float[]
        {
            -1151.77f,
            -1518.138f,
            10.85f
        };

        private float[] Fort_Zancudo_Tower = new float[]
        {
            -2358.132f,
            3249.754f,
            101.65f
        };

        private float[] Franklin_House = new float[]
        {
            7.119f,
            536.615f,
            176.2f
        };

        private bool GameIsRunning;

        private float[] Garment_Factory = new float[]
        {
            712.716f,
            -962.906f,
            30.6f
        };

        private float[] Humane_Labs = new float[]
        {
            3614.394f,
            3744.803f,
            28.9f
        };

        private float[] Humane_Labs_Tunnel = new float[]
        {
            3525.201f,
            3709.625f,
            21.2f
        };

        private float[] IAA_Office = new float[]
        {
            113.568f,
            -619.001f,
            206.25f
        };

        private float[] Lester_House = new float[]
        {
            1273.898f,
            -1719.304f,
            54.8f
        };

        private MemoryFunctions MemoryFunctions = new MemoryFunctions("GTA5", "GTA5.exe");

        private float[] Michael_House = new float[]
        {
            -813.603f,
            179.474f,
            72.5f
        };

        private float[] Mine_Shaft = new float[]
        {
            -595.342f,
            2086.008f,
            131.6f
        };

        private bool Thead_IsClosed = true;

        private float[] Torture_Room = new float[]
        {
            142.746f,
            -2201.189f,
            4.9f
        };
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
        bool Super_Jump = false;
        private void Super_Jump_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryFunctions.IsGameRunning())
            {
                if (Super_Jump_ToggleSwitch.IsChecked == true)
                {
                    Super_Jump = true;
                    Super_JumpThread();
                    Super_JumpThread();
                    Super_JumpThread();
                    Super_JumpThread();
                }
                else Super_Jump = false;
            }
        }

        private void Super_JumpThread()
        {
            Thread Thread1 = new Thread(() =>
            {
                while (Super_Jump)
                {
                    MemoryFunctions.Super_Jump();
                    Thread.Sleep(1);
                }
            });
            Thread1.IsBackground = true;
            Thread1.SetApartmentState(ApartmentState.MTA);
            Thread1.Start();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // CheckMainForm();
            //if (!MemoryFunctions.IsGameRunning())
                //System.Environment.Exit(0);
        }

        private void Radar_Hiding_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            float MAXHP=200;
            God_Mode_ToggleSwitch.IsChecked = true;
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
            if (Radar_Hiding_ToggleSwitch.IsChecked == true)
            {
                MAXHP = MemoryFunctions.Radar_Hiding();
            }
            else
            { MemoryFunctions.Radar_Hiding_Closing(MAXHP); this.God_Mode_isChecked = false;
                this.MemoryFunctions.GAME_set_God_Mode(this.God_Mode_ToggleSwitch.IsChecked);
                God_Mode_ToggleSwitch.IsChecked = false;
            }
        }

        private void JC_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("10w刷钱教程:\r\n进单人战局→ESC→线上→差事→开始差事→R*创建任务→跳伞→坚定信仰→开启10w刷钱\r\n需要注意的小技巧：进入差事里等倒计时3，2，1，GO；等GO完全消失3秒然后再点传送至终点上空→然后切入游戏按空格后迅速开伞→12W到手。如果在GO消失之前点了传送至终点上空，最终会失败得到0元。切记！！！\r\n--------------------------------------\r\n2k刷钱教程:\r\n传送至海滩的游乐场或者太平洋银行附近→打开2k刷钱→按小键盘＋开始刷钱（＋可以调整刷钱速度，按的次数越多越快，可以配合挂机脚本挂机刷，但不建议按5次以上，容易导致游戏报错。）\r\n--------------------------------------");
        }
        bool Explosive_Ammo= false;
        private void Explosive_Ammo_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            MemoryFunctions.Super_Jump();
            if (Explosive_Ammo_ToggleSwitch.IsChecked == true)
                {
                    Fire_Ammo_ToggleSwitch.IsChecked = false;
                    Fire_Ammo_ToggleSwitch.IsEnabled = false;
                    Explosive_Ammo = true;
                    Explosive_AmmoThread();
                    Explosive_AmmoThread();
                    Explosive_AmmoThread();
                    Explosive_AmmoThread();
                }
                else
                {
                    Explosive_Ammo = false;
                    Fire_Ammo_ToggleSwitch.IsEnabled = true;
                }
        }
        private void Explosive_AmmoThread()
        {
            Thread Thread1 = new Thread(() =>
            {
                while (Explosive_Ammo)
                {
                    MemoryFunctions.Explosive_Ammo();
                    //Thread.Sleep(1);
                }
            });
            Thread1.IsBackground = true;
            Thread1.SetApartmentState(ApartmentState.MTA);
            Thread1.Start();
        }


        bool Fire_Ammo= false;
        private void Fire_Ammo_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (Fire_Ammo_ToggleSwitch.IsChecked == true)
                {
                    Explosive_Ammo_ToggleSwitch.IsChecked = false;
                    Explosive_Ammo_ToggleSwitch.IsEnabled = false;
                    Fire_Ammo = true;
                    Fire_AmmoThread();
                    Fire_AmmoThread();
                    Fire_AmmoThread();
                    Fire_AmmoThread();
                }
                else
                {
                    Fire_Ammo = false;
                    Explosive_Ammo_ToggleSwitch.IsEnabled = true;
                }
        }
        private void Fire_AmmoThread()
        {
            Thread Thread1 = new Thread(() =>
            {
                while (Fire_Ammo)
                {
                    MemoryFunctions.Fire_Ammo();
                    Thread.Sleep(1);
                }
            });
            Thread1.IsBackground = true;
            Thread1.SetApartmentState(ApartmentState.MTA);
            Thread1.Start();
        }


        bool Explosive_Melee = false;
        private void Explosive_Melee_ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
                if (Explosive_Melee_ToggleSwitch.IsChecked == true)
                {
                    Explosive_Melee = true;
                    Explosive_MeleeThread();
                    Explosive_MeleeThread();
                    Explosive_MeleeThread();
                    Explosive_MeleeThread();
                }
                else Explosive_Melee = false;
        }

        private void Explosive_MeleeThread()
        {
            Thread Thread1 = new Thread(() =>
            {
                while (Explosive_Melee)
                {
                    MemoryFunctions.Explosive_Melee();
                    Thread.Sleep(1);
                }
            });
            Thread1.IsBackground = true;
            Thread1.SetApartmentState(ApartmentState.MTA);
            Thread1.Start();
        }

        void TimerDealy(object o, EventArgs e)//窗体收缩/放下
        {
            if (this.Top > 3)
            {
                return;
            }
            double mouse_x = System.Windows.Forms.Form.MousePosition.X;
            double mouse_y = System.Windows.Forms.Form.MousePosition.Y;

            bool is_in_collasped_range = (mouse_y > this.Top + this.Height) || (mouse_x < this.Left || mouse_x > this.Left + this.Width);
            bool is_in_visiable_range = (mouse_y < 1 && mouse_x >= this.Left && mouse_x <= this.Left + this.Width);         

            if (this.Top < 3 && this.Top >= 0 && is_in_collasped_range)
            {
                System.Threading.Thread.Sleep(300);
                this.Top = -this.ActualHeight - 3;
            }
            else if (this.Top < 0 && is_in_visiable_range)
            {
                this.Top = 1;
            }
        }


        private void label1_MouseEnter(object sender, MouseEventArgs e)
        {
            label1.Content = "点击进入我的个人博客~";
        }

        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            label1.Content = "mAsk°";
        }

        private void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://www.javanet.top/");
        }
    }
}
