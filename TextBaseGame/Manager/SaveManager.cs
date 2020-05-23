using System;
using System.Collections.Generic;
using System.IO;
using TextBaseGame.Entity;
using TextBaseGame.Utilities;

namespace TextBaseGame.Manager
{
    class SaveManager : Singleton<SaveManager>
    {
        public readonly string FilePath = @"C:\tmp\Save\save.sav";

        public void Save(Character character, int index)
        {
            if(index == 0)
                index++;

            string savePattern =
                          $"character : {index}"
                        + "\n{\n"
                        + $"     name  : {character.Name}" + ((character.Name == "Default Character") ? $"_{index}" : "" ) + "\n"
                        + $"     class : {character.CharacterClass.Name}\n"
                        + $"     level : {character.CharacterClass.ClassStats.Level}\n"
                        + "      stats:\n"
                        + $"             health : {character.CharacterClass.ClassStats.Health}\n"
                        + $"             defense: {character.CharacterClass.ClassStats.Defense}\n"
                        + $"             damage : {character.CharacterClass.ClassStats.Damage}\n"
                        + $"             mana   : {character.CharacterClass.ClassStats.Mana}\n"
                        + "}\n\n";

            if (File.Exists(FilePath))
            {

                string[] allLines = File.ReadAllLines(FilePath);

                string saveText = String.Empty;


                for (int i = 0; i < allLines.Length; i++)
                {
                    string line = allLines[i];
                    if (line.Contains($"character : {index}"))
                    {
                        ConsoleUI.Warning("A save file has been overwritten\n");
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
                        return;
                    }
                }

            }
            ConsoleUI.Warning("Creating a new Save for character : " + index);
            File.AppendAllText(FilePath, savePattern);

        }

        public void Load(Character character, int index)
        {
            string[] allLines = File.ReadAllLines(FilePath);
            List<string> loadText = new List<string>();

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains($"character : {index}"))
                {
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
                    ConsoleUI.Warning($"File at : {SaveManager.Instance.FilePath} has been loaded.");
                    return;

                }

            }
            ConsoleUI.Error("Can't find a valid save for the character : " + index);
        }

        public void DeleteAt(int index)
        {

            if (!Exists(index))
            {
                ConsoleUI.Error("No save found for the index : " + index);
                return; 
            }

            string[] lines = File.ReadAllLines(FilePath);
            string deleteText = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains($"character : {index}"))
                {
                    while (!lines[i].Contains("}") || i > lines.Length)
                    {
                        deleteText += lines[i++] + '\n';
                    }
                    if (lines[i].Contains("}"))
                        deleteText += lines[i] + "\n\n";
                }
            }

            string allText = File.ReadAllText(FilePath);

            allText = allText.Replace(deleteText, "");
            File.WriteAllText(FilePath, allText);

        }

        public int SaveCount()
        {
            int count = 0;
            string[] allLines = File.ReadAllLines(FilePath);

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains("character"))
                    count++;
            }
            return count;
        }

        public int GetSaveIndexOf(string name)
        {
            string[] allLines = File.ReadAllLines(FilePath);

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains("character"))
                {
                    string indexLine = line;
                    while (!line.Contains("}"))
                    {
                        line = allLines[i++];
                        if (line.Contains(name))
                        {
                            string indexStr = string.Empty;
                            for (int j = indexLine.IndexOf(':') + 1; j < indexLine.Length; j++)
                            {
                                indexStr += indexLine[j];
                            }
                            Int32.TryParse(indexStr.Trim(' '), out int index);
                            return index;
                        }
                        if (i >= allLines.Length - 1)
                            break;
                    }
                }
            }
            return 0;
        }

        public int[] GetValidIndex()
        {
            List<int> validIndex = new List<int>();
            string[] names = GetSaveNames();
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                validIndex.Add(GetSaveIndexOf(name));
            }
            return validIndex.ToArray();
        }

        public bool Exists(int index)
        {
            foreach(var validIndex in GetValidIndex())
            {
                if (index == validIndex)
                    return true;
            }
            return false;
        }
        public string[] GetSaveNames()
        {

            string[] allLines = File.ReadAllLines(FilePath);
            List<string> names = new List<string>();

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains("character"))
                {
                    while (!line.Contains("}"))
                    {
                        line = allLines[i++];
                        if (line.Contains("name"))
                        {
                            string tempName = string.Empty;
                            for (int j = line.IndexOf(':') + 1; j < line.Length; j++)
                            {
                                tempName += line[j];
                            }
                            names.Add(tempName);
                        }
                        if (i >= allLines.Length - 1)
                            break;
                    }
                }
            }
            return names.ToArray();
        }

        public string GetSaveName(int index)
        {
            string[] allLines = File.ReadAllLines(FilePath);
            string name = string.Empty;

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];
                if (line.Contains($"character : {index}"))
                {
                    while (!line.Contains("}"))
                    {
                        line = allLines[i++];
                        if (line.Contains("name"))
                        {
                            for (int j = line.IndexOf(':') + 1; j < line.Length; j++)
                            {
                                name += line[j];
                            }
                            return name.TrimStart(' ');
                        }
                        if (i >= allLines.Length - 1)
                            break;
                    }
                }
            }
            return name;
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

