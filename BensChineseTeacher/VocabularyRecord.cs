using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensChineseTeacher
{
    internal class VocabularyRecord
    {
        private readonly string simplifiedChinese;
        private readonly string traditionalChinese;
        private readonly string pinyin;
        private readonly string[] translations;

        public VocabularyRecord(string simplifiedChinese, string traditionalChinese, string pinyin, string[] translations)
        {
            this.simplifiedChinese = simplifiedChinese;
            this.traditionalChinese = traditionalChinese;
            this.pinyin = pinyin;
            this.translations = translations;
        }
        public string getSimplifiedChinese()
        {
            return simplifiedChinese;
        }
        public string getTraditionalChinese()
        {
            return traditionalChinese;
        }
        public string getPinyin()
        {
            return pinyin;
        }
        public string[] getTranslations()
        {
            return translations;
        }
        public string getFormattedTranslations() 
        {
            string formatted = translations[0];
            for (int i = 1; i < translations.Length; i++)
                formatted += ", " + translations[i];
            return formatted;
        }

        public bool matchesTranslation(string attempt)
        {
            for (int i = 0; i < translations.Length; i++)
            {
                if (translations[i].Trim().ToLower().Equals(attempt.Trim().ToLower())) return true;
            }
            return false;
        }

    }
}
