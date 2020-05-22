using System;
using System.Collections.Generic;
using System.IO;
using TextBaseGame.Entity;

namespace TextBaseGame.Manager
{
    class SaveManager : Singleton<SaveManager>
    {
        public readonly string FilePath = @"C:\tmp\Save\Save.txt";

        public void Save(Character character, int index)
        {

            string[] allLines = File.ReadAllLines(FilePath);

            string saveText = String.Empty;

            string savePattern =
                          $"character : {index}"
                        + "\n{\n"
                        + $"     name  : {character.Name}\n"
                        + $"     class : {character.CharacterClass.Name}\n"
                        + $"     level : {character.CharacterClass.ClassStats.Level}\n"
                        + "      stats:\n"
                        + $"             health : {character.CharacterClass.ClassStats.Health}\n"
                        + $"             defense: {character.CharacterClass.ClassStats.Defense}\n"
                        + $"             damage : {character.CharacterClass.ClassStats.Damage}\n"
                        + $"             mana   : {character.CharacterClass.ClassStats.Mana}\n"
                        + "}\n\n";

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains($"character : {index}"))
                {
                    Console.WriteLine("Character Save Founded !!!\n");
                    while (!line.Contains("}"))
                    {
                        line = allLines[i++];
                        saveText += line + '\n';

                        if (i >= allLines.Length - 1)
                            break;
                    }

                    string replaceText = File.ReadAllText(FilePath);
                    replaceText = replaceText.Replace(saveText, savePattern);
                    File.WriteAllText(FilePath, replaceText);

                    Console.WriteLine(saveText);
                    return;
                }
            }


            Console.WriteLine("Creating a new Save for character : " + index);
            File.AppendAllText(FilePath, savePattern);

        }

        public void Load(Character character, int index)
        {

            // NEW LOAD

            string[] allLines = File.ReadAllLines(FilePath);
            List<string> loadText = new List<string>();

            //string loadText = String.Empty;

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains($"character : {index}"))
                {
                    Console.WriteLine("Character Save Founded !!!\n");
                    while (!line.Contains("}"))
                    {
                        line = allLines[i++];
                        loadText.Add(line);

                        if (i >= allLines.Length - 1)
                            break;
                    }

                    foreach (var data in loadText)
                    {
                        TrySetAttribute("name", ref character.Name, data);
                        TrySetAttribute("class", ref character.CharacterClass.Name, data);
                        TrySetValue("character", ref character.Index, data);
                        TrySetValue("level", ref character.CharacterClass.ClassStats.Level, data);
                        TrySetValue("health", ref character.CharacterClass.ClassStats.Health, data);
                        TrySetValue("defense", ref character.CharacterClass.ClassStats.Defense, data);
                        TrySetValue("damage", ref character.CharacterClass.ClassStats.Damage, data);
                        TrySetValue("mana", ref character.CharacterClass.ClassStats.Mana, data);
                    }
                    return;

                }

            }
            Console.WriteLine("Can't find a valid save for the index : " + index);
        }

        bool TrySetAttribute(string attributeName, ref string attribute, string data)
        {
            if (data.Contains(attributeName))
            {
                attribute = GetAttribute(data);
                return true;
            }
            return false;
        }

        bool TrySetValue(string valueName, ref float value, string data)
        {
            if (data.Contains(valueName))
            {
                value = GetValue(data);
                return true;
            }
            return false;
        }

        float GetValue(string data)
        {
            string valueStr = string.Empty;
            float value = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != ':') continue;

                for (int j = i + 1; j < data.Length; j++)
                {
                    if (data[j] == ' ') continue;
                    valueStr += data[j];
                }

                float.TryParse(valueStr, out value);

            }
            return value;
        }

        string GetAttribute(string data)
        {
            string attributeStr = String.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != ':') continue;

                for (int j = i + 1; j < data.Length; j++)
                {
                    if (data[j] == ' ') continue;
                    attributeStr += data[j];
                }
                break;
            }

            return attributeStr;
        }
    }
}

