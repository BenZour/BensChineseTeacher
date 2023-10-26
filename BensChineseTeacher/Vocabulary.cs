using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensChineseTeacher
{
    internal class Vocabulary
    {
        private string vocabularySource;
        private Dictionary<int, VocabularyRecord> words;
        private Random random;

        public Vocabulary(string vocabularySource)
        {
            this.vocabularySource = vocabularySource;
            words = new Dictionary<int, VocabularyRecord>();
            random = new Random();
        }
        public void loadVocabulary()
        {
            using (var reader = new StreamReader(vocabularySource))
            {
                reader.ReadLine();
                int id = 0;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] splitLine = line.Split('	');
                    string[] splitTranslations = splitLine[4].Replace("\"", "").Split(';');
                    string[] prunedTranslations = new string[splitTranslations.Length];
                    for (int i = 0; i < splitTranslations.Length; i++) prunedTranslations[i] = splitTranslations[i].Trim();
                    VocabularyRecord splitLineRecord = new VocabularyRecord(splitLine[1], splitLine[2], splitLine[3], prunedTranslations);
                    words.Add(id, splitLineRecord);
                    id++;
                }
            }
        }

        public VocabularyRecord getRandomRecord()
        {
            Random random = new Random();
            return words[random.Next(words.Count)];
        }

        public int getVocabularyCount()
        {
            return words.Count;
        }

    }
}
