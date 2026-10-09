using System;
namespace Dnd
{
    public class DndCharacter
    {
        // приватні поля
        private string name = "";  // = "" не було null
        private CharacterClass charClass;
        private int level;
        private int health;

        // автовластивість за замовчуванням
        public string Faction { get; set; } = "Guild";
        // властивість
        public int Experience { get; private set; } = 0;

        // Властивості
        // перевірка імені
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                //  ім'я не порожне
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ім'я персонажа не може бути порожнім");
                }
                name = value;
            }
        }

        // перевірка класу
        public CharacterClass Class
        {
            get
            {
                return charClass;
            }
            set
            {
                // значення в enum
                if (!Enum.IsDefined(typeof(CharacterClass), value))
                {
                    throw new ArgumentException("Обрано неіснуючий клас");
                }
                charClass = value;
            }
        }

        // рівні
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                // від 1 до 20
                if (value < 1 || value > 20)
                {
                    throw new ArgumentException("Рівень повинен бути від 1 до 20");
                }
                level = value;
            }
        }

        // здоров'я
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                // здоров'я не може бути від'ємним
                if (value < 0)
                {
                    throw new ArgumentException("Здоров'я не може бути менше 0");
                }
                health = value;
            }
        }

        // обчислювальна властивість
        public string FullTitle
        {
            get
            {
                return $"{Name} ({Class}) - рівень {Level}";
            }
        }

        // Конструктор без параметрів
        public DndCharacter()
            : this( "Безіменний герой", CharacterClass.Warrior, 1, 100)
        {
        }


        // Перевантажений конструктор
        public DndCharacter(string name)
            : this( name, CharacterClass.Warrior, 1, 100)
        {
        }

        public DndCharacter(string name, CharacterClass charClass)
            : this(name, charClass, 1, 100)
        {
        }

        // Повний конструктор
        public DndCharacter(string name, CharacterClass charClass, int level, int health)
        {
            // пер властивості
            Name = name;
            Class = charClass;
            Level = level;
            Health = health;
        }

        // приватний метод
        private void LogAction(string message)
        {
            Console.WriteLine($"[LOG]: {message}");
        }

        // перевантаження методу
        public void GainExperience(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Experience += amount;

            LogAction( $"Персонаж {Name} отримав {amount} досвіду, " +  $"всього досвіду {Experience}");
        }

        public void GainExperience(int amount, string reason)
        {
            if (amount <= 0)
            {
                return;
            }
            Experience += amount;
            LogAction( $"Персонаж {Name} отримав {amount} досвіду " + $"за {reason}, всього досвіду {Experience}");
        }

        // перевантаження методу отримує шкоди
        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return;
            }
            // здоров'я0
            Health = Math.Max( 0, Health - damage);
            LogAction( $"Персонаж {Name} отримав {damage} шкоди, " + $"залишилось здоров'я {Health}");
        }

        public void TakeDamage(int damage, string damageType)
        {
            if (damage < 0)
            {
                return;
            }
            Health = Math.Max(0, Health - damage);

            LogAction($"Персонаж {Name} отримав {damage} шкоди " + $"типу {damageType}, " + $"залишилось здоров'я {Health}");
        }

        // метод текст
        public override string ToString()
        {
            return
                $"[Персонаж] {FullTitle} | " +
                $"Фракція: {Faction} | " +
                $"Здоров'я: {Health} | " +
                $"Досвід: {Experience}";
        }
    }
}