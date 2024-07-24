using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

internal class Program
{

    private static void Main(string[] args)
    {
        // main menu variables
        bool gameStarted = false;
        bool inMainMenu = true;
        string menuSelection;

        while (inMainMenu == true)
        {
            Console.WriteLine("Hi, welcome to ConsoleRPG.");
            Console.WriteLine("Write START to start a new game or EXIT to leave the application.");
            menuSelection = Console.ReadLine();
            menuSelection = menuSelection.ToUpper();

            switch (menuSelection)
            {
                case "START":
                    gameStarted = true;
                    inMainMenu = false;
                    Console.Clear();
                    break;

                case "EXIT":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please, type START or EXIT.");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    break;
            }
        }

        // variables for menus during the game
        bool characterCreated = false;
        bool inNavigationWindow = true;
        string whereToGo;

        // player object
        PlayerCharacter playerChar = new PlayerCharacter(null, 1, 100, 10, 0, 0, "Wooden sword", "Cloth armor");

        while (gameStarted == true)
        {

            // character creation
            while (characterCreated == false)
            {
                Console.WriteLine("Welcome to the character creation, please type your name:");
                playerChar.charName = Console.ReadLine();

                if (playerChar.charName == "")
                {
                 Console.WriteLine("Please, write your name.");
                }
                else
                {
                    characterCreated = true;
                    gameStarted = false;
                    inNavigationWindow = true;
                    Console.Clear();
                }

            }


            // game menu
            while (inNavigationWindow == true)
            {
                Console.WriteLine($"Hi {playerChar.charName} to view your stats type STATS, to visit the shop type SHOP, to search for a worthy opponent type BATTLE. If you want to leave the game type EXIT");
                whereToGo = Console.ReadLine();
                whereToGo = whereToGo.ToUpper();

                // check player equipment to match with a proper enemy
                if (playerChar.charAttack == 20)
                {
                    playerChar.charLevel = 2;
                }
                else if (playerChar.charAttack == 30)
                {
                    playerChar.charLevel = 3;
                }
                else if (playerChar.charAttack == 40)
                {
                    playerChar.charLevel = 4;
                }

                switch (whereToGo)
                {
                    case "STATS":
                        Console.Clear();
                        Console.WriteLine("STATS:");
                        Console.WriteLine("Level: " + playerChar.charLevel);
                        Console.WriteLine("Health: " + playerChar.charHealth);
                        Console.WriteLine("Attack: " + playerChar.charAttack);
                        Console.WriteLine("Defense: " + playerChar.charDefense);
                        Console.WriteLine("Gold: " + playerChar.charGold);
                        Console.WriteLine("");
                        Console.WriteLine("Equiped weapon: " + playerChar.equipedWeapon);
                        Console.WriteLine("Equiped armor: " + playerChar.equipedArmor);
                        Console.WriteLine("");
                        Console.WriteLine("");

                        break;

                    case "SHOP":
                        Console.Clear();
                        Shop(playerChar);
                        break;

                    case "BATTLE":
                        Console.Clear();
                        Console.WriteLine("You go into the forest looking for a worthy oponent.");
                        Battle(playerChar);

                        if (playerChar.charHealth <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("You have died.");
                            Console.WriteLine("Type whatever you want to exit the app.");
                            string typeGameOver = Console.ReadLine();

                            switch (typeGameOver)
                            {
                                default:
                                Environment.Exit(0);
                                break;
                            }
                        }

                        break;

                    case "EXIT":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        break;
                }
            }
        } // game loop end
    }

