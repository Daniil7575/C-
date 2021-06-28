using System;
using System.Collections.Generic;

namespace a_rmy
{
    abstract class Unit
    {
        public string un_name;
        public int un_health;
        public int un_damage;
        public int un_protec;

        public abstract void info();

        public abstract void attack();

        public Unit(string un_name, int un_health, int un_damage, int un_protec)  
        {
            this.un_name = un_name;
            this.un_health = un_health;
            this.un_damage = un_damage;
            this.un_protec = un_protec;
        }

        public Unit()
        {
        }
    }

    class Ammo
    {
        Random rnd = new Random();
        public string am_name;
        public string am_type;
        public int am_damage;
        public int am_count;

        public Ammo(string am_name, string am_type, int am_damage, int am_count)
        {
            this.am_name = am_name;
            this.am_type = am_type;
            this.am_damage = am_damage;
            this.am_count = am_count;
        }
        public Ammo()
        {
            string[] ammo_names = new string[] { "Astral Sonic Blast", "Magma whisperer", "Iron stick", "Steel hand", "Wooden ward", "Plague", "Magma Invisible Assault" };
            string[] ammo_types = new string[] { "Bolt", "Bullet", "Big arrow", "Rock", "Cow" };

            am_name = ammo_names[rnd.Next(0, 6)];
            am_type = ammo_types[rnd.Next(0, 5)];
            am_damage = rnd.Next(50, 97);
            am_count = rnd.Next(5, 20);
        }
    }

    class Armor
    {
        Random rnd = new Random();
        public string ar_name;
        public int ar_protec;

        public Armor(string ar_name, int ar_protec)
        {
            this.ar_name = ar_name;
            this.ar_protec = ar_protec;
        }

        public Armor()
        {
            string[] ar_names = new string[] {"Glass armor","Steel armor", "Iron armor", "Bronze armor", "Leather armor", "Hide armor", "Mithril armor"};
            ar_name = ar_names[rnd.Next(0, 7)];
            ar_protec = rnd.Next(10, 60);
        }
    }
    
    class Mech : Unit
    {
        public Ammo ammo;
        Random rnd = new Random();

        public Mech(string un_name, int un_health, Ammo ammo, int un_protec) : base(un_name, un_health, ammo.am_damage, un_protec)
        {
            this.ammo = ammo;
        }

        public Mech() : base()
        {
            string[] me_names = new string[] {"Cannon", "Ballista", "Catapult", "Trembling" };
            un_name = me_names[rnd.Next(0, 4)];
            un_health = rnd.Next(700, 1000);
            ammo = new Ammo();
            un_damage = ammo.am_damage;
            un_protec = rnd.Next(60,120);
        }


        public override void info()
        {
            Console.WriteLine($"я {un_name}\n  мои ОЖ:{un_health}\n  моя защита:{un_protec}\n  мой урон :{un_damage}\n  количество снарядов :{ammo.am_count}\n  тип снаряда :{ammo.am_type}\n  название снаряда :{ammo.am_name}");
        }

        public override void attack()
        {
            if (ammo.am_count > 0)
            {
                ammo.am_count--;
                Console.WriteLine($"Юнит {un_name} нанёс урон в размере {un_damage} единиц . У него осталось {ammo.am_count} снарядов\n\n");

            }
            else
            {
                Console.WriteLine($"Нехватка снарядов у юнита {un_name}");
            }
        }
    }

    class Soldier:Unit
    {
        Random rnd = new Random();
        public Armor armor;

        public Soldier(string un_name, int un_health, int un_damage, Armor armor) : base(un_name, un_health, un_damage, armor.ar_protec)
        {
            this.armor = armor;
        }

        public Soldier():base()
        {
            string[] so_names = new string[] {"Rogue", "Druid", "Bard", "Knight", "Berserker", "Fighter", "Drunk master", "Paladin"};
            un_name = so_names[rnd.Next(0, 7)];
            un_health = rnd.Next(100, 200);
            armor = new Armor();
            un_protec = armor.ar_protec;
            un_damage = rnd.Next(10, 20);
        }

        public override void info()
        {
            Console.WriteLine($"я {un_name}\n  мои ОЖ:{un_health}\n  моя броня :{armor.ar_name}\n  моя защита:{un_protec}\n  мой урон :{un_damage}");
        }

