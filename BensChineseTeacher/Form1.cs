namespace BensChineseTeacher
{
    public partial class Form1 : Form
    {
        private Vocabulary[] vocabularies;
        private List<Vocabulary> activeVocabularies;
        private Random random;
        private VocabularyRecord currentRecord;

        private int resultRight;
        private int resultPass;
        private int resultWrong;
        public Form1()
        {
            InitializeComponent();

            vocabularies = new Vocabulary[6];
            activeVocabularies = new List<Vocabulary>();
            random = new Random();
            currentRecord = null;

            RestartResults();

            LoadVocabulary();
            RefreshActiveVocabularies();
            LoadNewWord();
        }

        private void RestartResults()
        {
            resultRight = 0;
            resultPass = 0;
            resultWrong = 0;
            RefreshResults();
        }
        private void RefreshResults()
        {
            int total = resultPass + resultRight + resultWrong;
            label14.Text = "(" + total.ToString() + ")";
            double rightPercentage = resultRight;
            double passPercentage = resultPass;
            double wrongPercentage = resultWrong;

            if (total != 0)
            {
                rightPercentage = Math.Round((resultRight / (double)total) * 100, 1);
                passPercentage = Math.Round((resultPass / (double)total) * 100, 1);
                wrongPercentage = Math.Round((resultWrong / (double)total) * 100, 1);
            }

            label11.Text = rightPercentage.ToString() + " %";
            label12.Text = passPercentage.ToString() + " %";
            label13.Text = wrongPercentage.ToString() + " %";
        }

        private void TestVocabulary()
        {
            foreach (Vocabulary vocabulary in activeVocabularies)
            {
                List<string> words = new List<string>();
                while (words.Count < vocabulary.getVocabularyCount())
                {
                    string newWord = vocabulary.getRandomRecord().getSimplifiedChinese();
                    if (!words.Contains(newWord)) words.Add(newWord);
                }
                Console.WriteLine("Vocabulary reachable.");
            }
        }
        private void LoadVocabulary()
        {
            vocabularies[0] = new Vocabulary(@"src1.txt");
            vocabularies[1] = new Vocabulary(@"src2.txt");
            vocabularies[2] = new Vocabulary(@"src3.txt");
            vocabularies[3] = new Vocabulary(@"src4.txt");
            vocabularies[5] = new Vocabulary(@"src_bcc.txt");

            foreach (Vocabulary vocabulary in vocabularies)
            {
                if (vocabulary != null) vocabulary.loadVocabulary();
            }
        }

        private void RefreshActiveVocabularies()
        {
            if (checkBox1.Checked && !activeVocabularies.Contains(vocabularies[0])) activeVocabularies.Add(vocabularies[0]);
            if (!checkBox1.Checked && activeVocabularies.Contains(vocabularies[0])) activeVocabularies.Remove(vocabularies[0]);

            if (checkBox2.Checked && !activeVocabularies.Contains(vocabularies[1])) activeVocabularies.Add(vocabularies[1]);
            if (!checkBox2.Checked && activeVocabularies.Contains(vocabularies[1])) activeVocabularies.Remove(vocabularies[1]);

            if (checkBox3.Checked && !activeVocabularies.Contains(vocabularies[2])) activeVocabularies.Add(vocabularies[2]);
            if (!checkBox3.Checked && activeVocabularies.Contains(vocabularies[2])) activeVocabularies.Remove(vocabularies[2]);

            if (checkBox4.Checked && !activeVocabularies.Contains(vocabularies[3])) activeVocabularies.Add(vocabularies[3]);
            if (!checkBox4.Checked && activeVocabularies.Contains(vocabularies[3])) activeVocabularies.Remove(vocabularies[3]);

            if (checkBox5.Checked && !activeVocabularies.Contains(vocabularies[4])) activeVocabularies.Add(vocabularies[4]);
            if (!checkBox5.Checked && activeVocabularies.Contains(vocabularies[4])) activeVocabularies.Remove(vocabularies[4]);

            if (checkBox6.Checked && !activeVocabularies.Contains(vocabularies[5])) activeVocabularies.Add(vocabularies[5]);
            if (!checkBox6.Checked && activeVocabularies.Contains(vocabularies[5])) activeVocabularies.Remove(vocabularies[5]);
        }

        private void LoadNewWord()
        {
            /* display last word */
            if (currentRecord != null)
            {
                textBox2.Text = currentRecord.getSimplifiedChinese();
                textBox3.Text = currentRecord.getTraditionalChinese();
                textBox4.Text = currentRecord.getPinyin();
                richTextBox1.Text = currentRecord.getFormattedTranslations();
            }


            int randomVocabularyIndex = random.Next(activeVocabularies.Count);
            currentRecord = activeVocabularies[randomVocabularyIndex].getRandomRecord();
            if (radioButton1.Checked)
                label1.Text = currentRecord.getSimplifiedChinese();
            else if (radioButton2.Checked)
                label1.Text = currentRecord.getTraditionalChinese();
            else 
                label1.Text = currentRecord.getSimplifiedChinese() + " / " + currentRecord.getTraditionalChinese();
        }

        private void levelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshActiveVocabularies();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckAnswer();
            textBox1.Clear();
            LoadNewWord();
            textBox1.Focus();
        }

        private void CheckAnswer()
        {
            if (textBox1.Text == "")
            {
                label2.Visible = false;
                label3.Visible = false;

                label4.Text = "It means " + currentRecord.getFormattedTranslations() + "; pinyin is " + currentRecord.getPinyin() + ".";
                label4.Visible = true;

                resultPass++;
                RefreshResults();

            }
            else if (currentRecord.matchesTranslation(textBox1.Text))
            {
                label2.Visible = false;
                label4.Visible = false;

                label3.Text = "Correct! It means " + currentRecord.getFormattedTranslations() + "; pinyin is " + currentRecord.getPinyin() + ".";
                label3.Visible = true;

                resultRight++;
                RefreshResults();
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;

                label2.Text = "Wrong! It means " + currentRecord.getFormattedTranslations() + "; pinyin is " + currentRecord.getPinyin() + ".";
                label2.Visible = true;

                resultWrong++;
                RefreshResults();
            }
        }

        private void textBox1_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                CheckAnswer();
                textBox1.Clear();
                LoadNewWord();
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestartResults();
        }
    }
}