    public static void Shop(PlayerCharacter player)
    {
        // weapon list                  name, atk, defense, gold cost, item id             
        Item[] weaponList = { new Item("Uncommon sword", 10, 0, 100, 1), new Item("Sharp sword", 20, 0, 200, 2),
                              new Item("Shiny sword", 30, 0, 300, 3) };
        // armor list
        Item[] armorList = { new Item("Leather armor", 0, 10, 100, 1), new Item("Mail armor", 0, 20, 200, 2),
                             new Item("Full plater armor", 0, 30, 300, 3) };

        // shop strings and choices
        string shopType;
        string numberSelected;

        Console.Clear();
        Console.WriteLine("Hi, welcome to the shop. Please write which type of equipment you would like to buy.");
        Console.WriteLine("Write WEAPON or ARMOR. To leave type any other word or leave the space empty.");
        shopType = Console.ReadLine();
        shopType = shopType.ToUpper();

        // variable for tryparse out
        int itemNumber;

        switch (shopType)
        {
            case "WEAPON":
                Console.WriteLine("Here is a list of the available weapons, write the number of weapon you want to buy.");
                Console.WriteLine("Type the number of item you want to purchase.");
                foreach (Item weapon in weaponList)
                {
                    Console.WriteLine($"{weapon.itemNumber}. {weapon.itemName} - Attack: {weapon.weaponAttack} - Price: {weapon.itemPrice}.");
                }
                numberSelected = Console.ReadLine();
                Int32.TryParse(numberSelected, out itemNumber);

                switch (itemNumber)
                {
                    case 1:
                        if (player.charGold >= weaponList[0].itemPrice)
                        {
                            Console.WriteLine($"You selected {weaponList[0].itemName}. Total gold cost: {weaponList[0].itemPrice}.");
                            player.charAttack = 10;
                            player.charAttack = player.charAttack + weaponList[0].weaponAttack;
                            player.charGold = player.charGold - weaponList[0].itemPrice;
                            player.equipedWeapon = weaponList[0].itemName;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough gold to purchase this weapon.");
                        }
                        break;
                    case 2:
                        if (player.charGold >= weaponList[1].itemPrice)
                        {
                            Console.WriteLine($"You selected {weaponList[1].itemName}. Total gold cost: {weaponList[1].itemPrice}.");
                            player.charAttack = 10;
                            player.charAttack = player.charAttack + weaponList[1].weaponAttack;
                            player.charGold = player.charGold - weaponList[1].itemPrice;
                            player.equipedWeapon = weaponList[1].itemName;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough gold to purchase this weapon.");
                        }
                        break;
                    case 3:
                        if (player.charGold >= weaponList[2].itemPrice)
                        {
                            Console.WriteLine($"You selected {weaponList[2].itemName}. Total gold cost: {weaponList[2].itemPrice}.");
                            player.charAttack = 10;
                            player.charAttack = player.charAttack + weaponList[2].weaponAttack;
                            player.charGold = player.charGold - weaponList[2].itemPrice;
                            player.equipedWeapon = weaponList[2].itemName;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough gold to purchase this weapon.");
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("No valid option selected, returning to navegation menu.");
                        Console.WriteLine("");
                        break;
                }
                break;

            case "ARMOR":
                Console.WriteLine("Here is a list of the available armors, write the number of armor you want to buy.");
                Console.WriteLine("Type the number of item you want to purchase.");
                foreach (Item armor in armorList)
                {
                    Console.WriteLine($"{armor.itemNumber}. {armor.itemName} - Attack: {armor.armorDefense} - Price: {armor.itemPrice}.");
                }
                numberSelected = Console.ReadLine();
                Int32.TryParse(numberSelected, out itemNumber);

                switch (itemNumber)
                {
                    case 1:
                        if (player.charGold >= armorList[0].itemPrice)
                        {
                            Console.WriteLine($"You selected {armorList[0].itemName}. Total gold cost: {armorList[0].itemPrice}.");
                            player.charDefense = 0;
                            player.charDefense = player.charDefense + armorList[0].armorDefense;
                            player.charGold = player.charGold - armorList[0].itemPrice;
                            player.equipedArmor = armorList[0].itemName;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough gold to purchase this armor.");
                        }
                        break;
                    case 2:
                        if (player.charGold >= armorList[1].itemPrice)
                        {
                            Console.WriteLine($"You selected {armorList[1].itemName}. Total gold cost: {armorList[1].itemPrice}.");
                            player.charDefense = 0;
                            player.charDefense = player.charDefense + armorList[1].armorDefense;
                            player.charGold = player.charGold - armorList[1].itemPrice;
                            player.equipedArmor = armorList[1].itemName;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough gold to purchase this armor.");
                        }
                        break;
                    case 3:
                        if (player.charGold >= armorList[2].itemPrice)
                        {
                            Console.WriteLine($"You selected {armorList[2].itemName}. Total gold cost: {armorList[2].itemPrice}.");
                            player.charDefense = 0;
                            player.charDefense = player.charDefense + armorList[2].armorDefense;
                            player.charGold = player.charGold - armorList[2].itemPrice;
                            player.equipedArmor = armorList[2].itemName;
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough gold to purchase this armor.");
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("No valid option selected, returning to navegation menu.");
                        Console.WriteLine("");
                        break;
                }
                break;

            default:
                Console.WriteLine("No valid option selected, returning to navegation menu.");
                break;
        }
    }

    static void Battle(PlayerCharacter player)
    {
        // enemy list
        // level 1                      name, health, attack, gold, tier, id number
        Enemy[] firstTier = { new Enemy("Goblin", 20, 10, 50, 1, 1), new Enemy("Angry Duck", 10, 2, 50, 1, 2) };
        // level 2
        Enemy[] secondTier = { new Enemy("Armored goblin", 50, 20, 100, 2, 1), new Enemy("Ogre", 70, 30, 200, 2, 2) };
        // level 3
        Enemy[] thirdTier = { new Enemy("Angry Dragon", 130, 40, 300, 3, 1), new Enemy("Stone golem", 100, 35, 250, 3, 2) };

        Enemy enemyInBattle = new Enemy("", 0, 0, 0, 0, 0);

        Random randomNumber = new Random();

        // enemy number selector
        int enemyNumber;

        // enemy stats
        string enemyName = string.Empty;
        int enemyHealth = 0;
        int enemyAttack = 0;
        int enemyGold = 0;

        // bool variable for fighting
        bool inBattle = false;

        // choosing the right enemy based on player level
        Console.Clear();
        switch (player.charLevel)
        {
            case 1:
                enemyNumber = randomNumber.Next(1, 3);
                Console.WriteLine("You have entered the forest and encounter an enemy.");
                foreach (Enemy enemy in firstTier)
                {
                    if (enemyNumber == enemy.id)
                    {
                        enemyName = enemy.name;
                        enemyHealth = enemy.health;
                        enemyAttack = enemy.attack;
                        enemyGold = enemy.gold;

                    }
                }

                Console.WriteLine($"You have found a {enemyName}.");
                Console.WriteLine("Battle is starting...");
                inBattle = true;
                break;
            case 2:
                enemyNumber = randomNumber.Next(1, 3);
                Console.WriteLine("You have entered the forest and encounter an enemy.");
                foreach (Enemy enemy in firstTier)
                {
                    if (enemyNumber == enemy.id)
                    {
                        enemyName = enemy.name;
                        enemyHealth = enemy.health;
                        enemyAttack = enemy.attack;
                        enemyGold = enemy.gold;

                    }
                }

                Console.WriteLine($"You have found a {enemyName}.");
                Console.WriteLine("Battle is starting...");
                inBattle = true;
                break;
            case 3:
                enemyNumber = randomNumber.Next(1, 3);
                Console.WriteLine("You have entered the forest and encounter an enemy.");
                foreach (Enemy enemy in secondTier)
                {
                    if (enemyNumber == enemy.id)
                    {
                        enemyName = enemy.name;
                        enemyHealth = enemy.health;
                        enemyAttack = enemy.attack;
                        enemyGold = enemy.gold;

                    }
                }

                Console.WriteLine($"You have found a {enemyName}.");
                Console.WriteLine("Battle is starting...");
                inBattle = true;
                break;
            case 4:
                enemyNumber = randomNumber.Next(1, 3);
                Console.WriteLine("You have entered the forest and encounter an enemy.");
                foreach (Enemy enemy in thirdTier)
                {
                    if (enemyNumber == enemy.id)
                    {
                        enemyName = enemy.name;
                        enemyHealth = enemy.health;
                        enemyAttack = enemy.attack;
                        enemyGold = enemy.gold;

                    }
                }

                Console.WriteLine($"You have found a {enemyName}.");
                Console.WriteLine("Battle is starting...");
                inBattle = true;
                break;
        }

        enemyInBattle.name = enemyName;
        enemyInBattle.health = enemyHealth;
        enemyInBattle.attack = enemyAttack;
        enemyInBattle.gold = enemyGold;

        while (enemyInBattle.health >= 1)
        {

            if (player.charHealth <= 0)
            {
                return;
            }

            // battle command
            Console.WriteLine("");
            Console.WriteLine($"Your current HP: {player.charHealth}.");
            Console.WriteLine($"Enemy current HP: {enemyInBattle.health}.");
            Console.WriteLine("");
            Console.WriteLine("What are you going to do? ATTACK or RUN.");
            string battleCommand;
            battleCommand = Console.ReadLine();
            battleCommand = battleCommand.ToUpper();

            switch (battleCommand)
            {
                case "ATTACK":
                    DamageCalculation(player, enemyInBattle);
                    break;

                case "RUN":
                    Console.Clear();
                    Console.WriteLine("You are fleeing from your opponent.");
                    Console.WriteLine("");
                    player.charHealth = 100;
                    return;
                default:
                    Console.WriteLine("Wrong command.");
                    break;

            }
        }

        Console.Clear();
        Console.WriteLine("You have won the battle!");
        Console.WriteLine($"Your prize: {enemyInBattle.gold} gold coins.");
        Console.WriteLine("");
        player.charGold = player.charGold + enemyInBattle.gold;
        player.charHealth = 100;
    }

    static void DamageCalculation(PlayerCharacter player, Enemy enemy)
    {
        int playerHealth = player.charHealth;
        int enemyHealth = enemy.health;
        int goldReward = enemy.gold;

        int enemyHit = enemy.attack - (player.charDefense / 2);
        int playerHit = player.charAttack;

        Console.WriteLine($"You attack the enemy dealing: {playerHit} damage points.");
        Console.WriteLine($"{enemy.name} attacks you dealing: {enemyHit} damage points.");

        player.charHealth = playerHealth - enemyHit;
        enemy.health = enemyHealth - playerHit;
    }
}
    // player character class
    class PlayerCharacter(string name, int level, int health, int attack, int defense, int gold, string weapon, string armor)
    {
        public string charName = name;
        public int charLevel = level;
        public int charHealth = health;
        public int charAttack = attack;
        public int charDefense = defense;
        public int charGold = gold;
        public string equipedWeapon = weapon;
        public string equipedArmor = armor;

    }

    // item class
    // weapon + armor
    class Item(string itemName, int weaponAttack, int armorDefense, int itemPrice, int itemNumber)
    {
        public string itemName = itemName;
        public int weaponAttack = weaponAttack;
        public int armorDefense = armorDefense;
        public int itemPrice = itemPrice;
        public int itemNumber = itemNumber;
    }

    //enemy class
    class Enemy(string enemyName, int enemyHealth, int enemyAttack, int goldDropped, int enemyTier, int enemyNumber)
    {
        public string name = enemyName;
        public int health = enemyHealth;
        public int attack = enemyAttack;
        public int gold = goldDropped;
        public int tier = enemyTier;
        public int id = enemyNumber;
    }
