using System;
using System.Collections.Generic;

public interface IKemampuan
{
    string Nama { get; }
    int Cooldown { get; }
    void Gunakan(Robot target);
    void KurangiCooldown();
}

public abstract class Robot
{
    public string Nama { get; set; }
    public int HP { get; set; }
    public int Energi { get; set; }
    public int Armor { get; set; }
    public int Serangan { get; set; }
    public IKemampuan Kemampuan { get; set; }

    protected Robot(string nama, int hp, int energi, int armor, int serangan, IKemampuan kemampuan)
    {
        Nama = nama;
        HP = hp;
        Energi = energi;
        Armor = armor;
        Serangan = serangan;
        Kemampuan = kemampuan;
    }

    public void Serang(Robot target)
    {
        int damage = Math.Max(0, Serangan - target.Armor);
        target.HP -= damage;
        Console.WriteLine($"{Nama} menyerang {target.Nama}, menyebabkan kerusakan sebesar {damage}!");
    }

    public abstract void GunakanKemampuan(Robot target);

    public void CetakInformasi()
    {
        Console.WriteLine($"Robot: {Nama}, HP: {HP}, Energi: {Energi}, Armor: {Armor}, Serangan: {Serangan}");
    }

    public void PemulihanEnergi(int jumlah)
    {
        Energi += jumlah;
        Console.WriteLine($"{Nama} memulihkan {jumlah} energi.");
    }
}

public class BosRobot : Robot
{
    public BosRobot(string nama, int hp, int energi, int armor, int serangan, IKemampuan kemampuan)
        : base(nama, hp, energi, armor, serangan, kemampuan) { }

    public void SerangPemain(Robot target)
    {
        int damage = Math.Max(0, Serangan - target.Armor);
        target.HP -= damage; // Serangan mengurangi HP
        Console.WriteLine($"{Nama} menyerang {target.Nama}, menyebabkan kerusakan sebesar {damage}!");
    }

    public override void GunakanKemampuan(Robot target)
    {
        Console.WriteLine($"Bos {Nama} menggunakan kemampuan {Kemampuan.Nama} pada {target.Nama}!");
        Kemampuan.Gunakan(target);
    }

    public void Mati()
    {
        Console.WriteLine($"{Nama} telah mati!");
    }
}

public class Pemulihan : IKemampuan
{
    public string Nama => "Perbaikan";
    public int Cooldown { get; private set; }

    public Pemulihan()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        if (Cooldown == 0)
        {
            target.HP += 30;
            Console.WriteLine($"{target.Nama} menggunakan kemampuan {Nama}, HP pulih sebesar 30 poin!");
            Cooldown = 3;
        }
        else
        {
            Console.WriteLine($"Kemampuan {Nama} masih dalam cooldown.");
        }
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0) Cooldown--;
    }
}

public class Fireball : IKemampuan
{
    public string Nama => "Serangan Plasma";
    public int Cooldown { get; private set; }

    public Fireball()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        if (Cooldown == 0)
        {
            int damage = 50;
            target.HP -= damage;
            Console.WriteLine($"{target.Nama} diserang dengan {Nama}, menyebabkan kerusakan sebesar {damage}!");
            Cooldown = 4;
        }
        else
        {
            Console.WriteLine($"Kemampuan {Nama} masih dalam cooldown.");
        }
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0) Cooldown--;
    }
}

public class Deadlystorm : IKemampuan
{
    public string Nama => "Serangan Listrik";
    public int Cooldown { get; private set; }

    public Deadlystorm()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        if (Cooldown == 0)
        {
            int damage = 40;
            target.HP -= damage;
            Console.WriteLine($"{target.Nama} diserang dengan {Nama}, menyebabkan kerusakan sebesar {damage}!");
            Cooldown = 2;
        }
        else
        {
            Console.WriteLine($"Kemampuan {Nama} masih dalam cooldown.");
        }
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0) Cooldown--;
    }
}

public class LedakanNuklir : IKemampuan
{
    public string Nama => "Ledakan Nuklir";
    public int Cooldown { get; private set; }

    public LedakanNuklir()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        if (Cooldown == 0)
        {
            int damage = 100;
            target.HP -= damage;
            Console.WriteLine($"{target.Nama} terkena {Nama}, menyebabkan kerusakan besar sebesar {damage}!");
            Cooldown = 5;
        }
        else
        {
            Console.WriteLine($"Kemampuan {Nama} masih dalam cooldown.");
        }
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0) Cooldown--;
    }
}

