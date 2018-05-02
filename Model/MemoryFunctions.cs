using Model.Libraries.Memory;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Data;
using System.Linq;
using System.Collections.Generic;
namespace mAsk.Model
{
    public class MemoryFunctions : Memory
    {
        public enum GAME_VERSION_DETECTED
        {
            STEAM,
            SOCIALCLUB
        }

        public GAME_VERSION_DETECTED GAME_VERSION;

        int WorldPTR;
        int AmmoPTR;
        int ClipPTR;
        int BlipPTR;
        int PointerAddressA;

        int WorldPTR_SOCIALCLUB = 0x23d1c48;
        int AmmoPTR_SOCIALCLUB = 0xE88EB9;
        int ClipPTR_SOCIALCLUB = 0xE88E74;
        int BlipPTR_SOCIALCLUB = 0x1F9E750;
        int PointerAddressA_SOCIALCLUB = 0x2C5DD78;

        int WorldPTR_STEAM = 0x23E2130;
        int AmmoPTR_STEAM = 0xEA2D8D;
        int ClipPTR_STEAM = 0xEA2D48;
        int BlipPTR_STEAM = 0x1FD8840;
        int PointerAddressA_STEAM = 0x2C5DD78;
        /*
         * autoAssemble([[
aobscanmodule(GetPointerAddressA,GTA5.exe,48 8B 8C C2 xx xx xx xx 48 85 C9 74 19)
registersymbol(GetPointerAddressA)
]])
         * */

        //Player
        int[] OFFSETS_God_Mode = new int[] { 0x08, 0x189 };
        int[] OFFSETS_Wanted_Level = new int[] { 0x08, 0x10B8, 0x7F8 };
        int[] OFFSETS_Sprint_Speed = new int[] { 0x08, 0x10B8, 0x14C };
        int[] OFFSETS_Swim_Speed = new int[] { 0x08, 0x10B8, 0x0148 };
        int[] OFFSETS_Max_HP=new int[]{0x8,0x2A0};
        int[] OFFSETS_HP = new int[] { 0x8, 0x280 };
        int[] OFFSETS_Armor = new int[] { 0x8, 0x14B8 };
        int[] OFFSETS_No_Ragdoll = new int[] { 0x8, 0x10A8 };
        int[] OFFSETS_Seatbelt = new int[] { 0x8, 0x13EC };//on:201,off:200
        int[] OFFSETS_Stamina = new int[] { 0x8, 0x10B8, 0xC60 };
        int[] OFFSETS_Damage_Multiplier = new int[] { 0x8, 0x10B8, 0xC78 };
        int[] OFFSETS_Melee_Damage_Multiplier = new int[] { 0x8, 0x10B8, 0xC80 };
        int[] OFFSETS_Vehicle_Damage_Multiplier = new int[] { 0x08,0x10B8,0xC88 };
        int[] OFFSETS_InVehicle = new int[] { 0x08,0x146B };
        int[] OFFSETS_Fram_Eflags = new int[] { 0x08, 0x10B8,0x1F8 };