        public override void attack()
        {
            Console.WriteLine($"Юнит { un_name} нанёс урон в размере { un_damage} единиц ");
        }
    }

    class Animal:Unit
    {
        Random rnd = new Random();
        public int an_speed;

        public Animal(string un_name, int un_health, int un_damage, int un_protec, int an_speed) : base(un_name, un_health, un_damage, un_protec)
        {
            this.an_speed = an_speed;
        }

        public Animal() : base()
        {
            string[] an_names = new string[] {"Lion", "Big dog", "Hog", "Camel", "Leopard", "Elephant", "Rhino", "Bear"};
            un_name = an_names[rnd.Next(0, 3)];
            un_health = rnd.Next(150, 280);
            un_damage = rnd.Next(12, 19);
            un_protec = rnd.Next(0, 5);
            an_speed = rnd.Next(1, 20);
        }

        public override void info()
        {
            Console.WriteLine($"я {un_name}\n  мои ОЖ:{un_health}\n  моя защита:{un_protec}\n  мой урон :{un_damage}\n  моя скорость :{an_speed}");
        }

        public override void attack()
        {
            Console.WriteLine($"Юнит {un_name} нанёс урон в размере { un_damage} единиц ");
        }
    }

    class Rider:Unit
    {
        Soldier soldier;
        Animal animal;

        public Rider(Soldier soldier , Animal animal) : base(soldier.un_name+ " на "+ animal.un_name, soldier.un_health + animal.un_health ,soldier.un_damage + animal.un_damage, soldier.un_protec + animal.un_protec ) 
        {
            this.soldier = soldier;
            this.animal = animal;
        }

       

        public override void info()
        {
            Console.WriteLine($"я {un_name}\n  мои ОЖ:{un_health}\n  моя защита:{un_protec}\n  мoй урон :{un_damage}");
        }

        public override void attack()
        {
            Console.WriteLine($"Юнит {un_name} нанёс урон в размере {un_damage} единиц");
        }
    }