public class SeranganPlasma : IKemampuan
{
    public string Nama => "Serangan Plasma";
    public int Cooldown { get; private set; }

    public SeranganPlasma()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        if (Cooldown == 0)
        {
            int damage = 60;
            target.HP -= damage;
            Console.WriteLine($"{target.Nama} diserang dengan {Nama}, menyebabkan kerusakan sebesar {damage}!");
            Cooldown = 3;
        }
        else
        {
            Console.WriteLine($"Kemampuan {Nama} masih dalam cooldown.");
        }
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0) Cooldown--;
    }
}

public class RobotPenyerang : Robot
{
    public RobotPenyerang(string nama, int hp, int energi, int armor, int serangan, IKemampuan kemampuan)
        : base(nama, hp, energi, armor, serangan, kemampuan) { }

    public override void GunakanKemampuan(Robot target)
    {
        Console.WriteLine($"{Nama} menggunakan kemampuan {Kemampuan.Nama} pada {target.Nama}!");
        Kemampuan.Gunakan(target);
    }
}

public class Simulator
{
    private List<Robot> robots = new List<Robot>();
    private BosRobot bos;
    private Random rand = new Random();

    public Simulator()
    {

        robots.Add(new RobotPenyerang("Robot Alpha", 120, 100, 50, 50, new Deadlystorm()));
        robots.Add(new RobotPenyerang("Robot Ambatron", 100, 80, 40, 75, new Pemulihan()));
        robots.Add(new RobotPenyerang("Robot Cak Rusdi", 90, 80, 30, 85, new SeranganPlasma()));
        robots.Add(new RobotPenyerang("Robot Mas Narji", 80, 70, 15, 95, new Fireball()));

        bos = new BosRobot("Bos Diddy", 200, 150, 40, 70, new LedakanNuklir());
    }

    public void PilihRobotDanMainkan()
    {
        Console.WriteLine("Pilih robot Anda:");
        for (int i = 0; i < robots.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {robots[i].Nama} (HP: {robots[i].HP}, Energi: {robots[i].Energi}, Armor: {robots[i].Armor}, Serangan: {robots[i].Serangan})");
        }

        int pilihan = int.Parse(Console.ReadLine()) - 1;
        Robot robotPemain = robots[pilihan];
        robots.RemoveAt(pilihan);

        MainkanPermainan(robotPemain);
    }

    public void MainkanPermainan(Robot robotPemain)
    {
        bool permainanSelesai = false;
        int giliran = 1;

        while (!permainanSelesai)
        {
            Console.WriteLine($"\n--- Giliran {giliran} ---");
            Console.WriteLine($"\nGiliran {robotPemain.Nama}:");
            robotPemain.CetakInformasi();
            bos.CetakInformasi();

            Console.WriteLine("Pilih aksi:");
            Console.WriteLine("1. Serang Bos Robot");
            Console.WriteLine("2. Gunakan Kemampuan");
            Console.WriteLine("3. Pulihkan Energi");
            int aksi = int.Parse(Console.ReadLine());

            switch (aksi)
            {
                case 1:
                    robotPemain.Serang(bos);
                    break;
                case 2:
                    robotPemain.GunakanKemampuan(bos);
                    break;
                case 3:
                    Console.Write("Masukkan jumlah energi yang ingin dipulihkan: ");
                    int jumlahPemulihan = int.Parse(Console.ReadLine());
                    robotPemain.PemulihanEnergi(jumlahPemulihan);
                    break;
                default:
                    Console.WriteLine("Aksi tidak valid. Silakan pilih lagi.");
                    continue;
            }

            if (bos.HP > 0)
            {
                Console.WriteLine($"\nGiliran {bos.Nama}:");

                if (rand.Next(2) == 0)
                {
                    bos.SerangPemain(robotPemain);
                }
                else
                {
                    bos.GunakanKemampuan(robotPemain);
                }
            }

            robotPemain.Kemampuan.KurangiCooldown();
            bos.Kemampuan.KurangiCooldown();

            if (bos.HP <= 0)
            {
                Console.WriteLine($"{bos.Nama} telah dikalahkan! Anda menang!");
                permainanSelesai = true;
            }
            else if (robotPemain.HP <= 0)
            {
                Console.WriteLine($"{robotPemain.Nama} telah mati! Anda kalah.");
                permainanSelesai = true;
            }

            giliran++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Simulator simulator = new Simulator();
        simulator.PilihRobotDanMainkan();
    }
}