        public float Radar_Hiding()
        {
            float MAXHP=ReadFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Fram_Eflags));
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Max_HP), 0f);
            return MAXHP;
        }
        public void Radar_Hiding_Closing(float MAXHP)
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Max_HP), MAXHP);
        }

        //Weapon
        int[] OFFSETS_Spread = new int[] { 0x8, 0x10C8, 0x20, 0x74 };
        int[] OFFSETS_Recoil = new int[] { 0x8, 0x10C8, 0x20, 0x2A4 };
        int[] OFFSETS_Fast_Shoot = new int[] { 0x8, 0x10C8, 0x20, 0x134 };
        int[] OFFSETS_BULLET_DMG = new int[] { 0x8,0x10C8,0x20,0xB0};
        int[] OFFSETS_Reload_Multiplier = new int[] { 0x8, 0x10C8, 0x20, 0x12C };
        int[] OFFSETS_Reload_Vehicle = new int[] { 0x8, 0x10C8, 0x20, 0x128 };
        //Ammo
        int[] OFFSETS_Max_Ammo = new int[] { 0x8, 0x10C8, 0x20, 0x60, 0x28 };
        int[] OFFSETS_Ammo_Type = new int[] { 0x8, 0x10C8, 0x20, 0x60, 0x54 };
        int[] OFFSETS_current_Ammo = new int[] { 0x8, 0x10C8, 0x20, 0x60, 0x8,0x0,0x18 };

        //Vehile
        int[] OFFSETS_God_Mode_Vehicle = new int[] { 0x08, 0xD28, 0x189 };
        int[] OFFSETS_Vehicle_Health1 = new int[] { 0x08, 0xD28, 0x280 };
        int[] OFFSETS_Vehicle_Health2 = new int[] { 0x08, 0xD28, 0x89C };
        int[] OFFSETS_Vehicle_Boost = new int[] { 0x08, 0xD28, 0x320 };
        int[] OFFSETS_Vehicle_DirtLevel = new int[] { 0x08, 0xD28, 0x988 };
        int[] OFFSETS_Vehicle_Bullet_Proof_Tires = new int[] { 0x08, 0xD28, 0x8D3 };
        int[] OFFSETS_Vehicle_Tires = new int[] { 0x08, 0xD28, 0xB68 };
        int[] OFFSETS_Vehicle_Doors = new int[] { 0x08, 0xD28, 0xB80 };
        int[] OFFSETS_Vehicle_ACCELERATION = new int[] { 0x08, 0xD28, 0x8C8,0x4C };
        int[] OFFSETS_Vehicle_BRAKEFORCE = new int[] { 0x08, 0xD28, 0x8C8, 0x6C };
        int[] OFFSETS_Vehicle_TRACTION_CURVE_MIN = new int[] { 0x08, 0xD28, 0x8C8, 0x90 };
        int[] OFFSETS_Vehicle_DEFORM_MULTIPLIER = new int[] { 0x08, 0xD28, 0x8C8, 0xF8 };
        int[] OFFSETS_Vehicle_UPSHIFT = new int[] { 0x08, 0xD28, 0x8C8, 0x58 };
        int[] OFFSETS_Vehicle_SUSPENSION_FORCE = new int[] { 0x08, 0xD28, 0x8C8, 0xBC };
        int[] OFFSETS_Vehicle_Speed = new int[] { 0x08, 0xD28, 0x790 };
        int[] OFFSETS_Vehicle_Gravity = new int[] { 0x08, 0xD28,0xBCC };


        //TP
        int[] OFFSETS_X = new int[] { 0x8, 0x30, 0x50 };
        int[] OFFSETS_Y = new int[] { 0x8, 0x30, 0x54 };
        int[] OFFSETS_Z = new int[] { 0x8, 0x30, 0x58 };

        //MICS
        int[] OFFSETS_EXP = new int[] { 0x10 };
        int[] OFFSETS_10W_1 = new int[] { 0x8EA0 };
        int[] OFFSETS_10W_2 = new int[] { 0x8EB0 };

        //TP Way.

        public void TP_Way(float x,float y,float z)
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_X), x);
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Y), y);
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Z), z);
        }
        public float Game_Get_X()
        {
            return ReadFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_X));
        }

        public float Game_Get_Y()
        {
            return ReadFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Y));
        }
        public float Game_Get_Z()
        {
            return ReadFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Z));
        }
        public void Game_Set_X(float X_value)
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_X), X_value);
        }
        public void Game_Set_Y(float Y_value)
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Y), Y_value);
        }
        public void Game_Set_Z(float Z_value)
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Z), Z_value);
        }

        public MemoryFunctions(string exeName, string processName)
        {
            ExeName = exeName;
            ProcessName = processName;
            BaseAddress = GetBaseAddress(ProcessName);
            MyBaseAddress = BaseAddress;
            pHandle = GetProcessHandle();
        }
        static long MyBaseAddress;
        //InVehile
        public bool GAME_In_Vehile()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_InVehicle);
            if (ReadBytes(pointer, 2) == new byte[]{0x10})
                return false;
            else return true;
        }


        // God Mode.
        public byte[] GAME_get_God_Mode()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_God_Mode);
            return ReadBytes(pointer, 2);
        }
        public void GAME_set_God_Mode(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_God_Mode);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1, 0x69 });
            } else
            {
                WriteBytes(pointer, new byte[] { 0x0, 0x69 });
            }
        }

        //No Ragdoll.
        public void GAME_No_Ragdoll(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_No_Ragdoll);
            if (enabled==true)
                WriteBytes(pointer,  new byte[]{0x0});
            else WriteBytes(pointer, new byte[]{0x20});
        }
        //Armor.
        public void GAME_Armor(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Armor);
            WriteFloat(pointer, value);
        }

        //Get HP.
        public void GAME_Get_HP()
        {
            long pointer_max_hp = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Max_HP);
            long pointer_hp = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_HP);
            WriteFloat(pointer_hp, ReadFloat(pointer_max_hp));
        }

        // God Mode Vehicle.
        public void GAME_set_God_Mode_Vehicle(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_God_Mode_Vehicle);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        
        //2K Drop.
        public void GAME_Open_2K_Drop()
        {
            /*
            try
            {
                Byte[] array = mAsk.Properties.Resources.ped_dropper.ToArray<Byte>(); 
                SaveFile(array, @"C:\Windows\temp\mAsk.exe");
                Process.Start(@"C:\Windows\temp\mAsk.exe");
            }
            catch (Exception e)
            {
                MessageBox.Show("开启失败！请检查系统防火墙或杀毒软件！");
            }
            */
        }
        
        public void GAME_Close_2k_Drop()
        {
            try
            {
                Process[] pro = Process.GetProcessesByName("mAsk");
                if (pro.Length != 0)
                {
                    Process[] process = Process.GetProcesses();
                    foreach (Process proces in process)
                    {
                        if (proces.ProcessName == "mAsk")
                        {
                            proces.Kill();
                        }
                    }
                }
                File.Delete(@"C:\Windows\temp\mAsk.exe");
            }
            catch (Exception e)
            {
            }
        }

        private static void SaveFile(Byte[] array, string path)
        {
            FileStream fs = new FileStream(path, System.IO.FileMode.Create); 
            fs.Write(array, 0, array.Length);
            fs.Close();
        }

        // Wanted Level.
        public int GAME_get_Wanted_Level()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Wanted_Level);
            return ReadInteger(pointer, 4);
        }

        public void GAME_set_Wanted_Level(int value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Wanted_Level);
            WriteInteger(pointer, value, 4);
        }

        // Sprint Speed.
        public void GAME_set_Sprint_Speed(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Sprint_Speed);
            WriteFloat(pointer, value);
        }

        // Swim Speed.
        public void GAME_set_Swim_Speed(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Swim_Speed);
            WriteFloat(pointer, value);
        }

        //Vehicle_Damage_Multiplier.
        public void GAME_set_Vehicle_Damage_Multiplier(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Damage_Multiplier);
            WriteFloat(pointer, value);
        }

        // Unlimited Ammo.
        public void GAME_set_Unlimited_Ammo(bool? enabled)
        {
            // Ammo.
            long pointer = GetPointerAddress(BaseAddress + AmmoPTR);
            // Magazine.
            long pointer1 = GetPointerAddress(BaseAddress + ClipPTR);

            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x90, 0x90, 0x90});
                WriteBytes(pointer1, new byte[] { 0x90, 0x90, 0x90, 0x3B, 0xC8, 0x0F });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x41, 0x2B, 0xD1});
                WriteBytes(pointer1, new byte[] { 0x41, 0x2B, 0xC9 });
            }
        }

        /////////////////////////////////////////////////////////////////////

        //Vehlie ACCELERATION
        public float GAME_Get_Vehlie_ACCELERATION()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR,OFFSETS_Vehicle_ACCELERATION);
            return ReadFloat(pointer);
        }

        //Vehicle_BRAKEFORCE
        public float GAME_GET_Vehicle_BRAKEFORCE()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_BRAKEFORCE);
            return ReadFloat(pointer);
        }

        //Vehicle_UPSHIFT
        public float GAME_GET_Vehicle_UPSHIFT()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_UPSHIFT);
            return ReadFloat(pointer);
        }

        //Vehicle_DEFORM_MULTIPLIER
        public float GAME_GET_Vehicle_DEFORM_MULTIPLIER()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_DEFORM_MULTIPLIER);
            return ReadFloat(pointer);
        }

        //Vehicle_SUSPENSION_FORCE
        public float GAME_GET_Vehicle_SUSPENSION_FORCE()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_SUSPENSION_FORCE);
            return ReadFloat(pointer);
        }

        //Vehicle_DirtLevel
        public float GAME_GET_Vehicle_DirtLevel()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_DirtLevel);
            return ReadFloat(pointer);
        }

        //Vehicle_Bullet proof Tires
        public byte[] GAME_GET_Vehicle_Bullet_proof_Tires()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Bullet_Proof_Tires);
            return ReadBytes(pointer, 1);
        }

        //Vehicle_Gravity
        public float GAME_GET_Vehicle_Gravity()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Gravity);
            return ReadFloat(pointer);
        }
        //Vehicle_TRACTION_CURVE_MIN
        public float GAME_GET_Vehicle_TRACTION_CURVE_MIN()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_TRACTION_CURVE_MIN);
            return ReadFloat(pointer);
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        //Rockest
        public void GAME_SET_Rockest()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Boost);
            WriteFloat(pointer, 1.25f);
        }

        //Vehicle_DirtLevel
        public void GAME_SET_Vehicle_DirtLevel(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_DirtLevel);
            WriteFloat(pointer,value);
        }

        //Vehicle_Doors
        public void GAME_SET_Vehicle_Doors()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Doors);
            WriteBytes(pointer, new byte[] {0});
        }

        //Vehicle_Bullet proof Tires
        public void GAME_SET_Vehicle_Bullet_proof_Tires(bool? enable)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Bullet_Proof_Tires);
            if (enable == true)
                WriteBytes(pointer,new byte[] {44});
            else WriteBytes(pointer, new byte[] { 18 });
        }

        //FIX VEHLIE
        public void GAME_FIX_Vehlie()
        {
            long pointer1 = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Health1);
            long pointer2 = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Health2);
            WriteFloat(pointer1, 1000f);
            WriteFloat(pointer2, 1000f);
        }
        //Vehlie ACCELERATION
        public void GAME_Set_Vehlie_ACCELERATION(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_ACCELERATION);
            WriteFloat(pointer, value);
        }
        //Vehicle_BRAKEFORCE
        public void GAME_SET_Vehicle_BRAKEFORCE(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_BRAKEFORCE);
            WriteFloat(pointer, value);
        }

        //Vehicle_UPSHIFT
        public void GAME_SET_Vehicle_UPSHIFT(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_UPSHIFT);
            WriteFloat(pointer, value);
        }

        //Vehicle_DEFORM_MULTIPLIER
        public void GAME_SET_Vehicle_DEFORM_MULTIPLIER(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_DEFORM_MULTIPLIER);
            WriteFloat(pointer, value);
        }

        //Vehicle_SUSPENSION_FORCE
        public void GAME_SET_Vehicle_SUSPENSION_FORCE(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_SUSPENSION_FORCE);
            WriteFloat(pointer, value);
        }

        //Vehicle_Gravity
        public void GAME_SET_Vehicle_Gravity(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_Gravity);
            WriteFloat(pointer, value);
        }
        //Vehicle_TRACTION_CURVE_MIN
        public void GAME_SET_Vehicle_TRACTION_CURVE_MIN(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Vehicle_TRACTION_CURVE_MIN);
            WriteFloat(pointer,value);
        }
        //
        //Seatbelt
        public void GAME_Seatbelt(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Seatbelt);
            if (enabled == true)
                WriteBytes(pointer, new byte[] { 201 });
            else WriteBytes(pointer, new byte[] { 200 });
        }


        //10w.

        public void GAME_Set_10W(bool? enabled)
        {
            if (enabled == true)
            { 
                WriteInteger(GetPointerAddress(BaseAddress + PointerAddressA, OFFSETS_10W_1), 3004, 4);
                WriteInteger(GetPointerAddress(BaseAddress + PointerAddressA, OFFSETS_10W_2), 3004, 4); 
            }
            else 
            {
                WriteInteger(GetPointerAddress(BaseAddress + PointerAddressA, OFFSETS_10W_1), 80, 4);
                WriteInteger(GetPointerAddress(BaseAddress + PointerAddressA, OFFSETS_10W_2), 30, 4);
            }
        }

        //EXP

        public void GAME_set_EXP_Level_60(bool? enabled)
        {
            if(enabled==true)
            WriteFloat(GetPointerAddress(BaseAddress + PointerAddressA,OFFSETS_EXP), 60f);
            else WriteFloat(GetPointerAddress(BaseAddress + PointerAddressA, OFFSETS_EXP), 1f);
        }

        
         // Unlimited Ammo Clip.
        public void GAME_set_Unlimited_Ammo_Clip(bool? enabled)
        {
            // Ammo.
            long pointer = GetPointerAddress(BaseAddress + AmmoPTR);
            // Magazine.
            long pointer2 = GetPointerAddress(BaseAddress + ClipPTR);

            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x90, 0x90, 0x90 });
                WriteBytes(pointer2, new byte[] { 0x90, 0x90, 0x90, 0x3B, 0xC8, 0x0F });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x41, 0x2B, 0xC9 });
                WriteBytes(pointer2, new byte[] { 0x41, 0x2B, 0xC9, 0x3B, 0xC8, 0x0F });
            }
 
        }

        /*
        //Weapon
        int[] OFFSETS_Spread = new int[] { 0x8, 0x10C8, 0x20, 0x70 };
        int[] OFFSETS_Recoil = new int[] { 0x8, 0x10C8, 0x20, 0x2A4 };
        int[] OFFSETS_Fast_Shoot = new int[] { 0x8, 0x10C8, 0x20, 0x134 };
        int[] OFFSETS_BULLET_DMG = new int[] { 0x8,0x10C8,0x20,0xB0};
        int[] OFFSETS_Reload_Multiplier = new int[] { 0x8, 0x10C8, 0x20, 0x12C };
        int[] OFFSETS_Reload_Vehicle = new int[] { 0x8, 0x10C8, 0x20, 0x128 };
        //Ammo
        int[] OFFSETS_Max_Ammo = new int[] { 0x8, 0x10C8, 0x20, 0x60, 0x28 };
        int[] OFFSETS_Ammo_Type = new int[] { 0x8, 0x10C8, 0x20, 0x60, 0x54 };
        int[] OFFSETS_current_Ammo = new int[] { 0x8, 0x10C8, 0x20, 0x60, 0x8,0x0,0x18 };
        */

        //Weapon
        //Spread.
        public void GAME_Spread()
        {
            long pointer=GetPointerAddress(BaseAddress+WorldPTR,OFFSETS_Spread);
            WriteFloat(pointer,0);
        }
        //Fast Shoot.
        public void GAME_Fast_Shoot()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Fast_Shoot);
            WriteFloat(pointer, 0);
        }
        //Bullet_DMG.
        public void GAME_Bullet_DMG(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR,OFFSETS_BULLET_DMG);
            WriteFloat(pointer, value);
        }
        //Get Full Ammo.
        public void GAME_Get_Full_Ammo()
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_current_Ammo), GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Max_Ammo));
        }
        //Reload Multiplier.
        public void GAME_Reload_Multiplier()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Reload_Multiplier);
            WriteFloat(pointer, 20);
        }
        //Reload Vehicle.
        public void GAME_Reload_Vehicle()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Reload_Vehicle);
            WriteFloat(pointer, 20);
        }

        //Damage_Multiplier
        public void GAME_Damage_Multiplier(float value)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Damage_Multiplier);
            WriteFloat(pointer, value);
        }
        // Teleport to Waypoint.
        public void GAME_teleport_to_Waypoint()
        {
            for (var i = 0; i < 1000; i++)
            {
                long pointer = GetPointerAddress(BaseAddress + BlipPTR);
                long address = ReadPointer(pointer + (i * 8));
                if (address > 0)
                {
                    if (ReadInteger(address + 0x40, 4) == 8 && ReadInteger(address + 0x48, 4) == 84)
                    {
                        float waypointposX = ReadFloat(address + 0x10);
                        float waypointposY = ReadFloat(address + 0x14);
                        long worldptr = GetPointerAddress(BaseAddress + WorldPTR);
                        long player = ReadPointer(ReadPointer(worldptr) + 8);
                        byte[] vehicle_or_not = ReadBytes(player + 0x146B, 1);
                        if (vehicle_or_not[0] == 0)
                        {
                            player = ReadPointer(player + 0xD28);
                        }
                        long vehicle = ReadPointer(player + 0x30);
                        WriteFloat(vehicle + 0x50, waypointposX);
                        WriteFloat(vehicle + 0x54, waypointposY);
                        WriteFloat(vehicle + 0x58, -210);
                        WriteFloat(player + 0x90, waypointposX);
                        WriteFloat(player + 0x94, waypointposY);
                        WriteFloat(player + 0x98, -210);
                    }
                }
            }
        }

        public bool IsGameRunning()
        {
            Process[] process = Process.GetProcessesByName(ExeName);
            if (process.Length > 0)
            {
                string process_path = process[0].MainModule.FileName;
                FileInfo FileInfo = new FileInfo(process_path);
                if (FileInfo.Length == 60218776)
                {
                    GAME_VERSION = GAME_VERSION_DETECTED.SOCIALCLUB;
                }
                else
                {
                    GAME_VERSION = GAME_VERSION_DETECTED.STEAM;
                }

                if (GAME_VERSION == GAME_VERSION_DETECTED.SOCIALCLUB)
                {
                    WorldPTR = WorldPTR_SOCIALCLUB;
                    AmmoPTR = AmmoPTR_SOCIALCLUB;
                    ClipPTR = ClipPTR_SOCIALCLUB;
                    BlipPTR = BlipPTR_SOCIALCLUB;
                    PointerAddressA = PointerAddressA_SOCIALCLUB;
                }
                else
                {
                    WorldPTR = WorldPTR_STEAM;
                    AmmoPTR = AmmoPTR_STEAM;
                    ClipPTR = ClipPTR_STEAM;
                    BlipPTR = BlipPTR_STEAM;
                    PointerAddressA = PointerAddressA_STEAM;
                }
                return true;
            }
            else
            {
                MessageBox.Show("错误 : 你需要先启动 " + ExeName, "错误!",MessageBoxButton.OK,MessageBoxImage.Warning);
                return false;
            }
        }

        internal void Super_Jump()
        {
            WriteFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Fram_Eflags), 2.295887404E-41f);//2.295887404E-41
        }
        internal void Explosive_Ammo()
        {
            WriteEA(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Fram_Eflags));//2.869859255E-42
        }
        internal void Fire_Ammo ()
        {
            WriteFA(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Fram_Eflags));//5.73971851E-42
        }

        internal void Explosive_Melee()
        {
            WriteEM(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Fram_Eflags));//1.147943702E-41
        }
    }
}