    class Program
    {
        public static List<Unit> units = new List<Unit>();
        public static int totaldamage = 0;
        public static int totalhp = 0;
        public static int totalprotec = 0;
        public static int armycount = 0;
        public static bool armycheck = false;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Здравствуйте! Что будем делать?");
            while (true)
            {
                Console.WriteLine("1)Содать аримю из билета (17 билет )\n2)Вывести статистику об армии(кол-во, урон, ож, защита)\n3)Нанести урон юнитам\n4)Приказать одному юниту нанести удар\n5)Создать рандомного юнита\n6)Вывести информацию о каждом юните");
                for (int i = 0 ; i < units.Count; i++)
                {
                    totaldamage += units[i].un_damage;
                    totalhp += units[i].un_health;
                    totalprotec += units[i].un_protec;
                    armycount ++;
                }

                string a = Console.ReadLine();

                if (a == "1")
                {
                    Console.Clear();
                    units.Add(new Mech("Catapult", 874, new Ammo("Magma Invisible Assault", "Bullet", 26, 20), 60));
                    units.Add(new Soldier("Paladin", 204, 16, new Armor("Leather armor", 20)));
                    units.Add(new Animal("Elephant", 100, 50, 5, 9));
                    units.Add(new Animal("Rhino", 72, 30, 5, 13));
                    units.Add(new Animal("Bear", 53, 15, 3, 12));
                    units.Add(new Animal("Camel", 11, 0, 0, 14));
                    units.Add(new Rider(new Soldier("Paladin", 200, 20, new Armor()), new Animal("Elephant", 250, 7, 5, 10)));
                    units.Add(new Rider(new Soldier("Paladin", 200, 20, new Armor()), new Animal("Bear", 200, 12, 2, 10)));
                    armycheck = true;
                    Console.WriteLine("Армия создана");

                }

                else if (a == "2")
                {
                    Console.Clear();
                    if (armycheck) 
                    { 
                        if (units.Count == 0)
                        {
                            Console.WriteLine("У вас погибли все юниты!");
                        }
                        else
                        {
                            Console.WriteLine($"Ож армии :{totalhp}\nУрон армии :{totaldamage}\nЗащита армии :{totalprotec}\nКоличество юинтов :{armycount}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы никого не создавали");
                    }
                }

                else if (a == "3")
                {
                    Console.Clear();
                    if (armycheck)
                    {
                        if (units.Count > 0)
                        {
                            Console.WriteLine("Какой урон хотите нанести?");
                            int damage = Convert.ToInt32(Console.ReadLine());
                            int dap = damage - totalprotec;
                            if (damage - totalprotec <= 0)
                            {
                                Console.WriteLine("Защита армии больше нанесённого урона.");
                            }
                            else
                            {
                                for (int i = 0; i < units.Count; i += 0)
                                {

                                    Console.WriteLine($"Атакуется юнит {units[i].un_name}");

                                    if (dap - units[i].un_health >= 0)
                                    {
                                        dap -= units[i].un_health;
                                        Console.WriteLine($"Юнит {units[i].un_name} уничтожен");
                                        units.Remove(units[i]);
                                    }

                                    else
                                    {
                                        units[i].un_health -= dap;
                                        Console.WriteLine($"Юнит {units[i].un_name} выжил с {units[i].un_health} ож");
                                        break;
                                    }
                                }
                                if (units.Count == 0)
                                {
                                    Console.WriteLine("Вся армия погибла");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некого бить");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Увы, но вы никого не создавали");
                    }
                    
                    
                }
                else if(a == "4")
                {
                    Console.Clear();
                    if (armycheck)
                    {
                        if(units.Count > 0)
                        {
                            Console.WriteLine("Кто будет атаковать?\nУ вас есть :");
                            for(int i = 0; i < units.Count; i++)
                            {
                                Console.Write($"{i})");
                                units[i].info();
                                Console.WriteLine("\n\n");
                            }

                            units[Convert.ToInt32(Console.ReadLine())].attack();
                        }

                        else
                        {
                            Console.WriteLine("Все мертвы");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Создайте кого-нибудь");
                    }
                }

                else if (a == "5")
                {
                    Console.Clear();
                    Console.WriteLine("Кого хотите создать?");
                    Console.WriteLine("1)Механизм\n2)Солдат\n3)Животное\n4)Всадник");
                    string choose = Console.ReadLine();
                    Console.WriteLine("Сколько создать?");
                    int count = Convert.ToInt32(Console.ReadLine());
 
                    if (choose == "1")
                    {
                        for (int i = 0; i < count; i++) 
                        {
                            units.Add(new Mech());
                        }
                        armycheck = true;
                        Console.Clear();
                        Console.WriteLine("Готово!");
                    }

                    else if(choose == "2")
                    {
                        for(int i = 0; i < count; i++)
                        {
                            units.Add(new Soldier());
                        }
                        armycheck = true;
                        Console.Clear();
                        Console.WriteLine("Готово!");
                    }

                    else if(choose == "3")
                    {
                        for (int i = 0; i < count; i++)
                        {
                            units.Add(new Animal());
                        }
                        armycheck = true;
                        Console.Clear();
                        Console.WriteLine("Готово!");

                    }

                    else if(choose == "4")
                    {
                        for (int i = 0; i < count; i++)
                        {
                            units.Add(new Rider(new Soldier(), new Animal()));
                        }
                        armycheck = true;
                        Console.Clear();
                        Console.WriteLine("Готово!");
                    }

                    else
                    {
                        Console.WriteLine("Что-то пошло не так, попробуйте ещё раз");
                    }
                }
                else if(a == "6")
                {
                    Console.Clear();

                    if (armycheck) 
                    {
                        if(units.Count > 0)
                        {
                            for (int i = 0; i < units.Count; i++)
                            {
                                units[i].info();
                                Console.WriteLine("\n\n");
                            }
                        }

                        else
                        {
                            Console.WriteLine("Все мертвы");
                        }
                       
                    }

                    else
                    {
                        Console.WriteLine("Создайте хоть кого-нибудь. Пожалуйста");
                    }       
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Что-то пошло не так, попробуйте ещё раз");
                }

                totaldamage = 0;
                totalhp = 0;
                totalprotec = 0;
                armycount = 0;
            }
        }
    }
}
